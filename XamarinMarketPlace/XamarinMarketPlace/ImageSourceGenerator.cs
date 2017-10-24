using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMarketPlace
{
    class ImageSourceGenerator
    {
        public static ImageSource Call(byte[] photo)
        {
            return ImageSource.FromStream(() => new System.IO.MemoryStream(photo));
        }
    }
}
