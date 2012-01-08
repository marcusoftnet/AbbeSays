using System;
using System.Dynamic;
using Nancy;
using Simple.Data;
using AbbeSays.Web.Helpers;

namespace AbbeSays.Web
{
    public class QuotesModule : NancyModule
    {
        private const string LIKE_INCREMENTED_FORMAT = "Incremented likes for Quote {0}";
        private const string LIKE_DECREMENTED_FORMAT = "Decremented likes for Quote {0}";
        private readonly dynamic db;
        private bool IsAuthenticated
        {
            get { return Context.CurrentUser != null; }
        }


        public QuotesModule(IQuotesRepository quotesRepository)
            : base("/Quotes/")
        {
            db = Database.Open();

            Get["{Id}"] = parameters => { return View["quote.cshtml", GetQuoteVm(parameters.Id)]; };

            Get[""] = parameters => { return View["quoteList.cshtml", GetIndexVm(string.Empty)]; };

            Get["/Kid/{KidName}"] = parameters => { return View["quoteList.cshtml", GetIndexVm(parameters.KidName)]; };

            Post["AddLike"] = parameters =>
                {
                    var quoteId = IDFromLikedURL;
                    quotesRepository.AddLikeForQuote(quoteId);

                    return Response.AsJson(string.Format(LIKE_INCREMENTED_FORMAT, quoteId));
                };

            Post["RemoveLike"] = parameters =>
                {
                    var quoteId = IDFromLikedURL;
                    quotesRepository.RemoveLikeForQuote(quoteId);

                    return Response.AsJson(string.Format(LIKE_DECREMENTED_FORMAT, quoteId));
                };
        }

        private int IDFromLikedURL
        {
            get
            {
                string url = Request.Form["QuoteURL"];
                var id = url.Substring(url.LastIndexOf('/') + 1);

                return int.Parse(id);

            }
        }

        private dynamic GetQuoteVm(int quoteId)
        {
            dynamic vm = new ExpandoObject();
            vm.Quote = db.Quotes.Query()
                        .Join(db.Kids, Id: db.Quotes.KidId)
                        .Select(db.Quotes.Quote,
                                db.Quotes.SaidAt,
                                db.Quotes.Id,
                                db.Kids.Name.As("KidName"),
                                db.Kids.Id.As("KidId"),
                                db.Kids.BirthDate)
                        .Where(db.Quotes.Id == quoteId)
                        .SingleOrDefault();

            vm.KidAge = DateTimeExtensions.ToAgeString(vm.Quote.BirthDate, vm.Quote.SaidAt);
            vm.FullURL = FullUrl();

            vm.IsAuthenticated = IsAuthenticated;

            return vm;
        }

        private string FullUrl()
        {
            return Request.Url.Scheme + "://" + Request.Url.HostName + Request.Url.Path;
        }


        private object GetIndexVm(string kidName)
        {
            dynamic vm = new ExpandoObject();
            vm.Kids = db.Kids.All().ToList();
            vm.IsAuthenticated = IsAuthenticated;

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
            foreach (dynamic quote in quotes)
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
                        db.Quotes.Kids.BirthDate)
                .OrderByDescending(db.Quotes.SaidAt);
        }
    }
}