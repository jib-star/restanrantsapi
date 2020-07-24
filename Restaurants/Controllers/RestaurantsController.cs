using Restaurants.Models;
using Restaurants.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace Restaurants.Controllers
{

    [RoutePrefix("Restaurants")]
    public class RestaurantsController : ApiController
    {
        private readonly RestaurantsService Res_Service;
        public RestaurantsController()
        {
            Res_Service = new RestaurantsService();
        }

        [HttpPost]
        [Route("Get")]
        public object Get(string name)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel = Res_Service.GetRestaurants(name);
                return Json(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.Status = "Error";
                responseModel.Error.Code = "Catch";
                responseModel.Error.Message = ex.Message + "=====" + ex.StackTrace;
                return Json(responseModel);
            }
        }
    }
}
