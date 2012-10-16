using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SpiderfishInterceptor
{
    public static class SpiderfishGateway
    {
        public static Func<string, SpiderfishResponse> Init(string spiderfishUrl = "http://spiderfi.sh:4242")
        {
            spiderfishUrl = spiderfishUrl.TrimEnd(new[] {'/'});
            return (s) => Request(spiderfishUrl, s);
        }

        public static SpiderfishResponse Request(string spiderfishUrl, string rawUrl)
        {
            var client = new WebClient();
            var body = new StreamReader(client.OpenRead(BuildUrl(spiderfishUrl, rawUrl))).ReadToEnd();
            var headers = new NameValueCollection();
            foreach (var key in client.ResponseHeaders.AllKeys)
            {
                headers.Add(key, client.ResponseHeaders[key]);
            }
            return new SpiderfishResponse(200, headers, body);
        }

        private static string BuildUrl(string spiderfishUrl, string rawUrl)
        {
            var serializer = new JavaScriptSerializer();
            var data = HttpUtility.UrlEncode(serializer.Serialize(new { target=rawUrl }));
            return spiderfishUrl + "/?data=" + data;
        }
    }

    public class SpiderfishResponse
    {
        public int StatusCode { get; private set; }
        public NameValueCollection Headers { get; private set; }
        public string Body { get; private set; }

        public SpiderfishResponse(int statusCode, NameValueCollection headers, string body)
        {
            StatusCode = statusCode;
            Headers = headers;
            Body = body;
        }
    }
}
