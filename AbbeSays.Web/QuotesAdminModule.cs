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
        private dynamic db;
        private static readonly string[] _splitChars = new[] { ",", ";", " " };

        public QuotesAdminModule()
            : base("/Quotes")
        {
            db = Database.Open();
            this.RequiresAuthentication();

            Get["/Create"] = parameters => {
                                     return View["createQuote.cshtml", 
                                         GetCreateQouteViewModel(Context.CurrentUser.UserName)];
                                 };

            Post["/Create"] = paramenters =>
                                  {
                                      var id = SaveQuote(this.Bind<EditQuoteVm>());
                                      return Response.AsRedirect("/Quotes/" + id);
                                  };

            Get["/Update/{id}"] = parameters =>
                                      {
                                          return View["createQuote.cshtml", GetUpdateQuoteViewModel(parameters.Id)];
                                      };
        }

        private dynamic GetUpdateQuoteViewModel(int id)
        {
            var quote = db.Quotes.FindById(id);
            dynamic vm = new ExpandoObject();
            vm.Id = quote.Id;
            vm.Kids = GetKidsForParent(Context.CurrentUser.UserName);
            vm.SaidAt = quote.SaidAt;
            vm.Quote = quote.Quote;
            vm.KidId = quote.KidId;

            return vm;
        }

        // TODO: Duplication with model in GetUpdateQuoteViewModel
        private dynamic GetCreateQouteViewModel(string userName)
        {
            dynamic vm = new ExpandoObject();
            vm.Id = string.Empty;
            vm.KidId = string.Empty;
            vm.Kids = GetKidsForParent(userName);
            vm.Quote = string.Empty;
            vm.SaidAt = DateTime.Now.ToShortDateString();
            return vm;
        }

        private object GetKidsForParent(string userName)
        {
            return db.Kids.FindAll(db.Kids.Parents.UserName == userName).ToList();
        }

        private int SaveQuote(EditQuoteVm quoteInfo)
        {
            var id = 0;
            if(quoteInfo.Id == 0)
                id = db.Quotes.Insert(quoteInfo).Id;
            else
            {
                id = db.Quotes.Update(quoteInfo);
            }
            CreateTags(id);
            return id;
        }

        private void CreateTags(int id)
        {
            foreach (var tag in GetTagsFromRequest())
            {
                var tagId = GetOrCreateTag(tag);
                AssociateQuoteAndTag(id, tagId);
            }
        }

        private void AssociateQuoteAndTag(object id, int tagId)
        {
            if (!db.QuoteTag.ExistsByQuoteIdAndTagId(QuoteId: id, TagId: tagId))
                db.QuoteTag.Insert(QuoteId: id, TagId: tagId);
        }

        private IEnumerable<string> GetTagsFromRequest()
        {
            var tagsFromRequest = Request.Form.Tags.ToString();
            return tagsFromRequest.Split(_splitChars, StringSplitOptions.RemoveEmptyEntries);
        }

        private int GetOrCreateTag(object tag)
        {
            return !db.Tags.ExistsByTag(tag)
                ? db.Tags.Insert(Tag: tag).Id
                : db.Tags.FindByTag(tag).Id;
        }
    }

    public class EditQuoteVm
    {
        public int Id { get; set; }
        public int KidId { get; set; }
        public string Quote { get; set; }
        public DateTime SaidAt { get; set; }
    }
}