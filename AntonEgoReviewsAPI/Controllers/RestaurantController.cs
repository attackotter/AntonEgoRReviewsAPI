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
using System.Web.Http.Cors;

namespace AntonEgoReviewsAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RestaurantController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
            select    res.RestaurantId, res.RestaurantName, res.StreetAddress, res.City, res.State, res.Zip, res.Description, res.HoursOfOperation, Average = (SELECT AVG(CAST(Rating AS DECIMAL(5,2))) AS SQLAVG FROM RestaurantReview as rev WHERE rev.RestaurantId = res.RestaurantId)
            from dbo.Restaurant as res

            ";
            DataTable table = new DataTable();
            using(var con= new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString)) 
            using (var cmd = new SqlCommand(query,con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public HttpResponseMessage Post(Restaurant rest)
        {
            try
            {
                string query = @"
                insert into  dbo.Restaurant
                values ('" + rest.RestaurantName + "','" + rest.StreetAddress + "','" + rest.City + "','" + rest.State + "','" + rest.Zip + "','" + rest.Description + "','" + rest.HoursOfOperation + @"')
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
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Fail To Add Record" + ex.Message);
            }
        }


        public HttpResponseMessage Put(Restaurant rest)
        {
            try
            {
                string query = @"
                Update  dbo.Restaurant
                set  RestaurantName='" + rest.RestaurantName + @"',StreetAddress='" + rest.StreetAddress + @"', City='" + rest.City + @"',State='" + rest.State + @"',Zip='" + rest.Zip + @"',Description='" + rest.Description + @"', HoursOfOperation='" + rest.HoursOfOperation + @"'
                where RestaurantId= " + rest.RestaurantId +@"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Success, Updated Record");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update Record" + ex.Message);
            }
            
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                Delete From  dbo.Restaurant
                where RestaurantId= " + id + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Record";
            }
            catch (Exception ex)
            {
                return "Failed to Deleted Record" + ex.Message;
            }

        }
    }
}
