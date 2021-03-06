﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using reQuest.Services;
using reQuest.Interfaces;
using Xamarin.Forms.GoogleMaps;

namespace reQuest.iOS
{
	public class AuthenticateData : EventArgs, IAuthenticateData
	{
		public string UserID { get; set; }
		public string UserToken { get; set; }

	}

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate, IAuthenticate
    {
		// Define a authenticated user.
		private MobileServiceUser user;

		public event EventHandler<IAuthenticateData> userAuthenticated;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
			Xamarin.FormsGoogleMaps.Init("AIzaSyDitF8S4q5FZjklRCT-RyuP4-KfuCMrgI4");

			SQLitePCL.CurrentPlatform.Init();

            App.Init(this);

            LoadApplication(new reQuest.App());

            return base.FinishedLaunching(app, options);
        }

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Microsoft login using a server-managed flow.
                if (user == null)
                {
					user = await reQuestService.Instance.CurrentClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
                                    MobileServiceAuthenticationProvider.MicrosoftAccount);
                    if (user != null)
                    {
                        message = string.Format("You are now signed-in as {0}.", user.UserId);
						success = true;

						var authenticateData = new AuthenticateData();
						authenticateData.UserID = String.Copy(user.UserId);
						authenticateData.UserToken = String.Copy(user.MobileServiceAuthenticationToken);
						userAuthenticated(this, authenticateData);

                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            UIAlertView avAlert = new UIAlertView("Sign-in result", message, null, "OK", null);
            avAlert.Show();

            return success;
        }

    }
}
