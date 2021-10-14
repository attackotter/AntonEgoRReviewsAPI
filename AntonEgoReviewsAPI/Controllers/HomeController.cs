using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AntonEgoReviewsAPI.Controllers
{
    public class HomeController : ApiController
    {
        [Route("api/home/addrestraunt")]
        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
