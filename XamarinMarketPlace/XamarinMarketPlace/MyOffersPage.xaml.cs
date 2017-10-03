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
            manager = OfferManager.DefaultManager;
            Init();
			InitializeComponent ();
		}

        private async void Init()
        {
            offers = await manager.GetOffersByUserId(Constants.UserId);
            OffersView.ItemsSource = offers;
        }

        async void Offer_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}