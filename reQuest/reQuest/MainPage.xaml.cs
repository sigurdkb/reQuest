using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace reQuest
{
    public partial class MainPage : ContentPage
    {
		LocationViewModel locationViewModel = new LocationViewModel();

        public MainPage()
        {
            InitializeComponent();
			BindingContext = locationViewModel;
        }

		void startBeacon_Clicked(object sender, EventArgs e) 
		{
			if (App.Location != null)
			{
				App.Location.StartBeacon("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", "reQuest");
			}
		}
		void stopBeacon_Clicked(object sender, EventArgs e) 
		{
			App.Location.StopBeacon();
		}
		void startRanging_Clicked(object sender, EventArgs e)
		{
			if (App.Location != null)
			{
				App.Location.StartTrackDistance("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0", "reQuest");
				App.Location.distanceChanged += HandleDistanceChanged;

			}

		}
		void stopRanging_Clicked(object sender, EventArgs e)
		{
			locationViewModel.Distance++;
		}

		void HandleDistanceChanged(object sender, ILocationData e)
		{
            locationViewModel.Distance = e.Distance;
            locationViewModel.Latitude = e.Latitude;
            locationViewModel.Longitude = e.Longitude;
        }
    }
}
