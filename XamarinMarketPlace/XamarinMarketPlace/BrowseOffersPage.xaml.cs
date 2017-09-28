using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMarketPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowseOffersPage : ContentPage
	{
		public BrowseOffersPage ()
		{
			InitializeComponent ();
		}

        private void To_LandingPage_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LandingPage());
        }
        
    }
}