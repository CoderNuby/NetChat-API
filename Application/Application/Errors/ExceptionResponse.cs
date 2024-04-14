using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Errors
{
    public class ExceptionResponse : Exception
    {
        public HttpStatusCode Code { get; set; }
        public object Errors { get; set; }

        public ExceptionResponse(HttpStatusCode code, object errors = null) 
        {
            Code = code;
            Errors = errors;
        }
    }
}
