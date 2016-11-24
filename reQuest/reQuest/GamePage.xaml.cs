using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reQuest.Interfaces;
using reQuest.Models;
using reQuest.Services;
using reQuest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public partial class GamePage : ContentPage
	{
		private reQuestService service;
		private GameViewModel gameViewModel;
		private Quest quest;

		private bool isTarget { get; set; } = false;
		private bool isFinished { get; set; } = false;
		private bool isInBTRange { get; set; } = false;

		public GamePage(Quest quest, reQuestService service)
		{
			InitializeComponent();
			this.service = service;
			this.quest = quest;

			isTarget = (service.CurrentPlayer.Id == quest.OwnerId);

		}

		async override protected void OnAppearing()
		{
			var game = new Game() { QuestId = quest.Id };
			await service.SaveGameAsync(game);

			gameViewModel = new GameViewModel(game);
			BindingContext = gameViewModel;

			await StartGame();
		}

		async Task UpdateGame()
		{
			await service.RefreshDataAsync(true);

			GameMap.Pins.Clear();

			var ownerPin = new Pin()
			{
				Label = gameViewModel.Owner.ExternalId,
				Position = new Position(gameViewModel.Owner.Latitude, gameViewModel.Owner.Longitude),
				Type = PinType.SearchResult,
			};
			GameMap.Pins.Add(ownerPin);

			//var playerPin = new Pin()
			//{
			//	Label = service.CurrentPlayer.ExternalId,
			//	Position = new Position(service.CurrentPlayer.Latitude, service.CurrentPlayer.Longitude),
			//	Type = PinType.SearchResult,
			//};
			//GameMap.Pins.Add(playerPin);

			if (!isInBTRange)
			{
				gameViewModel.DistanceToTarget = App.Location.DistanceBetweenPostitions(gameViewModel.Owner.Latitude, gameViewModel.Owner.Longitude, service.CurrentPlayer.Latitude, service.CurrentPlayer.Longitude);				
			}

			GameMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(gameViewModel.Owner.Latitude, gameViewModel.Owner.Longitude), Distance.FromMeters(gameViewModel.DistanceToTarget)));
		}

		async Task StartGame()
		{
			if (isTarget && App.Location != null)
			{
				App.Location.StartBeacon("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", gameViewModel.Owner.Id);
			}
			else
			{
				App.Location.StartBeaconRanging("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", gameViewModel.Owner.Id);
				App.Location.collitionDetected += HandleCollisionDetected;
			}


			await UpdateGame();
		}

		async void HandleCollisionDetected(object sender, IBTData e)
		{
			isInBTRange = true;
			gameViewModel.DistanceToTarget = e.Distance;

			if (e.Distance < 1.0d && e.Distance > 0 && !isFinished)
			{
				isFinished = true;
				App.Location.StopBeaconRanging();
				await DisplayAlert("reQuest Won!", $"{service.CurrentPlayer.ExternalId} have reached {gameViewModel.Owner.ExternalId}", "Ok");
				await Navigation.PopAsync(true);
			}
			else
			{
				await UpdateGame();
			}

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
