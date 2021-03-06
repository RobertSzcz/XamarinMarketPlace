﻿using System;
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
        byte[] photo = null;
        ImageSource defaultImage = ImageSource.FromFile("nopicture.png");

		public AddOfferPage ()
		{
			InitializeComponent ();

            offerImage.Source = defaultImage;

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
            if (title == "" || price == "" || description == "" || photo == null)
            {
                await DisplayAlert("Error", "Please fill all the entries.", "OK");
            }
            else
            {
                Btn_AddOffer.IsEnabled = false;

                // this name for photo is going to be in offer table in azure
                // this is also going to be name in blob storage
                string photoId = Guid.NewGuid().ToString();
                
                // var stream = new System.IO.MemoryStream(photo);
                await BlobManager.UploadImage(photoId, photo);

                // create new offer and send it to the db
                var offer = new Offer {
                    Title = title,
                    Price = price,
                    Description = description,
                    UserId = userId,
                    PhotoId = photoId,
                    Removed = false
                };

                await AddItem(offer);

                // clear the entries
                EntryTitle.Text = "";
                EntryPrice.Text = "";
                EntryDescription.Text = "";
                offerImage.Source = null;
                photo = null;
            }
        }

        public async void TakePicture_Clicked(object sender, EventArgs e)
        {
            photo = await CameraManager.TakePictureAsync();
            offerImage.Source = ImageSourceGenerator.Call(photo);
            UpdateButtonStatus();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButtonStatus();
        }

        private void UpdateButtonStatus()
        {
            if (!string.IsNullOrWhiteSpace(EntryTitle.Text) &&
                !string.IsNullOrWhiteSpace(EntryPrice.Text) &&
                !string.IsNullOrWhiteSpace(EntryDescription.Text) &&
                photo != null)
            {
                Btn_AddOffer.IsEnabled = true;
            }
        }

        async void ViewImage()
        {
            if (offerImage.Source != defaultImage) {
                var viewImagePage = new ViewImagePage();
                viewImagePage.BindingContext = new { offerImage = offerImage.Source };
                await Navigation.PushModalAsync(viewImagePage);
            }

        }
    }
}