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
		bool authenticated = false;

        public MainPage()
        {
            InitializeComponent();
        }

        async void loginButton_Clicked(object sender, EventArgs e)
		{
			if (App.Authenticator != null)
			{
				authenticated = await App.Authenticator.Authenticate();
			}
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

	}
}
