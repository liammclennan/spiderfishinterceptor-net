using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SpiderfishInterceptor
{
    public class SpiderfishInterceptorModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
        }

        void OnBeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication) sender;

            if (!RequestClassifier.IsCrawlerRequest(app.Context.Request.RawUrl))
                return;

            SpiderfishResponse response;
            try
            {
                response = SpiderfishGateway.Init()(app.Context.Request.RawUrl);
                app.Context.Response.StatusCode = response.StatusCode;
                foreach (var key in response.Headers.AllKeys)
                {
                    app.Context.Response.Headers.Add(key, response.Headers[key]);
                }
                app.Context.Response.Write(response.Body);
                app.CompleteRequest();
            } catch (Exception ex)
            {
                // if there is an error then proceed to normal request processing
            }
        }

        public void Dispose()
        {}
    }
}
