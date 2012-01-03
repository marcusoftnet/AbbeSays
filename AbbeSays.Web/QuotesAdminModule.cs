using System;
using System.Collections.Generic;
using System.Dynamic;
using Nancy;
using Nancy.Security;
using Simple.Data;
using Nancy.ModelBinding;

namespace AbbeSays.Web
{
    public class QuotesAdminModule : NancyModule
    {
        private const string EDITVIEW_NAME = "editQuote.cshtml";
        private dynamic db;
        private static readonly string[] _splitChars = new[] { ",", ";", " " };

        public QuotesAdminModule()
            : base("/Quotes")
        {
            db = Database.Open();
            this.RequiresAuthentication();

            Get["/Create"] = parameters =>
                            {
                                return View[EDITVIEW_NAME, GetEditQuoteVM()];
                            };

            Post["/Edit"] = paramenters =>
                            {
                                var id = SaveOrUpdateQuote(this.Bind<EditQuoteVm>());
                                return Response.AsRedirect("/Quotes/" + id);
                            };

            Get["/Edit/{id}"] = parameters =>
                            {
                                return View[EDITVIEW_NAME, GetEditQuoteVM(parameters.Id)];
                            };
        }

        
        private EditQuoteVm GetEditQuoteVM(int id = -1)
        {
            var vm = new EditQuoteVm
                         {
                             Kids = GetKidsForParent(Context.CurrentUser.UserName), 
                             SaidAt = DateTime.Now
                         };

            var quote = db.Quotes.FindById(id);
            if (quote != null)
            {
                vm.Id = quote.Id;
                vm.SaidAt = quote.SaidAt;
                vm.Quote = quote.Quote;
                vm.KidId = quote.KidId;
            }

            return vm;
        }

        private IEnumerable<dynamic> GetKidsForParent(string userName)
        {
            return db.Kids.FindAll(db.Kids.Families.UserName == userName).ToList();
        }

        private int SaveOrUpdateQuote(EditQuoteVm quoteInfo)
        {
            int id;
            if (quoteInfo.Id != 0)
            {
                db.Quotes.Update(quoteInfo);
                id = quoteInfo.Id;
            }
            else
                id = db.Quotes.Insert(quoteInfo).Id;

            return id;
        }
    }

    public class EditQuoteVm
    {
        public int Id { get; set; }
        public int KidId { get; set; }
        public string Quote { get; set; }
        public DateTime SaidAt { get; set; }
        public IEnumerable<dynamic> Kids { get; set; }
    }
}