﻿using System.Collections.Generic;
using AbbeSays.Web.ViewModels;

namespace AbbeSays.Web
{
    public interface IQuotesRepository
    {
        IList<QuotesIndexVM> GetQuotes();
        void AddLikeForQuote(int quoteId);
        void RemoveLikeForQuote(int quoteId);
        QuoteViewModel GetQuote(int quoteId);
    }
}
