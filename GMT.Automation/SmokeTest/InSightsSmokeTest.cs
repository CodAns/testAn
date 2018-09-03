using System.Linq;
using NUnit.Framework;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.InSightTabBasicSearch;
using TestStack.White.UIItems.WPFUIItems;
using static GMT.White.AutomationFramework.InSightTabAdvanceSearch;

namespace GMT.White.AutomationFramework
{

    class InsightsSmokeTest : CommonInitialization<InsightsSmokeTest>
    {

        [Test]
        [Category("InsightsTab")]
        [Category("Basic Search")]
        public void InsightVerifyBrowseQueue()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of KS selected in Browse Queue
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            insightScreen1.getTabWin(gMTWindow);
            logger.Info("Select Insight Tab");

            insightScreen1.SelectBasicSearchOptions(InsightBaseFilter.browseQueue, "Chopra, Mitali");
            logger.Info("Apply filter to Browse Queue");

            Panel output = insightScreen1.GetInsightResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Insights per applied filter");

            int rowcount = result.Rows.Count();
            for (int y = 0; y <rowcount ; y++)
            {
                result.Rows[y].Cells[3].DoubleClick();
                Assert.AreEqual("Chopra, Mitali", insightScreen1.GetCreaterName());
                logger.Info("Assertion Passed, Result are for selected Km from 'Browse Queue'");
                gmtTab.OpenTab(gMTWindow, "Insights").Click();
            }
        }

        [Test]
        [Category("InsightsTab")]
        [Category("Basic Search")]
        public void InsightVerifyBasisClientName()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of ClientName filter.
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            insightScreen1.getTabWin(gMTWindow);
            logger.Info("Select Insight Tab");

            insightScreen1.SelectBasicSearchOptions(InsightBaseFilter.clientName, "Cigna");
            logger.Info("Apply filter ClientName=Cigna");

            Panel output = insightScreen1.GetInsightResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Insights per applied filter");

            for(int y =0; y <result.Rows.Count(); y++)
            Assert.AreEqual("Cigna", result.Rows[y].Cells[6].Value.ToString().TrimEnd());
            logger.Info("Asserted Insights returned are related to Cigna");
        }

        [Test]
        [Category("InsightsTab")]
        [Category("Basic Search")]
        public void InsightVerifyInsightCreation()
        {
            /////////////////////////////////
            /////  This Test will will create a new insight and then verify that insight is created.
            /////  Post verification it will delete the created Insight.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            insightScreen1.getTabWin(gMTWindow);
            logger.Info("Select Insight Tab");

            insightScreen1.AddInsight("TestAK");
            gmtTab.OpenTab(gMTWindow, "Insights").Click(); Wait();
            logger.Info("Create New Insight with Title- 'TestAK'");

            insightScreen1.SelectBasicSearchOptions(InsightBaseFilter.insightTitle, "TestAK");
            Panel output = insightScreen1.GetInsightResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Search For newly created insight");

            Assert.IsTrue(result.Rows.Count() == 1);
            result.Rows[0].Cells[3].DoubleClick();
            string insightTitle = gMTWindow.Get(SearchCriteria.ByAutomationId("ucInsightContentHeader")).Name;
            Assert.AreEqual(result.Rows[0].Cells[2].Value.ToString(), insightTitle);
            logger.Info("Asserted Single row with insight title-'TestAK' is returned");

            insightScreen1.DeleteInsight();
        }

        [Test]
        [Category("InsightsTab")]
        [Category("Advance Search")]
        public void InsightVerifyDateFilter()
        {
            /////////////////////////////////
            /////  This Test will Fetch for insights within applied date filter range.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            logger.Info("Select Insight Tab");
            insightScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            insightScreen2.DateFliter(DateType.InsightDate,FromType.Period,"","",PeriodBack.FullYear,PeriodAhead.Today);
            logger.Info("Apply date filter");

            Panel output = insightScreen2.GetInsightResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Search For newly created insight");

            Assert.IsFalse(insightScreen2.ChkIfNoResultreturned());
            Assert.IsTrue(result.Rows.Count() != 0);
            logger.Info("Verified some result is returned for applied filter");
           
        }

        [Test]
        [Category("InsightsTab")]
        [Category("Advance Search")]
        public void InsightVerifyContactLocInd()
        {
            /////////////////////////////////
            /////  This Test will Fetch for insights within applied date filter range.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            logger.Info("Select Insight Tab");
            insightScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            insightScreen2.AddContact(contact.Employee, "Chopra, Mitali");
            insightScreen2.treevLocation("Munich");
            insightScreen2.treevIndustry(industry.Primary,"Auto");
            logger.Info("Apply Contact, OffcLoc, Industry Filter");

            Panel output = insightScreen2.GetInsightResult();
            bool noResult = insightScreen2.ChkIfNoResultreturned();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related Proposal per applied filter");

            if (noResult == true)
                logger.Info("No result was found but filter search was performed without error");
            else
            {
                Assert.IsTrue(result.Rows.Count() != 0);
                logger.Info("Verified some result is returned for applied filter");
            }
        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchByKS()
        {
            /////////////////////////////////
            /////  This Test will Fetch for insights on basis of KS name.
            //////////////////////////////
            logger.Info("Get Window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            logger.Info("Open Case Tab");
            insightScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search");

            insightScreen2.SelectKM("Nair, Sindhu");
            logger.Info("Apply KM name Filter");

            Panel output = insightScreen2.GetInsightResult();
            bool noResult = insightScreen2.ChkIfNoResultreturned();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related Proposal per applied filter");

            if (noResult == true)
                logger.Info("No result was found but filter search was performed without error");
            else
            {
                Assert.IsTrue(result.Rows.Count() != 0);
                logger.Info("Verified some result is returned for applied filter");
            }
        }
    }
}


    




