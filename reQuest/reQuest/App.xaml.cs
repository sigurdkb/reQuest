using reQuest.Interfaces;
using reQuest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace reQuest
{
    public partial class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }
		public static ILocation Location { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public App()
        {
            InitializeComponent();

			MainPage = new GamePage();
        }

        protected override void OnStart()
        {
			// Handle when your app starts
			Location = DependencyService.Get<ILocation> ();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
