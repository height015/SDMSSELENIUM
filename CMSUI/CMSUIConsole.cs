using System.Globalization;
using CMSUI;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
				#region Analytics Operations
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
				#endregion

				//Flash Card
				#region FlashCard Operations
				ClickCMS(driver);
				Sleep(3000);
				ClickFlashCard(driver);
				Sleep(3000);
				ClickNewFcard(driver);
				Sleep(3000);
				PopUpDataSet(driver);
				Sleep(3000);
				ProcessFlashCardDataSetSelector(driver);
				Sleep(3000);
				PopUpTimePeriod(driver);
				Sleep(3000);
				ProcessTimePeriodFlashCardSelector(driver);
				#endregion

				//FeaturedContent
				#region FeaturedContent Operations
				ClickCMS(driver);
				Sleep(3000);
				ClickFeaturedContentCard(driver);
				Sleep(3000);
				ClickNewFContentCard(driver);
				Sleep(3000);
				PopUpDataSet(driver);
				Sleep(3000);
				ProcessFContentDataSetSelector(driver);
				Sleep(3000);
				PopUpTimePeriod(driver);
				Sleep(3000);
				ProcessTimePeriodFContentSelector(driver);
				#endregion

				//Quick Flashes
				#region Quick Flashes Operations

				ClickCMS(driver);
				Sleep(3000);
				ClickQuickFlashCard(driver);
				Sleep(3000);
				ClickNewQuickFlashCard(driver);
				Sleep(3000);
				PopUpDataSet(driver);
				Sleep(3000);
				ProcessQFlashDataSetSelector(driver);
				Sleep(3000);
				PopUpTimePeriod(driver);
				Sleep(3000);
				ProcessTimePeriodQFlashSelector(driver);

				#endregion
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


	#region Analytics


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

				rowCount = rows.Count();

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


	public static void ProcessTimePeriodSelector(IWebDriver driver)
	{
		try
		{
			var analytics = new Analytics(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSAnalytis();

			var perodTypeValz = analytics.readonlyInput;
			var dataVal = perodTypeValz.GetAttribute("data-value");
			var perodTypeVal = perodTypeValz.GetAttribute("value");



			switch (perodTypeVal)
			{
				case "Daily":
					break;
				case "Weekly":
					int startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
					int stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);

					var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));  
					var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));  

					DateTime startOfYear = new DateTime(startYear, 1, 1);
					string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					DateTime endOfYear = new DateTime(stopYear, 12, 31);
					string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					startyr.Clear();
					stopyr.Clear();
					startyr.SendKeys(formattedstartOfYear);
					perodTypeValz.Click();
					stopyr.SendKeys(formattedendOfYear);
					perodTypeValz.Click();


					break;
				case "Monthly":
					break;
				case "Quarterly":
					break;
				case "Semiannual":
					break;
				case "Annually":
					startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
					stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
					analytics.OpenStartDateDrpDwn.Clear();
					analytics.OpenStopDateDrpDwn.Clear();
					analytics.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
					perodTypeValz.Click();
					analytics.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
					perodTypeValz.Click();
					break;
				case "Biannual":
					break;
				default:
					break;
			}

			Sleep(4000);


			analytics.btnContinueSelection.Click();

			Sleep(3000);

			var dataSetLink = driver.FindElement(By.LinkText("Content"));
			dataSetLink.Click();

			Sleep(3000);

			ProcessAnalyticContent(driver);

			Sleep(3000);

			analytics.ClickOk();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}


	public static void ProcessAnalyticContent(IWebDriver driver)
	{
		try
		{
			var analytics = new Analytics(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSAnalytis();


			var name = retVal.AnalyticsDataSector?.Name;
			var title = retVal.AnalyticsDataSector?.Title;
			var titleSeries = retVal.AnalyticsDataSector?.SeriesTitle;
			var chatType = retVal.AnalyticsDataSector.ChartTypeIndex;
			var contentType = retVal.AnalyticsDataSector.ContentSpotIndex;
			var Note = retVal.AnalyticsDataSector?.Note;

			analytics.txtBoxName.SendKeys(name);
			analytics.txtBoxTitle.SendKeys(title);
			analytics.txtBoxSeries.SendKeys(titleSeries);
			analytics.dropDwnSeriesType.SelectDropDown(chatType);
			analytics.dropDwnContentSpot.SelectDropDown(contentType);
			analytics.txtNote.SendKeys(Note);

			analytics.btnSaveConten.Click();
			Sleep(3000);

			analytics.btnSaves.Click();

			Sleep(3000);

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLink = wait.Until(d => analytics.btnYes);

			dataSetLink.Click();
			Sleep(3000);

		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}

	}
	#endregion

	#region Flash Cards



	public static void ClickFlashCard(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Flash Cards"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static bool ClickNewFcard(IWebDriver driver)
	{
		try
		{
			var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-cards/add-content?sectorId=-1']"));
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



	public static void ProcessFlashCardDataSetSelector(IWebDriver driver)
	{
		try
		{
			var flashcard = new FlashCards(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFlashCard();

			flashcard.dropDownSector.SelectDropDown(retVal.FlashCardDataSector.SectorIndex);
			Sleep(1000);
			flashcard.dropDownCategory.SelectDropDown(retVal.FlashCardDataSector.CategoryIndex);
			Sleep(1000);
			flashcard.dropDownTable.SelectDropDown(retVal.FlashCardDataSector.TableIndex);

			Sleep(4000);
			var indicatoros = flashcard.table;
			var rowCount = 0;
			if (indicatoros != null)
			{
				var rowCounts = flashcard.rows.Count();
				var rows = flashcard.rows;
				var rowIndexes = retVal.FlashCardDataSector.GetIndexArray();

				rowCount = rows.Count();

				foreach (var item in rowIndexes)
				{

					IWebElement checkbox = flashcard.rows[item].FindElement(By.Name("SelIndiIds"));
					checkbox.Click();
					rowCount--;
					if (rowCount <= 0)
					{
						break;
					}

				}
			}

			Sleep(3000);



			flashcard.ClickContinue();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}



	public static void ProcessTimePeriodFlashCardSelector(IWebDriver driver)
	{
		try
		{
			var analytics = new FlashCards(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFlashCard();

			var perodTypeValz = analytics.readonlyInput;
			var dataVal = perodTypeValz.GetAttribute("data-value");
			var perodTypeVal = perodTypeValz.GetAttribute("value");



			switch (perodTypeVal)
			{
				case "Daily":
					break;
				case "Weekly":
					int startYear = int.Parse(retVal.FlashCardDataSector.StartDate);
					int stopYear = int.Parse(retVal.FlashCardDataSector.StopDate);

					var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));
					var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));

					DateTime startOfYear = new DateTime(startYear, 1, 1);
					string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					DateTime endOfYear = new DateTime(stopYear, 12, 31);
					string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					startyr.Clear();
					stopyr.Clear();
					startyr.SendKeys(formattedstartOfYear);
					perodTypeValz.Click();
					stopyr.SendKeys(formattedendOfYear);
					perodTypeValz.Click();


					break;
				case "Monthly":
					break;
				case "Quarterly":
					break;
				case "Semiannual":
					break;
				case "Annually":
					startYear = int.Parse(retVal.FlashCardDataSector.StartDate);
					stopYear = int.Parse(retVal.FlashCardDataSector.StopDate);
					analytics.OpenStartDateDrpDwn.Clear();
					analytics.OpenStopDateDrpDwn.Clear();
					analytics.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
					perodTypeValz.Click();
					analytics.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
					perodTypeValz.Click();
					break;
				case "Biannual":
					break;
				default:
					break;
			}

			Sleep(4000);


			analytics.btnContinueSelection.Click();

			Sleep(3000);

			var dataSetLink = driver.FindElement(By.LinkText("Content"));
			dataSetLink.Click();

			Sleep(3000);

			ProcessFlashCardContent(driver);

			Sleep(3000);

			analytics.ClickOk();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}


	public static void ProcessFlashCardContent(IWebDriver driver)
	{
		try
		{
			var analytics = new FlashCards(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFlashCard();


			var name = retVal.FlashCardDataSector?.Name;
			var title = retVal.FlashCardDataSector?.Title;
			var titleSeries = retVal.FlashCardDataSector?.SeriesTitle;
			var chatType = retVal.FlashCardDataSector.ChartTypeIndex;
			var contentType = retVal.FlashCardDataSector.ContentSpotIndex;
			var Note = retVal.FlashCardDataSector?.Note;

			analytics.txtBoxName.SendKeys(name);
			analytics.txtBoxTitle.SendKeys(title);
			analytics.txtBoxSeries.SendKeys(titleSeries);
			analytics.dropDwnSeriesType.SelectDropDown(chatType);
			analytics.dropDwnContentSpot.SelectDropDown(contentType);
			analytics.txtNote.SendKeys(Note);

			analytics.btnSaveConten.Click();
			Sleep(3000);

			analytics.btnSaves.Click();

			Sleep(3000);

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLink = wait.Until(d => analytics.btnYes);

			dataSetLink.Click();
			Sleep(3000);

		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}

	}
	#endregion

	#region FeaturedContent

	public static void ClickFeaturedContentCard(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Featured Contents"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static bool ClickNewFContentCard(IWebDriver driver)
	{
		try
		{
			var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-contents/add-content?sectorId=-1']"));
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
	public static void ProcessFContentDataSetSelector(IWebDriver driver)
	{
		try
		{
			var Fcontent = new FeaturedContents(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();

			Fcontent.dropDownSector.SelectDropDown(retVal.FeaturedContentDataSector.SectorIndex);
			Sleep(1000);
			Fcontent.dropDownCategory.SelectDropDown(retVal.FeaturedContentDataSector.CategoryIndex);
			Sleep(1000);
			Fcontent.dropDownTable.SelectDropDown(retVal.FeaturedContentDataSector.TableIndex);

			Sleep(4000);
			var indicatoros = Fcontent.table;
			var rowCount = 0;
			if (indicatoros != null)
			{
				var rowCounts = Fcontent.rows.Count();
				var rows = Fcontent.rows;
				var rowIndexes = retVal.FeaturedContentDataSector.GetIndexArray();

				rowCount = rows.Count();

				foreach (var item in rowIndexes)
				{

					IWebElement checkbox = Fcontent.rows[item].FindElement(By.Name("SelIndiIds"));
					checkbox.Click();
					rowCount--;
					if (rowCount <= 0)
					{
						break;
					}

				}
			}

			Sleep(3000);



			Fcontent.ClickContinue();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static void ProcessTimePeriodFContentSelector(IWebDriver driver)
	{
		try
		{
			var Fcontent = new FeaturedContents(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();

			var perodTypeValz = Fcontent.readonlyInput;
			var dataVal = perodTypeValz.GetAttribute("data-value");
			var perodTypeVal = perodTypeValz.GetAttribute("value");



			switch (perodTypeVal)
			{
				case "Daily":
					break;
				case "Weekly":
					int startYear = int.Parse(retVal.FeaturedContentDataSector.StartDate);
					int stopYear = int.Parse(retVal.FeaturedContentDataSector.StopDate);

					var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));
					var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));

					DateTime startOfYear = new DateTime(startYear, 1, 1);
					string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					DateTime endOfYear = new DateTime(stopYear, 12, 31);
					string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					startyr.Clear();
					stopyr.Clear();
					startyr.SendKeys(formattedstartOfYear);
					perodTypeValz.Click();
					stopyr.SendKeys(formattedendOfYear);
					perodTypeValz.Click();


					break;
				case "Monthly":
					break;
				case "Quarterly":
					break;
				case "Semiannual":
					break;
				case "Annually":
					startYear = int.Parse(retVal.FeaturedContentDataSector.StartDate);
					stopYear = int.Parse(retVal.FeaturedContentDataSector.StopDate);
					Fcontent.OpenStartDateDrpDwn.Clear();
					Fcontent.OpenStopDateDrpDwn.Clear();
					Fcontent.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
					perodTypeValz.Click();
					Fcontent.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
					perodTypeValz.Click();
					break;
				case "Biannual":
					break;
				default:
					break;
			}

			Sleep(4000);

			Fcontent.btnContinueSelection.Click();

			Sleep(3000);
			var dataSetLink = driver.FindElement(By.LinkText("Content"));
			dataSetLink.Click();

			Sleep(3000);

			ProcessFContent(driver);

			Sleep(3000);

			Fcontent.ClickOk();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void ProcessFContent(IWebDriver driver)
	{
		try
		{
			var Fcontent = new FeaturedContents(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSFeaturedContent();


			var name = retVal.FeaturedContentDataSector?.Name;
			var title = retVal.FeaturedContentDataSector?.Title;
			var titleSeries = retVal.FeaturedContentDataSector?.SeriesTitle;
			var chatType = retVal.FeaturedContentDataSector.ChartTypeIndex;
			var contentType = retVal.FeaturedContentDataSector.ContentSpotIndex;
			var Note = retVal.FeaturedContentDataSector?.Note;

			Fcontent.txtBoxName.SendKeys(name);
			Fcontent.txtBoxTitle.SendKeys(title);
			Fcontent.txtBoxSeries.SendKeys(titleSeries);
			Fcontent.dropDwnSeriesType.SelectDropDown(chatType);
			Fcontent.dropDwnContentSpot.SelectDropDown(contentType);
			Fcontent.txtNote.SendKeys(Note);

			Fcontent.btnSaveConten.Click();
			Sleep(3000);

			Fcontent.btnSaves.Click();

			Sleep(3000);

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLink = wait.Until(d => Fcontent.btnYes);

			dataSetLink.Click();
			Sleep(3000);

		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}

	}
	#endregion

	#region Quick Flashes


	public static void ClickQuickFlashCard(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Quick Flashes"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static bool ClickNewQuickFlashCard(IWebDriver driver)
	{
		try
		{
			var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/q-flashes/add-content?sectorId=-1']"));
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
	public static void ProcessQFlashDataSetSelector(IWebDriver driver)
	{
		try
		{
			var Fcontent = new QuickFlashes(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSQFlash();

			Fcontent.dropDownSector.SelectDropDown(retVal.QFlashDataSector.SectorIndex);
			Sleep(1000);
			Fcontent.dropDownCategory.SelectDropDown(retVal.QFlashDataSector.CategoryIndex);
			Sleep(1000);
			Fcontent.dropDownTable.SelectDropDown(retVal.QFlashDataSector.TableIndex);

			Sleep(4000);
			var indicatoros = Fcontent.table;
			var rowCount = 0;
			if (indicatoros != null)
			{
				var rowCounts = Fcontent.rows.Count();
				var rows = Fcontent.rows;
				var rowIndexes = retVal.QFlashDataSector.GetIndexArray();

				rowCount = rows.Count();

				foreach (var item in rowIndexes)
				{

					IWebElement checkbox = Fcontent.rows[item].FindElement(By.Name("SelIndiIds"));
					checkbox.Click();
					rowCount--;
					if (rowCount <= 0)
					{
						break;
					}

				}
			}

			Sleep(3000);



			Fcontent.ClickContinue();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static void ProcessTimePeriodQFlashSelector(IWebDriver driver)
	{
		try
		{
			var Fcontent = new QuickFlashes(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSQFlash();

			var perodTypeValz = Fcontent.readonlyInput;
			var dataVal = perodTypeValz.GetAttribute("data-value");
			var perodTypeVal = perodTypeValz.GetAttribute("value");



			switch (perodTypeVal)
			{
				case "Daily":
					break;
				case "Weekly":
					int startYear = int.Parse(retVal.QFlashDataSector.StartDate);
					int stopYear = int.Parse(retVal.QFlashDataSector.StopDate);

					var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));
					var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));

					DateTime startOfYear = new DateTime(startYear, 1, 1);
					string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					DateTime endOfYear = new DateTime(stopYear, 12, 31);
					string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

					startyr.Clear();
					stopyr.Clear();
					startyr.SendKeys(formattedstartOfYear);
					perodTypeValz.Click();
					stopyr.SendKeys(formattedendOfYear);
					perodTypeValz.Click();


					break;
				case "Monthly":
					break;
				case "Quarterly":
					break;
				case "Semiannual":
					break;
				case "Annually":
					startYear = int.Parse(retVal.QFlashDataSector.StartDate);
					stopYear = int.Parse(retVal.QFlashDataSector.StopDate);
					Fcontent.OpenStartDateDrpDwn.Clear();
					Fcontent.OpenStopDateDrpDwn.Clear();
					Fcontent.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
					perodTypeValz.Click();
					Fcontent.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
					perodTypeValz.Click();
					break;
				case "Biannual":
					break;
				default:
					break;
			}

			Sleep(4000);

			Fcontent.btnContinueSelection.Click();

			Sleep(3000);
			var dataSetLink = driver.FindElement(By.LinkText("Content"));
			dataSetLink.Click();

			Sleep(3000);

			ProcessQFlash(driver);

			Sleep(3000);

			Fcontent.ClickOk();


		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void ProcessQFlash(IWebDriver driver)
	{
		try
		{
			var Fcontent = new QuickFlashes(driver);

			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonCMSQFlash();


			var name = retVal.QFlashDataSector?.Name;
			var title = retVal.QFlashDataSector?.Title;
			var titleSeries = retVal.QFlashDataSector?.SeriesTitle;
			var chatType = retVal.QFlashDataSector.ChartTypeIndex;
			var contentType = retVal.QFlashDataSector.ContentSpotIndex;
			var Note = retVal.QFlashDataSector?.Note;

			Fcontent.txtBoxName.SendKeys(name);
			Fcontent.txtBoxTitle.SendKeys(title);
			Fcontent.txtBoxSeries.SendKeys(titleSeries);
			Fcontent.dropDwnSeriesType.SelectDropDown(chatType);
			Fcontent.dropDwnContentSpot.SelectDropDown(contentType);
			Fcontent.txtNote.SendKeys(Note);

			Fcontent.btnSaveConten.Click();
			Sleep(3000);

			Fcontent.btnSaves.Click();

			Sleep(3000);

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var dataSetLink = wait.Until(d => Fcontent.btnYes);

			dataSetLink.Click();
			Sleep(3000);

		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}

	}
	#endregion


	#region Release Calendars

	//public static void ClickReleaseCalendarCard(IWebDriver driver)
	//{
	//	try
	//	{
	//		var dataSetLink = driver.FindElement(By.LinkText("Release Calendars"));
	//		dataSetLink.Click();
	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
	//	}
	//}
	//public static bool ClickNewReleaseCalendarCard(IWebDriver driver)
	//{
	//	try
	//	{
	//		var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/q-flashes/add-content?sectorId=-1']"));
	//		btnNewReq.Click();

	//		Sleep(2000);
	//		return true;
	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
	//		return false;
	//	}
	//}
	//public static void ProcessQFlashDataSetSelector(IWebDriver driver)
	//{
	//	try
	//	{
	//		var Fcontent = new QuickFlashes(driver);

	//		JsonFileReader jsonFileReader = new();

	//		var retVal = jsonFileReader.ReadJsonCMSQFlash();

	//		Fcontent.dropDownSector.SelectDropDown(retVal.QFlashDataSector.SectorIndex);
	//		Sleep(1000);
	//		Fcontent.dropDownCategory.SelectDropDown(retVal.QFlashDataSector.CategoryIndex);
	//		Sleep(1000);
	//		Fcontent.dropDownTable.SelectDropDown(retVal.QFlashDataSector.TableIndex);

	//		Sleep(4000);
	//		var indicatoros = Fcontent.table;
	//		var rowCount = 0;
	//		if (indicatoros != null)
	//		{
	//			var rowCounts = Fcontent.rows.Count();
	//			var rows = Fcontent.rows;
	//			var rowIndexes = retVal.QFlashDataSector.GetIndexArray();

	//			rowCount = rows.Count();

	//			foreach (var item in rowIndexes)
	//			{

	//				IWebElement checkbox = Fcontent.rows[item].FindElement(By.Name("SelIndiIds"));
	//				checkbox.Click();
	//				rowCount--;
	//				if (rowCount <= 0)
	//				{
	//					break;
	//				}

	//			}
	//		}

	//		Sleep(3000);



	//		Fcontent.ClickContinue();


	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
	//	}
	//}
	//public static void ProcessTimePeriodQFlashSelector(IWebDriver driver)
	//{
	//	try
	//	{
	//		var Fcontent = new QuickFlashes(driver);

	//		JsonFileReader jsonFileReader = new();

	//		var retVal = jsonFileReader.ReadJsonCMSQFlash();

	//		var perodTypeValz = Fcontent.readonlyInput;
	//		var dataVal = perodTypeValz.GetAttribute("data-value");
	//		var perodTypeVal = perodTypeValz.GetAttribute("value");



	//		switch (perodTypeVal)
	//		{
	//			case "Daily":
	//				break;
	//			case "Weekly":
	//				int startYear = int.Parse(retVal.QFlashDataSector.StartDate);
	//				int stopYear = int.Parse(retVal.QFlashDataSector.StopDate);

	//				var startyr = driver.FindElement(By.Id("txtWeekStartPeriod"));
	//				var stopyr = driver.FindElement(By.Id("txtWeekStopPeriod"));

	//				DateTime startOfYear = new DateTime(startYear, 1, 1);
	//				string formattedstartOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

	//				DateTime endOfYear = new DateTime(stopYear, 12, 31);
	//				string formattedendOfYear = startOfYear.ToString("yyyy-'W'ww", CultureInfo.InvariantCulture);

	//				startyr.Clear();
	//				stopyr.Clear();
	//				startyr.SendKeys(formattedstartOfYear);
	//				perodTypeValz.Click();
	//				stopyr.SendKeys(formattedendOfYear);
	//				perodTypeValz.Click();


	//				break;
	//			case "Monthly":
	//				break;
	//			case "Quarterly":
	//				break;
	//			case "Semiannual":
	//				break;
	//			case "Annually":
	//				startYear = int.Parse(retVal.QFlashDataSector.StartDate);
	//				stopYear = int.Parse(retVal.QFlashDataSector.StopDate);
	//				Fcontent.OpenStartDateDrpDwn.Clear();
	//				Fcontent.OpenStopDateDrpDwn.Clear();
	//				Fcontent.OpenStartDateDrpDwn.SendKeys(startYear.ToString());
	//				perodTypeValz.Click();
	//				Fcontent.OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
	//				perodTypeValz.Click();
	//				break;
	//			case "Biannual":
	//				break;
	//			default:
	//				break;
	//		}

	//		Sleep(4000);

	//		Fcontent.btnContinueSelection.Click();

	//		Sleep(3000);
	//		var dataSetLink = driver.FindElement(By.LinkText("Content"));
	//		dataSetLink.Click();

	//		Sleep(3000);

	//		ProcessQFlash(driver);

	//		Sleep(3000);

	//		Fcontent.ClickOk();


	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
	//	}
	//}

	//public static void ProcessQFlash(IWebDriver driver)
	//{
	//	try
	//	{
	//		var Fcontent = new QuickFlashes(driver);

	//		JsonFileReader jsonFileReader = new();

	//		var retVal = jsonFileReader.ReadJsonCMSQFlash();


	//		var name = retVal.QFlashDataSector?.Name;
	//		var title = retVal.QFlashDataSector?.Title;
	//		var titleSeries = retVal.QFlashDataSector?.SeriesTitle;
	//		var chatType = retVal.QFlashDataSector.ChartTypeIndex;
	//		var contentType = retVal.QFlashDataSector.ContentSpotIndex;
	//		var Note = retVal.QFlashDataSector?.Note;

	//		Fcontent.txtBoxName.SendKeys(name);
	//		Fcontent.txtBoxTitle.SendKeys(title);
	//		Fcontent.txtBoxSeries.SendKeys(titleSeries);
	//		Fcontent.dropDwnSeriesType.SelectDropDown(chatType);
	//		Fcontent.dropDwnContentSpot.SelectDropDown(contentType);
	//		Fcontent.txtNote.SendKeys(Note);

	//		Fcontent.btnSaveConten.Click();
	//		Sleep(3000);

	//		Fcontent.btnSaves.Click();

	//		Sleep(3000);

	//		var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
	//		var dataSetLink = wait.Until(d => Fcontent.btnYes);

	//		dataSetLink.Click();
	//		Sleep(3000);

	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
	//	}

	//}
	
	#endregion


	private static void Sleep(int timeVal)
	{
		Thread.Sleep(timeVal);
	}
	
} 