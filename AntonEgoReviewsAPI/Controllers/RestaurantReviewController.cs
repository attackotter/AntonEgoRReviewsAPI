using AntonEgoReviewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AntonEgoReviewsAPI.Controllers
{
    public class RestaurantReviewController : ApiController
    {

        public HttpResponseMessage Get(int id)
        {
            string query = @"
            select    RestaurantId, Rating from 
            dbo.RestaurantReview
            where RestaurantId= " + id + @"";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public HttpResponseMessage Post(RestaurantReview review)
        {
            try
            {
                string query = @"
                insert into  dbo.RestaurantReview
                values ('" + review.RestaurantId + "','" + review.Rating + @"')
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Success Added Record");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Fail To Add Record" + ex.Message);
            }
        }
    }
}
