using System.Web;

namespace lab1_a.Handlers
{
    public class GetHandler : IHttpHandler
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

            string result = $"GET-Http-BPA:ParmA = {request["ParmA"]}, ParmB = {request["ParmB"]}";
            response.Write(result);
        }
    }
}
