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

namespace reQuest.ViewModels
{
    public class QuestViewModel : INotifyPropertyChanged
    {
        private Player owner;
        private string title;
        private Topic topic;
        private TimeSpan timeLimit;
        private ImageSource imageSource;


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
        public ImageSource ImageSource
        {
            get { return imageSource; }
			set
			{
				imageSource = value;
				OnPropertyChanged(nameof(ImageSource));
			}
		}

        //public MobileServiceFile File { get; private set; }

        public QuestViewModel()
        {
            //imageSource = ImageSource.FromResource("reQuest-Logo.png");
        }

        //public QuestViewModel(MobileServiceFile file, Quest quest)
        //{
        //    File = file;
        //    FileHelper.GetLocalFilePathAsync(quest.Id, file.Name).ContinueWith(x => this.Uri = x.Result);
        //}

    //    public ICommand AcquirePictureCommand
    //    {
    //        get
    //        {
				//return new Command(async () =>
    //            {
    //                await CrossMedia.Current.Initialize();
    //                //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
    //                //{
    //                //    DisplayAlert("No Camera", ":( No camera available.", "OK");
    //                //    return;
    //                //}
    //                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
    //                {
    //                    Directory = "reQuest",
    //                    Name = "test.jpg"
    //                });
    //                if (file == null)
    //                    return;

				//	//await DisplayAlert("File Location", file.Path, "OK");
				//	Debug.WriteLine($"File Location: {file.Path}");

    //                imageSource = ImageSource.FromUri(new Uri(file.Path));

    //                OnPropertyChanged(nameof(ImageSource));
    //            });
    //        }
    //    }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
