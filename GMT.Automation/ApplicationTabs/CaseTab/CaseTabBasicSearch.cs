    using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class CaseTabBasicSearch
    {

        public enum CaseBaseFilter { caseCode, clientName, caseName, industry, capability, knowledgeSpecialist }
        public Window gmtWin = null;

        public CaseTabBasicSearch()
        {

        }

        public void getTabWin(Window gMtWin)
        {
            gmtWin = gMtWin;
        }

        public void SelectBasicSearchOptions(CaseBaseFilter filter, string filterValue)
        {
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetCases"));
            switch (filter)
            {
                case CaseBaseFilter.caseCode:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(filterValue);
                    break;

                case CaseBaseFilter.capability:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectCapability")).SetValue(filterValue);
                    break;

                case CaseBaseFilter.caseName:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtCaseName")).SetValue(filterValue);
                    break;

                case CaseBaseFilter.clientName:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtClientName")).SetValue(filterValue);
                    break;

                case CaseBaseFilter.industry:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectIndustry")).SetValue(filterValue);
                    break;

                case CaseBaseFilter.knowledgeSpecialist:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtKnowledgeSpecialist")).SetValue(filterValue);
                    break;
            }

        }

        public Panel GetCasesResult()
        {
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetCases"));
            getCases.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucCaseObjectExplorer"));
            return Pane;
        }



    }

}



