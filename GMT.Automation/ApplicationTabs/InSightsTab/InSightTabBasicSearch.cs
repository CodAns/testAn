using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class InSightTabBasicSearch
    {
        public enum InsightBaseFilter { browseQueue, insightTitle, caseCode, clientName, industry, capability, insightID }
        public Window gmtWin = null;
        public InSightTabBasicSearch()
        {

        }

        public void getTabWin(Window gMtWin)
        {
            gmtWin = gMtWin;
        }

        public void SelectBasicSearchOptions ( InsightBaseFilter filter, string filterValue)
        {
            switch(filter)
            {
                case InsightBaseFilter.browseQueue:
                    gmtWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboQueue")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.insightTitle:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtInsightTitle")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.caseCode:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.clientName:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtClientCompany")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.industry:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectIndustry")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.capability:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectCapability")).SetValue(filterValue);
                    break;

                case InsightBaseFilter.insightID:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtID")).SetValue(filterValue);
                    break;
            }

        }

        public string GetCreaterName()
        {
            return gmtWin.Get(SearchCriteria.ByAutomationId("lblCreatedByText")).Name;
        }

        public void AddInsight(string title)
        {
           gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnAddInsight")).Click();
           gmtWin.Get(SearchCriteria.ByAutomationId("txtTitle")).SetValue(title);
           gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
        }

        public void DeleteInsight()
        {
            gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnDeleteInsight")).Click();
            gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Yes")).Click();
        }


        public Panel GetInsightResult()
        {
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetInsights"));
            getCases.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucInsightObjectExplorer"));
            return Pane;
        }
    }

}



