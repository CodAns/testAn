using System;
using System.Threading;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.UIItems.TreeItems;
using System.Windows.Automation;
//using System.Windows.Forms;

namespace GMT.White.AutomationFramework
{
    class WorkSpaceTab
    {
        public enum ContentSettingPreference { NewContent, ContentCleanUp, ContentInvetory }
        public enum CaseSettingPreference { NewCases, EndingCases, ActiveCases }
        public enum TimeFrame { OneWeek, TwoWeek, ThreeWeek, OneMonth, NoSel}
        public enum Scope { Mine, ByPractice_Office, All, NoSel}
        public enum OtherKS { Auto, Capability, Industry, NoSel }
        public enum Role { LeadKS, PrimaryKS, SecondryKS, NoSel }
        public enum FilterType {Secondry, Primary, All}
        public enum ContentWorkPanel { ContentDetails, ReviewContent, DocumentOption, Notes,Contacts, CaseCode_ClientName, Terms, KS }
        public enum CaseWorkPanel { CaseDetails, Outreach, TasksNotes, ContentRequested, Terms, SimilarContentSubmission, KS, Priority, RequestServed }

        static Window gmtWin = null;
        static Window prefWin = null;
        static string outTable = null;

        public WorkSpaceTab()
        {
            
        }

        public void getLinkWin(Window gMtWin)
        {
            gmtWin = gMtWin;
        }

        public void OpenContentSettings(ContentSettingPreference prefType)
        {
            switch (prefType)
            {
                case ContentSettingPreference.NewContent:
                    outTable = "NewContent";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnNewContentPreference")).Click();
                    prefWin = gmtWin.ModalWindow("New Content Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break;
                case ContentSettingPreference.ContentCleanUp:
                    outTable = "ContentForReview";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnCFRPreferences")).Click();
                    prefWin = gmtWin.ModalWindow("Content Clean Up Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break;
                case ContentSettingPreference.ContentInvetory:
                    outTable = "ReviewMoreContent";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnRMCPreferences")).Click();
                    prefWin = gmtWin.ModalWindow("Content Inventory Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break;
            }
        }

        public void OpenCaseSettings(CaseSettingPreference prefType)
        {
            switch (prefType)
            {
                case CaseSettingPreference.NewCases: outTable = "NewCases";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnNewCasePreferences")).Click();
                    prefWin = gmtWin.ModalWindow("New Cases Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break;
                case CaseSettingPreference.EndingCases:  outTable = "EndingCases";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnEndCasePreferences")).Click();
                    prefWin = gmtWin.ModalWindow("Ending Cases Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break; 
                case CaseSettingPreference.ActiveCases:  outTable = "ActiveCases";
                    gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnActiveCasesPreference")).Click();
                    prefWin = gmtWin.ModalWindow("Active Cases Preferences");
                    prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnDefault")).Click(); break; 
            }
        }

        public void PrefSelection(TimeFrame time, Scope scope, OtherKS otherKS, Role role)
        {
            ComboBox cb = prefWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboPeriod"));
            if (time != TimeFrame.NoSel && cb.Items[(int)time].Enabled==true) cb.Items[(int)time].Select();
            ComboBox cb1 = prefWin.Get<ComboBox>(SearchCriteria.ByAutomationId("cboScope"));
            if (scope != Scope.NoSel && cb1.Items[(int)scope].Enabled == true) cb1.Items[(int)scope].Select();
            GroupBox rb = prefWin.Get<GroupBox>(SearchCriteria.ByAutomationId("grpOtherKS"));
            if (otherKS != OtherKS.NoSel && rb.Items[(int)otherKS].Enabled == true) rb.Items[(int)otherKS].Click();
            ListBox lb = prefWin.Get<ListBox>(SearchCriteria.ByAutomationId("chkBoxListRole"));
            if (role != Role.NoSel && lb.Items[(int)role].Enabled == true) lb.Items[(int)role].Click();
        }

        public void SelectPractice(string pracName, FilterType filter)
        {
            Button selectPrac = prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnPractice"));
            if (selectPrac.Enabled==true)
            {
                selectPrac.Click();
                ListBox lv = gmtWin.Get<ListBox>(SearchCriteria.ByAutomationId("cblPracticeAreaNames"));
                foreach (ListItem t in lv.Items)
                { if (t.Name.ToString().ToLower().Contains(pracName.ToLower())) t.Click(); }
                prefWin.ModalWindow("Select Practice Area Names").Items[(int)filter].Click();
                gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnOK")).Click();
            }
        }

        public void TreevLocation(string offcName)
        {
            Button region = prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnOfficeRegion"));
            if (region.Enabled == true)
            {
                region.Click();
                Tree managingOfficeRegion = prefWin.Get<Tree>(SearchCriteria.ByAutomationId("tvHierarchy"));

                DateTime beginWait = DateTime.Now;
                while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                    Thread.Sleep(250);

                Treev.TreeHiearchy.ExpandTree(managingOfficeRegion);

                TreeNode na = managingOfficeRegion.Get<TreeNode>(SearchCriteria.ByText(offcName));
                na.Click();

                while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                    Thread.Sleep(250);

                prefWin.Keyboard.PressSpecialKey(KeyboardInput.SpecialKeys.SPACE);
                prefWin.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();
            }
        }

        public Table GetResult()
        {
            Button getResult = gmtWin.Get<Button>(SearchCriteria.ByAutomationId("btnOK"));
            getResult.Click();
            Table output = gmtWin.Get<Table>(SearchCriteria.ByAutomationId("dg"+outTable));
            return output;
        }

        public void ClickCaseWorkpanel(CaseWorkPanel panelName)
        {
            switch (panelName)
            {
                case CaseWorkPanel.CaseDetails: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Case Details")).Click(); break;
                case CaseWorkPanel.ContentRequested: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Content Requested")).Click(); break;
                case CaseWorkPanel.KS: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "KS")).Click(); break;
                case CaseWorkPanel.Outreach: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Outreach")).Click(); break;
                case CaseWorkPanel.Priority: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Priority")).Click(); break;
                case CaseWorkPanel.RequestServed: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Request Served")).Click(); break;
                case CaseWorkPanel.SimilarContentSubmission: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Similar Content & Submissions")).Click(); break;
                case CaseWorkPanel.TasksNotes: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Tasks & Notes")).Click(); break;
                case CaseWorkPanel.Terms: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Terms")).Click(); break;

            }
        }

        public void ClickContentWorkpanel(ContentWorkPanel panelName)
        {
            switch (panelName)
            {
                case ContentWorkPanel.Terms: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Terms")).Click(); break;
                case ContentWorkPanel.KS: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "KS")).Click(); break;
                case ContentWorkPanel.Contacts: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Contacts")).Click(); break;
                case ContentWorkPanel.ContentDetails: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Content Details")).Click(); break;
                case ContentWorkPanel.Notes: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Notes")).Click(); break;
                case ContentWorkPanel.ReviewContent: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Review Content")).Click(); break;
                case ContentWorkPanel.DocumentOption: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Document Option")).Click(); break;
                case ContentWorkPanel.CaseCode_ClientName: gmtWin.Get<Button>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Case Code(s)/Client Name(s)")).Click(); break;
            }
        }

        public bool ChkIfNoRecordFound()
        {
           return gmtWin.Exists(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "No Record Selected"));
        }
    }

}



