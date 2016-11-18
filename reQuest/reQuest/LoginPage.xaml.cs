using System;
using System.Collections.Generic;
using reQuest.Services;
using Xamarin.Forms;

namespace reQuest
{
	public partial class LoginPage : ContentPage
	{
		private reQuestService service;

		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			service = reQuestService.Instance;
		}

		async void onLoginClicked(object sender, EventArgs args)
		{
			await service.RefreshDataAsync(true);
			if (service.SetCurrentPlayer(username.Text))
			{
				await Navigation.PushAsync(new MainPage());
			}
		}
	}
}
