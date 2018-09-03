using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using System.Windows.Automation;

namespace GMT.White.AutomationFramework
{
    class Tabs
    {
       
        public Tabs()
        { }

        public Window win = null;
       
        public Button OpenTab(Window gMTWindow, string tabName)
        {
            win = gMTWindow;
            
            IUIItem leftPane = win.Get(SearchCriteria.ByAutomationId("stackStrip"));
            return leftPane.Get<Button>(SearchCriteria.ByText(tabName));  
        }

        public Hyperlink OpenWorkspace(Window gMTWindow, string WorkspaceName)
        {
            win = gMTWindow;
            IUIItem leftPane = win.Get(SearchCriteria.ByAutomationId("workspaceSplitter"));
            return leftPane.Get<Hyperlink>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, WorkspaceName));
        }

        public bool ChkIfNoResultFromSearch()
        {
            bool pop = false;
            if (win.Exists(SearchCriteria.ByText("No cases were found for this search criteria.")) == true)
            {
                pop = win.Get(SearchCriteria.ByText("No cases were found for this search criteria.")).Visible;
                win.Get<Button>(SearchCriteria.ByText("OK")).Click();
            }
            return pop;
        }

        public string GetCaseAbstractStatus()
        {
            win.Get<Button>(SearchCriteria.ByAutomationId("btnEditTracking")).Click();
            Window winM = win.ModalWindow("Edit Case Tracking Details");
            string stats = winM.Get<ComboBox>(SearchCriteria.ByAutomationId("cboAbstractStatus")).SelectedItemText.ToString();
            winM.Get<Button>(SearchCriteria.ByAutomationId("Close")).DoubleClick();
            return stats;
        }

        public string GetOfficeName()
        {
            return win.Get(SearchCriteria.ByAutomationId("lblCaseOffice")).Name.ToString();
        }

        
    }
}


