using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PRJ.Common.Entities
{
    public class ApiResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Value { get; set; }
    }
}
