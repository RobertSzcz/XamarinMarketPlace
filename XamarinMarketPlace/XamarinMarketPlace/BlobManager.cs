﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMarketPlace
{
    public class BlobManager
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;

        public BlobManager()
        {
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=xamarinproject;AccountKey=kUr9XCbX7ZiRil0DHb/HP/vnMj+uludogR0Y/gfj8whDeni/Pl0sCRybz5orrAzHIDVO+21vXTyyHaayGt1djA==;EndpointSuffix=core.windows.net");
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task PerformBlobOperation(string userId, string photoName, byte[] photo)
        {
            // give this method userid, picture name and imagestream, and it creates a blobcontainer with userId, blob with picture name and puts image inside of it
            
            // we want to put our images inside blobcontainer "images"
            CloudBlobContainer container = blobClient.GetContainerReference(userId);
            await container.CreateIfNotExistsAsync();

            // create blob with the name provided
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(photoName);
            
            // finally push the picture into azure as a stream
            await blockBlob.UploadFromByteArrayAsync(photo, 0, photo.Length);
        }
        
        public async Task<byte[]> GetBlob(string userId, string photoname)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(userId);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blob = container.GetBlockBlobReference(photoname);
            
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
