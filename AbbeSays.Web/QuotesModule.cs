using System;
using System.Dynamic;
using Nancy;
using Simple.Data;

namespace AbbeSays.Web
{
    public class QuotesModule : NancyModule
    {
        private dynamic db;

        public QuotesModule()
            : base("/Quotes/")
        {
            db = Database.Open();

            Get["{Id}"] = parameters => { return View["quote.cshtml", GetQuoteVm(parameters.Id)]; };

            Get["List"] = parameters => { return View["quoteList.cshtml", GetIndexVm(string.Empty)]; };
            Get["List/Kid/{KidName}"] = parameters => { return View["quoteList.cshtml", GetIndexVm(parameters.KidName)]; };
        }

        private object GetQuoteVm(int quoteId)
        {
            return db.Quotes.Find(
                        db.Quotes.Id == quoteId &&
                        db.Quotes.Kids.Id == db.Quotes.Id);
        }

        private object GetIndexVm(string kidName)
        {
            dynamic vm = new ExpandoObject();
            vm.Kids = db.Kids.All().ToList();

            dynamic quoteQuery = GetQuotes(kidName);
            vm.Quotes = CleanUpHTMLInQuotes(quoteQuery.ToList());

            return vm;
        }

        private object GetQuotes(string kidName)
        {
            var quoteQuery = IndexViewQuery();
            if (!string.IsNullOrEmpty(kidName))
            {
                quoteQuery = quoteQuery.Where(db.Quotes.Kids.Name == kidName);
            }
            return quoteQuery;
        }

        private dynamic CleanUpHTMLInQuotes(dynamic quotes)
        {
            foreach (var quote in quotes)
            {
                var s = quote.Quote.Replace(Environment.NewLine, "<br/>"); ;
                quote.Quote = s;
            }

            return quotes;
        }

        private dynamic IndexViewQuery()
        {
            return db.Quotes.All()
                .Select(db.Quotes.Id,
                        db.Quotes.KidId,
                        db.Quotes.Quote,
                        db.Quotes.SaidAt,
                        db.Quotes.Kids.Name,
                        db.Quotes.Kids.BirthDate);
        }
    }
}