using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using reQuest.Models;
using reQuest.Services;
using Xamarin.Forms.GoogleMaps;
using Newtonsoft.Json;

namespace reQuest
{
	public class GameViewModel : INotifyPropertyChanged
	{
		private reQuestService service;
		private double distanceToTarget;
		private Quest quest;
		private Player owner;

		public List<Player> Players { get; set; } = new List<Player>();
		public List<Player> Participants { get; set; } = new List<Player>();

		public double DistanceToTarget
		{
			get { return distanceToTarget; }
			set
			{
				if (distanceToTarget != value)
				{
					distanceToTarget = value;
					OnPropertyChanged(nameof(DistanceToTarget));
				}
			}
		}

		public Player Owner
		{
			get { return owner; }
			set
			{
				if (owner != value)
				{
					owner = value;
					OnPropertyChanged(nameof(Owner));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public GameViewModel(Game game)
		{
			service = reQuestService.Instance;
			quest = service.Quests.FirstOrDefault(q => q.Id == game.QuestId);
			owner = service.Players.FirstOrDefault(p => p.Id == quest.OwnerId);

			var playerIds = JsonConvert.DeserializeObject<List<string>>(game.Players);
			foreach (var playerId in playerIds)
			{
				Players.Add(service.Players.FirstOrDefault(p => p.Id == playerId));
			}
			var participantIds = JsonConvert.DeserializeObject<List<string>>(game.Participants);
			foreach (var participantId in participantIds)
			{
				Participants.Add(service.Players.FirstOrDefault(p => p.Id == participantId));
			}
		}

		public void Update(Game game)
		{
			Players.Clear();
			var playerIds = JsonConvert.DeserializeObject<List<string>>(game.Players);
			foreach (var playerId in playerIds)
			{
				Players.Add(service.Players.FirstOrDefault(p => p.Id == playerId));
			}
			Participants.Clear();
			var participantIds = JsonConvert.DeserializeObject<List<string>>(game.Participants);
			foreach (var participantId in participantIds)
			{
				Participants.Add(service.Players.FirstOrDefault(p => p.Id == participantId));
			}
			
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
