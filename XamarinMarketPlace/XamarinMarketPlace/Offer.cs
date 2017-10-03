using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinMarketPlace
{
    public class Offer
    {
        // 'id' and 'done' are there just to be sure this works
        string userId;
        string id;
        string title;
        string description;
        string price;

        [JsonProperty(PropertyName = "userId")]
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [JsonProperty(PropertyName = "price")]
        public string Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
