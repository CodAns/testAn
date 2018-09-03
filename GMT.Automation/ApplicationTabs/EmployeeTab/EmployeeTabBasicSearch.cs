    using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.ListBoxItems;
//using System;
//using TestStack.White.UIItems.Actions;

namespace GMT.White.AutomationFramework
{
    class EmployeeTabBasicSearch
    {
        public enum basicFilter { LastName, EmployeeCode, Office, AllEmployee, IncludeAlumni};


        public EmployeeTabBasicSearch()
        {

        }

        public void empBasicFilter(Window gmtWindow, basicFilter filter, string filterValue)
        {
            switch (filter)
            {
                case basicFilter.LastName:
                    gmtWindow.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeLastName")).Click();
                    gmtWindow.Get(SearchCriteria.ByAutomationId("txtLastName")).SetValue(filterValue);
                    break;

                case basicFilter.EmployeeCode:
                    gmtWindow.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeCode")).Click();
                    gmtWindow.Get(SearchCriteria.ByAutomationId("txtEmployeeCode")).SetValue(filterValue);
                    break;

                case basicFilter.Office:
                    gmtWindow.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeOffice")).Click();
                    gmtWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("cboEmployeeOffice")).SetValue(filterValue);
                    break;

                case basicFilter.AllEmployee:
                    if (filterValue == "Y")
                        gmtWindow.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    break;

                case basicFilter.IncludeAlumni:
                    if (filterValue == "Y")
                        gmtWindow.Get<CheckBox>(SearchCriteria.ByAutomationId("chkShowArchivedContent")).Click();
                    break;
            }
        }
    
        public Panel GetEmployeeResult(Window gmtWin)
        {
            Button getEmployee = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch"));
            getEmployee.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucEmployeeObjectExplorer"));
            return Pane;
        }




    }
}



