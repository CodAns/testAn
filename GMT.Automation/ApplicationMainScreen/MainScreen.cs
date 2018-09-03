using TestStack.White.UIItems.WindowItems;
using TestStack.White;


namespace GMT.White.AutomationFramework
{
    class MainScreen
    {

        public MainScreen()
        { }

        public Window AppScreen(Application _App)
        {
            Window gMTWindow = _App.GetWindow("GXC Management - Version: 1.8.69.0");
            return gMTWindow;
        }

    }
}


