namespace SpiderfishInterceptor
{
    public static class RequestClassifier
    {
        public static bool IsCrawlerRequest(string rawUrl)
        {
            return rawUrl.Contains("_escaped_fragment_=");
        }
    }
}
