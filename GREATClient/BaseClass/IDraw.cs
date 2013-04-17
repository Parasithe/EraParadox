//
//  IDraw.cs
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

namespace GREATClient
{
    public abstract class IDraw
    {
		/// <summary>
		/// Gets the parent of the object.
		/// </summary>
		/// <value>The parent.</value>
		public Container Parent { get; private set; }

		/// <summary>
		/// Gets or sets the z-index within the parent's container.
		/// The higher the z-index, the "closer" it is while displaying it.
		/// Don't change it manually
		/// </summary>
		/// <value>The z.</value>
		public int Z { get; set; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>The position.</value>
		public Vector2 Position { get; set; }

		public virtual Vector2 GetAbsolutePosition()
		{
			return Position+Parent.GetAbsolutePosition();
		}

		public IDraw() 
		{
			Parent = null;
			Z = 0;
		}

		/// <summary>
		/// Load the drawable object.
		/// </summary>
		/// <param name="container">Its container.</param>
		/// <param name="content">The content manager, used to draw.</param>
		public void Load(Container container, GraphicsDevice gd)
		{
			Parent = container;
			OnLoad(Parent.Content, gd);
		}
		/// <summary>
		/// Raises the load event.
		/// Will be call after it is had to a container
		/// </summary>
		/// <param name="content">Content.</param>
		protected virtual void OnLoad(ContentManager content, GraphicsDevice gd) {

		}

		/// <summary>
		/// After load was call.
		/// </summary>
		public void UnLoad()
		{
			Parent = null;
			OnUnload();
		}
		/// <summary>
		///  After Unload was call.
		/// </summary>
		protected virtual void OnUnload() { }

		/// <summary>
		/// Draw the specified batch.
		/// </summary>
		/// <param name="batch">Batch.</param>
		public abstract void Draw(SpriteBatch batch);
		/// <summary>
		/// Update
		/// Dt is disference of time since last call
		/// </summary>
		/// <param name="dt">Dt.</param>
		public virtual void Update(GameTime dt) { }
    }
}
