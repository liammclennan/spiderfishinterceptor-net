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

            var gateway = SpiderfishGateway.Init();
            gateway(app.Context.Request.RawUrl, app);
        }

        public void Dispose()
        {}
    }
}
