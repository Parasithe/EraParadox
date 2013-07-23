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
		/// Gets the input manager.
		/// </summary>
		/// <returns>The input manager.</returns>
		InputManager inputManager;
		public InputManager InputManager {
			get {
				if (inputManager != null) {
					return inputManager;
				} else if (this.Game != null) {
					inputManager = (InputManager)this.Game.Services.GetService(typeof(InputManager));
					return inputManager;
				} else {
					return null;
				}
			}
		}

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

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="GREATClient.IDraw"/> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
		public bool Visible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="GREATClient.IDraw"/> is updatable.
		/// </summary>
		/// <value><c>true</c> if updatable; otherwise, <c>false</c>.</value>
		public bool Updatable { get; set; }

		/// <summary>
		/// Gets the absolute position.
		/// </summary>
		/// <returns>The absolute position.</returns>
		public virtual Vector2 GetAbsolutePosition()
		{
			return Position+Parent.GetAbsolutePosition();
		}

		/// <summary>
		/// Gets or sets the game.
		/// </summary>
		/// <value>The game.</value>
		public virtual Game Game 
		{ 
			get {
				return Parent == null ? null : Parent.Game;
			}
			protected set { throw new NotImplementedException("Only the screen can set the Game"); }
		}

		public IDraw() 
		{
			Parent = null;
			Z = 0;
			Visible = true;
			Updatable = true;
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
		public virtual void Draw(SpriteBatch batch)
		{
			if(Visible)
				OnDraw(batch);
		}

		/// <summary>
		/// Called after Draw if visible
		/// </summary>
		/// <param name="batch">Batch.</param>
		protected abstract void OnDraw(SpriteBatch batch);

		/// <summary>
		/// Update
		/// Dt is disference of time since last call
		/// </summary>
		/// <param name="dt">Dt.</param>
		public void Update(GameTime dt)
		{
			if(Updatable)
				OnUpdate(dt);
		}

		/// <summary>
		/// Called afet Update if Updatable
		/// </summary>
		/// <param name="dt">Dt.</param>
		protected virtual void OnUpdate(GameTime dt)
		{

		}
    }
}

