using System.ComponentModel;
using System.Xml.Linq;
using DataSetUiTest;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class DataConsole 
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    //http://197.255.51.104:9008
    //http://197.255.51.104:9035
    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new LoginMain();

            var data = new DataConsole();
       
            bool login = loginObj.LoginSuccess(driver);

            //if (login)
            //{
            //    Sleep(3000);
            //    ClickDataSet(driver);
            //    Sleep(3000);
            //    ClickSector(driver);
            //    Sleep(3000);

            //    CreateNewDataSectorSuccess(driver);

            //    ClickNewRequest(driver);
            //    Sleep(2000);
            //    SelectCheckBoxes(driver);

            //    Sleep(2000);

            //    data.RequestInfBox(driver);
            //}

            //ClickDashBorad(driver);
            //Sleep(3000);
            //ClickDataCatalogCard(driver);
            //Sleep(3000);
            //ClickCategoryCard(driver);
            //ClickNewDataCategoryButton(driver);
            //ClickNewRequest(driver);
            //SelectCheckBoxes(driver);
            //data.CategoryRequestInfBox(driver);
            //ClickDashBorad(driver);

            //ClickDataSet(driver);
            //ClickTableCard(driver);
            //TableCatalogueSelectorPopUp(driver);
            //TableCreateNewPopUp(driver);


			ClickDataSet(driver);
            ClickIndicators(driver);
            IndicatorCataloguePopUp(driver);
            CreateNewDataIndicatorPopUp(driver);

		}

    }

    public static void ClickDashBorad(IWebDriver driver)
    {
        try
        {
            var  dashBoard = driver.FindElement(By.LinkText("Dashboard"));

            dashBoard.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void ClickDataSet(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Datasets"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }


    //Or This  
    public static bool ClickCategoryCategoryCard(IWebDriver driver)
    {
        try
        {
            var catCard = driver.FindElement(By.CssSelector("div.col-md-6.col-lg-3 a[data-modal].card"));
            catCard.Click();
            Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #region Sector creation

    public static void ClickSector(IWebDriver driver)
    {
        try
        {
            var dataSetLinkSec = driver.FindElement(By.LinkText("Sectors"));

            dataSetLinkSec.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static string CreateNewDataSectorSuccess(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var loginVal = jsonFileReader.ReadJsonFileCreateSector();

            var createSec = new CreateDataSector(driver);

            createSec.ClickNew();

            Sleep(2000);

            createSec.EnterNameAndTitle(loginVal.SectorField.Name, loginVal.SectorField.Title);

            createSec.ClickSubmit();

            Sleep(3000);

            var teaxVal = createSec.textMsgRes.Text;

            Sleep(3000);

            createSec.ClickOk();

            return teaxVal;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.StackTrace} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }

    public static bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();

            Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public static string SelectCheckBoxes(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var createSec = new NewRequest(driver);

            for (int i = 1; i < createSec.rows.Count; i++)
            {
                IWebElement checkbox = createSec.rows[i].FindElement(By.Name("SelItemIds"));

                // Check the checkbox
                checkbox.Click();
            }

            Sleep(2000);
            return createSec.rows.Count.ToString();
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }

    public bool RequestInfBox(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();

            var createSec = new NewRequest(driver);

            var RequestInforVal = jsonFileReader.ReadJsonFileForSelectCheckBoxesProcessNewRequest();

            Sleep(3000);

            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);

            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

            if (createSec.rows.Count > 10)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);

            }


            //or Use this

            if (!(bool)((IJavaScriptExecutor)driver).ExecuteScript(
    "var elem = arguments[0],                 " +
    "  box = elem.getBoundingClientRect(),    " +
    "  cx = box.left + box.width / 2,         " +
    "  cy = box.top + box.height / 2,         " +
    "  e = document.elementFromPoint(cx, cy); " +
    "for (; e; e = e.parentElement) {         " +
    "  if (e === elem)                        " +
    "    return true;                         " +
    "}                                        " +
    "return false;                            ", button))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            }

            Sleep(3000);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));

            //The Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Sleep(4000);

            button.Click();

            Sleep(3000);

            createSec.EnterRequestInfo(RequestInforVal.RequestInformation.Title, RequestInforVal.RequestInformation.Reason);

            Sleep(3000);
            createSec.ClickSubmit();


			//var waito = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			////var clickOk = wait.Until(d => d.FindElement(By.CssSelector("button.confirm")));
			//var clickOk = wait.Until(d => createSec.btnClickOk); 
			Sleep(4000);
            createSec.ClickOk(); 

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }


	public bool CategoryRequestInfBox(IWebDriver driver)
	{
		try
		{
			JsonFileReader jsonFileReader = new();

			var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();

			var createSec = new NewRequest(driver);

			var RequestInforVal = jsonFileReader.ReadJsonFileForSelectCheckBoxesProcessCatNewRequest();

			Sleep(3000);

			IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
			((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);

			IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

			if (createSec.rows.Count > 10)
			{
				((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);

			}


			//or Use this

			if (!(bool)((IJavaScriptExecutor)driver).ExecuteScript(
	"var elem = arguments[0],                 " +
	"  box = elem.getBoundingClientRect(),    " +
	"  cx = box.left + box.width / 2,         " +
	"  cy = box.top + box.height / 2,         " +
	"  e = document.elementFromPoint(cx, cy); " +
	"for (; e; e = e.parentElement) {         " +
	"  if (e === elem)                        " +
	"    return true;                         " +
	"}                                        " +
	"return false;                            ", button))
			{
				((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
			}

			Sleep(3000);

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));

			//The Sleep is Inportant so the Pop-Div is loaded to the  DOM
			Sleep(4000);

			button.Click();

			Sleep(3000);

			createSec.EnterRequestInfo(RequestInforVal.CatRequestInformation.Title, RequestInforVal.CatRequestInformation.Reason);

			Sleep(2000);

			createSec.ClickSubmit();

			Sleep(3000);

			var waitx = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLinks = waitx.Until(d => d.FindElement(By.CssSelector("button.confirm")));

			dataSetLinks.Click();
			//createSec.ClickOk();

			return true;


		}

		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
			return false;
		}
	}


	public static void ClickProcessButtonOnSelectedDataSet(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static string CreateNewDataSectorFailed(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var loginVal = jsonFileReader.ReadJsonFileCreateSectorFailed();

            var createSec = new CreateDataSector(driver);

            createSec.ClickNew();
            var name = "";
            var title = "";

            if (string.IsNullOrEmpty(loginVal.SectorFieldEmpty?.Name))
            {
                name = "";
            }
            if (string.IsNullOrEmpty(loginVal.SectorFieldEmpty?.Title) || string.IsNullOrWhiteSpace(loginVal.SectorFieldEmpty?.Title))
            {
                title = "";
            }

            Sleep(2000);

            createSec.EnterNameAndTitle(name, title);

            createSec.ClickSubmit();

            var teaxVal = createSec.textMsgRes.Text;

            Console.WriteLine(teaxVal);
            Sleep(2000);
            createSec.ClickOk();

            Sleep(2000);
            createSec.ClickClose();

            return teaxVal;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return string.Empty;
        }
    }


    #endregion

    #region Data Category

 

    //This is used when on the dashboard
    public static void ClickDataCatalogCard(IWebDriver driver)
    {
        try
        {
            var cartCard = driver.FindElement(By.LinkText("Data Catalogues."));

            cartCard.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void ClickCategoryCard(IWebDriver driver)
    {
        try
        {
            Sleep(3000);
            var cartCard = driver.FindElement(By.LinkText("Categories"));
            Sleep(3000);
            cartCard.Click();
            Sleep(5000);

            var catSec = new CategorySector(driver);

            SelectElement dropdown = new SelectElement(catSec.dropDownBox);


            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForSelectOptionCatalogSelector();
            // Select by value

            dropdown.SelectByText(retVal.CatalogueSelector?.OptionToSelect);

            Sleep(3000);
            //// Select by index (0-based)
            //dropdown.SelectByIndex(2);
            catSec.ClickContinue();
            // Select by visible text
            //dropdown.SelectByText("Option Text");

            Sleep(3000);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickNewDataCategoryButton(IWebDriver driver)
    {
        try
        {
            var newDataCategoryButton = driver.FindElement(By.CssSelector("a.item-button[href*='/dataset/categories/add']"));

            Sleep(3000);
            newDataCategoryButton.Click();
            Sleep(2000);

            var catSec = new CategorySector(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForEnterNewDataCategory();

            catSec.EnterDataCategory(retVal.DataCategory.Name, retVal.DataCategory.Title);

            Sleep(3000);

            catSec.ClickSubmit();

            Sleep(5000);

            catSec.ClickOk();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public static bool ClickNewRequestDataCategoryButton(IWebDriver driver)
    {
        try
        {
            ClickNewRequest(driver);

            Sleep(4000);

            SelectCheckBoxes(driver);

            var catSec = new CategorySector(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForEnterNewDataCategory();

            catSec.EnterDataCategory(retVal.DataCategory.Name, retVal.DataCategory.Title);

            Sleep(2000);

            catSec.ClickSubmit();

            Sleep(3000);

            catSec.ClickOk();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #endregion

    #region Tables

    public static void ClickTableCard(IWebDriver driver)
    {
        try
        {
            var tableCard = driver.FindElement(By.LinkText("Tables"));

            tableCard.Click();

            Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void TableCatalogueSelectorPopUp(IWebDriver driver)
    {
        try
        {
            Sleep(1000);
            var table = new Tables(driver);



            SelectElement dropdown = new SelectElement(table.dropDownCascadeSecor);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            // Select by value

            dropdown.SelectByText(retVal.TableDataSelector?.OptionToSelect);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


            SelectElement dropdownDataCat = new SelectElement(table.dropDownCat);

            var counting = dropdownDataCat.Options.Count;

            if (dropdownDataCat.Options.Count <= 1)
            {
                Console.WriteLine("Empty List");
            }

            dropdownDataCat.SelectByIndex(1);

            table.ClickContinue();

            Sleep(3000);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void TableCreateNewPopUp(IWebDriver driver)
    {
        try
        {
            Sleep(1000);
            var table = new Tables(driver);

            table.ClickNew();

            Sleep(3000);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileForNewDataTable();

            // Select by value
            table.EnterTableInfoData(retVal.TableNewData.Name, retVal.TableNewData.Title, retVal.TableNewData.Description);

            SelectElement dropdown = new SelectElement(table.dropDownFeq);

            SelectElement dropUn = new SelectElement(table.dropDownUnit);

            var dropFreq = jsonFileReader.ReadJsonFileForTableFrequency();
            // Select by value

            dropdown.SelectByText(dropFreq.TableFrequency.OptionToSelect);

            var dropUnit = jsonFileReader.ReadJsonFileForTableUnit();

            dropUn.SelectByIndex(dropUnit.TableUnit.OptionToSelect);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var switchBoxControl = jsonFileReader.ReadJsonFileNewDataTableSettings();

            var txtBoxVal = jsonFileReader.ReadJsonFileDataTableTxt();

            string textData1;
            string textData2;
            string textData3;
            string textData4;
            string textData5;

         
            if (switchBoxControl.DataTableSetting.Data1 == true)
            {
                textData1 = txtBoxVal.DataTableTxtVal.Txt1;

                string readonlyAttribute = table.txtDataLab1.GetAttribute("readonly");


                textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                table.txtDataLab1.SendKeys(textData1);

                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox1;

                    Sleep(3000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);

                    textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                    table.txtDataLab1.SendKeys(textData1);

                }
            }

            if (switchBoxControl.DataTableSetting.Data2 == true)
            {
                textData2 = txtBoxVal.DataTableTxtVal.Txt2;

                string readonlyAttribute = table.txtDataLab2.GetAttribute("readonly");

                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox2;
                    
                    Sleep(3000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);

                    //table.Enable(switBox);
                    Sleep(2000);
                    textData2 = txtBoxVal.DataTableTxtVal.Txt2;

                    table.txtDataLab2.SendKeys(textData2);
                   
                }
            }

            if (switchBoxControl.DataTableSetting.Data3 == true)
            {
                textData3 = txtBoxVal.DataTableTxtVal.Txt3;

                string readonlyAttribute = table.txtDataLab3.GetAttribute("readonly");

                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox3;

                    Sleep(3000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);

                    textData3 = txtBoxVal.DataTableTxtVal.Txt3;
                    table.txtDataLab3.SendKeys(textData3);

                }
            }

            if (switchBoxControl.DataTableSetting.Data4 == true)
            {
                textData4 = txtBoxVal.DataTableTxtVal.Txt4;

                string readonlyAttribute = table.txtDataLab4.GetAttribute("readonly");

                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox4;
                    Sleep(3000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);

                    textData4 = txtBoxVal.DataTableTxtVal.Txt4;
                    table.txtDataLab4.SendKeys(textData4);

                }
            }

            if (switchBoxControl.DataTableSetting.Data5 == true)
            {
                textData5 = txtBoxVal.DataTableTxtVal.Txt5;

                string readonlyAttribute = table.txtDataLab5.GetAttribute("readonly");

                if (readonlyAttribute != null)
                {
                    var switBox = table.checkBox5;

                    Sleep(3000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);

                    textData5 = txtBoxVal.DataTableTxtVal.Txt5;
                    table.txtDataLab5.SendKeys(textData5);

                }
            }

            Sleep(3000);

            table.ClickSubmit();

			Sleep(4000);

            table.ClickOk();


		}
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }



    #endregion

    #region Indicators
    public static void ClickIndicators(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Indicators"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void IndicatorCataloguePopUp(IWebDriver driver)
    {
        try
        {
            var inidi = new Indicator(driver);

            JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileDataIndicator();

            Sleep(2000);
            //Access the Sector
            SelectElement dropdownSec = new SelectElement(inidi.dropDownCascadeSecor);
            dropdownSec.SelectByIndex(retVal.IndicatorDataSelector.SectorIndex);

			Sleep(2000);
			//Access the Category
			SelectElement dropdownCat = new SelectElement(inidi.dropDownCat);
            dropdownCat.SelectByIndex(retVal.IndicatorDataSelector.CategoryIndex);

			Sleep(2000);
			//Access the Category
			SelectElement dropdownTab = new SelectElement(inidi.dropDownTable);
            dropdownTab.SelectByIndex(retVal.IndicatorDataSelector.TableIndex);

            Sleep(3000);

            inidi.ClickContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static void CreateNewDataIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            var inidi = new Indicator(driver);

            Sleep(2000);

            inidi.ClickNew();

			JsonFileReader jsonFileReader = new();

            var retVal = jsonFileReader.ReadJsonFileNewDataIndicator();

            Sleep(4000);

            //SelectElement dropdown = new SelectElement(inidi.IndiComboTree);

            //dropdown.SelectByIndex(retVal.NewDataIndicator.TopLevelIndi);

            string textName = retVal.NewDataIndicator.Name;
            inidi.txtBoxName.SendKeys(textName);

            string textTit = retVal.NewDataIndicator.Title;
            inidi.txtBoxTitle.SendKeys(textTit);

            if (retVal.NewDataIndicator.EmboldenIndicatorTitle == true)
            {

				Sleep(3000);
				var switBox = inidi.checkBoxEmph;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
            }

            int dispOrd = retVal.NewDataIndicator.DisplayOrder;
            inidi.displayOrder.Clear();
			inidi.displayOrder.SendKeys(dispOrd.ToString());

            if (retVal.NewDataIndicator.DisplayInChart == true)
            {
                var switBox = inidi.DisplayInChart;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                Sleep(3000);
                var texttoDispay = retVal.NewDataIndicator.TitleToDisplay;
                inidi.txtGrapTit.SendKeys(texttoDispay);
            }
            
                Sleep(3000);
                inidi.ClickSubmit();
                Sleep(3000);
			    inidi.ClickOk();

		}
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }


    #endregion



    private static void Sleep(int time)
    {
        Thread.Sleep(time);
    }


}