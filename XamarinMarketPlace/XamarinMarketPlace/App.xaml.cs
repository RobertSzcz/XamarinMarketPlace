using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinMarketPlace
{
    public partial class App : Application
    {

        //Temp variable to check user-signedin status
        public static bool userIsSignedIn { get; set; }

        public App()
        {
            InitializeComponent();

            userIsSignedIn = false;

            //MainPage = new XamarinMarketPlace.MainPage();
            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
