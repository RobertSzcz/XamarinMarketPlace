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
    public partial class SignOutPage : ContentPage
    {
        public SignOutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            if (App.userIsSignedIn)
            {
                SigningOutAlertYesNoClicked();
            }
            else
            {
                Navigation.PushModalAsync(new NavigationPage(new LandingPage()));
            }
        }

        async void SigningOutAlertYesNoClicked()
        {
            var answer = await DisplayAlert("Signing Out", "Are you sure you want to sign out?", "Yes", "No");
            if (answer)
            {
                App.userIsSignedIn = false;
                await Navigation.PushModalAsync(new NavigationPage(new LandingPage()));
            }
            else
            {
                var tabbedPage = this.Parent as TabbedPage;
                tabbedPage.CurrentPage = tabbedPage.Children[0];
            }
        }
    }
}