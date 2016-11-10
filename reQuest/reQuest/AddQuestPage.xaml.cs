using System;
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

namespace reQuest
{
    public partial class AddQuestPage : ContentPage
    {
        private reQuestService service;

        //public Quest Quest { get; set; }
        //public ObservableCollection<QuestImage> Images { get; set; }
        public QuestViewModel questViewModel = new QuestViewModel();


        public AddQuestPage()
        {
            InitializeComponent();
            this.BindingContext = questViewModel;
            service = reQuestService.DefaultManager;

        }

        public async void OnAdd(object sender, EventArgs e)
        {
			//var quest = new Quest { Title = title.Text };
   //         await service.SaveQuestAsync(quest);


   //         //IPlatform mediaProvider = DependencyService.Get<IPlatform>();

   //         //string sourceImagePath = await mediaProvider.TakePhotoAsync(App.UIContext);

			//if (image.Source != null)
   //         {
			//	string imagePath = image.Source.GetValue(UriImageSource.UriProperty).ToString();
			//	MobileServiceFile file = await this.service.AddImage(quest, imagePath);

   //         }

            //newQuestTitle.Text = string.Empty;
            //newQuestTitle.Unfocus();
        }

		public async void OnAcquireClicked(object sender, EventArgs e)
		{ 
			await CrossMedia.Current.Initialize();
			//if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			//{
			//    DisplayAlert("No Camera", ":( No camera available.", "OK");
			//    return;
			//}
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "reQuest",
				Name = "test.jpg"
			});
			if (file == null)
				return;

			//await DisplayAlert("File Location", file.Path, "OK");
			Debug.WriteLine($"File Location: {file.Path}");

			var quest = new Quest { Title = title.Text };
			await service.SaveQuestAsync(quest);


			//IPlatform mediaProvider = DependencyService.Get<IPlatform>();

			//string sourceImagePath = await mediaProvider.TakePhotoAsync(App.UIContext);

			if (image.Source != null)
			{
				await this.service.AddImage(quest, file.Path);

			}


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
