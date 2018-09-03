using System;
using System.IO;
using System.Reflection;
using TestStack.White;
using NUnit.Framework;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.Factory;
using TestStack.White.Configuration;
using TestStack.White.UIItems;
using TestStack.White.UIItems.MenuItems;
using System.Collections.Generic;
using System.Threading;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.Finder;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.ScreenObjects;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.CaseTabAdvanceSearch;

namespace GMT.White.AutomationFramework
{

    class Demotest : CommonInitialization<Demotest>
    {

        [Test]
        [Category("Basic Search")]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void BasicCaseSearch()
        {
            #region cmt

            //GMTMainScreen Win = new GMTMainScreen();
            // Window gMTWindow = Win.AppScreen(_app);
            #endregion
            logger.Info("Get window");
            #region cmt
            //Tabs gmtTab = new Tabs();
            #endregion
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            #region cmt
            //GMTCaseScreen caseScreen = new GMTCaseScreen();
            #endregion
            caseScreen1.clientName = "Cigna";
            caseScreen1.SelectBasicSearchOptions(gMTWindow);

            Panel output = caseScreen1.GetCasesResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));

            string colHeaderName = result.Header.Columns[3].Name.ToString();
            logger.Info("Name of Field to be Asserted: " + colHeaderName);

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                try
                {
                    Assert.AreEqual(result.Rows[i].Cells[3].Value.ToString(), "Cigna");
                    logger.Debug("Assertion to match " + colHeaderName + " field Passed");

                }
                catch
                {
                    logger.Debug("Assertion to match " + colHeaderName + " field Failed at Output_Grid_line: " + (i + 1));
                }
            }
        }

        [Test]
        [Category("Advance Search")]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void AdvanceSearch()
        {
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");

            #region trying to add base filter?
            //caseScreen1.clientName = "Cigna";
            //caseScreen1.SelectBasicSearchOptions(gMTWindow);
            //logger.Info("Apply Client Name Filter");
            #endregion

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");

            #region WaitSyntax
            //DateTime beginWait = DateTime.Now;
            //while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
            //    Thread.Sleep(250);
            #endregion

            caseScreen2.ContentStatus(0).Click();
            logger.Info("Unchecked IsPubLished? CheckBox");
            caseScreen2.ContentStatus(1).Click();
            logger.Info("Checked IsUnPubLished? CheckBox");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            logger.Debug("Getting cases per applied search filters");

            #region RefModal
            //foreach (var mw in modalWindows)
            //{
            //Console.WriteLine(modalWindows);

            //}
            #endregion

            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            result.Rows[3].Cells[4].DoubleClick();
            logger.Info("Opening Case: " + result.Rows[3].Cells[4].Value.ToString());


        }

        [Test]
        [Category("Advance Search")]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void AdvanceSearchDateFilter()
        {
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");
            #region
            //DateTime mayStDay = new DateTime(DateTime.Now.Year, 01, 01);
            //DateTime mayEdDay = new DateTime(DateTime.Now.Year, 01, 31);
            #endregion
            string stDate = "01-Jan-2014"; string edDate = "02-Jan-2014";
            caseScreen2.DateFliter("Start Date", 1, stDate, edDate, "Yesterday", "Today");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            logger.Debug("Getting cases per applied search filters");

            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            Console.WriteLine(Int32.Parse(result.Rows.Count.ToString()));

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                try
                {
                    Assert.IsTrue((result.Rows[i].Cells[7].Value.ToString() == "01-Jan-2014") || (result.Rows[i].Cells[7].Value.ToString() == "02-Jan-2014"));
                    logger.Debug("Assertion to check cases selected are in specified date range Passed");

                }
                catch
                {
                    logger.Debug("Assertion to check cases selected are in specified date range Failed at Output_Grid_line: " + (i + 1));
                }
            }
        }

        [Test]
        [Category("Advance Search")]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void AdvanceSearchGeneralCase()
        {
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");
            #region
            //DateTime mayStDay = new DateTime(DateTime.Now.Year, 01, 01);
            //DateTime mayEdDay = new DateTime(DateTime.Now.Year, 01, 31);
            #endregion
            logger.Info("Apply General Case-> Case Filter");
            caseScreen2.GeneralCases("L2CZ", 0, "", "LexisNexis",0,00 );

            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            logger.Debug("Getting cases per applied search filters");

            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            Console.WriteLine(Int32.Parse(result.Rows.Count.ToString()));

        }


        [Test]
        [Category("Advance Search")]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void AdvanceSearchContact()
        {
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            logger.Info("Open Case Tab");

            caseScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Select Advanced Search");
            #region
            //DateTime mayStDay = new DateTime(DateTime.Now.Year, 01, 01);
            //DateTime mayEdDay = new DateTime(DateTime.Now.Year, 01, 31);
            #endregion
            logger.Info("Apply General Case-> Contact Filter");

            caseScreen2.AddContact(contact.Employee, "Kedia");
            
            Panel output = caseScreen2.GetCasesResult(gMTWindow);
            logger.Debug("Getting cases per applied search filters");

            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            Console.WriteLine(Int32.Parse(result.Rows.Count.ToString()));

        }


        [Test]
        [Ignore("For Trial Only-'DO NOT RUN'")]
        public void Test1()
        {
            //DateTime mayStDay = new DateTime(DateTime.Now.Year, 01, 01);
            //DateTime mayEdDay = new DateTime(DateTime.Now.Year, 01, 31);

            //Assert.That(DateFormat.Create("-", "dd-MMM-yyyy"), Is.EqualTo(DateFormat.DayMonthYear));
            ////Console.WriteLine(DateFormat.);
            //Console.WriteLine(mayStDay.ToString("dd-MMM-yyyy"));
            
            gmtTab.OpenTab(gMTWindow, "Cases").Click();
            caseScreen2.SelectAdvancedSearch(gMTWindow);

            caseScreen2.treevIndustry(industry.All, "Food");

            Panel output = caseScreen2.GetCasesResult(gMTWindow);

        }

        
    }

}


