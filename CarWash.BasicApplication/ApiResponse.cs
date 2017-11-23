using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicDDD.BasicApplication
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public object Result { get; set; }


        public ApiResponse()
        {
            
        }

        public ApiResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }
    }
}