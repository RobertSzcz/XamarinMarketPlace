using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

// from azure sample code "toDoItem"
// modified for our purposes
namespace XamarinMarketPlace
{
    public partial class OfferManager
    {
        static OfferManager defaultInstance = new OfferManager();
        MobileServiceClient client;
        IMobileServiceTable<Offer> offerTable;
        
        private OfferManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            this.offerTable = client.GetTable<Offer>();
        }

        public static OfferManager DefaultManager
        {
            get { return defaultInstance; }
            private set { defaultInstance = value; }
        }

        public async Task<ObservableCollection<Offer>> GetOffersAsync()
        {
            try
            {
                IEnumerable<Offer> items = await offerTable
                    .Where (offer => offer.Removed == false)
                    .ToEnumerableAsync();

                return new ObservableCollection<Offer>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Offer>> GetOffersByUserId(string userId)
        {
            try
            {
                IEnumerable<Offer> items = await offerTable
                    .Where (offer => offer.UserId == userId)
                    .Where (offer => offer.Removed == false)
                    .ToEnumerableAsync();

                return new ObservableCollection<Offer>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveOfferAsync(Offer offer)
        {
            if (offer.Id == null)
            {
                await offerTable.InsertAsync(offer);
            }
            else
            {
                await offerTable.UpdateAsync(offer);
            }
        }
    }
}