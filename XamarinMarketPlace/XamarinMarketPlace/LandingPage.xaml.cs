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
	public partial class LandingPage : ContentPage
	{
		public LandingPage ()
		{
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void SignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInPage());
        }

        private void Browse_Clicked(object sender, EventArgs e)
        {
            App.userIsSignedIn = false;
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}