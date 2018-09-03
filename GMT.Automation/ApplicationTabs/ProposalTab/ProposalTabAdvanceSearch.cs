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

namespace GMT.White.AutomationFramework
{
    class ProposalTabAdvanceSearch
    {

        public Window advanceSearch = null;
        public Window gmtWin = null;

        public enum ContentStat { isPublished, isUnPublished, isArchieved}
        public enum DateType { CreatedDate, LastModified, PublishedDate, ArchivedDate, ProposalsDate, OrignalPublishedDate, ReviewDate }
        public enum PeriodBack { Yesterday, Today, OneWeek, TwoWeeks, ThreeWeeks, OneMonth, OneQuarter, SixMonths, FullYear }
        public enum PeriodAhead { Today, OneWeek, TwoWeeks, ThreeWeeks, OneMonth, OneQuarter, SixMonths, FullYear }
        public enum FromType { Date, Period }
        public enum contact { Employee, CaseCode, Office, All }
        public enum industry { All, Primary, Secondry }
        public enum capability { All, Primary, Secondry }
        public enum keywordTool { All, Primary, Secondry }
        public enum geography { All, Primary, Secondry }
        public enum enablers { All, Primary, Secondry }

        public ProposalTabAdvanceSearch()
        {
            Logger.Setup();
        }

        public CheckBox ContentStatus(ContentStat status)
        {
            CheckBox[] currentStatusBox = new CheckBox[3];
            currentStatusBox[(int)status] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsPublished"));
            currentStatusBox[(int)status] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsUnPublished"));
            currentStatusBox[(int)status] = advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkIsArchived"));
            return currentStatusBox[(int)status];
        }

        public void SelectAdvancedSearch(Window gMtWin)
        {
            gMtWin.Get(SearchCriteria.ByAutomationId("lnkAdvancedSearch")).Click();
            advanceSearch = gMtWin.ModalWindow("Advanced Proposal Search");
            gmtWin = gMtWin;
        }

        public void DateFliter(
            DateType dateType,
            FromType fromType,             //  For  date (fromType=0) or for Period (fromType=1)
            string startDate,
            string endDate,
            PeriodBack fromBack,
            PeriodAhead toAhead
            )
        {
            ComboBox Drp = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboDateType"));
            Drp.Items[(int)dateType].Select();
            Console.WriteLine(Drp.SelectedItemText.ToString());

            switch (fromType)
            {
                case FromType.Date:
                    advanceSearch.Get<RadioButton>(SearchCriteria.ByAutomationId("rdbDate")).Click();
                    DateTimePicker stDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteStartDate"));
                    stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(startDate);
                    DateTimePicker edDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteEndDate"));
                    edDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(endDate);
                    break;

                case FromType.Period:
                    ComboBox Drp2 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboFromPeriod"));
                    Drp2.Items[(int)fromBack].Select();
                    ComboBox Drp3 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboToPeriod"));
                    Drp3.Items[(int)toAhead].Select();
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
            }
            return pop;
        }

        public void GeneralProposal(string caseCode, string clientCompany)
        {
            advanceSearch.Get<TextBox>(SearchCriteria.ByAutomationId("txtCaseCode")).SetValue(caseCode);

            if (clientCompany.Length > 2)
            {
                advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnAddClient")).Click();
                Window clientCompanyWin = advanceSearch.ModalWindow("Select Case or Custom Company");

                Client.ClientSelection.ClientName(clientCompanyWin, clientCompany);
            }

            Panel proposalSource = advanceSearch.Get<Panel>(SearchCriteria.ByAutomationId("ehProposalStatus"));
            ComboBox inSou = proposalSource.Get<ComboBox>(SearchCriteria.ByAutomationId("CheckableCombo"));

            foreach (ListItem source in inSou.Items)
            {
                //caseType.Items[2].Check()
                source.Check();
            }
        }

        public void AddContact(contact search, string keyword)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnEmployees")).Click();
            Window addContactWin = advanceSearch.ModalWindow("Add Contact");
            switch (search)
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

        public void DateUsage(FromType fromType, string usageStartDate, string usageEndDate, PeriodBack fromBack ,PeriodAhead toAhead)
        {
            switch (fromType)
            {
                case FromType.Date:
                    DateTimePicker stDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteUsageDateFrom"));
                    stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(usageStartDate);
                    DateTimePicker edDate = advanceSearch.Get<DateTimePicker>(SearchCriteria.ByAutomationId("dteUsageDateTo"));
                    stDate.Get(SearchCriteria.ByAutomationId("txtDateTime")).SetValue(usageEndDate);
                    break;

                case FromType.Period:
                    ComboBox Drp2 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboUsagePeriodFrom"));
                    Drp2.Items[(int)fromBack].Select();
                    ComboBox Drp3 = advanceSearch.Get<ComboBox>(SearchCriteria.ByAutomationId("cboUsagePeriodTo"));
                    Drp3.Items[(int)toAhead].Select();
                    break;
            }
        }

        public CheckBox IncludeCss()
        {
           return advanceSearch.Get<CheckBox>(SearchCriteria.ByAutomationId("chkUsageIncludeCSS"));
        }


        public void treevLocation(string offcName)
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOfficeRegion")).Click();
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
            switch (ind)
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

        public Panel GetProposalResult()
        {
            advanceSearch.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
            Button getCases = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnGetProposals"));
            getCases.Click();
            Panel Pane = gmtWin.Get<Panel>(SearchCriteria.ByAutomationId("ucProposalObjectExplorer"));
            return Pane;
        }
    }
}


        

    

        

    





