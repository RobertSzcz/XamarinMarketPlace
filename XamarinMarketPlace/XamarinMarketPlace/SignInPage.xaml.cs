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
	public partial class SignInPage : ContentPage
	{
		public SignInPage ()
		{
			InitializeComponent ();
		}

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            App.userIsSignedIn = true;
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}