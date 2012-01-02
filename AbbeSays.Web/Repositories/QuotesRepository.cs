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
    }
}