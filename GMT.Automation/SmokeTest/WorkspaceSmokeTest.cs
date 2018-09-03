using System;
using System.Linq;
using NUnit.Framework;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.WorkSpaceTab;

namespace GMT.White.AutomationFramework
{

    class WorkspaceSmokeTest : CommonInitialization<WorkspaceSmokeTest>
    {
        [Test]
        [Category("WorkSpaceCases")]
        public void CheckNewCases()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Case-> 'New Cases' 
            /////  If result is retuned it verifies Case details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Cases").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Case link");

            workSpaceTab.OpenCaseSettings(CaseSettingPreference.NewCases);
            workSpaceTab.PrefSelection(TimeFrame.OneMonth, Scope.All, OtherKS.NoSel, Role.NoSel);
            logger.Info("Open 'NewCase' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");
            

            if (output.Rows.Count>=1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Case Detail");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Outreach);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Outreach");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.TasksNotes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Tasks & Notes");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.ContentRequested);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Requested");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.SimilarContentSubmission);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Similar Content & Submission");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Priority);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Priority");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.RequestServed);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Request Served");
            } else logger.Info("No result found for criteria but system didn't throw any error");
        }

        [Test]
        [Category("WorkSpaceCases")]
        public void CheckEndingCases()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Case-> 'Ending Cases' 
            /////  If result is retuned it verifies Case details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Cases").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Case link");

            workSpaceTab.OpenCaseSettings(CaseSettingPreference.EndingCases);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Toronto");
            logger.Info("Open 'EndCase' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");

            if (output.Rows.Count >= 1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Case Detail");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Outreach);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Outreach");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.TasksNotes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Tasks & Notes");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.ContentRequested);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Requested");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.SimilarContentSubmission);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Similar Content & Submission");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Priority);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Priority");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.RequestServed);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Request Served");
            }
            else logger.Info("No result found for criteria but system didn't throw any error");
        }

        [Test]
        [Category("WorkSpaceCases")]
        public void CheckActiveCases()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Case-> 'Active Cases' 
            /////  If result is retuned it verifies Case details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Cases").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Case link");

            workSpaceTab.OpenCaseSettings(CaseSettingPreference.ActiveCases);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Boston");
            workSpaceTab.SelectPractice("HealthCare",FilterType.Primary);
            logger.Info("Open 'ActiveCase' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");

            if (output.Rows.Count >= 1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Case Detail");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Outreach);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Outreach");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.TasksNotes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Tasks & Notes");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.ContentRequested);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Requested");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.SimilarContentSubmission);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Similar Content & Submission");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.Priority);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Priority");
                workSpaceTab.ClickCaseWorkpanel(CaseWorkPanel.RequestServed);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Request Served");
            }
            else logger.Info("No result found for criteria but system didn't throw any error");
        }

        [Test]
        [Category("WorkSpaceContent")]
        public void CheckNewContent()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Content-> 'New Content' 
            /////  If result is retuned it verifies Content details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Content").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Content link");

            workSpaceTab.OpenContentSettings(ContentSettingPreference.NewContent);
            workSpaceTab.PrefSelection(TimeFrame.OneMonth, Scope.All, OtherKS.NoSel, Role.NoSel);
            logger.Info("Open 'NewContent' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");

            if (output.Rows.Count >= 1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Detail");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.ReviewContent);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Review Content");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.DocumentOption);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Document Option");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Notes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Notes");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Contacts);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Contacts");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.CaseCode_ClientName);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for CaseCode(s)/ClientName(s)");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
            }
            else logger.Info("No result found for criteria but system didn't throw any error");
        }

        [Test]
        [Category("WorkSpaceContent")]
        public void CheckContentCleanUp()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Content-> 'Content Clean Up' 
            /////  If result is retuned it verifies Case details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Content").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Content link");

            workSpaceTab.OpenContentSettings(ContentSettingPreference.ContentCleanUp);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Toronto");
            logger.Info("Open 'Content Clean Up' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");

            if (output.Rows.Count >= 1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Detail");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.ReviewContent);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Review Content");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.DocumentOption);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Document Option");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Notes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Notes");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Contacts);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Contacts");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.CaseCode_ClientName);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for CaseCode(s)/ClientName(s)");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
            }
            else logger.Info("No result found for criteria but system didn't throw any error");
        }

        [Test]
        [Category("WorkSpaceContent")]
        public void CheckContentInventory()
        {
            /////////////////////////////////
            /////  This Test will apply filter on WorkSpace-Content-> 'Content Inventory' 
            /////  If result is retuned it verifies Case details is not blank
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get Window");
            gmtTab.OpenWorkspace(gMTWindow, "Content").Click();
            workSpaceTab.getLinkWin(gMTWindow);
            logger.Info("Open WorkSpace Content link");

            workSpaceTab.OpenContentSettings(ContentSettingPreference.ContentInvetory);
            workSpaceTab.PrefSelection(TimeFrame.OneWeek, Scope.ByPractice_Office, OtherKS.NoSel, Role.NoSel);
            workSpaceTab.TreevLocation("Boston");
            workSpaceTab.SelectPractice("HealthCare", FilterType.Primary);
            logger.Info("Open 'Content Clean Up' Settings and apply filter");

            Table output = workSpaceTab.GetResult();
            logger.Info("Fetch Results");

            if (output.Rows.Count >= 1)
            {
                output.Rows[1].Cells[3].RightClick();
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Content Detail");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.ReviewContent);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Review Content");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.DocumentOption);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Document Option");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Notes);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Notes");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Contacts);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Contacts");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.CaseCode_ClientName);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for CaseCode(s)/ClientName(s)");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.Terms);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for Terms");
                workSpaceTab.ClickContentWorkpanel(ContentWorkPanel.KS);
                Assert.IsFalse(workSpaceTab.ChkIfNoRecordFound());
                logger.Info("Verified some details is there for KS");
            }
            else logger.Info("No result found for criteria but system didn't throw any error");
        }
    }
}


    




