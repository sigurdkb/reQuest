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

namespace reQuest.ViewModels
{
    public class QuestViewModel : INotifyPropertyChanged
    {
		private string Id;
        private Player owner;
        private string title;
		private Topic topic;
        private TimeSpan timeLimit;
        private string uri;

		private reQuestService service;


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

        public TimeSpan TimeLimit
        {
            get { return timeLimit; }
            set
            {
                timeLimit = value;
                OnPropertyChanged(nameof(TimeLimit));
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

		public QuestViewModel()
		{ 
		}

        public QuestViewModel(Quest quest)
		{
			service = reQuestService.Instance;

			Id = quest.Id;
			owner = service.Players.FirstOrDefault(p => p.Id == quest.OwnerId);
			title = quest.Title;
			topic = service.Topics.FirstOrDefault(t => t.Id == quest.TopicId); 
			timeLimit = quest.TimeLimit;

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var reQuestFolder = System.IO.Path.Combine(rootFolder.Path, "..", "Documents", "reQuest");
			uri = System.IO.Path.Combine(reQuestFolder, quest.Id + ".jpg");
			Debug.WriteLine($"QuestViewModel:QuestViewModel:uri: {uri}");


        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
