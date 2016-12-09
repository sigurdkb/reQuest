using reQuest.Models;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using reQuest.ViewModels;

namespace reQuest
{
	public partial class ViewQuestPage : CarouselPage
    {
        private reQuestService service;
		private QuestViewModel QuestVM { get; set; }

        public ViewQuestPage(QuestViewModel questVM, reQuestService service)
        {
            InitializeComponent();
			this.Title = questVM.Title;
			this.service = service;
			this.QuestVM = questVM;

			this.BindingContext = QuestVM;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var ownerPin = new Pin() { Position = new Position(QuestVM.Owner.Latitude, QuestVM.Owner.Longitude), Label = QuestVM.Owner.ExternalId };
			QuestMap.Pins.Add(ownerPin);
			QuestMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(service.CurrentPlayer.Latitude, service.CurrentPlayer.Longitude), Distance.FromMeters(100d)));

			if (service.CurrentPlayer.ExternalId == QuestVM.Owner.ExternalId)
			{
				accept.Text = "View";
			}
		}


		public async void OnAcceptClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new GamePage(QuestVM, service));

			
		}
    }
}
