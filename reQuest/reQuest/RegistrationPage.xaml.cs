using System;
using System.Collections.Generic;
using reQuest.Interfaces;
using Xamarin.Forms;
using reQuest.Services;

namespace reQuest
{
	public partial class RegistrationPage : ContentPage
	{
		bool authenticated = false;
		RegistrationViewModel registrationViewModel = new RegistrationViewModel();

		public RegistrationPage()
		{
			InitializeComponent();
			BindingContext = registrationViewModel;
		}
		async void loginButton_Clicked(object sender, EventArgs e)
		{
			if (App.Authenticator != null)
			{
				App.Authenticator.userAuthenticated += handleUserAuthenticated;
				authenticated = await App.Authenticator.Authenticate();
			}
		}
		async void sidButton_Clicked(object sender, EventArgs e)
		{
			if (authenticated)
			{
				var sid = await reQuestService.Instance.GetUserSid();
				registrationViewModel.ExternalID = sid.ToString();
			}
		}

		void handleUserAuthenticated(object sender, IAuthenticateData e)
		{
			//registrationViewModel.ExternalID = e.UserID;
			registrationViewModel.ExternalToken = e.UserToken;
		}
}
}
