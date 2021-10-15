using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntonEgoReviewsAPI.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Description { get; set; }
        public string HoursOfOperation { get; set; }
        public string AverageRating { get; set; }

    }
}