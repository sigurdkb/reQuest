using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace reQuest
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		async void onLoginClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new MainPage());
		}
	}
}
