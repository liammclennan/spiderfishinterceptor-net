using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpiderfishInterceptor
{
    public static class SpiderfishGateway
    {
        public static Action<string, HttpApplication> Init(string spiderfishUrl = "http://spiderfi.sh:4242")
        {
            spiderfishUrl = spiderfishUrl.TrimEnd(new[] {'/'});
            return (s,a) => Request(spiderfishUrl, s, a);
        }

        public static void Request(string spiderfishUrl, string rawUrl, HttpApplication app)
        {
            
        }
    }
}
