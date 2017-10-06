using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMarketPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPreviewPage : ContentPage
    {
        public OfferPreviewPage()
        {
            InitializeComponent();

            offerImage.Source = FileImageSource.FromUri(
                new Uri("http://www.123mobilewallpapers.com/wp-content/uploads/2014/09/snow_on_hill.jpg"));
        }
    }
}