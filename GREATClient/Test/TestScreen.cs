//
//  TestScreen.cs
//
//  Author:
//       The Parasithe <bipbip500@hotmail.com>
//
//  Copyright (c) 2013 The Parasithe
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
using GREATLib.Entities;
using GREATLib.Entities.Physics;
using GREATLib.Entities.Player;
using GREATLib.Entities.Player.Champions.AllChampions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using GREATLib.Entities.Player.Spells;
using System;

namespace GREATClient
{
    public class TestScreen : Screen
    {
		ChampionsInfo ChampionsInfo { get; set; }

		KeyboardState oldks;
		MouseState oldms;


		DrawableChampionSprite champSprite;



		public TestScreen(ContentManager content) : base(content)
        {
			oldms = new MouseState();
			ChampionsInfo = new ChampionsInfo();
        }
		protected override void OnLoadContent()
		{
			champSprite = new DrawableChampionSprite(new StickmanChampion() { Position = new Vec2(200f, 100f) }, ChampionsInfo);

			AddChild(champSprite);

			DrawableTriangle tr =  new DrawableTriangle(true);
			tr.Ascendant = false;
			tr.Tint = Color.Blue;
			tr.Scale = new Vector2(1f,2f);

			oldks = Keyboard.GetState();
			oldms = Mouse.GetState();

			Container cc = new Container(Content);
			cc.AddChild(new FPSCounter());
			AddChild(cc);



			//Test particle
			/*ParticleSystem sys = new ParticleSystem(Content, 1000, null);
			sys.Position = new Vector2(100, 100);
			AddChild(sys);*/

			AddChild(new PingCounter(yo));

		}


		/// <summary>
		/// Boobies reference
		/// </summary>
		protected double yo()
		{
			return 32d;
		}

		protected override void OnUpdate(GameTime dt)
		{
			//TODO: remove. testing the physics engine
			KeyboardState ks = Keyboard.GetState();
			MouseState ms = Mouse.GetState();

			if (ks.IsKeyDown(Keys.Escape))
				Exit = true;

			if (ks.IsKeyDown(Keys.E)) { champSprite.PlayAnimation(AnimationInfo.JUMP);}
			if (ks.IsKeyDown(Keys.Q)) { champSprite.PlayAnimation(AnimationInfo.RUN);}

			oldks = ks;
			oldms = ms;

			base.OnUpdate(dt);
		}
    }
}

