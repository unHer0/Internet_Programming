using System.Text.Json;
using System.Web;
using System.IO;
using System;

namespace lab1_a.Handlers
{
    public class PostXYHandler : IHttpHandler
    {   
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest rq = context.Request;
            HttpResponse rs = context.Response;
            rs.ContentType = "text/plain";

            if (rq.Params["XData"] != null &&
                rq.Params["YData"] != null)
            {
                int x = Int32.Parse(rq.Params["XData"]);
                int y = Int32.Parse(rq.Params["YData"]);
                rs.Write($"result: {x + y}");
            }
        }
    }
}
