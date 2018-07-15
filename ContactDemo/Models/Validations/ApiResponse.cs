using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ContactDemo.Models.Validations
{
    public class ApiResponse
    {
        public bool StatusIsSuccessful { get; set; }
        public ErrorStateResponse ErrorState { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseResult { get; set; }
    }

    public class ErrorStateResponse
    {
        public IDictionary<string, string[]> ModelState { get; set; }
    }
}