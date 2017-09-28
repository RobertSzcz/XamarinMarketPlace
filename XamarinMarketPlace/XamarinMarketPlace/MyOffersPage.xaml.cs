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
	public partial class MyOffersPage : ContentPage
	{
		public MyOffersPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditOfferPage());
        }
    }
}