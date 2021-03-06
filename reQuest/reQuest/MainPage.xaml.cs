﻿using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Microsoft.WindowsAzure.MobileServices.Files;
using System.Collections.ObjectModel;
using reQuest.ViewModels;
using System.Diagnostics;

namespace reQuest
{
	public partial class MainPage : CarouselPage
    {
        private reQuestService service;

		public ObservableCollection<QuestViewModel> Quests = new ObservableCollection<QuestViewModel>();



        public MainPage()
        {
			InitializeComponent();
			this.CurrentPage = mainStartPage;
			NavigationPage.SetHasBackButton(this, false);

			service = reQuestService.Instance;
			App.Location.StartLocationTracking();
			App.Location.positionChanged += HandlePositionChanged;


			//TeamMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(58.3341d, 8.5777d), Distance.FromMeters(100d)));
        }

		protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RefreshItems(true, syncItems: false);

        }

		void HandlePositionChanged(object sender, IGPSData e)
		{
			service.SetCurrentPosition(e.Latitude, e.Longitude);
			TeamMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(e.Latitude, e.Longitude), Distance.FromMeters(100d)));

		}

        async void onAddQuestClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new AddQuestPage());
        }

        void onTeammapClicked(object sender, EventArgs args)
        {
            this.CurrentPage = teamMapPage;
        }

		async void OnSelection(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
			{
				return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
			}
			var questVM = e.SelectedItem as QuestViewModel;

			await Navigation.PushAsync(new ViewQuestPage(questVM, service));
		}


        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false, true);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }

        private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
        {
			//using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			//{

			await service.RefreshDataAsync(syncItems);

			Quests.Clear();

			foreach (var quest in service.Quests)
			{
				Quests.Add(new QuestViewModel(quest));
			}
			//}

			questList.ItemsSource = Quests;


		}



    }
}
