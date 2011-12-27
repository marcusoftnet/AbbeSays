using System.Collections.Generic;
using Nancy;
using Nancy.Testing;

namespace AbbeSays.Specs.Helpers
{
    public class QuotePageAutomator
    {
        private readonly DefaultNancyBootstrapper _bootstrapper;
        private readonly Browser _browser;
        private BrowserResponse LatestsBrowserResponse { get; set; }

        public IList<object> Quotes
        {
            get
            {
                // TODO: read from browser response
                return new List<object>();
            }
        }

        public IList<object> Kids
        {
            get
            {
                // TODO: read from browser response
                return new List<object>();
            }
        }

        public QuotePageAutomator()
        {
            _bootstrapper = new DefaultNancyBootstrapper();
            _browser = new Browser(_bootstrapper);
        }

        public void Visit()
        {
            LatestsBrowserResponse = _browser.Get("/", with => with.HttpRequest());
        }
    }
}