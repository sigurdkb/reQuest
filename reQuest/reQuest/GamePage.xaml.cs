using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public partial class GamePage : ContentPage
	{
		GameViewModel gameViewModel = new GameViewModel() { Players = new Dictionary<string, Position>() };

		public GamePage()
		{
			InitializeComponent();
			BindingContext = gameViewModel;

			gameViewModel.Players.Add("mariub06", new Position(58.3340d, 8.5774d));
			gameViewModel.Players.Add("sigurdkb", new Position(58.3338d, 8.5772d));
			gameViewModel.Players.Add("josteinn", new Position(58.3336d, 8.5770d));
			gameViewModel.Player = "mariub06";
			gameViewModel.Target = "sigurdkb";

			GameMap.MoveToRegion(MapSpan.FromCenterAndRadius(gameViewModel.Players[gameViewModel.Target], Distance.FromKilometers(0.1d)));

			foreach (KeyValuePair<string, Position> player in gameViewModel.Players)
			{
				var pin = new Pin()
				{
					Label = player.Key,
					Position = player.Value
				};
				if (player.Key == gameViewModel.Target)
				{
					pin.Type = PinType.Place;
				}
				else if (player.Key == gameViewModel.Player)
				{
					pin.Type = PinType.SavedPin;
				}
				else
				{
					pin.Type = PinType.Generic;
				}

				GameMap.Pins.Add(pin);
			}

		}


	}
}
