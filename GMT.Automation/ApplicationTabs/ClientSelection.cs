using System;
using System.Threading;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.TreeItems;

namespace GMT.White.AutomationFramework.Client
{
     class  ClientSelection 
    {

        public  ClientSelection()
        {
            
        }

        public static void ClientName(Window clientCompanyWin, string clientCompany)
        {
            clientCompanyWin.Get(SearchCriteria.ByAutomationId("txtClientName")).SetValue(clientCompany);

            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
               Thread.Sleep(250);

            clientCompanyWin.Get(SearchCriteria.ByAutomationId("btnAdd")).Click();

            Table selectClientCompany = clientCompanyWin.Get<Table>(SearchCriteria.ByAutomationId("dgDisplaySelection"));

            if (selectClientCompany.Rows.Count.ToString() == "0")
            {
                throw new System.ArgumentException("Invalid Client Name entered");
            }

            clientCompanyWin.Get<Button>(SearchCriteria.ByAutomationId("btnSave")).Click();

            //Window SelectClient = advanceSearch.Get<Window>(SearchCriteria.ByAutomationId("frmSelectClient"));

            

        }

    }
}


        

    

        

    





