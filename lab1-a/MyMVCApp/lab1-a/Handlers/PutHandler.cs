using System;
using System.Web;

namespace lab1_a.Handlers
{
    public class PutHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentType = "text/plain";

            string result = $"PUT-Http-BPA:ParmA = {request["ParmA"]}, ParmB = {request["ParmB"]}";
            response.Write(result);
        }
    }
}
