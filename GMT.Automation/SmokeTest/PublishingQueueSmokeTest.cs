using NUnit.Framework;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using System.Linq;
using System;
using static GMT.White.AutomationFramework.PublishingQueueExplorer;

namespace GMT.White.AutomationFramework
{

    class PublishingQueueSmokeTest : CommonInitialization<PublishingQueueSmokeTest>
    {

        [Test]
        [Category("PublishingQueue")]
        public void ChkForBlankUserName()
        {
            /////////////////////////////////
            /////  This Test will Filter out QUEUE search result if Username is blank
            //////////////////////////////

            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.UserName(" ");
            logger.Info("Remove UserName");

            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.GreaterOrEqual(outRowCount, 1);
            logger.Info("Output result set has " + outRowCount + " Rows");
            for(int i=0; i<outRowCount; i++)
            {
                for (int y = 0; y < outColCount; y++)
                {
                    string colVal = searchResult.Rows[i].Cells[y].Value.ToString();
                    Assert.IsNotEmpty(colVal);
                    
                }
            }
            logger.Info("Asserted result table has no null cell value");
        }

        [Test]
        [Category("PublishingQueue")]
        public void ChkForSomeUserName()
        { /////////////////////////////////
            /////  This Test will Filter out QUEUE search result if Username is "Jain"
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.UserName("Jain");
            logger.Info("Apply UserName= 'Jain'");

            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.GreaterOrEqual(outRowCount, 1);
            logger.Info("Output result set has " + outRowCount + " Rows");
            for (int i = 0; i < outRowCount; i++)
            {
                for (int y = 0; y < outColCount; y++)
                {
                    string colVal = searchResult.Rows[i].Cells[y].Value.ToString();
                    Assert.IsNotEmpty(colVal);

                }
            }
            logger.Info("Asserted result table has no null cell value");
        }

        [Test]
        [Category("PublishingQueue")]
        public void ChkQueResultwithDateFilter()
        { /////////////////////////////////
          /////  This Test will Filter out QUEUE search result within applied date filter
          //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.UserName(" ");
            logger.Info("Remove UserName");
            pubQueExp.DateFilter(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-7));
            logger.Info("Apply Date Filter");

            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.GreaterOrEqual(outRowCount, 1);
            logger.Info("Output result set has " + outRowCount + " Rows");
            for (int i = 0; i < outRowCount; i++)
            {
                for (int y = 0; y < outColCount; y++)
                {
                    string colVal = searchResult.Rows[i].Cells[y].Value.ToString();
                    Assert.IsNotEmpty(colVal);

                }
            }
            logger.Info("Asserted result table has no null cell value");
        }

        [Test]
        [Category("PublishingQueue")]
        public void ChkQueResultUsingShowItems()
        { /////////////////////////////////
          /////  This Test will Filter out QUEUE search result post toggleing Show Items Checkboxes
          //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.DateFilter(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-7));
            logger.Info("Apply Date Filter");
            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowItemsInProgress);
            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowFailedItems);
            logger.Info("UnCheck Inprogress/Failed item checkbox");
            
            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.GreaterOrEqual(outRowCount, 1);
            logger.Info("Output result set has " + outRowCount + " Rows");
            for (int i = 0; i < outRowCount; i++)
            {
                for (int y = 0; y < outColCount; y++)
                {
                    string colVal = searchResult.Rows[i].Cells[y].Value.ToString();
                    Assert.IsNotEmpty(colVal);

                }
            }
            logger.Info("Asserted result table has no null cell value");
        }

        [Test]
        [Category("PublishingQueue")]

        public void ChkQueNoResultRemovingShowItems()
        { /////////////////////////////////
          /////  This Test will check out all show Item CheckBox and would verify no result is returned.
          //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.DateFilter(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-7));
            logger.Info("Apply Date Filter");

            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowItemsInProgress);
            pubQueExp.ChkIfNoResultFromQueSearch();
            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowFailedItems);
            pubQueExp.ChkIfNoResultFromQueSearch();
            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowItemsAwaitingProcessing);
            pubQueExp.ChkIfNoResultFromQueSearch();
            pubQueExp.ShowItemCheckBox(showItemsCheckBox.ShowCompletedItems);
            Assert.IsTrue(pubQueExp.ChkIfNoResultFromQueSearch());
            logger.Info("No Result return popup appears");

            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.AreEqual(0,outRowCount);
            logger.Info("Asserted no rows are returned ");
        }

        [Test]
        [Category("PublishingQueue")]
        [Ignore("Contain Execution Error")]
        public void ChkQueResultContentTypeFilter()
        { /////////////////////////////////
          /////  This Test will Filter out QUEUE search result within applied Content filter
          //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Publishing Queue").Click();
            pubQueExp.getWin(gMTWindow);
            logger.Info("Open Publishing Queue Tab");

            pubQueExp.DateFilter(DateTime.Today.AddDays(-14), DateTime.Today.AddDays(-7));
            logger.Info("Apply Date Filter");

            pubQueExp.ClickContentType(contentType.Employees);
            pubQueExp.ClickContentType(contentType.HomePages);
            pubQueExp.ClickContentType(contentType.Lists);
            pubQueExp.ClickContentType(contentType.PracticeAreaPages);
            pubQueExp.ClickContentType(contentType.Proposals);
            pubQueExp.ClickContentType(contentType.Taxonomies);
            logger.Info("Select Only Cases & Insights content type");

            Table searchResult = pubQueExp.outSearchQue();
            logger.Info("Get Publishing Queue based on applied search Filter");
            int outRowCount = searchResult.Rows.Count();
            int outColCount = searchResult.Header.Columns.Count();
            logger.Info("Get data row count to check if result is returned");

            Assert.GreaterOrEqual(outRowCount, 1);
            logger.Info("Output result set has " + outRowCount + " Rows");
            for (int i = 0; i < outRowCount; i++)
            {
                for (int y = 0; y < outColCount; y++)
                {
                    string colVal = searchResult.Rows[i].Cells[y].Value.ToString();
                    Assert.IsNotEmpty(colVal);

                }
            }
            logger.Info("Asserted result table has no null cell value");
        }
    }
}


    




