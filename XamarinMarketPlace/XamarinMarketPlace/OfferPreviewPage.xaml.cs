using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.IO;

namespace XamarinMarketPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPreviewPage : ContentPage
    {

        string phonenumber = "233232";

        public OfferPreviewPage()
        {
            InitializeComponent();

            //offerImage.Source = FileImageSource.FromUri(
            //    new Uri("http://www.123mobilewallpapers.com/wp-content/uploads/2014/09/snow_on_hill.jpg"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetImage();
        }

        private async void GetImage()
        {
            var offer = BindingContext as Offer;

            var bytes = await BlobManager.GetImage(offer.PhotoId);

            offerImage.Source = ImageSourceGenerator.Call(bytes);
        }

        async void Call_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + phonenumber + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();

                try {
                    if (dialer != null)
                    {
                        //App.PhoneNumbers.Add(phonenumber.Text);
                        dialer.Dial(phonenumber);
                    }
                } catch (Exception err) { Debug.WriteLine(err); }

            }
        }
        public void layoutTapped(object sender, EventArgs e)
        {
            grid1.BackgroundColor = new Color(255, 0, 0);
            
        }
    }
}