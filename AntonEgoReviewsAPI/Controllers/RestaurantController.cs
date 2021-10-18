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
    public class RestaurantController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
            {
                conn.Open();
                DataTable table = new DataTable();
                SqlCommand cmd = new SqlCommand("GetRestruants", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    cmd.CommandType = CommandType.Text;
                    table.Load(rdr);
                }
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
        }

        [Route("api/Restaurant/AddRestaurant")]
        [HttpPost]
        public HttpResponseMessage AddRestaurant(Restaurant rest)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                {
                    conn.Open();
                    DataTable table = new DataTable();
                    SqlCommand cmd = new SqlCommand("AddRestruant", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@RestaurantName", rest.RestaurantName));
                    cmd.Parameters.Add(new SqlParameter("@StreetAddress", rest.StreetAddress));
                    cmd.Parameters.Add(new SqlParameter("@City", rest.City));
                    cmd.Parameters.Add(new SqlParameter("@State", rest.State));
                    cmd.Parameters.Add(new SqlParameter("@Zip", rest.Zip));
                    cmd.Parameters.Add(new SqlParameter("@Description", rest.Description));
                    cmd.Parameters.Add(new SqlParameter("@HoursOfOperation", rest.HoursOfOperation));
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    string id = cmd.Parameters["@id"].Value.ToString();

                    //add returend new id to create user review 
                    rest.RestaurantId = Convert.ToInt32(id);
                    conn.Close();

                    // add the restaurant review
                    AddRating(rest);
                    return Request.CreateResponse(HttpStatusCode.OK, "Added new Record");
                }

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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateRestruant", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@RestaurantId", rest.RestaurantId));
                    cmd.Parameters.Add(new SqlParameter("@RestaurantName", rest.RestaurantName));
                    cmd.Parameters.Add(new SqlParameter("@StreetAddress", rest.StreetAddress));
                    cmd.Parameters.Add(new SqlParameter("@City", rest.City));
                    cmd.Parameters.Add(new SqlParameter("@State", rest.State));
                    cmd.Parameters.Add(new SqlParameter("@Zip", rest.Zip));
                    cmd.Parameters.Add(new SqlParameter("@Description", rest.Description));
                    cmd.Parameters.Add(new SqlParameter("@HoursOfOperation", rest.HoursOfOperation));
                    cmd.ExecuteNonQuery();
                    conn.Close();
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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DeleteRestaurantAndReviews", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@RestaurantId", id));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return "Deleted Record";
            }
            catch (Exception ex)
            {
                return "Failed to Deleted Record" + ex.Message;
            }

        }



        
        [Route("api/Restaurant/AddReview")]
        [HttpPost]
        public HttpResponseMessage AddRating(Restaurant rest)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Restaurant"].ConnectionString))
                {
                    conn.Open();
                    DataTable table = new DataTable();
                    SqlCommand cmd = new SqlCommand("AddReview", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@RestaurantId", rest.RestaurantId));
                    cmd.Parameters.Add(new SqlParameter("@Review", rest.Rating));
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }

                    return Request.CreateResponse(HttpStatusCode.OK, "Success Added Review");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Fail To Add Review" + ex.Message);
            }
        }
    }
}
