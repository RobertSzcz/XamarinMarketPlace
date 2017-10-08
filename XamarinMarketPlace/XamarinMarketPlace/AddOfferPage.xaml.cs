using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMarketPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddOfferPage : ContentPage
	{
        OfferManager manager;

		public AddOfferPage ()
		{
			InitializeComponent ();

            manager = OfferManager.DefaultManager;
		}

        async Task AddItem(Offer offer)
        {
            // sends offer to the manager to be handled
            await manager.SaveOfferAsync(offer);
        }

        public async void AddOffer_Clicked(object sender, EventArgs e)
        {
            string title = EntryTitle.Text;
            string price = EntryPrice.Text;
            string description = EntryDescription.Text;
            // testing purposes
            string userId = Constants.UserId;
            
            // checks whether all information is filled
            if (title == "" || price == "" || description == "")
            {
                await DisplayAlert("Error", "Please fill all the entries.", "OK");
            }
            else
            {
                // create new offer and send it to the db
                var offer = new Offer { Title = title, Price = price, Description = description, UserId = userId };
                await AddItem(offer);

                // clear the entries
                EntryTitle.Text = "";
                EntryPrice.Text = "";
                EntryDescription.Text = "";
            }
        }

        public async void TakePicture_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", "No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Pictures",
                Name = Guid.NewGuid().ToString()
            });

            if (file == null) return;

            Image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EntryTitle.Text) && 
                !string.IsNullOrWhiteSpace(EntryPrice.Text) &&
                !string.IsNullOrWhiteSpace(EntryDescription.Text))
            {
                Btn_AddOffer.IsEnabled = true;
            }
        }
    }
}