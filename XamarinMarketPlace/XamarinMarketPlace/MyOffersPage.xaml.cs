using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMarketPlace
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyOffersPage : ContentPage
	{
        OfferManager manager;
        ObservableCollection<Offer> offers = new ObservableCollection<Offer>();

		public MyOffersPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            // get the default manager to handle azure
            manager = OfferManager.DefaultManager;
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            // updates listview from azure
            Init();
        }

        private async void Init()
        {
            // get the data from Azure db and put it in collection
            offers = await manager.GetOffersByUserId(Constants.UserId);
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
            var editOfferPage = new EditOfferPage() { BindingContext = offer };
            await Navigation.PushAsync(editOfferPage);
        }
    }
}