using AbbeSays.Web;
using Machine.Specifications;
using Nancy.Testing;
using NSubstitute;

namespace AbbeSays.Tests.Modules
{
    public abstract class when_updating_likes_for_qoute
    {
        protected const int TEST_QUOTE_ID = 1;
        protected static Browser _browser;
        protected static BrowserResponse _result;
        protected static IQuotesRepository _mockRepository;

        protected Establish context = () => 
                {
                    _mockRepository = Substitute.For<IQuotesRepository>();

                    var bootstrapper = new ConfigurableBootstrapper(with =>  with.Dependency(_mockRepository));
                    _browser = new Browser(bootstrapper);
                };
    }

    public class when_adding_likes_for_qoute : when_updating_likes_for_qoute
    {

        private Because of = () =>
                {
                    _result = _browser.Post(
                        "/Quotes/AddLike",
                        with =>
                            {
                                with.FormValue("QuoteId", TEST_QUOTE_ID.ToString());
                                with.HttpRequest();
                            });
                };

        private It should_have_called_repository = () => _mockRepository.Received(1).AddLikeForQuote(TEST_QUOTE_ID);
        private It should_return_a_confirmation_JSON_string = () => _result.Body.AsString().ShouldContain("Incremented");
    }

    public class when_removing_likes_for_qoute : when_updating_likes_for_qoute
    {

        private Because of = () =>
        {
            _result = _browser.Post(
                "/Quotes/RemoveLike",
                with =>
                {
                    with.FormValue("QuoteId", TEST_QUOTE_ID.ToString());
                    with.HttpRequest();
                });
        };

        private It should_have_called_repository  = () => _mockRepository.Received(1).RemoveLikeForQuote(TEST_QUOTE_ID);
        private It should_return_a_confirmation_JSON_string = () => _result.Body.AsString().ShouldContain("Decremented");
    }
}