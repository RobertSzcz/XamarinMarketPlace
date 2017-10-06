using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMarketPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowseOffersPage : ContentPage
	{
        OfferManager manager = OfferManager.DefaultManager;
        ObservableCollection<Offer> offers = new ObservableCollection<Offer>();

        public BrowseOffersPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            GetOffers();
            InitializeComponent();
        }

        private async void GetOffers()
        {
            offers = await manager.GetOffersAsync();
            OffersView.ItemsSource = offers;
        }
        
        public async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            // this method is fired also when item is de-selected
            if (e.SelectedItem == null)
            {
                return;
            }

            Offer offer;

            // just so that the selection is not visual
            ((ListView)sender).SelectedItem = null;

            // we cast the selected object to offer
            offer = (Offer)e.SelectedItem;

            // send the data to editoffer page
            var offerPreviewPage = new OfferPreviewPage() { BindingContext = offer };
            await Navigation.PushAsync(offerPreviewPage);
        }
    }
}