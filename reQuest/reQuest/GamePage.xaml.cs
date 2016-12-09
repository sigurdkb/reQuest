using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
		private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };

		private reQuestService service;
		private QuestViewModel questVM;

		private bool isTarget { get; set; } = false;
		private bool isFinished { get; set; } = false;
		private bool isInBTRange { get; set; } = false;

		public GamePage(QuestViewModel questVM, reQuestService service)
		{
			InitializeComponent();
			this.service = service;
			this.questVM = questVM;

			isTarget = (service.CurrentPlayer.Id == questVM.Owner.Id);

		}

		async override protected void OnAppearing()
		{

			BindingContext = questVM;

			if (App.Location != null)
			{
				if (isTarget)
				{
					targetData.IsVisible = false;
					await ViewGame();
				}
				else
				{
					await StartGame();
				}
			}
		}

		async Task StartGame()
		{
			App.Location.StartBeaconRanging("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", questVM.Owner.Id);
			App.Location.collitionDetected += HandleCollisionDetected;
			App.Location.positionChanged += HandlePositionChanged;
			await AddCurrentPlayer();
			await UpdateGame();
		}

		async Task ViewGame()
		{
			App.Location.StartBeacon("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", questVM.Owner.Id);
			App.Location.positionChanged += HandlePositionChanged;

			var quest = service.Quests.FirstOrDefault(q => q.Id == questVM.QuestId);

			while (quest.WinnerId == null)
			{
				await UpdateGame();
				quest = service.Quests.FirstOrDefault(q => q.Id == questVM.QuestId);
				if (quest.WinnerId == null)
				{
					await Task.Delay(5000);
				}
			}

			App.Location.StopBeacon();
			var winner = service.Players.FirstOrDefault(p => p.Id == quest.WinnerId);

			await DisplayAlert("reQuest Finished!", $"{winner.ExternalId} have reached you", "Ok");
			await Navigation.PopAsync(true);
		}

		async Task UpdateGame()
		{
			await service.RefreshDataAsync(true);

			GameMap.Pins.Clear();

			var quest = service.Quests.FirstOrDefault(q => q.Id == questVM.QuestId);
			var playerIds = JsonConvert.DeserializeObject<List<string>>(quest.ActivePlayerIds, jsonSerializerSettings);
			if (playerIds != null)
			{
				foreach (var playerId in playerIds)
				{
					if (playerId == service.CurrentPlayer.Id) { continue; }

					var player = service.Players.FirstOrDefault(p => p.Id == playerId);
					//var pinImage = new Image() { Source = ImageSource.FromResource("user.png") };

					var playerPin = new Pin()
					{
						Label = player.ExternalId,
						Position = new Position(player.Latitude, player.Longitude),
						Type = PinType.SearchResult,
						//Icon = BitmapDescriptorFactory.FromView(pinImage)
					};
					GameMap.Pins.Add(playerPin);
				}

			}

			if (!isTarget)
			{
				var ownerPin = new Pin()
				{
					Label = questVM.Owner.ExternalId,
					Position = new Position(questVM.Owner.Latitude, questVM.Owner.Longitude),
					Type = PinType.SearchResult,
					//Icon = BitmapDescriptorFactory.FromView(new Image() { Source = ImageSource.FromResource("question.png") } )
				};
				GameMap.Pins.Add(ownerPin);

			}

			if (isTarget)
			{
				GameMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(questVM.Owner.Latitude, questVM.Owner.Longitude), Distance.FromMeters(70)));
			}
			else
			{
				if (!isInBTRange)
				{
					questVM.DistanceToTarget = App.Location.DistanceBetweenPostitions(questVM.Owner.Latitude, questVM.Owner.Longitude, service.CurrentPlayer.Latitude, service.CurrentPlayer.Longitude);
					Debug.WriteLine($"GamePage:UpdateGame:questVM.DistanceToTarget: {questVM.DistanceToTarget}");

				}
				GameMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(questVM.Owner.Latitude, questVM.Owner.Longitude), Distance.FromMeters(questVM.DistanceToTarget * 4)));

			}
		}

		public async Task AddCurrentPlayer()
		{
			var quest = service.Quests.FirstOrDefault(q => q.Id == questVM.QuestId);

			if (!quest.ActivePlayerIds.Contains(service.CurrentPlayer.Id))
			{
				var playerIds = new List<string>();
				if (quest.ActivePlayerIds.Length != 0)
				{
					playerIds = JsonConvert.DeserializeObject<List<string>>(quest.ActivePlayerIds, jsonSerializerSettings);
				}

				playerIds.Add(service.CurrentPlayer.Id);

				quest.ActivePlayerIds = JsonConvert.SerializeObject(playerIds);
				await service.SaveQuestAsync(quest);
			}
		}


		public async Task EndGame()
		{
			var quest = service.Quests.FirstOrDefault(q => q.Id == questVM.QuestId);

			quest.WinnerId = service.CurrentPlayer.Id;
			await service.SaveQuestAsync(quest);

			App.Location.StopBeaconRanging();

			await DisplayAlert("reQuest Won!", $"{service.CurrentPlayer.ExternalId} have reached {questVM.Owner.ExternalId}", "Ok");
			await Navigation.PopAsync(true);


		}



		async void HandlePositionChanged(object sender, IGPSData e)
		{
			await UpdateGame();
		}

		async void HandleCollisionDetected(object sender, IBTData e)
		{
			isInBTRange = true;
			if (e.Distance > 0)
			{
				questVM.DistanceToTarget = e.Distance;
			}

			if (e.Distance < 0.3d && e.Distance > 0 && !isFinished)
			{
				isFinished = true;
				await EndGame();
			}
			else
			{
				await UpdateGame();
			}

		}
	}
}
