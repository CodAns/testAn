using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;
using TestStack.White.UIItems.TableItems;
using System;
//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class PublishingQueueExplorer
    {

        public enum showItemsCheckBox { ShowItemsAwaitingProcessing, ShowCompletedItems, ShowItemsInProgress, ShowFailedItems }
        public enum contentType { Cases, Employees, HomePages, Insights, PracticeAreaPages, Proposals, Taxonomies, Lists }

        public Window gmtWin = null;

        public PublishingQueueExplorer()
        {

        }

        public void getWin(Window gmtWin1)
        {
            gmtWin = gmtWin1;
        }

        public void DateFilter(DateTime fromDate, DateTime toDate)
        {
            gmtWin.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dtePublishingFromDate")).SetDate(fromDate, DateFormat.DayMonthYear);
            gmtWin.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dtePublishingToDate")).SetDate(toDate, DateFormat.DayMonthYear);
        }

        public void UserName(string userName)
        {
            gmtWin.Get(SearchCriteria.ByAutomationId("txtUserName")).SetValue(userName);
        }

        public void ShowItemCheckBox(showItemsCheckBox checkItem)
        {
            switch (checkItem)
            {
                case showItemsCheckBox.ShowCompletedItems:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByAutomationId("chkShowCompletedItems")).Click();
                    break;
                case showItemsCheckBox.ShowFailedItems:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByAutomationId("chkShowFailedItems")).Click();
                    break;
                case showItemsCheckBox.ShowItemsAwaitingProcessing:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByAutomationId("chkShowItemsAwaitingProcessing")).Click();
                    break;
                case showItemsCheckBox.ShowItemsInProgress:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByAutomationId("chkShowItemsInProgress")).Click();
                    break;
            }
        }

        public void ClickContentType(contentType checkItem)
        {
            switch (checkItem)
            {
                case contentType.Cases:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Cases")).Click();
                    break;

                case contentType.Employees:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Employees")).Click();
                    break;
                case contentType.HomePages:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Home Pages")).Click();
                    break;
                case contentType.Insights:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Insights")).Click();
                    break;
                case contentType.Lists:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Lists")).Click();
                    break;
                case contentType.PracticeAreaPages:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Practice Area Pages")).Click();
                    break;
                case contentType.Proposals:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Proposals")).Click();
                    break;
                case contentType.Taxonomies:
                    gmtWin.Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Taxonomies")).Click();
                    break;
            }
        }

        public bool ChkIfNoResultFromQueSearch()
        {
            bool pop = false;
            if (gmtWin.Exists(SearchCriteria.ByText("No items were found for this search criteria.")) == true)
            {
                pop = gmtWin.Get(SearchCriteria.ByText("No items were found for this search criteria.")).Visible;
                gmtWin.Get<Button>(SearchCriteria.ByText("OK")).Click();
                // return pop;
            }
            return pop;
        }

        public Table outSearchQue()
        {
            gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch")).Click();
            ChkIfNoResultFromQueSearch();
            return gmtWin.Get<Table>(SearchCriteria.ByAutomationId("dgPublishingQueue")); 
        }
    }
}



