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
        private string ownerId;
        private string title;
        private string topicId;
        private TimeSpan timeLimit;
        private string uri;


        public string OwnerId
        {
			get { return ownerId; }
            set
            {
                ownerId = value;
                OnPropertyChanged(nameof(OwnerId));
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

        public string TopicId
        {
            get { return topicId; }
            set
            {
                topicId = value;
                OnPropertyChanged(nameof(TopicId));
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

        //public MobileServiceFile File { get; private set; }

		public QuestViewModel()
		{ 
		}

        public QuestViewModel(Quest quest)
		{
			Id = quest.Id;
			ownerId = quest.OwnerId;
			title = quest.Title;
			topicId = quest.TopicId;
			timeLimit = quest.TimeLimit;

			IFolder rootFolder = FileSystem.Current.LocalStorage;
			//Debug.WriteLine($"QuestViewModel:QuestViewModel:rootFolder: {rootFolder.Path}");

			var reQuestFolder = System.IO.Path.Combine(rootFolder.Path, "..", "Documents", "reQuest");
			//Debug.WriteLine($"QuestViewModel:QuestViewModel:reQuestFolder: {reQuestFolder}");

			                                                  
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
