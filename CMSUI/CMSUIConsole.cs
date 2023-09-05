using CMSUI;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class CMSUIConsole
{
	private static readonly string _URL = "http://197.255.51.104:9035";

	//http://197.255.51.104:9008
	//http://197.255.51.104:9035
	public static void Main(string[] args)
	{
		using (var driver = new ChromeDriver())
		{
			var loginObj = new LoginMain();

			var data = new CMSUIConsole();

			bool login = loginObj.LoginSuccess(driver);

			
			if (login)
			{
				//Analytics
				ClickCMS(driver);
				Sleep(3000);
				ClickAnalyticCard(driver);
				Sleep(3000);
				ClickNew(driver);
				Sleep(3000);
				PopUpDataSet(driver);
				Sleep(3000);
				ProcessDataSetSelector(driver);
				Sleep(3000);
				PopUpTimePeriod(driver);
				Sleep(3000);
				ProcessTimePeriodSelector(driver);
			}
		}

	}

	public static void ClickCMS(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Content Mgt."));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}


	#region Analytics

	public static bool ClickClear(IWebDriver driver)
	{
		try
		{
			var btnClear = driver.FindElement(By.LinkText("Clear"));
			btnClear.Click();
			Sleep(2000);
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
			return false;
		}
	}

	public static void ClickAnalyticCard(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Analytic Contents"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static bool ClickNew(IWebDriver driver)
	{
		try
		{
			var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/analytics/add-content?sectorId=-1']"));
			btnNewReq.Click();

			Sleep(2000);
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
			return false;
		}
	}

	public static void PopUpDataSet(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Dataset"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void ProcessDataSetSelector(IWebDriver driver)
	{
		try
		{
			var analytics = new Analytics(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSAnalytis();

			analytics.dropDownSector.SelectDropDown(retVal.AnalyticsDataSector.SectorIndex);
			Sleep(1000);
			analytics.dropDownCategory.SelectDropDown(retVal.AnalyticsDataSector.CategoryIndex);
			Sleep(1000);
			analytics.dropDownTable.SelectDropDown(retVal.AnalyticsDataSector.TableIndex);

			Sleep(4000);
			var indicatoros = analytics.table;
			var rowCount = 0;
			if (indicatoros != null)
			{
				var rowCounts = analytics.rows.Count();
				var rows = analytics.rows;
				var rowIndexes = retVal.AnalyticsDataSector.GetIndexArray();

				rowCount = rows.Count() - 1;

				foreach (var item in rowIndexes)
				{

					IWebElement checkbox = analytics.rows[item].FindElement(By.Name("SelIndiIds"));
					checkbox.Click();
					rowCount--;
					if (rowCount <= 0)
					{
						break;
					}

				}
			}

			Sleep(3000);


			analytics.ClickContinue();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void PopUpTimePeriod(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Time-Period"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}


	public static void ProcessTimePeriodSelector(IWebDriver driver)
	{
		try
		{
			var analytics = new Analytics(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSAnalytis();

			var perodTypeVal = analytics.readonlyInput.GetCssValue("value");

			var val = ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].value;", analytics.readonlyInput);

			string value = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].value;", analytics.readonlyInput);

			string emt = val.ToString();
			

			switch (perodTypeVal)
			{
				case "Daily":
					break;
				case "Weekly":
					int startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
					int stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
					DateTime startOfYear = new DateTime(startYear, 1, 1);
					DateTime endOfYear = new DateTime(stopYear, 12, 31);
					analytics.OpenStartDateDrpDwn.Clear();
					analytics.OpenStopDateDrpDwn.Clear();
					analytics.OpenStartDateDrpDwn.SelectDropDown(startOfYear.ToString());
					analytics.OpenStopDateDrpDwn.SelectDropDown(endOfYear.ToString());

					break;
				case "Monthly":
					break;
				case "Quarterly":
					break;
				case "Semiannual":
					break;
				case "Annual":
					 startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
					 stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
					analytics.OpenStartDateDrpDwn.Clear();
					analytics.OpenStopDateDrpDwn.Clear();
					analytics.OpenStartDateDrpDwn.SelectDropDown(startYear.ToString());
					analytics.OpenStopDateDrpDwn.SelectDropDown(stopYear.ToString());
					break;
				case "Biannual":
					break;
				default:
					break;
			}

			Sleep(4000);

			analytics.btnContinueSelection.Click();

		
			Sleep(3000);


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}


	#endregion
	private static void Sleep(int timeVal)
	{
		Thread.Sleep(timeVal);
	}
}