﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.WindowsAzure.MobileServices.Files;
using Xamarin.Forms;
using reQuest.Services;
using reQuest.Models;
using reQuest.ViewModels;
using reQuest.Interfaces;
using Plugin.Media;
using System.Diagnostics;
using System.IO;

namespace reQuest
{
    public partial class AddQuestPage : ContentPage
    {
        private reQuestService service;

		//public Quest Quest { get; set; }
		//public ObservableCollection<QuestImage> Images { get; set; }
		//public QuestViewModel QuestViewModel { get; set; } = new QuestViewModel();


        public AddQuestPage()
        {
            InitializeComponent();
            //this.BindingContext = QuestViewModel;
			service = reQuestService.Instance;

			foreach (var t in service.Topics)
			{
				topic.Items.Add(t.LongName);
			}

        }

   //     public async void OnAdd(object sender, EventArgs e)
   //     {
			//var quest = new Quest { Title = title.Text };
   //         await service.SaveQuestAsync(quest);



			//if (image.Source != null)
			//{
			//	FileHelper.GetLocalFilePathAsync(quest.Id, Path.GetFileName(QuestViewModel.Uri)).ContinueWith(x => QuestViewModel.Uri = x.Result);
			//	Debug.WriteLine($"File Location: {QuestViewModel.Uri}");
			//	MobileServiceFile file = await this.service.AddImage(quest, QuestViewModel.Uri);

   //         }

   //         //newQuestTitle.Text = string.Empty;
   //         //newQuestTitle.Unfocus();
   //     }

		public async void OnAcquireClicked(object sender, EventArgs e)
		{
			var quest = new Quest
			{
				Title = title.Text,
				OwnerId = service.CurrentPlayer.Id,
				TopicId = service.Topics.ElementAt(topic.SelectedIndex).Id,
				Timeout = new TimeSpan(0, int.Parse(timeout.Items.ElementAtOrDefault(timeout.SelectedIndex)), 0)
			};
			await service.SaveQuestAsync(quest);

			await CrossMedia.Current.Initialize();

			//if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			//{
			//    DisplayAlert("No Camera", ":( No camera available.", "OK");
			//    return;
			//}
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "reQuest",
				Name = quest.Id + ".jpg"
			});
			if (file == null)
				return;

			Debug.WriteLine($"AddQuestPage:OnAcquireClicked:file.Path: {file.Path}");
			//QuestViewModel.Uri = file.Path;
			await service.AddImage(quest, file.Path);

			//image.Source = ImageSource.FromUri(new Uri(file.Path));
			image.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

		}
    }
}
