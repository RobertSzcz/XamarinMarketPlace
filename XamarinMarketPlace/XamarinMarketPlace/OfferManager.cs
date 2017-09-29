using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinMarketPlace
{
    public partial class OfferManager
    {
        static OfferManager defaultInstance = new OfferManager();
        MobileServiceClient client;

        IMobileServiceTable<Offer> offerTable;

        const string offlineDbPath = @"localstore.db";

        private OfferManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);

            this.offerTable = client.GetTable<Offer>();
        }

        public static OfferManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public bool IsOfflineEnabled
        {
            get { return offerTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Offer>; }
        }

        public async Task<ObservableCollection<Offer>> GetOffersAsync(bool syncOffers = false)
        {
            try
            {
                IEnumerable<Offer> items = await offerTable
                    .Where(offerItem => !offerItem.Done)
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