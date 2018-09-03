using System;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.UIItems.ListBoxItems;
using System.Threading;
using TestStack.White.UIItems.TreeItems;
using System.Windows.Automation;

namespace GMT.White.AutomationFramework
{
    class CaseTabAdvanceSearch
    {

        Window advanceSearch = null;
        public enum contact { Employee, CaseCode, Office, All }
        public enum industry { All, Primary, Secondry }
        public enum capability { All, Primary, Secondry }
        public enum keywordTool { All, Primary, Secondry }
        public enum geography { All, Primary, Secondry }
        public enum enablers { All, Primary, Secondry }
        public enum tasktype { Abstract, Proposal, Summary, OnePager, noSelection }
        public enum taskstatus { Complete, Due, Exempt, InProgress, NotStarted, Overdue, noSelection }
        public enum selType { Task, Contact, Content, Notes }


        public CaseTabAdvanceSearch()
        {

        }

        public void SelectAdvancedSearch(Window gMtWin)
        {
            gMtWin.Get(SearchCriteria.ByAutomationId("lnkAdvancedSearch")).Click();
            advanceSearch = gMtWin.ModalWindow("Advanced Case Search");
        }
        public CheckBox ContentStatus(int i)
        {
            CheckBox[] currentStatusBox = new CheckBox[3];
            currentStatusBox[0] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsPublished"));
            currentStatusBox[1] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsUnPublished"));
            currentStatusBox[2] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsArchived"));

            return currentStatusBox[i];
        }

        public void DateFliter(
            string dateType,         //  For Date Type Drop down
            int fromType,             //  For  date (fromType=0) or for Period (fromType=1)
            string startDate,
            string endDate,
            string fromKeyword,       //  For From Drop down
            string toKeyword          //  to From Drop down
            )
        {
            ComboBox Drp = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboDateType"));
            Drp.Select(dateType);
            Console.WriteLine(Drp.SelectedItemText.ToString());

            if (fromType == 0)
            {
                advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbDate")).Click();
                DateTimePicker stDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteStartDate"));
                stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(startDate);

                #region WaitSyntax
                //DateTime beginWait = DateTime.Now;
                //while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                //    Thread.Sleep(250);
                #endregion

                DateTimePicker edDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteEndDate"));
                edDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(endDate);
            }
            else
            {
                ComboBox Drp2 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboFromPeriod"));
                Drp2.Select(fromKeyword);

                ComboBox Drp3 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboToPeriod"));
                Drp3.Select(toKeyword);
            }

        }

        public void GeneralCases(string caseCode, int isGlobalEnduringClient, string caseStatus, string clientCompany, int durationFrom, int durationTo)
        {
            advanceSearch.Get<TextBox>(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(caseCode);
            if (isGlobalEnduringClient == 1) { advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsGlobalEnduring")).Click(); }
            if (caseStatus.Length > 2)
            { if (caseStatus == "Closed")
                    advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboCaseStatus")).Select(1);
                else advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboCaseStatus")).Select(0);
            }
            if (clientCompany.Length > 2)
            {
                advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnAddClient")).Click();
                Window clientCompanyWin = advanceSearch.ModalWindow("Select Case or Custom Company");

                Client.ClientSelection.ClientName(clientCompanyWin, clientCompany);
            }

            if (durationFrom > 0 & durationFrom < 13)
            {
                if (durationTo > 0 & durationTo < 13)
                {
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtDurationFrom")).SetValue(durationFrom);
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtDurationTo")).SetValue(durationTo);
                }
                else if (durationFrom >= 13 | durationFrom < 0) throw new System.ArgumentException("Invalid DurationTo selection enter between 1-12");
            }
            else if (durationFrom >= 13 | durationFrom < 0)
            {
                throw new System.ArgumentException("Invalid DurationFrom selection enter between 1-12");
            }

            Panel cseTpe = advanceSearch.Get<Panel>(SearchCriteria.ByAutomationId("ehCaseType"));
            ComboBox caseType = cseTpe.Get<ComboBox>(SearchCriteria.ByAutomationId("CheckableCombo"));

            foreach (ListItem Billable in caseType.Items)
            {
                //caseType.Items[2].Check()
                Billable.Check();
            }
        }

        public void AddContact(contact search, string keyword)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnEmployees")).Click();
            Window addContactWin = advanceSearch.ModalWindow("Add Contact");
            //Panel empSearch = addContactWin.Get<Panel>(SearchCriteria.ByAutomationId("panLeft"));
            contact c = search;
            switch (c)
            {
                case contact.Employee:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeLastName")).Click();
                    addContactWin.Get(SearchCriteria.ByAutomationId("txtLastName")).SetValue(keyword);
                    break;
                case contact.CaseCode:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optCaseCode")).Click();
                    addContactWin.Get(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(keyword);
                    addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch")).Click();
                    addContactWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Select All")).Click();
                    break;
                case contact.Office:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optEmployeeOffice")).Click();
                    addContactWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboEmployeeOffice")).SetValue(keyword);
                    addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSearch")).Click();
                    addContactWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Select All")).Click();
                    break;
                case contact.All:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    break;
                default:
                    addContactWin.Get<RadioButton>(SearchCriteria.ByAutomationId("btnCancel")).Click();
                    break;
            }

            addContactWin.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }

        public void task(string taskStartDate, string taskEndDate)
        {
            DateTimePicker stDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteTaskCompletedFromDate"));
            stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(taskStartDate);

            DateTimePicker edDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteTaskCompletedToDate"));
            stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(taskEndDate);
        }


        public void treevLocation(string offcName)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnManagingOfficeRegion")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                Thread.Sleep(250);

            Treev.TreeHiearchy.ExpandTree(managingOfficeRegion);

            TreeNode na = managingOfficeRegion.Get<TreeNode>(SearchCriteria.ByText(offcName));
            na.Click();

            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                Thread.Sleep(250);

            advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }


        public void treevIndustry(industry ind, string ter)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSelectIndustries")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));


            DateTime beginWait = DateTime.Now;
            industry c = ind;
            switch (c)
            {
                case industry.Primary:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optPrimary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case industry.Secondry:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optSecondary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case industry.All:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);
                    // will select all option filtered in search
                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }


        public void treevCapability(capability cap, string ter)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSelectCapabilities")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

            DateTime beginWait = DateTime.Now;
            capability c = cap;
            switch (c)
            {
                case capability.Primary:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optPrimary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case capability.Secondry:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optSecondary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case capability.All:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    //Console.WriteLine(managingOfficeRegion.Nodes.);
                    //treeNode = managingOfficeRegion.Node(ter);
                    // treeNode.Expand();
                    //advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);

                    break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }


        public void treevKeywordTools(keywordTool keyTool, string ter)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSelectKeywords")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

            DateTime beginWait = DateTime.Now;
            keywordTool c = keyTool;
            switch (c)
            {
                case keywordTool.Primary:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optPrimary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case keywordTool.Secondry:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optSecondary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case keywordTool.All:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }

        public void treevGeography(geography geo, string ter)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSelectGeography")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

            DateTime beginWait = DateTime.Now;
            geography c = geo;
            switch (c)
            {
                case geography.Primary:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optPrimary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case geography.Secondry:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optSecondary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case geography.All:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }

        public void treevEnablers(enablers ena, string ter)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSelectEnablers")).Click();
            Tree managingOfficeRegion = advanceSearch.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

            DateTime beginWait = DateTime.Now;
            enablers c = ena;
            switch (c)
            {
                case enablers.Primary:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optPrimary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case enablers.Secondry:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optSecondary")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }

                    break;

                case enablers.All:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAll")).Click();
                    advanceSearch.Get(SearchCriteria.ByAutomationId("txtTermName")).SetValue(ter);

                    while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                        Thread.Sleep(250);

                    foreach (TreeNode node in managingOfficeRegion.Nodes)
                    {
                        Console.WriteLine(node.Name.ToString());
                        node.Select();
                        advanceSearch.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                    }
                    break;
            }

            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
        }

        public void searchAcrossTerm(string operation)
        {
            if (operation == "AND")
            { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optAND")).Click(); }
            else if (operation == "OR") { advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("optOR")).Click(); }
        }

        public void trackingDateSecl(selType sel, string taskDateFrom, string taskDateTo)
        {
            if ((taskDateFrom.Length > 0 && taskDateTo.Length <= 0) || (taskDateTo.Length > 0 && taskDateFrom.Length <= 0))
            { throw new System.ArgumentException("Missing Start or End Date"); }

            selType c = sel;
            string str = "";
            switch (c)
            {
                case selType.Task: str = "TaskCompleted"; break;
                case selType.Content: str = "ContentReq"; break;
                case selType.Contact: str = "Contact"; break;
                case selType.Notes: str = "NoteCreated"; break;
            }

            if (taskDateFrom.Length > 2)
            {
                DateTimePicker fromDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dte" + str + "FromDate"));
                fromDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(taskDateFrom);
            }

            if (taskDateTo.Length > 2)
            {
                DateTimePicker toDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dte" + str + "ToDate"));
                toDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(taskDateTo);
            }

        }

        public void trackingTasks(tasktype opt, taskstatus opt1, int termConfirmation)
        {
            Panel TaskType = advanceSearch.Get<Panel>(SearchCriteria.ByAutomationId("ehTaskType"));
            ComboBox cb = TaskType.Get<ComboBox>(SearchCriteria.ByAutomationId("CheckableCombo"));

            Panel TaskType1 = advanceSearch.Get<Panel>(SearchCriteria.ByAutomationId("ehTaskStatus"));
            ComboBox cb1 = TaskType1.Get<ComboBox>(SearchCriteria.ByAutomationId("CheckableCombo"));

            DateTime beginWait = DateTime.Now;

            if (opt != tasktype.noSelection)
            {
                tasktype c = opt;
                switch (c)
                {
                    case tasktype.Abstract: cb.Items[0].Check(); break;
                    case tasktype.Proposal: cb.Items[1].Check(); break;
                    case tasktype.Summary: cb.Items[2].Check(); break;
                    case tasktype.OnePager: cb.Items[3].Check(); break;
                   // case tasktype.noSelection: break;
                }
            }
 
            if (opt1 != taskstatus.noSelection)
            {
                taskstatus c1 = opt1;
                switch (c1)
                {
                    case taskstatus.Complete: cb1.Items[0].Check(); break;
                    case taskstatus.Due: cb1.Items[1].Check(); break;
                    case taskstatus.Exempt: cb1.Items[2].Check(); break;
                    case taskstatus.InProgress: cb1.Items[3].Check(); break;
                    case taskstatus.NotStarted: cb1.Items[4].Check(); break;
                    case taskstatus.Overdue: cb1.Items[5].Check(); break;
                  //  case taskstatus.noSelection: break;
                }
            }

            if (termConfirmation == 1)
            { advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkTermConfirmation")).Click(); }
            
        }

        public string SelectKM()
        {
            Panel TaskType = advanceSearch.Get<Panel>(SearchCriteria.ByAutomationId("ehKnowledgeSpecialist"));
            ComboBox cb = TaskType.Get<ComboBox>(SearchCriteria.ByAutomationId("CheckableCombo"));//.Click();

            return cb.Items[1].Get<CheckBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty,"Agarwal, Ankita")).ToString();
        }

        public Panel GetCasesResult(Window gmtWin)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetCases"));
            getCases.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucCaseObjectExplorer"));
            return Pane;  
        }
    }
}


        

    

        

    





