using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurants.Models
{
    public class ResponseModel
    {
        public string Status { get; set; } = "Fail"; // Success, Fail, Error
        public object Data { get; set; }
        public ErrorModel Error { get; set; } = new ErrorModel();

        public class ErrorModel
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }
    }
}