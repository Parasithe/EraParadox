//
//  MainChampion.cs
//
//  Author:
//       Jesse <jesse.emond@hotmail.com>
//
//  Copyright (c) 2013 Jesse
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using GREATLib;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GREATLib.Entities;
using GREATLib.Network;
using System.Diagnostics;
using System;

namespace GREATClient.Network
{
	/// <summary>
	/// Represents the champion's data of the main player, the one playing the game on
	/// this instance of the program.
	/// </summary>
    public class MainClientChampion : IEntity, IUpdatable
    {
		/// <summary>
		/// Gets or sets the last acknowledged action ID of the player.
		/// That is to say, the last action from us that the server executed.
		/// </summary>
		public uint LastAcknowledgedActionID { get; set; }

		/// <summary>
		/// Gets the drawn position of the champion.
		/// This is the position where we should currently draw the champion.
		/// </summary>
		/// <value>The drawn position.</value>
		public Vec2 DrawnPosition { get; private set; }
		/// <summary>
		/// Gets or sets the server position.
		/// This is the last position that we received from the server.
		/// </summary>
		/// <remarks>This shouldn't be changed within the class, only set when the server
		/// gives us a new position.</remarks>
		Vec2 ServerPosition { get; set; }

		GameMatch Match { get; set; }

		Queue<PlayerAction> PackagedActions { get; set; }
		Queue<PlayerAction> UnacknowledgedActions { get; set; }

		public MainClientChampion(ChampionSpawnInfo spawnInfo, GameMatch match)
			: base(spawnInfo.ID, spawnInfo.SpawningPosition)
        {
			LastAcknowledgedActionID = IDGenerator.NO_ID;
			Match = match;

			DrawnPosition = Position;
			ServerPosition = Position;

			PackagedActions = new Queue<PlayerAction>();
			UnacknowledgedActions = new Queue<PlayerAction>();
        }

		/// <summary>
		/// Update the champion, applying client-side prediction.
		/// </summary>
		public void Update(GameTime deltaTime)
		{
			// client-side prediction
			Match.ApplyPhysicsUpdate(ID, deltaTime.ElapsedGameTime.TotalSeconds);

			DrawnPosition = Position;

			//TODO: remove, used for testing purposes
			ILogger.Log(Position.ToString());
		}

		/// <summary>
		/// Take the new position given by the server and resimulate our unacknowledged actions
		/// from there.
		/// </summary>
		public override void AuthoritativeChangePosition(Vec2 position)
		{
			ServerPosition = position;

			// resimulate the unacknowledged actions
			Position = (Vec2)ServerPosition.Clone();

			// remove the acknowledged actions
			while (UnacknowledgedActions.Count > 0 &&
			       UnacknowledgedActions.Peek().ID <= LastAcknowledgedActionID) {
				UnacknowledgedActions.Dequeue();
			}

			// go through the unacknowledged actions and just replay them
			float lastActionTime = UnacknowledgedActions.Count > 0 ? UnacknowledgedActions.Peek().Time : 0f;
			foreach (PlayerAction action in UnacknowledgedActions) {
				ExecuteAction(action.Type);

				float deltaTime = action.Time - lastActionTime;
				Debug.Assert(deltaTime >= 0f);

				if (deltaTime > 0f) { // we don't want to do a physics update if we're on the same frame (dt==0)
					Match.ApplyPhysicsUpdate(ID, deltaTime);
				}
			}
		}

		public void PackageAction(PlayerAction action)
		{
			PackagedActions.Enqueue(action);
			UnacknowledgedActions.Enqueue(action);

			ExecuteAction(action.Type);
		}

		void ExecuteAction(PlayerActionType type)
		{
			switch (type) {
				case PlayerActionType.MoveLeft: 
					Match.Move(ID, HorizontalDirection.Left);
					break;

					case PlayerActionType.MoveRight:
					Match.Move(ID, HorizontalDirection.Right);
					break;

					case PlayerActionType.Jump:
					Match.Jump(ID);
					break;

					default:
					Debug.Fail(String.Format("Invalid action type \"{0}\"", type));
					break;
			}
		}

		public Queue<PlayerAction> GetActionPackage()
		{
			return PackagedActions;
		}
    }
}

