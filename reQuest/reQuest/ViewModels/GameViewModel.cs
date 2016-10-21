using System;
using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public class GameViewModel
	{
		public Dictionary<string, Position> Players { get; set; }
		public string Player { get; set; }
		public string Target { get; set; }
		public double DistanceToTarget { get; set; }


	}
}
