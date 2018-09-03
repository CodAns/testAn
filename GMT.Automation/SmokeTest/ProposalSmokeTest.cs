using System.Linq;
using NUnit.Framework;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.ProposalTabBasicSearch;
using static GMT.White.AutomationFramework.ProposalTabAdvanceSearch;

namespace GMT.White.AutomationFramework
{

    class ProposalSmokeTest : CommonInitialization<ProposalSmokeTest>
    {

        [Test]
        [Category("ProposalTab")]
        [Category("Basic Search")]
        public void PropVerifyBrowseQueue()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of KS selected in Browse Queue
            /////  The Filter is applied on basic filter field
            //////////////////////////////

            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            proposalScreen1.getTabWin(gMTWindow);
            logger.Info("Select Proposals Tab");

            proposalScreen1.SelectBasicSearchOptions(ProposalBaseFilter.browseQueue, "Chopra, Mitali");
            logger.Info("Apply filter to Browse Queue");

            Panel output = proposalScreen1.GetProposalResult();
            bool noResult = proposalScreen1.ChkIfNoResultreturned();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Insights per applied filter");

            if(noResult == false)
            {
                Assert.IsTrue(result.Rows.Count() != 0);
                logger.Info("Asserted that some result is returened for applied Filter.");
            } else { logger.Info("Cannot Assert since no result exsist for applied filter."); }
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Basic Search")]
        public void PropVerifyBasisClientName()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of Ind/Cap filter.
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            proposalScreen1.getTabWin(gMTWindow);
            logger.Info("Select Proposals Tab");

            proposalScreen1.SelectBasicSearchOptions(ProposalBaseFilter.clientName, "Cigna");
            logger.Info("Apply filter ClientName=Cigna");

            Panel output = proposalScreen1.GetProposalResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Insights per applied filter");

            for(int y =0; y <result.Rows.Count(); y++)
            Assert.IsTrue(result.Rows[y].Cells[5].Value.ToString().TrimEnd().Contains("Cigna"));
            logger.Info("Asserted Insights returned are related to Cigna");
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Basic Search")]
        
        public void PropVerifyProposalCreation()
        {
            /////////////////////////////////
            /////  This Test will will create a new Proposal and then verify that insight is crated.
            /////  Post verification it will delete the created Proposal.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            proposalScreen1.getTabWin(gMTWindow);
            logger.Info("Select Proposal Tab");

            proposalScreen1.AddProposal("TestAK");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click(); Wait();
            logger.Info("Create New Proposal with Title- 'TestAK'");

            proposalScreen1.SelectBasicSearchOptions(ProposalBaseFilter.proposalTitle, "TestAK");
            Panel output = proposalScreen1.GetProposalResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Search For newly created Proposal");

            Assert.IsTrue(result.Rows.Count() == 1);
            result.Rows[0].Cells[3].DoubleClick();
            string proposalTitle = gMTWindow.Get(SearchCriteria.ByAutomationId("ucProposalContentHeader")).Name;
            Assert.AreEqual(result.Rows[0].Cells[2].Value.ToString(), proposalTitle);
            logger.Info("Asserted Single row with insight title-'TestAK' is returned");

            proposalScreen1.DeleteProposal();
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Advance Search")]

        public void PropAdvanceSearchByTeamMember()  // bug 10114
        {
            /////////////////////////////////
            /////  This Test will Filter out Proposal on basis of CaseCode, and ClientName 
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            logger.Info("Open Proposal Tab");

            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");

            proposalScreen2.GeneralProposal("ABV6", "Sony");
            logger.Info("Set CaseCode, ClientName Filter");

            Panel output = proposalScreen2.GetProposalResult();
            bool noResult = proposalScreen2.ChkIfNoResultreturned();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Debug("Getting cases per applied search filters");
            int i = result.Rows.Count();
            if (noResult == true)
                logger.Info("No result was found but filter search was performed without error");
            else
            {
                Assert.IsTrue(i == 1, "Single Output Row fetched");
                Assert.AreEqual("ABV6", result.Rows[0].Cells[4].Value.ToString().TrimEnd());
                Assert.IsTrue(result.Rows[0].Cells[5].Value.ToString().Contains("Sony"));
                logger.Debug("Assert Passed Single CaseCode related row is returned");
            }
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Advance Search")]

        public void PropAdvanceSearchByDatePeriod()  
        {
            /////////////////////////////////
            /////  This Test will Fetch for proposals within applied date filter range.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            logger.Info("Select Proposal Tab");
            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            proposalScreen2.DateFliter(DateType.ProposalsDate, FromType.Period, "", "", PeriodBack.OneQuarter, PeriodAhead.Today);
            logger.Info("Apply date filter");

            Panel output = proposalScreen2.GetProposalResult();
            bool noResult = proposalScreen2.ChkIfNoResultreturned();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Search For newly created insight");

            if (noResult == true)
                logger.Info("No result was found but filter search was performed without error");
            else
            {
                Assert.IsTrue(result.Rows.Count() != 0);
                logger.Info("Verified some result is returned for applied filter");
            }
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Advance Search")]

        public void PropAdvanceSearchByUsage()
        {
            /////////////////////////////////
            /////  This Test will Fetch for proposals within applied Usage filter range including CSS.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            logger.Info("Select Proposal Tab");
            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            proposalScreen2.GeneralProposal("", "Merck");
            proposalScreen2.DateUsage( FromType.Period,"", "", PeriodBack.Today, PeriodAhead.Today);
            proposalScreen2.IncludeCss().Click();
            logger.Info("Apply Usage filter range including CSS");

            Panel output = proposalScreen2.GetProposalResult();
            bool noResult = proposalScreen2.ChkIfNoResultreturned();
          
            logger.Info("Search For newly created insight");

            if (noResult != true)
                logger.Info("result was found for filter search without error");
        }

        [Test]
        [Category("ProposalTab")]
        [Category("Advance Search")]
        public void PropAdvanceSearchOfficeHiearchy()
        {
            /////////////////////////////////
            /////  This Test will Filter out Proposal on basis of OfficeRegion
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            logger.Info("Select Proposal Tab");
            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            proposalScreen2.treevLocation("Washington, D.C.");
            logger.Info("From Office/Region Select 'Washington, D.C.' Office");
            Panel output = proposalScreen2.GetProposalResult();
            bool noResult = proposalScreen2.ChkIfNoResultreturned();
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
        [Category("ProposalTab")]
        [Category("Advance Search")]
        public void PropAdvanceSearchByIndAndORCap()
        {
            /////////////////////////////////
            /////  This Test will Filter out Proposal on basis of Ind-Cap And/OR combination
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            logger.Info("Select Proposal Tab");
            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            proposalScreen2.treevIndustry(industry.Primary, "Car Rental");
            proposalScreen2.treevCapability(capability.Primary, "Food");
            proposalScreen2.searchAcrossTerm("AND");
            logger.Info("Applied Industry&Capability Filter at primary level with 'AND' toggle ");

            Panel output = proposalScreen2.GetProposalResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related proposals per applied filter");
            Wait();

            Assert.IsTrue(proposalScreen2.ChkIfNoResultreturned());
            logger.Info("Assert Passed No Search result was found for this and combination");

            proposalScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search again");
            proposalScreen2.searchAcrossTerm("OR");
            logger.Info("Applied Industry&Capability Filter at primary level with 'OR' toggle ");

            output = proposalScreen2.GetProposalResult();
            result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related proposals per applied filter");

            for (int y = 0; y < result.Rows.Count(); y++)
            {
                if ((result.Rows[y].Cells[11].Value.ToString().Contains("Car Rental") | result.Rows[y].Cells[11].Value.ToString().Contains("Food")) == true)
                { logger.Info("Assert Passed for Fetched Row: " + (y + 1) + " in result Table is per applied industry or Capability"); };
            }
        }

    }
}


    




