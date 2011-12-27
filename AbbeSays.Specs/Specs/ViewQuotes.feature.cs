﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.239
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AbbeSays.Specs.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ViewingQuotesFeature : Xunit.IUseFixture<ViewingQuotesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ViewQuotes.feature"
#line hidden
        
        public ViewingQuotesFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Viewing quotes", "In order to keep track of what different kids has said\r\nAs a user of the site\r\nI " +
                    "want to see the quotes the kids has made during the years", ProgrammingLanguage.CSharp, new string[] {
                        "ignore"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Kid",
                        "Quote",
                        "Said at"});
            table1.AddRow(new string[] {
                        "Albert",
                        "Quote 1",
                        "2011-01-01"});
            table1.AddRow(new string[] {
                        "Albert",
                        "Quote 2",
                        "2011-01-02"});
            table1.AddRow(new string[] {
                        "Arvid",
                        "Quote 3",
                        "2011-10-01"});
            table1.AddRow(new string[] {
                        "Gustav",
                        "Quote 4",
                        "2011-11-01"});
#line 8
 testRunner.Given("the following quotes in the database", ((string)(null)), table1);
#line hidden
        }
        
        public virtual void SetFixture(ViewingQuotesFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Viewing quotes")]
        [Xunit.TraitAttribute("Description", "Viewing quotes for Albert")]
        public virtual void ViewingQuotesForAlbert()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Viewing quotes for Albert", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 16
 testRunner.When("I navigate to the quotes page");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Kid",
                        "Quote",
                        "Said at"});
            table2.AddRow(new string[] {
                        "Albert",
                        "Quote 1",
                        "2011-01-01"});
            table2.AddRow(new string[] {
                        "Albert",
                        "Quote 2",
                        "2011-01-02"});
            table2.AddRow(new string[] {
                        "Arvid",
                        "Quote 3",
                        "2011-10-01"});
            table2.AddRow(new string[] {
                        "Gustav",
                        "Quote 4",
                        "2011-11-01"});
#line 17
 testRunner.Then("I should see the following quotes:", ((string)(null)), table2);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name"});
            table3.AddRow(new string[] {
                        "Albert"});
            table3.AddRow(new string[] {
                        "Arvid"});
            table3.AddRow(new string[] {
                        "Gustav"});
#line 23
  testRunner.And("I should see a list of the following kids:", ((string)(null)), table3);
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ViewingQuotesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ViewingQuotesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
