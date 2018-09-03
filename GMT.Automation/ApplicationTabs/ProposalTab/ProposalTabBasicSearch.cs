    using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.ListBoxItems;
using System.Windows.Automation;

//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class ProposalTabBasicSearch
    {
        public enum ProposalBaseFilter { browseQueue, proposalTitle, caseCode, clientName, industry, capability, proposalID }
        public Window gmtWin = null;
        public ProposalTabBasicSearch()
        {

        }

        public void getTabWin(Window gMtWin)
        {
            gmtWin = gMtWin;
        }

        public void SelectBasicSearchOptions (ProposalBaseFilter filter, string filterValue)
        {
            switch(filter)
            {
                case ProposalBaseFilter.browseQueue:
                    gmtWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboQueue")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.proposalTitle:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtProposalTitle")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.caseCode:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.clientName:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtClientCompany")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.industry:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectIndustry")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.capability:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtSelectCapability")).SetValue(filterValue);
                    break;

                case ProposalBaseFilter.proposalID:
                    gmtWin.Get(SearchCriteria.ByAutomationId("txtProposalID")).SetValue(filterValue);
                    break;
            }

        }

        public bool ChkIfNoResultreturned()
        {
            bool pop = false;
            if (gmtWin.Exists(SearchCriteria.ByText("No proposals were found for this search criteria.")) == true)
            {
                pop = gmtWin.Get(SearchCriteria.ByText("No proposals were found for this search criteria.")).Visible;
                gmtWin.Get<Button>(SearchCriteria.ByText("OK")).Click();
                // return pop;
            }
            return pop;
        }

        public void AddProposal(string title)
        {
            gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnAddProposal")).Click();
            gmtWin.Get(SearchCriteria.ByAutomationId("txtTitle")).SetValue(title);
            gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
        }

        public void DeleteProposal()
        {
            gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnDeleteProposal")).Click();
            gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Yes")).Click();
        }

        public Panel GetProposalResult()
        {
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetProposals"));
            getCases.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucProposalObjectExplorer"));
            return Pane;
        }
    }

}



