using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace XamarinMarketPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPreviewPage : ContentPage
    {
        public OfferPreviewPage()
        {
            InitializeComponent();

            offerImage.Source = FileImageSource.FromUri(
                new Uri("http://www.123mobilewallpapers.com/wp-content/uploads/2014/09/snow_on_hill.jpg"));
        }

        async void Call_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + phonenumber.Text + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();

                try {
                    if (dialer != null)
                    {
                        //App.PhoneNumbers.Add(phonenumber.Text);
                        dialer.Dial(phonenumber.Text);
                    }
                } catch (Exception err) { Debug.WriteLine(err); }

            }
        }
    }
}