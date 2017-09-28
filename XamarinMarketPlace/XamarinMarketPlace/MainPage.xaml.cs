using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMarketPlace
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            InitLandingPage();
        }

        /*
        async protected override void OnAppearing()
        {
            //base.OnAppearing();
            await Navigation.PushAsync(new LandingPage());
        }
        */

        async void InitLandingPage()
        {
            await Navigation.PushModalAsync(new LandingPage());
        }

    }
}
