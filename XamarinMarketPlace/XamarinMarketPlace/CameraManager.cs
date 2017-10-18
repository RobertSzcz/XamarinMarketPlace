using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Media;
using Xamarin.Forms;

namespace XamarinMarketPlace
{
    class CameraManager
    {
        public static async Task<System.IO.MemoryStream> TakePictureAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                // display some alert ?
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Pictures",
                Name = Guid.NewGuid().ToString()
            });

            if (file == null) return null;

            var ms = new System.IO.MemoryStream();
            file.GetStream().CopyTo(ms);
            return ms;
        }
    }
}
