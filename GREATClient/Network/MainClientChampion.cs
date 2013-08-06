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
using System;
using GREATLib;
using GREATClient.Network.Physics;
using Microsoft.Xna.Framework;

namespace GREATClient.Network
{
	/// <summary>
	/// Represents the champion's data of the main player, the one playing the game on
	/// this instance of the program.
	/// </summary>
    public class MainClientChampion : IUpdatable
    {
		const float DEFAULT_HORIZONTAL_ACCELERATION = 0.8f;

		/// <summary>
		/// Gets the drawn position of the champion.
		/// This is the position where we should currently draw the champion.
		/// </summary>
		/// <value>The drawn position.</value>
		public Vec2 DrawnPosition { get; set; }

		/// <summary>
		/// Gets or sets the simulated position of the champion.
		/// This is the position of our client-side prediction of the game state.
		/// </summary>
		/// <value>The simulated position.</value>
		public Vec2 SimulatedPosition { get; set; }

		/// <summary>
		/// Gets or sets the velocity of the entity.
		/// </summary>
		public Vec2 Velocity { get; set; }
		/// <summary>
		/// Gets or sets the movement speed of the entity.
		/// </summary>
		/// <value>The move speed.</value>
		public float MoveSpeed { get; set; }

		/// <summary>
		/// Gets or sets the horizontal acceleration of the entity, which is how much of our horizontal velocity
		/// we maintain on each physics pass.
		/// </summary>
		/// <example>0.9 would keep 90% of the entity's X velocity every frame.</example>
		public float HorizontalAcceleration { get; set; }

		/// <summary>
		/// Gets the direction of the entity during the current frame.
		/// </summary>
		public HorizontalDirection Direction { get; set; }

		private PhysicsEngine Physics { get; set; }

		private int XMovement;

		public MainClientChampion()
        {
			//TODO: toremove
			Physics = new PhysicsEngine();
			DrawnPosition = new Vec2(500f, 300f);
			SimulatedPosition = DrawnPosition;
			MoveSpeed = 400f;

			Velocity = new Vec2();
			Direction = HorizontalDirection.None;
			XMovement = 0;

			HorizontalAcceleration = DEFAULT_HORIZONTAL_ACCELERATION;
        }

		/// <summary>
		/// Update the champion, applying client-side prediction.
		/// </summary>
		public void Update(GameTime deltaTime)
		{
			// client-side prediction
			Physics.Update(deltaTime.ElapsedGameTime.TotalSeconds, this, ref XMovement);
			DrawnPosition = SimulatedPosition;
		}

		public void MoveLeft()
		{
			Physics.Move(this, HorizontalDirection.Left, ref XMovement);
		}
		public void MoveRight()
		{
			Physics.Move(this, HorizontalDirection.Right, ref XMovement);
		}
		public void Jump()
		{
			throw new NotImplementedException();
		}
    }
}
