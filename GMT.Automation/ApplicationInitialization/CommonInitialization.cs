using log4net;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace GMT.White.AutomationFramework
{
    class CommonInitialization<T>
    {
        public Application _app;
        public static readonly ILog logger = LogManager.GetLogger(typeof(T));

        public  MainScreen Win = null;
        public Tabs gmtTab = null;
        public Window gMTWindow = null;
        public CaseTabBasicSearch caseScreen1 = null;
        public CaseTabAdvanceSearch caseScreen2 = null;
        public EmployeeTabBasicSearch empScreen1 = null;
        public EmployeeTabAdvanceSearch empScreen2 = null;
        public PublishingQueueExplorer pubQueExp = null;
        public SavedReportsTab savedReport = null;
        public InSightTabBasicSearch insightScreen1 = null;
        public InSightTabAdvanceSearch insightScreen2 = null;
        public ProposalTabBasicSearch proposalScreen1 = null;
        public ProposalTabAdvanceSearch proposalScreen2 = null;
        public WorkSpaceTab workSpaceTab = null;
        

        public CommonInitialization()
        {
            Logger.Setup();
        }

        public void Wait()
        {
            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                Thread.Sleep(250);
        }

        [SetUp]
        public void SetupTest()
        {
            logger.Debug("Configuring log4net");

            var applicationPath = TestContext.Parameters["ApplicationPath"].ToString();
            logger.Debug("Get Application Path");
            var _applicationLaunch = Application.Launch(applicationPath);
            logger.Debug("Launching GMT Application");
            _applicationLaunch.Process.WaitForExit();
           // Thread.Sleep(2000);
            logger.Debug("Wait to Get Application correct application instance");
            Process[] processes = Process.GetProcessesByName("GXCManagement");
            if (processes.Length == 0)
                _app = Application.Launch(applicationPath);
            else
                _app = Application.Attach("GXCManagement");
           // _app = Application.Attach("GXCManagement");
            logger.Debug("Get Application correct application instance");

            Win = new MainScreen();
            gmtTab = new Tabs();
            caseScreen1 = new CaseTabBasicSearch();
            caseScreen2 = new CaseTabAdvanceSearch();
            empScreen1 = new EmployeeTabBasicSearch();
            empScreen2 = new EmployeeTabAdvanceSearch();
            pubQueExp = new PublishingQueueExplorer();
            savedReport = new SavedReportsTab();
            insightScreen1 = new InSightTabBasicSearch();
            insightScreen2 = new InSightTabAdvanceSearch();
            proposalScreen1 = new ProposalTabBasicSearch();
            proposalScreen2 = new ProposalTabAdvanceSearch();
            workSpaceTab = new WorkSpaceTab();
            

            logger.Debug("Starting Test");
            gMTWindow = Win.AppScreen(_app);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                logger.Debug("Closing GMT Application");
                _app.Close();
            }
            catch (Exception ex)
            {
                logger.Error("Unable to Close GMT Application", ex);
            }

            logger.Debug("Application Closed");
        }
    }
}
