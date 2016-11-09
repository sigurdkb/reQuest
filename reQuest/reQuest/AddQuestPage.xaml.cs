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
            var quest = new Quest { Title = questViewModel.Title };
            await service.SaveQuestAsync(quest);


            //IPlatform mediaProvider = DependencyService.Get<IPlatform>();

            //string sourceImagePath = await mediaProvider.TakePhotoAsync(App.UIContext);

            if (questViewModel.ImageSource != null)
            {
                MobileServiceFile file = await this.service.AddImage(quest, questViewModel.ImageSource.GetValue(UriImageSource.UriProperty).ToString());

            }

            //newQuestTitle.Text = string.Empty;
            //newQuestTitle.Unfocus();
        }
    }
}
