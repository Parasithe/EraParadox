//
//  MainDrawableChampion.cs
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
using GREATClient.Network;
using GREATClient.BaseClass;
using GREATLib.Network;
using GREATLib.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GREATLib;

namespace GREATClient.GameContent
{
    public sealed class MainDrawableChampion : DrawableChampion<MainClientChampion>
    {
		const bool VIEW_DEBUG_RECTS = false;
		/// <summary>
		/// TODO: class for debug info
		/// </summary>
		DrawableRectangle ChampionServerRect { get; set; }
		DrawableRectangle ChampionSimulatedRect { get; set; }
		DrawableRectangle ChampionNoCorrection { get; set; }

		public MainDrawableChampion(ChampionSpawnInfo spawnInfo, GameMatch match)
			: base(new MainClientChampion(spawnInfo, match))
        {
        }

		protected override void OnLoad(Microsoft.Xna.Framework.Content.ContentManager content, Microsoft.Xna.Framework.Graphics.GraphicsDevice gd)
		{
			Parent.AddChild(ChampionServerRect = new DrawableRectangle(new Rectangle(0, 0, 15, 30), Color.Green));
			Parent.AddChild(ChampionSimulatedRect = new DrawableRectangle(new Rectangle(0, 0, 15, 30), Color.Red));
			Parent.AddChild(ChampionNoCorrection = new DrawableRectangle(new Rectangle(0, 0, 15, 30), Color.Yellow));
			ChampionNoCorrection.Visible = ChampionServerRect.Visible = ChampionSimulatedRect.Visible = VIEW_DEBUG_RECTS;

			base.OnLoad(content, gd);
		}

		protected override void OnUpdate(GameTime dt)
		{
			base.OnUpdate(dt);

			ChampionServerRect.Position = GameLibHelper.ToVector2(Champion.ServerPosition);
			ChampionSimulatedRect.Position = GameLibHelper.ToVector2(Champion.Position);
			ChampionNoCorrection.Position = GameLibHelper.ToVector2(Champion.NoCorrPos);
		}

		/// <summary>
		/// Packages a client-side action to be sent to the server. This also simulates the action locally
		/// for client-side prediction.
		/// </summary>
		public void PackageAction(PlayerActionType action)
		{
			PlayerAction toPackage = new PlayerAction(
				IDGenerator.GenerateID(),
				action,
				(float)Client.Instance.GetTime().TotalSeconds,
				Champion.Position);

			Champion.PackageAction(toPackage);
		}

		public Queue<PlayerAction> GetActionPackage()
		{
			return Champion.GetActionPackage();
		}
    }
}
