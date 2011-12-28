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

            Get["/Create"] = parameters =>
                                 {
                                     return View["createQuote.cshtml", 
                                         GetCreateQouteViewModel(Context.CurrentUser.UserName)];
                                 };

            Post["/Create"] = paramenters =>
                                  {
                                      var id = SaveQuote(this.Bind<CreateQuoteVm>());
                                      return Response.AsRedirect("/Quotes/" + id);
                                  };
        }

        private dynamic GetCreateQouteViewModel(string userName)
        {
            dynamic vm = new ExpandoObject();
            vm.Kids = db.Kids.FindAll(db.Kids.Parents.UserName == userName).ToList();
            return vm;
        }

        private int SaveQuote(CreateQuoteVm quoteInfo)
        {
            var id = db.Quotes.Insert(quoteInfo).Id;

            CreateTags((object) id);

            return id;
        }

        private void CreateTags(object id)
        {
            foreach (var tag in  GetTagsFromRequest())
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

    public class CreateQuoteVm
    {
        public int KidId { get; set; }
        public string Quote { get; set; }
        public DateTime SaidAt { get; set; }
    }
}