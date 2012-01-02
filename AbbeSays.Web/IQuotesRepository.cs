using System.Collections.Generic;
using AbbeSays.Web.ViewModels;

namespace AbbeSays.Web
{
    public interface IQuotesRepository
    {
        IList<QuotesIndexVM> GetQuotes();
    }
}
