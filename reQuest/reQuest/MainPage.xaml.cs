using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public partial class MainPage : CarouselPage
    {

        public MainPage()
        {
			InitializeComponent();
			this.CurrentPage = mainStartPage;
			NavigationPage.SetHasBackButton(this, false);

			TeamMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(58.3341d, 8.5777d), Distance.FromMeters(100d)));

        }

		void onTeammapClicked(object sender, EventArgs args)
		{
			this.CurrentPage = teamMapPage;
		}


    }
}
