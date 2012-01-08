using System.Collections.Generic;
using AbbeSays.Web.ViewModels;
using Simple.Data;

namespace AbbeSays.Web.Repositories
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly dynamic db;

        public QuotesRepository()
        {
            db = Database.Open();
        }

        public IList<QuotesIndexVM> GetQuotes()
        {
            return db.Quotes.All()
                .Select(db.Quotes.Id,
                        db.Quotes.KidId,
                        db.Quotes.Quote,
                        db.Quotes.SaidAt,
                        db.Quotes.Kids.Name,
                        db.Quotes.Kids.BirthDate)
                .OrderBy(db.Quotes.SaidAt)
            .ToList<QuotesIndexVM>();
        }

        public void AddLikeForQuote(int quoteId)
        {
            UpdateLikes(quoteId, 1);
        }

        private void UpdateLikes(int quoteId, int increment)
        {
            dynamic quote = GetQuote(quoteId);
            quote.Likes = quote.Likes + increment;
            db.Quotes.Update(quote);
        }

        public void RemoveLikeForQuote(int quoteId)
        {
            UpdateLikes(quoteId, -1);
        }

        public QuoteViewModel GetQuote(int quoteId)
        {
            return db.Quotes.FindById(quoteId);
        }
    }
}