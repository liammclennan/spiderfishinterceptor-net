Spiderfi.sh Interceptor
=======================

Spiderfi.sh Interceptor is the .net client for [spiderfi.sh](http://spiderfi.sh/), a service that allows Google to index client-side web applications. 

Spiderfi.sh Interceptor will intercept all incoming requests. Requests that come from the google crawler will Spiderfi.sh to render the result.

Usage
-----

1. Add the fragment meta tag to your layout/masterpage. This is not required if your site uses #! urls.

    <meta name="fragment" content="!">

2. Add the spiderfi.sh interceptor nuget package to your web project. This will modify your web.config file to register Spiderfi.sh Interceptor as an http module. 