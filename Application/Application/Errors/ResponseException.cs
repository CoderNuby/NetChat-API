using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Errors
{
    public class ResponseException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public object Errors { get; set; }

        public ResponseException(HttpStatusCode code, object errors = null) 
        {
            Code = code;
            Errors = errors;
        }
    }
}
