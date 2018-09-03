using NUnit.Framework;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using System.Linq;
using System;
using static GMT.White.AutomationFramework.PublishingQueueExplorer;
using TestStack.White.UIItems.Finders;

namespace GMT.White.AutomationFramework
{

    class SavedReportsTest : CommonInitialization<SavedReportsTest>
    {
        [Test]
        [Category("SavedReport")]
        public void ChkCasesSavedReport()
        {
            /////////////////////////////////
            /////  This will search for Saved Report and run the same to verify some result is returned.
            /////  If there is no saved report this test case will throw exception.
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Saved Reports").Click();
            savedReport.getWin(gMTWindow);
            logger.Info("Open Saved Reports Tab");
            savedReport.getSavedReport();
            logger.Info("Find Saved Report for User");
            savedReport.runSavedReport();
            logger.Info("Run Saved view");
            Wait();
            Assert.IsNotNull(savedReport.GetOutputRowsCount(gMTWindow));
            logger.Debug("Assertion to check ran saved view returns result passed");
        }
    }
}


    




