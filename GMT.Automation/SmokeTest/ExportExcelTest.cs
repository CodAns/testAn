using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using NUnit.Framework.Internal;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using static GMT.White.AutomationFramework.WorkSpaceTab;
using static GMT.White.AutomationFramework.CaseTabBasicSearch;
using static GMT.White.AutomationFramework.InSightTabBasicSearch;
using static GMT.White.AutomationFramework.ProposalTabBasicSearch;

namespace GMT.White.AutomationFramework
{

    class ExportExcelTest : CommonInitialization<ExportExcelTest>
    {
        public void Verify_Excel()
        {
            Wait();
            Application _app1 = Application.Attach("EXCEL");
            Window winExcl = _app1.GetWindow(SearchCriteria.ByClassName("XLMAIN"), InitializeOption.NoCache);
            Assert.IsTrue(winExcl.TitleBar.Name.Contains("Excel"));
            logger.Info("Verified related Excel is downloaded and opened");
            //Wait();
            _app1.Close();
            logger.Info("Closing Excel");
        }

        [Test]
        [Category("ExcelExport")]
        public void CheckActiveCasesXcl()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Case-> 'Active Cases' 
            /////  It will export excel and verify some excel is opened realing to same.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Cases").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Case link");

            workSpaceTab.OpenCaseSettings(CaseSettingPreference.ActiveCases);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Boston");
            workSpaceTab.SelectPractice("HealthCare", FilterType.Primary);
            logger.Info("Open 'ActiveCase' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");
            gMTWindow.Get<Button>(SearchCriteria.ByAutomationId("btnExcelActiveCases")).Click();
            logger.Info("Click to Open Excel");
            Verify_Excel();
        }

        [Test]
        [Category("ExcelExport")]
        public void CheckContentInventoryXcl()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Content-> 'Content Inventory' 
            /////  It will export excel and verify some excel is opened realing to same.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Content").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Content link");

            workSpaceTab.OpenContentSettings(ContentSettingPreference.ContentInvetory);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Boston");
            workSpaceTab.SelectPractice("HealthCare", FilterType.Primary);
            logger.Info("Open 'Content Clean Up' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");
            gMTWindow.Get<Button>(SearchCriteria.ByAutomationId("btnExcelReviewMoreContent")).Click();
            logger.Info("Click to Open Excel");
            Verify_Excel();
        }

        [Test]
        [Category("ExcelExport")]
        public void CheckCaseTabXcl()
        {
            /////////////////////////////////
            /////  This Test will Filter out Cases on basis of Client NAme
            /////  It will export excel and verify some excel is opened realing to same.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");

            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            caseScreen1.getTabWin(gMTWindow);
            logger.Info("Open Case Tab");
            caseScreen1.SelectBasicSearchOptions(CaseBaseFilter.clientName, "Cigna");
            logger.Info("Apply Client Name basic filter");

            Panel output = caseScreen1.GetCasesResult();
            logger.Info("Fetch Results");
            gMTWindow.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Export")).Click();
            logger.Info("Click to Open Excel");
            Verify_Excel();
        }

        [Test]
        [Category("ExcelExport")]
        public void CheckInsightTabXcl()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of KS selected in Browse Queue
            /////  It will export excel and verify some excel is opened realing to same.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Insights").Click();
            insightScreen1.getTabWin(gMTWindow);
            logger.Info("Select Insight Tab");

            insightScreen1.SelectBasicSearchOptions(InsightBaseFilter.browseQueue, "Chopra, Mitali");
            logger.Info("Apply filter to Browse Queue");

            Panel output = insightScreen1.GetInsightResult();
            logger.Info("Fetch Results");
            gMTWindow.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Export")).Click();
            logger.Info("Click to Open Excel");
            Verify_Excel();
        }

        [Test]
        [Category("ExcelExport")]
        public void CheckProposalTabXcl()
        {
            /////////////////////////////////
            /////  This Test will Filter out Insights on basis of Ind/Cap filter.
            /////  It will export excel and verify some excel is opened realing to same.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Application window");
            gmtTab.OpenTab(gMTWindow, "Proposals").Click();
            proposalScreen1.getTabWin(gMTWindow);
            logger.Info("Select Proposals Tab");

            proposalScreen1.SelectBasicSearchOptions(ProposalBaseFilter.clientName, "Cigna");
            logger.Info("Apply filter ClientName=Cigna");

            Panel output = proposalScreen1.GetProposalResult();
            logger.Info("Fetch Results");
            gMTWindow.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Export")).Click();
            logger.Info("Click to Open Excel");
            Verify_Excel();
        }
    }
}


    




