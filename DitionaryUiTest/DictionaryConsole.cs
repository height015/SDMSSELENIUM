﻿using DictionaryUiTest;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class DictionaryConsole
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static void Main(string[] args)
    {
        using (var driver = new ChromeDriver())
        {
            var loginObj = new LoginMain();

            var data = new DictionaryConsole();

            bool login = loginObj.LoginSuccess(driver);

            if (login)
            {
                Sleep(3000);
                ClickDictionary(driver);
                Sleep(3000);
                ClickFrequency(driver);
                Sleep(3000);
                ClickFrequencyNewRequest(driver);
                Sleep(3000);
                data.DataEntryFrequency(driver);
                Sleep(3000);


				ClickDictionary(driver);
				Sleep(3000);
				ClickUnit(driver);
				Sleep(3000);
				ClickUnitNewRequest(driver);
				Sleep(3000);
				data.UnitDataEntry(driver);
				Sleep(3000);



				ClickDictionary(driver);
				Sleep(3000);
				ClickDataEntity(driver);
				Sleep(3000);
				ClickDataEntityNewRequest(driver);
				Sleep(3000);
				data.DataEntityEntry(driver);
				Sleep(3000);


				ClickDictionary(driver);
				Sleep(3000);
				ClickDataSource(driver);
				Sleep(3000);
				ClickDataSourceNewRequest(driver);
				Sleep(3000);
				data.DataSourceEntry(driver);
				Sleep(3000);
                ClickNewRequest(driver);
                Sleep(3000);
                ClickRequestType(driver);



			}


		}

    }

    public static void ClickDictionary(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Dictionaries"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
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
	public static string ClickRequestType(IWebDriver driver)
	{
		try
		{
			JsonFileReader lx = new();

			var retVals = lx.ReadJsonFileSelectCheckBoxes();

			var create = new Units(driver);

			var reqType = retVals.CheckBoxNumbers.RequestType;

			Sleep(3000);

			IWebElement btn;

			switch (reqType)
			{
				case (int)RequestType.AuthorizationRequest:
					btn = driver.FindElement(By.LinkText("Authorize"));
					btn.Click();
					break;
				case (int)RequestType.UnAuthorization:
					btn = driver.FindElement(By.LinkText("Unauthorize"));
					btn.Click();
					break;
				case (int)RequestType.ArchiveRequest:
					btn = driver.FindElement(By.LinkText("Archive"));
					btn.Click();
					break;
				case (int)RequestType.UnarchiveRequest:
					btn = driver.FindElement(By.LinkText("Unarchive"));
					btn.Click();
					break;
				case (int)RequestType.ModificationRequest:
					btn = driver.FindElement(By.LinkText("Modify"));
					btn.Click();
					break;
				case (int)RequestType.PublicationRequest:
					btn = driver.FindElement(By.LinkText("Publish"));
					btn.Click();
					break;
				case (int)RequestType.UnpublicationRequest:
					btn = driver.FindElement(By.LinkText("Unpublish"));
					btn.Click();
					break;
				default:
					btn = driver.FindElement(By.LinkText("Authorize"));
					btn.Click();
					break;
			}

			Sleep(7000);
			var table = create.table;
			var rowCount = 0;
			if (table != null)
			{
				var rows = create.rows;
				var rowIndexes = retVals.CheckBoxNumbers.GetIndexArray();

				rowCount = rows.Count() - 1;

				foreach (var item in rowIndexes)
				{

					IWebElement checkbox = create.rows[item].FindElement(By.Name("SelItemIds"));
					checkbox.Click();
					rowCount--;
					if (rowCount <= 0)
					{
						break;
					}

				}
			}
			Sleep(3000);

			return rowCount.ToString();
		}

		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
			return string.Empty;
		}
	}

	public static bool CreateNewReqGenericPopUp(IWebDriver driver)
	{
		try
		{
			JsonFileReader jsonFileReader = new();

			var genericVal = new Units(driver);

			var RequestInforVal = jsonFileReader.ReadJsonFileForNewRequestIndicator();
			Sleep(3000);

			IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
			((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
			IWebElement button = driver.FindElement(By.Id("btnReqSelect"));

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

			genericVal.EnterRequestInfo(RequestInforVal.IndicatorRequestData.Title, RequestInforVal.IndicatorRequestData.Reason);
			Sleep(2000);

			genericVal.ClickSave();

			Sleep(8000);

			genericVal.ClickOk();

			return true;
		}

		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
			return false;
		}
	}



	#region Frequency
	public static void ClickFrequency(IWebDriver driver)
    {
        try
        {
            var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            freqLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickFrequencyNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/frequencies/add']"));
            dataSetLinkNewReq.Click();
            Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool DataEntryFrequency(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var freqVal = jsonFileReader.ReadJsonFileDataFrequency();

            var createSec = new Frequency(driver);

            Sleep(3000);

            createSec.NewDataFrequencyEntery(freqVal.DataFrequency.Name, freqVal.DataFrequency.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

            createSec.ClickOk();

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    } 
    #endregion

    #region Unit
    public static void ClickUnit(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Units"));
            dataSetLink.Click();

            Sleep(3000);

            //var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            //freqLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickUnitNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
            dataSetLinkNewReq.Click();
            Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool UnitDataEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();

            var freqVal = jsonFileReader.ReadJsonFileDataUnit();

            var createSec = new Units(driver);

            Sleep(3000);

            createSec.NewDataUnitEntery(freqVal.DataUnit.Name, freqVal.DataUnit.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

            createSec.ClickOk();

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #endregion

    #region Data Entities
    public static void ClickDataEntity(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Entities"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickDataEntityNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/data-entities/add']"));
            dataSetLinkNewReq.Click();
            Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public bool DataEntityEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var freqVal = jsonFileReader.ReadJsonFileDataEntity();
            var createSec = new DictionaryUiTest.DataEntities(driver);

            Sleep(3000);

            createSec.NewDatadataEntityEntry(freqVal.DataEntities.Name, freqVal.DataEntities.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

            createSec.ClickOk();

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #endregion

    #region Data Source
    public static void ClickDataSource(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Sources"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public static bool ClickDataSourceNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='dictionary/data-sources/add']"));
            dataSetLinkNewReq.Click();
            Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    public bool DataSourceEntry(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var freqVal = jsonFileReader.ReadJsonFileDataSource();
            var createSec = new DataSources(driver);

            Sleep(3000);

            createSec.NewDatadataSourceEntry(freqVal.DataSource.Name, freqVal.DataSource.ShortName);

            Sleep(3000);

            createSec.ClickSubmit();

            Sleep(3000);

            createSec.ClickOk();

            return true;

        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    #endregion
    



    private static void Sleep(int timeVal)
    {
        Thread.Sleep(timeVal);
    }
}

