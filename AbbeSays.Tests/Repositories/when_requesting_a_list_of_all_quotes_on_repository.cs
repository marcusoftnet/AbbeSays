using System;
using System.Collections.Generic;
using AbbeSays.Web;
using AbbeSays.Web.Repositories;
using AbbeSays.Web.ViewModels;
using Machine.Specifications;
using Simple.Data;

namespace AbbeSays.Tests.Repositories
{
    public class when_requesting_a_list_of_all_quotes_on_repository
    {
        private static IList<QuotesIndexVM> _quotes;

        Establish context = () =>
                                {
                                    var inMemoryAdapter = new InMemoryAdapter();
                                    inMemoryAdapter.SetAutoIncrementKeyColumn("Quotes", "Id");
                                    inMemoryAdapter.ConfigureJoin("Quotes", "Id", "Kids", "Kids", "KidId", "Kid");

                                    Database.UseMockAdapter(inMemoryAdapter);

                                    dynamic db = Database.Open();
                                    db.Kids.Insert(Id: 1, Name: "Albert", BirthDate: DateTime.Now.AddYears(-3));
                                    db.Kids.Insert(Id: 2, Name: "Arvid", BirthDate: DateTime.Now.AddYears(-1));

                                    db.Quotes.Insert(KidId: 1, Quote: "Second quote", SaidAt: DateTime.Now.AddDays(-2));
                                    db.Quotes.Insert(KidId: 1, Quote: "Third quote", SaidAt: DateTime.Now.AddDays(-1));
                                    db.Quotes.Insert(KidId: 2, Quote: "First quote", SaidAt: DateTime.Now.AddDays(-3));
                                };

        Because of = () =>
                         {
                             IQuotesRepository quotesRepository = new QuotesRepository();
                             _quotes = quotesRepository.GetQuotes();
                         };

        It should_contain_all_the_quotes_in_the_database = () => ShouldExtensionMethods.ShouldEqual(_quotes.Count, 3);
        It should_order_the_list_by_date_descending = () =>
                                                          {
                                                              _quotes[0].Quote.ShouldEqual("First quote");
                                                              _quotes[1].Quote.ShouldEqual("Second quote");
                                                              _quotes[2].Quote.ShouldEqual("Third quote");
                                                          };

        // It should_include_then_name_of_the_kid = () => { _quotes[0].Name.ShouldEqual("Albert"); };
        // It should_include_then_name_of_the_kid = () => { _quotes[0].Name.ShouldEqual("Albert"); };
    }
}