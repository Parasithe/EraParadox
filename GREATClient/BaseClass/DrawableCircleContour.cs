//
//  DrawableCircle.cs
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
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GREATClient.BaseClass
{
    public class DrawableCircleContour : Drawable
    {
		/// <summary>
		/// Gets or sets the radius.
		/// </summary>
		/// <value>The radius.</value>
		private int Radius { get; set; }

		public DrawableCircleContour(int radius)
        {
			Radius = radius;
        }
		public override float GetEffectiveAlpha()
		{
			return Alpha * (Parent != null ? Parent.GetEffectiveAlpha() : 1);
		}
		public Texture2D CreateCircle(int radius, GraphicsDevice gd)
		{
			int outerRadius = radius*2 + 2; // So circle doesn't go out of bounds
			Texture2D texture = new Texture2D(gd, outerRadius, outerRadius);
			
			Color[] data = new Color[outerRadius * outerRadius];
			
			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++)
				data[i] = Color.Transparent;
			
			// Work out the minimum step necessary using trigonometry + sine approximation.
			double angleStep = 1f/radius;
			
			for (double angle = 0; angle < Math.PI*2; angle += angleStep)
			{
				// Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
				int x = (int)Math.Round(radius + radius * Math.Cos(angle));
				int y = (int)Math.Round(radius + radius * Math.Sin(angle));
				
				data[y * outerRadius + x + 1] = Color.White;
			}
			
			texture.SetData(data);
			return texture;
		}

		protected override void OnLoad(ContentManager content, GraphicsDevice gd)
		{
			Texture = CreateCircle(Radius,gd);
		}

		protected override void OnDraw(SpriteBatch batch)
		{
			batch.Begin();
			batch.Draw(Texture,GetAbsolutePosition(),SourceRectangle,Tint * GetEffectiveAlpha(),(float)Orientation,
			           RelativeOrigin * new Vector2(Texture.Width, Texture.Height),Scale,Effects,0);
			batch.End();
		}

		public override bool IsBehind(Vector2 position)
		{
			if (Parent != null) {
				Vector2 realPos = (GetAbsolutePosition() - RelativeOrigin * new Vector2(Texture.Width, Texture.Height)) 
					+ (new Vector2(Texture.Width, Texture.Height) * new Vector2(0.5f, 0.5f));
				return (position.X >= realPos.X - Radius && position.X <= realPos.X + Radius) &&
					(position.Y >= realPos.Y - Radius && position.Y <= realPos.Y + Radius);
			}
			return false;
		}
    }
}

