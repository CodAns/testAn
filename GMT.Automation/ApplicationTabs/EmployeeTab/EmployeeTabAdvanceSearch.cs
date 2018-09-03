using System;
using System.Threading;
using TestStack.White.UIItems.TreeItems;
using System.Windows.Automation;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework;
using System.Collections.Generic;

namespace GMT.White.AutomationFramework
{
    class EmployeeTabAdvanceSearch
    {

        Window advanceSearch = null;

        public enum GeneralEmployees { OfficeRegion, LevelDeptt};
        public enum Employee { Employee, CaseCode, Office, All };
        public enum Affiliation { Industry, Capability, Enabler};
        public enum Operator { And, Or };

        public EmployeeTabAdvanceSearch()
        {
            Logger.Setup();
        }

        public void SelectAdvancedSearch(Window gMtWin)
        {
            gMtWin.Get(SearchCriteria.ByAutomationId("lnkAdvancedSearch")).Click();
            advanceSearch = gMtWin.ModalWindow("Advanced People Search");
        }
        public CheckBox employeeStatus(int i)
        {
            CheckBox[] currentStatusBox = new CheckBox[2];
            currentStatusBox[0] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("ckbActive"));
            currentStatusBox[1] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("ckbAlumni"));
            return currentStatusBox[i];
        }

        public void GeneralEmployeeEmp(Employee search, string keyword)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnEmployees")).Click();
            Window addContactWin = advanceSearch.ModalWindow("Add Contact");
            //Panel empSearch = addContactWin.Get<Panel>(SearchCriteria.ByAutomationId("panLeft"));
            switch (search)
            {
                case Employee.Employee:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeLastName")).Click();
                    addContactWin.Get(SearchCriteria.ByAutomationId("txtLastName")).SetValue(keyword);
                    break;
                case Employee.CaseCode:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optCaseCode")).Click();
                    addContactWin.Get(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(keyword);
                    addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch")).Click();
                    addContactWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Select All")).Click();
                    break;
                case Employee.Office:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeOffice")).Click();
                    addContactWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboEmployeeOffice")).SetValue(keyword);
                    addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch")).Click();
                    addContactWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Select All")).Click();
                    break;
                case Employee.All:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    break;
                default:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("btnCancel")).Click();
                    break;
            }
            addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }

        public void GeneralEmployeetv(GeneralEmployees filter,string filterValue)
        {
            switch(filter)
            {
                case GeneralEmployees.OfficeRegion:
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOfficeRegion")).Click();
                    Tree offcRgn = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

                    Treev.TreeHiearchy.ExpandTree(offcRgn);
                    TreeNode Offc = offcRgn.Get<TreeNode>(SearchCriteria.ByText(filterValue));
                    Offc.Click();
                    advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
                    break;

                case GeneralEmployees.LevelDeptt:
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnLevelDepartment")).Click();
                    Tree lvlDpt = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvLevelDepartment"));

                    Treev.TreeHiearchy.ExpandTree(lvlDpt);
                    TreeNode lvdp = lvlDpt.Get<TreeNode>(SearchCriteria.ByText(filterValue));
                    lvdp.Click();
                    advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
                    break;
            }
        }

        public CheckBox hasPracticeAreaAff()
        {
            CheckBox cbx = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("ckbHasPracticeAreaAffiliation"));
            return cbx;
        }
        public void affiliations(Affiliation type, string affiliationValue, string andOrAcrossField,string andOrAcrossAff)
        {
            
        DateTime beginWait = DateTime.Now;
            switch (type)
            {
                case Affiliation.Industry:
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnIndustryAffiliationTerm")).Click();
                    Tree IndAff = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtSearchTerm")).SetValue(affiliationValue);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in IndAff.Nodes)
                    {
                        node.Click();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    if (andOrAcrossField == "And")
                    { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click(); }
                    if (andOrAcrossField == "OR")
                    { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click(); }

                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
                    break;

                case Affiliation.Capability:
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnCapabilityAffiliationTerm")).Click();
                    Tree capAff = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtSearchTerm")).SetValue(affiliationValue);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in capAff.Nodes)
                    {
                        node.Click();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    if (andOrAcrossField == "And")
                        advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click();
                    if (andOrAcrossField == "OR")
                        advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click();

                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
                    break;

                case Affiliation.Enabler:
                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnEnablerAffiliationTerm")).Click();
                    Tree eblAff = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtSearchTerm")).SetValue(affiliationValue);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in eblAff.Nodes)
                    {
                        node.Click();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    if (andOrAcrossField == "And")
                    { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click(); }
                    if (andOrAcrossField == "OR")
                    { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click(); }

                    advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
                    break;
            }

            if (andOrAcrossAff == "And")
                advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click();
            if (andOrAcrossAff == "OR")
                advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click();
        }

        public CheckBox hasAskMeAboutTopics()
        {
            CheckBox cbx = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("ckbHasAskmeaboutTopic"));
            return cbx;
        }

        public void askMeAboutTopic(string searchKeyword, string mappedToTerm, Operator AndOr)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnAskMeAboutTopic")).Click();

            advanceSearch.Get(SearchCriteria.ByAutomationId("txtAskMeAbout")).SetValue(searchKeyword);
            advanceSearch.Get(SearchCriteria.ByAutomationId("btnSearch")).Click();

            Table searchResult = advanceSearch.Get<Table>(SearchCriteria.ByAutomationId("dgSearchResults"));
            Table selectedTerm = advanceSearch.Get<Table>(SearchCriteria.ByAutomationId("dgDisplaySelection"));

            List<string> selVal = new List<string>();
            for (int i = 0; i < Int32.Parse(searchResult.Rows.Count.ToString()); i++)
            {
                bool va = searchResult.Rows[i].Cells[2].Value.ToString().ToLower().Contains(mappedToTerm.ToLower());
                if (va.ToString() == "True")
                {
                    searchResult.Rows[i].Cells[0].Click();
                    searchResult.Rows[i].Cells[0].DoubleClick();

                    DateTime beginWait = DateTime.Now;
                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    
                    selVal.Add(searchResult.Rows[i].Cells[1].Value.ToString());
                }
            }
            for (int j = 0; j < Int32.Parse(selectedTerm.Rows.Count.ToString()); j++)
            {
                Assert.AreEqual(selVal[j] ,selectedTerm.Rows[j].Cells[0].Value.ToString());
                
                
                Console.WriteLine(selectedTerm.Rows[j].Cells[0].Value.ToString());
            }

            switch (AndOr)
            {
                case Operator.And: advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click(); break;
                case Operator.Or: advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click(); break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOk")).Click();
        }

        public void askMeAboutMapping(string searchKeyword, string mappedToTerm, Operator AndOr)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnAskMeAboutMapping")).Click();

            advanceSearch.Get(SearchCriteria.ByAutomationId("txtSearch")).SetValue(searchKeyword);
            advanceSearch.Get(SearchCriteria.ByAutomationId("btnSearch")).Click();

            Table searchResult = advanceSearch.Get<Table>(SearchCriteria.ByAutomationId("dgSearchResults"));
            Table selectedTerm = advanceSearch.Get<Table>(SearchCriteria.ByAutomationId("dgDisplaySelection"));

            List<string> selVal = new List<string>();
            for (int i = 0; i < Int32.Parse(searchResult.Rows.Count.ToString()); i++)
            {
                bool va = searchResult.Rows[i].Cells[1].Value.ToString().ToLower().Contains(mappedToTerm.ToLower());
                if (va.ToString() == "True")
                {
                    searchResult.Rows[i].Cells[0].Click();
                    searchResult.Rows[i].Cells[0].DoubleClick();

                    selVal.Add(searchResult.Rows[i].Cells[1].Value.ToString());
                }
            }
            for (int j = 0; j < Int32.Parse(selectedTerm.Rows.Count.ToString()); j++)
            {
                Assert.AreEqual(selVal[j], selectedTerm.Rows[j].Cells[0].Value.ToString());


                Console.WriteLine(selectedTerm.Rows[j].Cells[0].Value.ToString());
            }

            switch (AndOr)
            {
                case Operator.And: advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbAnd")).Click(); break;
                case Operator.Or: advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbOr")).Click(); break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOk")).Click();
        }

        public Panel GetEmployeeResult(Window gmtWin)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
            Button getEmployee = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch"));
            getEmployee.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucEmployeeObjectExplorer"));
            return Pane;
        }

    }
}


        

    

        

    





