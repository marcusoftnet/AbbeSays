using System;
using AbbeSays.Web;
using AbbeSays.Web.Repositories;
using Machine.Specifications;
using Simple.Data;

namespace AbbeSays.Tests.Repositories
{
    public class when_updateLike_on_quotes_respository
    {
        protected const int QUOTEID = 1;
        protected static IQuotesRepository _quotesRepository;

        Establish context = () =>
        {
            var inMemoryAdapter = new InMemoryAdapter();
            inMemoryAdapter.SetKeyColumn("Quotes", "Id");
            Database.UseMockAdapter(inMemoryAdapter);

            dynamic db = Database.Open();
            db.Quotes.Insert(Id: QUOTEID, Quote: "A quote", SaidAt: DateTime.Now.AddDays(-2), Likes: QUOTEID);

            _quotesRepository = new QuotesRepository();

        };
    }

    public class with_positive_number : when_updateLike_on_quotes_respository
    {
        Because of = () => _quotesRepository.AddLikeForQuote(QUOTEID);

        private It should_have_2_likes = () => _quotesRepository.GetQuote(QUOTEID).Likes.ShouldEqual(2);
    }

    public class with_negative_number : when_updateLike_on_quotes_respository
    {
        Because of = () => _quotesRepository.RemoveLikeForQuote(QUOTEID);

        private It should_have_0_likes = () => _quotesRepository.GetQuote(QUOTEID).Likes.ShouldEqual(0);
    }
}
