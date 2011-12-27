using System;
using System.Collections.Generic;
using AbbeSays.Specs.Helpers;
using TechTalk.SpecFlow;

namespace AbbeSays.Specs.Steps
{
    [Binding]
    public class QuotePageSteps
    {
        [When(@"I navigate to the quotes page")]
        public void GoToQuotePage()
        {
            var quotePageAutomator = new QuotePageAutomator();
            ScenarioContext.Current.Set(quotePageAutomator);
            quotePageAutomator.Visit();
        }

        [Then(@"I should see the following quotes:")]
        public void AssertQuotes(IList<dynamic> quotes)
        {
            var quotesOnPage = ScenarioContext.Current.Get<QuotePageAutomator>().Quotes;
        }

        [Then(@"I should see a list of the following kids:")]
        public void AssertKids(IList<dynamic> kids)
        {
            var kidsOnPage = ScenarioContext.Current.Get<QuotePageAutomator>().Kids;
        }
    }
}
