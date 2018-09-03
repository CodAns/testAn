using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using NUnit.Framework;

namespace GMT.White.AutomationFramework
{
    [TestFixture]
    class GMTMainScreenSample
    {

        public Application _app;
        public GMTMainScreenSample()
        { }

        [SetUp]
        public void Setup()
        {
            var applicationPath = @"T:\BainAppsTesting\GXCManagement\windows7\GXC Management.LNK";
            var _applicationLaunch = Application.Launch(applicationPath);
            _applicationLaunch.Process.WaitForExit();
            _app = Application.Attach("GXCManagement");
        }
        public Window AppWindow()
        {
            Setup();
            Window gMTWindow = _app.GetWindow("GXC Management - Version: 1.8.69.0");
            return gMTWindow;
        }

        [TearDown]

        public void TearDown()
        {
            _app.Close();
        }
    }
}

