using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using NUnit.Framework;

namespace SpiderfishInterceptor.Tests
{
    [TestFixture]
    public class WhenRequestComesFromCrawler
    {
        [Test]
        public void RegularUrls()
        {
            var request = "http://domain.com:8098/some/path?query1=val1&_escaped_fragment_=";
            Assert.IsTrue(RequestClassifier.IsCrawlerRequest(request));
        }
        
        [Test]
        public void HashbangUrls()
        {
            var request = "http://domain.com:8098/some/path?query1=val1&_escaped_fragment_=hash/bang";
            Assert.IsTrue(RequestClassifier.IsCrawlerRequest(request));
        }
    }

    [TestFixture]
    public class WhenRequestComesFromNonCrawler
    {
        [Test]
        public void RegularUrls()
        {
            var request = "http://domain.com:8098/some/path?query1=val1";
            Assert.IsFalse(RequestClassifier.IsCrawlerRequest(request));
        }
        
        [Test]
        public void HashbangUrls()
        {
            var request = "http://domain.com:8098/some/path?query1=val1#!hash/bang";
            Assert.IsFalse(RequestClassifier.IsCrawlerRequest(request));
        }
    }

    [TestFixture]
    public class Request
    {
        [Test]
        public void Honeypot()
        {
            var target = "http://honeypot.withouttheloop.com/?_escaped_fragment_=";
            var response = SpiderfishGateway.Init()(target);
            Assert.IsTrue(response.Body.Contains("Handlebars"));
        }
        
        [Test]
        public void HoneypotArticle()
        {
            var target = "http://honeypot.withouttheloop.com/page/backbone?_escaped_fragment_=";
            var response = SpiderfishGateway.Init()(target);
            Assert.IsTrue(response.Body.Contains("In the beginning"));
        }
    }

    [TestFixture]
    public class HttpRequests
    {
        [Test]
        public void Do()
        {
            var client = new WebClient();
            //var stream = client.OpenRead("http://honeypot.withouttheloop.com");
            //Console.WriteLine(new StreamReader(stream).ReadToEnd());

            var url = "http://spiderfi.sh:4242/?data=" +
                      "%7B%22target%22%3A%22http%3A%2F%2Fhoneypot.withouttheloop.com%22%7D";
            Console.WriteLine(url);
            Console.WriteLine(new StreamReader(client.OpenRead(url)).ReadToEnd());

        }

        [Test]
        public void HttpWebRequest()
        {
            var url = "http://spiderfi.sh:4242/?data=" +
                      "%7B%22target%22%3A%22http%3A%2F%2Fhoneypot.withouttheloop.com%22%7D";
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)myReq.GetResponse();
            Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
        }

        [Test]
        public void Encoding()
        {
            var target = "http://honeypot.withouttheloop.com";
            var serializer = new JavaScriptSerializer();
            Console.WriteLine(HttpUtility.UrlEncode(serializer.Serialize(new {target})));
        }
    }
}
