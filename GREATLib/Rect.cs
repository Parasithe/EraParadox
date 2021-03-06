//
//  Rect.cs
//
//  Author:
//       Jesse <>
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

namespace GREATLib
{
	/// <summary>
	/// A 2D rectangle used within the game library.
	/// </summary>
    public class Rect
    {
		public float X { get; set; }
		public float Y { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public float Left { get { return X; } }
		public float Right { get { return Left + Width; } }
		public float Top { get { return Y; } }
		public float Bottom { get { return Top + Height; } }

        public Rect()
			: this (0f,0f,0f,0f)
        {
        }
		public Rect(float x, float y, float width, float height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public bool Intersects(Rect b)
		{
			return !(b.Left > Right ||
				b.Right < Left ||
				b.Top > Bottom ||
				b.Bottom < Top);
		}
		public static bool Intersects(Rect a, Rect b)
		{
			return a.Intersects(b);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Rect))
				return false;
			Rect r = obj as Rect;
			return r.X == X && r.Y == Y && r.Width == Width && r.Height == Height;
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public static Vec2 Center(Rect r)
		{
			return new Vec2(r.Left + r.Width / 2f,
			                r.Top + r.Height / 2f);
		}
    }
}

