using System;
using System.Linq;
using NUnit.Framework;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.CaseTabAdvanceSearch;
using static GMT.White.AutomationFramework.CaseTabBasicSearch;
using System.Threading;

namespace GMT.White.AutomationFramework
{

    class CaseSmokeTest : CommonInitialization<CaseSmokeTest>
    {

        [Test]
        [Category("CaseTab")]
        [Category("Basic Search")]
        public void SearchClientName()
        {
            /////////////////////////////////
            /////  This Test will Filter out Cases on basis of Client NAme
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            #region cmt
            //Tabs gmtTab = new Tabs();
            #endregion
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            caseScreen1.getTabWin(gMTWindow);
            logger.Info("Open Case Tab");
            #region cmt
            //GMTCaseScreen caseScreen = new GMTCaseScreen();
            #endregion
            
            caseScreen1.SelectBasicSearchOptions(CaseBaseFilter.clientName, "Cigna");
            logger.Info("Apply Client Name basic filter");

            Panel output = caseScreen1.GetCasesResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get cases per applied filter");

            string colHeaderName = result.Header.Columns[3].Name.ToString();
            logger.Info("Name of Field to be Asserted: " + colHeaderName);

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                Assert.AreEqual( "Cigna", result.Rows[i].Cells[3].Value.ToString());
                logger.Debug("Assertion to match " + colHeaderName + " field Passed");
            }

        }

        [Test]
        [Category("CaseTab")]
        [Category("Basic Search")]
       // [Category("Server Check")]
        public void SearchCaseCode()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of case code
            /////  The Filter is applied on basis filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            #region cmt
            //Tabs gmtTab = new Tabs();
            #endregion
            logger.Info("Search for the Tab section");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");
            caseScreen1.getTabWin(gMTWindow);
            #region cmt
            //GMTCaseScreen caseScreen = new GMTCaseScreen();
            #endregion
            caseScreen1.SelectBasicSearchOptions(CaseBaseFilter.caseCode, "L2CZ");
            logger.Info("Apply Case Code basic filter");

            Panel output = caseScreen1.GetCasesResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Case data per applied filter");

            int i = Int32.Parse(result.Rows.Count.ToString());
            {
                Assert.IsTrue(i == 1, "Single Output Row fetched");
                Assert.AreEqual("L2CZ", result.Rows[0].Cells[4].Value.ToString());
                logger.Debug("Assert Passed Single CaseCode related row is returned");
            }

        }

        [Test]
        [Category("CaseTab")]
        [Category("Basic Search")]
        public void SearchCaseName()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of case name
            /////  The Filter is applied on basis filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            caseScreen1.getTabWin(gMTWindow);
            logger.Info("Open Case Tab");
            
            caseScreen1.SelectBasicSearchOptions(CaseBaseFilter.caseName, "Retail Test Pilot");
            logger.Info("Apply Case Name basic filter");

            Panel output = caseScreen1.GetCasesResult();
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Getting result per applied filter");

            int i = Int32.Parse(result.Rows.Count.ToString());
            {
                Assert.IsTrue(i == 1, "Single Output Row fetched");
                Assert.AreEqual("Retail Test Pilot", result.Rows[0].Cells[5].Value.ToString());
                logger.Debug("Assert Passed: Single CaseName related row is returned");
            }

            caseScreen1.SelectBasicSearchOptions(CaseBaseFilter.caseName, "Test");
            output = caseScreen1.GetCasesResult();
            result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            for (int y = 0; y < Int32.Parse(result.Rows.Count.ToString()); y++)
            {
                Console.WriteLine(result.Rows[y].Cells[5].Value.ToString());
                Assert.IsTrue(result.Rows[y].Cells[5].Value.ToString().ToLower().Contains("test"));
            }

            logger.Debug("Assert Passed: Result returned " + result.Rows.Count.ToString() + " rows with applied case name filter");

        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchByTeamMember()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of CaseCode, CaseSattus, ClientName and Contact filter
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");
            #region
            //DateTime mayStDay = new DateTime(DateTime.Now.Year, 01, 01);
            //DateTime mayEdDay = new DateTime(DateTime.Now.Year, 01, 31);
            #endregion
            logger.Info("Apply General Case-> Contact Filter");

            caseScreen2.AddContact(contact.Employee, "Martin, Jeremy");

            logger.Info("Set CaseCode, CaseSattus, ClientName ");
            caseScreen2.GeneralCases("L2CZ", 0, "Closed", "LexisNexis", 0, 0);

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            logger.Debug("Getting cases per applied search filters");

            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));

            int i = Int32.Parse(result.Rows.Count.ToString());
            {
                Assert.IsTrue(i == 1, "Single Output Row fetched");
                Assert.AreEqual("L2CZ", result.Rows[0].Cells[4].Value.ToString());
                logger.Debug("Assert Passed Single CaseCode related row is returned");
            }

        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        //[Category("Server")]
        public void AdvanceSearchByIndustry()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of Industry
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");
            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search");
            //Thread.Sleep(2000);
            caseScreen2.treevIndustry(industry.Primary, "Public Health");
            logger.Info("Applied Industry Filter at primary level");
            //Thread.Sleep(2000);
            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");
            //Thread.Sleep(2000);
            for (int y = 0; y < Int32.Parse(result.Rows.Count.ToString()); y++)
            {
                Assert.AreEqual("Public Health", result.Rows[y].Cells[10].Value.ToString());
                logger.Info("Assert Passed for Fetched Row: "+(y+1)+" in result Table is per applied industry");
            }
        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchByCapability()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of Capability
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open CaseTab");
            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance search filter");

            caseScreen2.treevCapability(capability.Primary, "Food");
            logger.Info("Applied Capability Filter at primary level");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");

            for (int y = 0; y < Int32.Parse(result.Rows.Count.ToString()); y++)
            {
                Assert.IsTrue(result.Rows[y].Cells[11].Value.ToString().Contains("Food"));
                logger.Info("Assert Passed for Fetched Row: " + (y + 1) + " in result Table is per applied capability");
            }
        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchOfficeHiearchy()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of OfficeRegion
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");
            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance search");

            caseScreen2.treevLocation("Washington, D.C.");
            logger.Info("From Office/Region Select 'Washington, D.C.' Office");
            caseScreen2.GeneralCases("L2CZ", 0, "", "", 0, 0);
            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");

            for(int i=0; i< result.Rows.Count(); i++)
            {
                if (result.Rows[i].Cells[4].Value.ToString() == "L2CZ")
                {
                    result.Rows[i].Cells[4].DoubleClick();
                    logger.Info("Open Case: L2CZ Details");
                    Wait();
                    Assert.AreEqual("Washington, D.C.", gmtTab.GetOfficeName());
                    logger.Info("Assert Passed Case returned is from selected "+ gmtTab.GetOfficeName() + " office");
                }
            }
        }

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchByIndAndORCap()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of Ind-Cap And/OR combination
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");
            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search");

            caseScreen2.treevIndustry(industry.Primary, "Car Rental");
            caseScreen2.treevCapability(capability.Primary, "Food");
            caseScreen2.searchAcrossTerm("AND");
            logger.Info("Applied Industry&Capability Filter at primary level with 'AND' toggle ");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");
            Wait();

            Assert.IsTrue(gmtTab.ChkIfNoResultFromSearch());
            logger.Info("Assert Passed No Search result was found for this and combination");

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search again");
            caseScreen2.searchAcrossTerm("OR");
            logger.Info("Applied Industry&Capability Filter at primary level with 'OR' toggle ");

             output = caseScreen2.GetCasesResult(gMTWindow);
             result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");

            for (int y = 0; y < Int32.Parse(result.Rows.Count.ToString()); y++)
            {
               if ((result.Rows[y].Cells[11].Value.ToString().Contains("Car Rental") | result.Rows[y].Cells[11].Value.ToString().Contains("Food")) == true)
                { logger.Info("Assert Passed for Fetched Row: " + (y + 1) + " in result Table is per applied industry or Capability"); };
            }
        }

        //[Test]
        //[Category("CaseTab")]
        //[Category("Advance Search")]
        //public void AdvanceSearchKeywordTool()
        //{
        //    logger.Info("Get Window");
        //    gmtTab.OpenTab(gMTWindow, "Cases").Click();
        //    logger.Info("Open Case Tab");
        //    caseScreen2.SelectAdvancedSearch(gMTWindow);
        //    logger.Info("Select Advance Search");

        //    caseScreen2.treevKeywordTools(keywordTool.Primary, "Aged");
        //    Panel output = caseScreen2.GetCasesResult(gMTWindow);
        //}

        //[Test]
        //[Category("CaseTab")]
        //[Category("Advance Search")]
        //public void AdvanceSearchTermGeoOREnablers()
        //{
        //    gmtTab.OpenTab(gMTWindow, "Cases").Click();
        //    caseScreen2.SelectAdvancedSearch(gMTWindow);

        //    caseScreen2.treevGeography(geography.Primary, "Americas");
        //    caseScreen2.treevEnablers(enablers.Primary, "Analytics");
        //    caseScreen2.searchAcrossTerm("OR");

        //    Panel output = caseScreen2.GetCasesResult(gMTWindow);
        //}

        [Test]
        [Category("CaseTab")]
        [Category("Advance Search")]
        public void AdvanceSearchTrackType()
        {
            /////////////////////////////////
            /////  This Test will Filter out Case on basis of Abstract Status
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");
            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advance Search");

            caseScreen2.trackingDateSecl(selType.Task, "25-Jan-2018", "28-Feb-2018");
            caseScreen2.trackingTasks(tasktype.Abstract, taskstatus.Due, 0);
            logger.Info("Applied Task Tracking Filters");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get the related cases per applied filter");

            for (int y = 0; y < result.Rows.Count(); y++)
            {
                result.Rows[y].Cells[3].DoubleClick();
                Assert.AreEqual("Due", gmtTab.GetCaseAbstractStatus());
                logger.Info("Assertion Passed, Result are of select Abstract Status");
                gmtTab.OpenTab(gMTWindow, "Cases").Click();
            }
        }
    }
}


    




