using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace AbbeSays.Specs.Helpers
{
    [Binding]
    public class DBSteps
    {
        private const int TEST_PARENTID = 100;

        [Given(@"the following quotes in the database")]
        public void LoadQuotes(IList<dynamic> quotes)
        {
            foreach (var quote in quotes)
            {
                var kidId = GetOrCreateKidId(quote.Kid);

                InMemoryDB.DB.Quotes.Insert(KidId: kidId, Quote: quote.Quote, SaidAt: quote.SaidAt);
            }
        }

        private static int GetOrCreateKidId(string name)
        {
            var kidId = -1;
            //if (InMemoryDB.DB.Kids.ExistsByName(name))
            //    kidId = InMemoryDB.DB.Kids.FindByName(name).Id;
            //else
                kidId = InMemoryDB.DB.Kids.Insert(ParentId: TEST_PARENTID, Name: name,
                                                  BirthDate: DateTime.Now.AddYears(-1)).Id;
            return kidId;
        }
    }
}
