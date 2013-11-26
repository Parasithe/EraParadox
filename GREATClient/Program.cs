using System;
using System.Collections.Generic;
using System.Linq;
using GREATLib.Entities.Champions;

namespace GREATClient
{
	static class Program
	{
		static GreatGame game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Random r = new Random();

			string ip = args.Length > 0 ? args.First() : "localhost";

			int champion = r.Next(Enum.GetValues(typeof(ChampionTypes)).Length);

			if (args.Length > 1) {
				int.TryParse(args[1], out champion);
			}

			Client.IP = ip;
			Client.Champion = champion;

			Console.WriteLine("Creating game...");
			game = new GreatGame();
			game.Run();
		}
	}
}
