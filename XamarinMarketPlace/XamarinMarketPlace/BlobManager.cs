using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMarketPlace
{
    public class BlobManager
    {
        public BlobManager()
        {

        }

        private static CloudBlobContainer GetContainer()
        {
            var account = CloudStorageAccount.Parse(Constants.ConnectionString);

            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference("photos");

            return container;
        }

        public static async Task UploadImage(string photoName, byte[] photo)
        {
            // give this method userid, picture name and imagestream, and it creates a blobcontainer with userId, blob with picture name and puts image inside of it
            var container = GetContainer();

            await container.CreateIfNotExistsAsync();

            var imageBlob = container.GetBlockBlobReference(photoName);

            await imageBlob.UploadFromByteArrayAsync(photo, 0, photo.Length);
        }
        
        public static async Task<byte[]> GetImage(string photoname)
        {
            var container = GetContainer();

            var blob = container.GetBlockBlobReference(photoname);

            if (await blob.ExistsAsync())
            {
                await blob.FetchAttributesAsync();

                byte[] blobBytes = new byte[blob.Properties.Length];

                await blob.DownloadToByteArrayAsync(blobBytes, 0);

                return blobBytes;
            }
            return null;
        }
    }
}
