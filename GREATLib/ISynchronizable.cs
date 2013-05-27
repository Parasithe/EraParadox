//
//  ISynchronizable.cs
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
using GREATLib.Entities;

namespace GREATLib
{
	/// <summary>
	/// Represents an entity that is synchronized between the server and the clients.
	/// </summary>
    public class ISynchronizable
    {
		/// <summary>
		/// Gets or sets the unique identifier of the entity.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

        public ISynchronizable()
        {
			Id = EntityIDGenerator.NO_ID;
        }
    }
}

