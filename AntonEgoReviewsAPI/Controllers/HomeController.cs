using AntonEgoReviewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;


namespace AntonEgoReviewsAPI.Controllers
{
    public class HomeController : ApiController
    {
        [Route("api/home/addrestraunt")]
        [HttpPost]
        public async Task<Restaurant> SaveNewRestraunt([FromBody] Restaurant selectedRestraunt)
        {
            return new Restaurant();
        }

        [Route("api/home/addrestraunt")]
        [HttpPost]
        public async Task<Restaurant> DeleteRestraunt([FromBody] Restaurant selectedRestraunt)
        {
            return new Restaurant();
        }

        [Route("api/home/addrestraunt")]
        [HttpPost]
        public async Task<Restaurant> EditRestraunt([FromBody] Restaurant selectedRestraunt)
        {
            return new Restaurant();
        }

        [Route("api/home/addrestraunt")]
        [HttpPost]
        public async Task<Restaurant> GetAverageRestraunt([FromBody] Restaurant selectedRestraunt)
        {
            return new Restaurant();
        }
    }
}
