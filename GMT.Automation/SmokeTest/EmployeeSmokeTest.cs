using System;
using NUnit.Framework;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TableItems;
using NUnit.Framework.Internal;
using static GMT.White.AutomationFramework.EmployeeTabBasicSearch;
using static GMT.White.AutomationFramework.EmployeeTabAdvanceSearch;

namespace GMT.White.AutomationFramework
{

    class EmployeeSmokeTest : CommonInitialization<EmployeeSmokeTest>
    {

        [Test]
        [Category("EmployeeTab")]
        [Category("Basic Search")]

        public void SearchEmpByLastName()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis of Last Name
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Get Employee Tab");

            empScreen1.empBasicFilter(gMTWindow, basicFilter.LastName, "aggarwal");
            empScreen1.empBasicFilter(gMTWindow, basicFilter.IncludeAlumni, "Y");
            logger.Info("Apply LAst Name Basic Filter and Include Alum");

            Panel output = empScreen1.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Search Result per applied filter");

            string colHeaderName = result.Header.Columns[2].Name.ToString();
            logger.Info("Name of Field to be Asserted: " + colHeaderName);

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("aggarwal"));
                logger.Debug("Assertion to match " + colHeaderName + " field Passed");
            }
        }

        [Test]
        [Category("EmployeeTab")]
        [Category("Basic Search")]

        public void SearchEmpByEmpCode()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis of ECode
            /////  The Filter is applied on basic filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Get Employee Tab");

            empScreen1.empBasicFilter(gMTWindow, basicFilter.EmployeeCode, "40407");
            empScreen1.empBasicFilter(gMTWindow, basicFilter.IncludeAlumni, "Y");
            logger.Info("Apply LAst Name Basic Filter and Include Alum");

            Panel output = empScreen1.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Search Result per applied filter");

            string colHeaderName = result.Header.Columns[2].Name.ToString();
            logger.Info("Name of Field to be Asserted: " + colHeaderName);

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("kedia, anshul"));
                logger.Info("Assert if correct Employee is selected against passed ECode");
                Assert.IsTrue(result.Rows[i].Cells[5].Value.ToString().ToLower().Contains("40407"));
                logger.Debug("Assertion to match " + colHeaderName + " field Passed");
            }
        }

        //[Test]
        //[Category("EmployeeTab")]
        //[Category("Advance Search")]
        //public void Searchbas()
        //{
        //    logger.Info("Get window");
        //    gmtTab.OpenTab(gMTWindow, "Employees").Click();
        //    logger.Info("Get Employee Tab");

        //    empScreen1.empBasicFilter(gMTWindow,basicFilter.AllEmployee, "Y");   // Enter 'Y' to opt all
        //    logger.Info("All Employee selected");
        //    empScreen1.empBasicFilter(gMTWindow, basicFilter.IncludeAlumni, "Y");   // Enter 'Y' to opt all
        //    logger.Info("Select to include Alum");

        //    Panel output = empScreen1.GetEmployeeResult(gMTWindow);
        //    var toolBar = output.Get(SearchCriteria.ByAutomationId("tlbObjectExplorer"));
        //    List<IUIItem> resultCount = toolBar.GetMultiple(SearchCriteria.ByNativeProperty(AutomationElement.LocalizedControlTypeProperty, "text")).ToList<IUIItem>();
        //    string Count1 = string.Empty;
        //    foreach (IUIItem ui in resultCount)
        //    {  Count1 = resultCount.Find(x => x.Name.Contains("Results")).Name.ToString(); }
        //    logger.Info("Find All Employee Count");

        //    //string acCount = gMTWindow.Get(SearchCriteria.ByAutomationId("lblTotalComponents")).Name.ToString();
        //    //Assert.AreEqual((acCount + " Results"), Count1);
        //    //logger.Info("Asserted that the total Emp count is same per EmployeeStat");

        //    empScreen1.empBasicFilter(gMTWindow, basicFilter.IncludeAlumni, "Y");   // Enter 'Y' to opt all
        //    logger.Info("Select to Exclude Alum");
        //    output = empScreen1.GetEmployeeResult(gMTWindow);
        //    toolBar = output.Get(SearchCriteria.ByAutomationId("tlbObjectExplorer"));
        //    resultCount = toolBar.GetMultiple(SearchCriteria.ByNativeProperty(AutomationElement.LocalizedControlTypeProperty, "text")).ToList<IUIItem>();
        //    // string Count2 = resultCount.Find(x => x.Name.Contains("Results")).Name.ToString();
        //    string Count2 = string.Empty;
        //    foreach (IUIItem ui in resultCount)
        //    { Count2 = ui.Name.Contains("Results").ToString(); }
        //    logger.Info("Find All Employee without Alum Count");

        //    Console.WriteLine(Count1 + "  " + Count2);
        //    Assert.AreNotEqual(Count1, Count2);
        //    logger.Info("Asserted that the count is not same");

        //}

        [Test]
        [Category("EmployeeTab")]
        [Category("Advance Search")]
        public void SearchAdvanceChkAlum()
        {
            /////////////////////////////////
            /////  This Test will Filter out Alumni 
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Open Employee Tab");
            empScreen2.SelectAdvancedSearch(gMTWindow);

            empScreen2.employeeStatus(1).Click();            // Pass ) to get Active Emp and 1 for Alum
            empScreen2.employeeStatus(0).Click();
            logger.Info("Select EmpStatus= Alum");

            Panel output = empScreen2.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Data per selected filter");

            Assert.IsTrue(result.Visible);
            logger.Info("Result is attained per applied filter");

        }

        [Test]
        [Category("EmployeeTab")]
        [Category("Advance Search")]
        public void AdvSrchGeneralEmployee()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis of Last Name
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Open Employee Tab");
            empScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");
            
            empScreen2.GeneralEmployeeEmp(Employee.Employee, "Abdou");
            logger.Info("Apply Employee Emp Last Name Filter");

            Panel output = empScreen2.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Data per selected filter");

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("abdou, alex"));
                logger.Info("Assert if correct Employee is selected against passed LastName");
                Assert.IsTrue(result.Rows[i].Cells[5].Value.ToString().ToLower().Contains("01exa"));
                logger.Debug("Assertion to match Employee Code Passed");
            }
        }

        [Test]
        [Category("EmployeeTab")]
        [Category("Advance Search")]
        public void AdvSrchAffiliation()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis Industry/Enabler Affiliation
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Open Employee Tab");
            empScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            empScreen2.GeneralEmployeetv(GeneralEmployees.OfficeRegion, "New York");
            logger.Info("Select Office Region");
            empScreen2.GeneralEmployeetv(GeneralEmployees.LevelDeptt, "Principal");
            logger.Info("Apply Level/Deptt Filter");

            empScreen2.hasPracticeAreaAff().Click();
            empScreen2.affiliations(Affiliation.Industry, "Health", "OR", "And");
            empScreen2.affiliations(Affiliation.Enabler, "", "OR", "And");
            logger.Info("Apply Industry and Enabler Affiliation Filter");

            Panel output = empScreen2.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Data per selected filter");

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                if(result.Rows[i].Cells[5].Value.ToString().ToLower().Contains("22aad"))
                {
                    string c = result.Rows[i].Cells[2].Value.ToString().ToLower();  
                    Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("adler, amanda"));
                    logger.Info("Assert if correct Employee is selected against applied filter");
                    Assert.IsTrue(result.Rows[i].Cells[4].Value.ToString().ToLower().Contains("m"));
                    logger.Info("Assert if filtered employee are of Principal Level");
                }
                
            }
        }

        [Test]
        [Category("EmployeeTab")]
        [Category("Advance Search")]
        public void AdvSrchAskAbtMeTopic()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis 'AskMeTopic'
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Open Employee Tab");
            empScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            empScreen2.GeneralEmployeetv(GeneralEmployees.LevelDeptt, "Principal");
            logger.Info("Apply Level/Deptt Filter");

            empScreen2.hasAskMeAboutTopics().Click();
            empScreen2.askMeAboutTopic("Test", "digital",Operator.Or);
            logger.Info("Search for the Topic with keyword -'Test'");

            Panel output = empScreen2.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Data per selected filter");

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                    string c = result.Rows[i].Cells[2].Value.ToString().ToLower();
                    Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("lemaire, laurence"));
                    logger.Info("Assert if correct Employee is selected against applied filter");
                    Assert.IsTrue(result.Rows[i].Cells[5].Value.ToString().ToLower().Contains("01lae"));
                    logger.Debug("Assertion to match Employee Code Passed");
            }
        }

        [Test]
        [Category("EmployeeTab")]
        [Category("Advance Search")]
        public void AdvSrchAskAbtMeMapping()
        {
            /////////////////////////////////
            /////  This Test will Filter out Employee on basis AskAbtMapping
            /////  The Filter is applied on Advance filter field
            //////////////////////////////
            logger.Debug("STARTED RUNNING TEST CASE: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Get window");
            gmtTab.OpenTab(gMTWindow, "Employees").Click();
            logger.Info("Open Employee Tab");
            empScreen2.SelectAdvancedSearch(gMTWindow);
            logger.Info("Open Advance Search");

            empScreen2.hasAskMeAboutTopics().Click();
            empScreen2.askMeAboutMapping("case", "Case", Operator.Or);
            logger.Info("Apply Ask Me About Topic Filter");

            Panel output = empScreen2.GetEmployeeResult(gMTWindow);
            Table result = output.Get<Table>(SearchCriteria.ByAutomationId("dgObjectExplorer"));
            logger.Info("Get Data per selected filter");

            for (int i = 0; i < Int32.Parse(result.Rows.Count.ToString()); i++)
            {
                string c = result.Rows[i].Cells[2].Value.ToString().ToLower();
                Assert.IsTrue(result.Rows[i].Cells[2].Value.ToString().ToLower().Contains("tait, thomas"));
                logger.Info("Assert if correct Employee is selected against applied filter");
                Assert.IsTrue(result.Rows[i].Cells[5].Value.ToString().ToLower().Contains("15tmt"));
                logger.Debug("Assertion to match Employee Code Passed");
            }
        }
    }

}

            
    




