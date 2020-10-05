using System;
using System.IO;
using System.Text;
using System.Web;

namespace lab1_a.Handlers
{
    public class GetPostHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest currentRequest = context.Request;
            HttpResponse currentResponse = context.Response;

            switch (currentRequest.HttpMethod)
            {
                case "GET":
                    using (FileStream fstream = File.OpenRead(
                        Path.Combine(HttpRuntime.AppDomainAppPath, "GetPostPage.html")))
                    {
                        byte[] byteArray = new byte[fstream.Length];
                        fstream.Read(byteArray, 0, byteArray.Length);
                        string result = Encoding.Default.GetString(byteArray);
                        result = result.Substring(result.IndexOf("<!DOCTYPE html>"));
                        currentResponse.Write(result);
                    }
                    break;
                case "POST":
                    if (currentRequest.Params["x"] != null &&
                        currentRequest.Params["y"] != null)
                    {
                        int x = Int32.Parse(currentRequest.Params["x"]);
                        int y = Int32.Parse(currentRequest.Params["y"]);
                        currentResponse.Write($"result: {x * y}");
                    }
                    break;
            }
        }
    }
}
