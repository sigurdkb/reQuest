using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public partial class GamePage : ContentPage
	{
		Player currentPlayer = null;
		Player currentTarget = null;
		GameViewModel gameViewModel = new GameViewModel() { Player = "Sigurd", Target = "Jostein" };

		public GamePage()
		{
			InitializeComponent();
			BindingContext = gameViewModel;
		}

		async override protected void OnAppearing()
		{
			await StartGame();

		}

		async Task<IEnumerable<Player>> UpdateGame()
		{
			var players = reQuest.Services.reQuestService.Instance.Players;

			GameMap.Pins.Clear();

			foreach (var player in players)
			{
				if (player.ExternalId == gameViewModel.Player)
				{
					currentPlayer = player;
				}
				else if (player.ExternalId == gameViewModel.Target)
				{
					currentTarget = player;
					var pin = new Pin()
					{
						Label = player.ExternalId,
						Position = new Position(player.Latitude, player.Longitude),
						Type = PinType.SearchResult,
					};
					GameMap.Pins.Add(pin);
				}
			}

			gameViewModel.DistanceToTarget = DistanceBetween(currentPlayer.Latitude, currentPlayer.Longitude, currentTarget.Latitude, currentTarget.Longitude);

			GameMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(currentPlayer.Latitude, currentPlayer.Longitude) , Distance.FromMeters(gameViewModel.DistanceToTarget)));

			return players;
	
		}

		async Task<IEnumerable<Player>> StartGame()
		{
			if (Target.IsToggled && App.Location != null)
			{
				App.Location.StartBeacon("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", "reQuest");
			}
			else
			{
				App.Location.StartBeaconRanging("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", "reQuest");
				App.Location.positionChanged += HandlePositionChanged;
			}

			var result = await UpdateGame();

			return result;
		}

		async void HandlePositionChanged(object sender, IGPSData e)
		{
			if (currentPlayer != null)
			{
				var player = new Player()
				{
					Id = currentPlayer.Id,
					ExternalId = currentPlayer.ExternalId,
					Latitude = e.Latitude,
					Longitude = e.Longitude
				};

				//player = await reQuest.Services.reQuestService.Instance.UpdatePlayer(player);

			}

			var result = await UpdateGame();

		}

		public static double DistanceBetween(double latA, double longA, double latB, double longB)
		{
			var RadianLatA = Math.PI * latA / 180;
			var RadianLatb = Math.PI * latB / 180;
			var RadianLongA = Math.PI * longA / 180;
			var RadianLongB = Math.PI * longB / 180;

			double theDistance = (Math.Sin(RadianLatA)) *
					Math.Sin(RadianLatb) +
					Math.Cos(RadianLatA) *
					Math.Cos(RadianLatb) *
					Math.Cos(RadianLongA - RadianLongB);

			return (((Math.Acos(theDistance) * (180.0 / Math.PI)))) * 69.09d * 1.6093d;
		}

	}
}
