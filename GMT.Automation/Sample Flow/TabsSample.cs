//using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.ScreenObjects;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White;
using TestStack.White.UIItems.Finders;
//using static WindowAutomation.gMTMainScreen;
using TestStack.White.UIItems.WPFUIItems;
using System.Collections.Generic;

namespace GMT.White.AutomationFramework
{
    class TabsSample
    {

        public TabsSample()
        { }

        public Button OpenCaseTab()
        {
            GMTMainScreenSample Win = new GMTMainScreenSample();
            Window scWin = Win.AppWindow();
            var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
            var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Cases"));

            return tbButton;
        }

        //public Button OpenInsightTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Insights"));

        //    return tbButton;
        //}

        //public Button OpenProposalTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Proposals"));

        //    return tbButton;
        //}

        //public Button OpenEmployeeTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Employees"));

        //    return tbButton;
        //}

        //public Button OpenPracticeAreaPageTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Practice Area Pages"));

        //    return tbButton;
        //}

        //public Button OpenTaxonomiesTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Taxonomies"));

        //    return tbButton;
        //}

        //public Button OpenSettingTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Settings"));

        //    return tbButton;
        //}

        //public Button OpenPublishingQueueTab(Window scWin)
        //{
        //    var leftPane = scWin.Get(SearchCriteria.ByAutomationId("stackStrip"));
        //    var tbButton = leftPane.Get<Button>(SearchCriteria.ByText("Publishing Queue"));

        //    return tbButton;
        //}
    }
}


