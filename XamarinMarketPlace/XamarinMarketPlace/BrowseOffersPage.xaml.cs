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
            GetOffers();
            InitializeComponent();
        }

        private async void GetOffers()
        {
            offers = await manager.GetOffersAsync();
            OffersView.ItemsSource = offers;
        }

        async void Offer_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void To_LandingPage_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LandingPage());
        }
        
    }
}