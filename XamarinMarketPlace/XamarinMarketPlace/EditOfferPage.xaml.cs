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
	public partial class EditOfferPage : ContentPage
	{
        OfferManager manager;
        byte[] photo = null;
        Boolean updatePhoto = false;
        bool viewingImage;

        public EditOfferPage ()
		{
            manager = OfferManager.DefaultManager;
            // load photo from BlobManager here!
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            viewingImage = false;
            GetImage();
        }

        protected override void OnDisappearing()
        {
            if(!viewingImage) Navigation.PopAsync();
        }

        private async Task UpdateItem(Offer offer)
        {
            // sends offer to the manager to be handled
            await manager.SaveOfferAsync(offer);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EntryTitle.Text) &&
                !string.IsNullOrWhiteSpace(EntryPrice.Text) &&
                !string.IsNullOrWhiteSpace(EntryDescription.Text))
            {
                btn_SaveChanges.IsEnabled = true;
            }
        }

        public async void SaveChangesClicked(object sender, EventArgs e)
        {
            Offer offer = BindingContext as Offer;

            // checks whether all information is filled
            if (EntryPrice.Text == "" || EntryDescription.Text == "")
            {
                await DisplayAlert("Error", "Please fill all the entries.", "OK");
            }
            else
            {
                if (updatePhoto)
                {
                    string photoId = Guid.NewGuid().ToString();
                    await BlobManager.UploadImage(photoId, photo);
                    offer.PhotoId = photoId;
                }
                offer.Price = EntryPrice.Text;
                offer.Description = EntryDescription.Text;

                // update offer
                await UpdateItem(offer);

                // pop this page and go back to my offers
                await Navigation.PopAsync();
            }
        }

        public async void TakePictureClicked(object sender, EventArgs e)
        {
            photo = await CameraManager.TakePictureAsync();
            offerImage.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(photo));
            updatePhoto = true;
        }

        public async void DeleteOfferClicked(object sender, EventArgs e)
        {
            Offer offer = BindingContext as Offer;

            var answer = await DisplayAlert("Alert", "Are you sure you want to delete this offer?", "Yes", "No");

            if (answer)
            {
                offer.Removed = true;

                await UpdateItem(offer);

                await Navigation.PopAsync();
            }
        }

        async void ViewImage()
        {
            var viewImagePage = new ViewImagePage();
            viewImagePage.BindingContext = new { offerImage = offerImage.Source };
            viewingImage = true;

            await Navigation.PushModalAsync(viewImagePage);
        }

        private async void GetImage()
        {
            var offer = BindingContext as Offer;

            var bytes = await BlobManager.GetImage(offer.PhotoId);

            offerImage.Source = ImageSourceGenerator.Call(bytes);
        }
    }
}