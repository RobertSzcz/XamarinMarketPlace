using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace XamarinMarketPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPreviewPage : ContentPage
    {

        string phonenumber = "233232";
        bool offerInfoIsFullscreen = false;

        public OfferPreviewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            SetOfferInfoLayoutBounds();
            GetImage();
        }

        protected override void OnDisappearing()
        {
            Navigation.PopAsync();
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
                        dialer.Dial(phonenumber);
                    }
                } catch (Exception err) { Debug.WriteLine(err); }

            }
        }
        public void layoutTapped(object sender, EventArgs e)
        {
            var main = this.Bounds;

            if (!offerInfoIsFullscreen)
            {
                offerInfoBackground.LayoutTo(new Rectangle(0, 0, main.Width, main.Height), 300, Easing.SinIn);
                offerInfoLayout.LayoutTo(new Rectangle(0, 0, main.Width, main.Height), 300, Easing.SinIn);
                callButton.LayoutTo(new Rectangle(callButton.X, 10, callButton.Width, callButton.Height), 300, Easing.SinIn);
                offerInfoIsFullscreen = true;
            } else
            {
                double callButtonPosY = main.Height - (main.Height / 3) - (callButton.Height / 2);

                offerInfoBackground.LayoutTo(new Rectangle(0, main.Height - (main.Height / 3), main.Width, main.Height), 300, Easing.SinIn);
                offerInfoLayout.LayoutTo(new Rectangle(0, main.Height - (main.Height / 3), main.Width, main.Height), 300, Easing.SinIn);
                callButton.LayoutTo(new Rectangle(callButton.X, callButtonPosY, callButton.Width, callButton.Height), 300, Easing.SinIn);
                offerInfoIsFullscreen = false;
            }
        }

        private void SetOfferInfoLayoutBounds()
        {
            var main = this.Bounds;

            double callButtonPosY = main.Height - (main.Height / 3) - (callButton.Height / 2);
            double callButtonPosX = main.Width - (main.Width / 3);

            
            offerInfoBackground.Layout(new Rectangle(0, main.Height - (main.Height / 3), main.Width, main.Height));
            offerInfoLayout.Layout(new Rectangle(0, main.Height - (main.Height / 3), main.Width, main.Height));
            callButton.Layout(new Rectangle(callButtonPosX, callButtonPosY, callButton.Width, callButton.Height));

        }
    }
}