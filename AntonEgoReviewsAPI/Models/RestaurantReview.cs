using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntonEgoReviewsAPI.Models
{
    public class RestaurantReview
    {
        public int RestaurantId { get; set; }
        public int Rating { get; set; }
    }
}