using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.ErrorHandler
{
    public class Error : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object ErrorMessage { get; set; }
        public Error(HttpStatusCode statusCode, object Errormessage = null)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = Errormessage;
        }
    }
}
