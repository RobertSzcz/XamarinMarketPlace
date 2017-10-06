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

		public EditOfferPage ()
		{
            manager = OfferManager.DefaultManager;
			InitializeComponent ();
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
                offer.Price = EntryPrice.Text;
                offer.Description = EntryDescription.Text;

                // update offer
                await UpdateItem(offer);

                // pop this page and go back to my offers
                await Navigation.PopAsync();
            }
        }
    }
}