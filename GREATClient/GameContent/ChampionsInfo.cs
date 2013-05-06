//
//  ChampionsInfo.cs
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
using System.Collections.Generic;
using GREATLib.Entities.Player.Champions;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;

namespace GREATClient
{
	/// <summary>
	/// The information of an individual champion.
	/// </summary>
	[Serializable()]
	public class ChampionInfo
	{
		[System.Xml.Serialization.XmlElement("type", typeof(ChampionTypes))]
		public ChampionTypes Type { get; set; }

		[System.Xml.Serialization.XmlElement("name")]
		public string Name { get; set; }

		[System.Xml.Serialization.XmlElement("assetname")]
		public string AssetName { get; set; }

		[System.Xml.Serialization.XmlElement("description")]
		public string Description { get; set; }
	}

	[Serializable()]
	[System.Xml.Serialization.XmlRoot("championcollection")]
	public class ChampionInfoCollection
	{
		[System.Xml.Serialization.XmlArray("champions")]
		[System.Xml.Serialization.XmlArrayItem("champion", typeof(ChampionInfo))]
    	public ChampionInfo[] Champions { get; set; }
	}

	/// <summary>
	/// The class holding various information about all the champions
	/// in the game. Whenever a new champion is added, this list must be updated.
	/// </summary>
    public class ChampionsInfo
    {
		private Dictionary<ChampionTypes, ChampionInfo> Info { get; set; }

        public ChampionsInfo()
        {
			FillInfo();
        }

		public ChampionInfo GetInfo(ChampionTypes champion)
		{
			Debug.Assert(Info.ContainsKey(champion));
			return Info[champion];
		}

		private void FillInfo()
		{
			const string CHAMPIONS_PATH = "Content/champions.xml";
			Info = new Dictionary<ChampionTypes, ChampionInfo>();

			ChampionInfoCollection champions = null;

			XmlSerializer serializer = new XmlSerializer(typeof(ChampionInfoCollection));

			StreamReader reader = new StreamReader(CHAMPIONS_PATH);
			champions = (ChampionInfoCollection)serializer.Deserialize(reader);
			reader.Close();

			foreach (ChampionInfo info in champions.Champions)
			{
				Debug.Assert(!Info.ContainsKey(info.Type));
				Info.Add(info.Type, info);
			}
		}
    }
}
