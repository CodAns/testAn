using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;
using TestStack.White.UIItems.TableItems;
using System;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WPFUIItems;
using NUnit.Framework;
//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class SavedReportsTab
    {

        public Panel rptWin = null;

        public SavedReportsTab()
        {

        }

        public void getWin(Window rptWin1)
        {
            rptWin = rptWin1.Get<Panel>(SearchCriteria.ByAutomationId("ppFlyout"));
        }

        public void getSavedReport()
        {
            rptWin.Get<TreeNode>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "My Reports")).Click();
        }

        public void runSavedReport()
        {
            if (rptWin.Exists(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Report 1")) == true)
            {
                rptWin.Get(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Report 1")).Click();
                rptWin.Get(SearchCriteria.ByAutomationId("btnRun")).Click();
            }else { throw new Exception("No Saved Report View Exsist. Please create a saved view"); }
        }

        public Int32 GetOutputRowsCount(Window gMTWindow)
        {
            Panel Pane = gMTWindow.Get<Panel>(SearchCriteria.ByAutomationId("ucCaseObjectExplorer"));
            Table result = Pane.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            return Int32.Parse(result.Rows.Count.ToString());
        }

    }
}



