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

            var browseOffersPage = new NavigationPage(new BrowseOffersPage());
            browseOffersPage.Title = "Browse Offers";
            Children.Add(browseOffersPage);

            if (App.userIsSignedIn)
            {
                Children.Add(new AddOfferPage());

                var myOffersPage = new NavigationPage(new MyOffersPage());
                myOffersPage.Title = "My Offers";
                Children.Add(myOffersPage);

                var signOutPage = new SignOutPage();
                signOutPage.Title = "Sign Out";
                Children.Add(signOutPage);

            }
            else
            {
                var signInPage = new SignOutPage();
                signInPage.Title = "Sign In";
                Children.Add(signInPage);
            }

        }

    }
}
