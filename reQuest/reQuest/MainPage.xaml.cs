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
				authenticated = await App.Authenticator.Authenticate();

			// Set syncItems to true to synchronize the data on startup when offline is enabled.
			//if (authenticated == true)
			//	await RefreshItems(true, syncItems: false);
		}
    }
}
