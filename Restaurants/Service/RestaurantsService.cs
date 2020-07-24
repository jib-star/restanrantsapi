using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Restaurants.Service
{
    public class RestaurantsService
    {
        public RestaurantsModel restaurants;
        public ResponseModel GetRestaurants(string name)
        {
            ResponseModel responseModel = new ResponseModel();

            try { 
                    restaurants = new RestaurantsModel();
                    string API_KEY = ConfigurationManager.AppSettings["API_KEY"];

                    DateTime dateExpire = DateTime.Now.AddYears(2);
                    MemoryCacherService memCache = new MemoryCacherService();
              
                    var Search_res = memCache.GetValue(name);
                    if (Search_res != null)
                    {
                        responseModel.Status = "Success";
                        responseModel.Data = Search_res;
                    }
                    else
                    {
                        string url = @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=restaurants+in+" + name + "&key=" + API_KEY;

                        WebRequest request = WebRequest.Create(url);
                        WebResponse response = request.GetResponse();
                        Stream data = response.GetResponseStream();
                        StreamReader reader = new StreamReader(data);

                        // json-formatted string from maps api
                        var responseFromGoogle = reader.ReadToEnd();
                        response.Close();
                        restaurants = JsonConvert.DeserializeObject<RestaurantsModel>(responseFromGoogle);

                        if (restaurants.status.ToUpper() == "OK")
                        {
                            responseModel.Status = "Success";
                            responseModel.Data = restaurants.results;
                            memCache.Add(name, responseFromGoogle, dateExpire);
                        }
                        else
                        {
                            responseModel.Status = restaurants.status;
                            responseModel.Error.Code = "Call Google Map API";
                            responseModel.Error.Message = restaurants.error_message;
                        }

                    }

                    return responseModel;

            }
            catch (Exception ex)
            {
                responseModel.Status = "Error";
                responseModel.Error.Code = "Catch";
                responseModel.Error.Message = ex.Message;
                return responseModel;
            }
        }
    }
}