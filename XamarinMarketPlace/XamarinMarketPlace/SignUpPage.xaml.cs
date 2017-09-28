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
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

        async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            //cant do multiple await actions
            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new MainPage());
        }
    }
}