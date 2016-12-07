using Microsoft.WindowsAzure.MobileServices.Files;
using Plugin.Media;
using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;
using Newtonsoft.Json;

namespace reQuest.ViewModels
{
	public class QuestViewModel : INotifyPropertyChanged
	{
		private reQuestService service;

		//private JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };


		private string title;
		private string description;
		private Player owner;
		private Topic topic;
		private TimeSpan timeout;
		private string uri;
		private double distanceToTarget;
		//private List<string> activePlayerIds;
		//private List<string> passivePlayerIds;

		public string QuestId { get; set; }

		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		public string Description
		{
			get { return description; }
			set
			{
				description = value;
				OnPropertyChanged(nameof(Description));
			}
		}

		public Player Owner
		{
			get { return owner; }
			set
			{
				owner = value;
				OnPropertyChanged(nameof(Owner));
			}
		}
		public Topic Topic
		{
			get { return topic; }
			set
			{
				topic = value;
				OnPropertyChanged(nameof(Topic));
			}
		}

		public TimeSpan Timeout
		{
			get { return timeout; }
			set
			{
				timeout = value;
				OnPropertyChanged(nameof(Timeout));
			}
		}
		public string Uri
		{
			get { return uri; }
			set
			{
				uri = value;
				OnPropertyChanged(nameof(Uri));
			}
		}
		public double DistanceToTarget
		{
			get { return distanceToTarget; }
			set
			{
				distanceToTarget = value;
				OnPropertyChanged(nameof(DistanceToTarget));
			}
		}
		//public List<string> ActivePlayerIds
		//{
		//	get { return activePlayerIds; }
		//	set
		//	{
		//		activePlayerIds = value;
		//		OnPropertyChanged(nameof(ActivePlayerIds));
		//	}
		//}
		//public List<string> PassivePlayerIds
		//{
		//	get { return passivePlayerIds; }
		//	set
		//	{
		//		passivePlayerIds = value;
		//		OnPropertyChanged(nameof(PassivePlayerIds));
		//	}
		//}



		public QuestViewModel()
		{
		}

		public QuestViewModel(Quest quest)
		{
			service = reQuestService.Instance;

			QuestId = quest.Id;
			owner = service.Players.FirstOrDefault(p => p.Id == quest.OwnerId);
			title = quest.Title;
			description = quest.Description;
			topic = service.Topics.FirstOrDefault(t => t.Id == quest.TopicId);
			timeout = quest.Timeout;

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var reQuestFolder = System.IO.Path.Combine(rootFolder.Path, "..", "Documents", "reQuest");
			uri = System.IO.Path.Combine(reQuestFolder, quest.Id + ".jpg");
			//Debug.WriteLine($"QuestViewModel:QuestViewModel:uri: {uri}");

		}

		//public async Task AddCurrentPlayer()
		//{
		//	var quest = service.Quests.FirstOrDefault(q => q.Id == QuestId);

		//	var playerIds = new List<string>();
		//	if (quest.ActivePlayerIds.Length != 0)
		//	{
		//		playerIds = JsonConvert.DeserializeObject<List<string>>(quest.ActivePlayerIds, jsonSerializerSettings);
		//	}

		//	if (!playerIds.Contains(service.CurrentPlayer.Id))
		//	{
		//		playerIds.Add(service.CurrentPlayer.Id);

		//		quest.ActivePlayerIds = JsonConvert.SerializeObject(playerIds);
		//		await service.SaveQuestAsync(quest);
		//	}


		//	RefreshPlayerIds();
		//}

		//public void RefreshPlayerIds()
		//{
		//	var quest = service.Quests.FirstOrDefault(q => q.Id == QuestId);

		//	activePlayerIds = JsonConvert.DeserializeObject<List<string>>(quest.ActivePlayerIds, jsonSerializerSettings);
		//	passivePlayerIds = JsonConvert.DeserializeObject<List<string>>(quest.PassivePlayerIds, jsonSerializerSettings);

		//}

		//public async Task RegisterWin()
		//{ 
		//	var quest = service.Quests.FirstOrDefault(q => q.Id == QuestId);

		//	quest.WinnerId = service.CurrentPlayer.Id;
		//	await service.SaveQuestAsync(quest);



		//}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
