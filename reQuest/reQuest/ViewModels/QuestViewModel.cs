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

		private string title;
		private Player owner;
		private Topic topic;
		private TimeSpan timeout;
		private string uri;
		private List<string> activePlayerIds;
		private List<string> passivePlayerIds;


		public string QuestId { get; set; }

		public Player Owner
		{
			get { return owner; }
			set
			{
				owner = value;
				OnPropertyChanged(nameof(Owner));
			}
		}
		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged(nameof(Title));
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
		public List<string> ActivePlayerIds
		{
			get { return activePlayerIds; }
			set
			{
				activePlayerIds = value;
				OnPropertyChanged(nameof(ActivePlayerIds));
			}
		}
		public List<string> PassivePlayerIds
		{
			get { return passivePlayerIds; }
			set
			{
				passivePlayerIds = value;
				OnPropertyChanged(nameof(PassivePlayerIds));
			}
		}



		public QuestViewModel()
		{
		}

		public QuestViewModel(Quest quest)
		{
			service = reQuestService.Instance;

			QuestId = quest.Id;
			owner = service.Players.FirstOrDefault(p => p.Id == quest.OwnerId);
			title = quest.Title;
			topic = service.Topics.FirstOrDefault(t => t.Id == quest.TopicId);
			timeout = quest.Timeout;

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var reQuestFolder = System.IO.Path.Combine(rootFolder.Path, "..", "Documents", "reQuest");
			uri = System.IO.Path.Combine(reQuestFolder, quest.Id + ".jpg");
			//Debug.WriteLine($"QuestViewModel:QuestViewModel:uri: {uri}");

			activePlayerIds = JsonConvert.DeserializeObject<List<string>>(quest.ActivePlayerIds);
			passivePlayerIds = JsonConvert.DeserializeObject<List<string>>(quest.PassivePlayerIds);



		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
