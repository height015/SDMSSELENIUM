using Commons.Contracts.CMS.FlashCards;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using Newtonsoft.Json;

namespace Commons.Services.CMS.FlashCard;

public class FlashCardService : IFlashCards
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private readonly IWebDriver _webDriver;
    public FlashCardService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement dropDownSector => _webDriver.FindElement(By.Id("DSectorId"));
    public IWebElement dropDownCategory => _webDriver.FindElement(By.Id("DCategoryId"));
    public IWebElement dropDownTable => _webDriver.FindElement(By.Id("DTableId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnDSContinue"));
    public IWebElement btnContinueSelection => _webDriver.FindElement(By.Id("btnContSelection"));
    public IWebElement OpenStartDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStartPeriod"));
    public IWebElement OpenStopDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStopPeriod"));
    public IWebElement readonlyInput => _webDriver.FindElement(By.XPath("//input[@type='text' and @aria-describedby='secname' and @data-value]"));
    public IWebElement contentPopUp => _webDriver.FindElement(By.CssSelector("a[href='/shop/analytics/manage'][data-modal].item-button"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement txtBoxSeries => _webDriver.FindElement(By.Id("SeriesTitle"));
    public IWebElement dropDwnSeriesType => _webDriver.FindElement(By.Id("SeriesType"));
    public IWebElement dropDwnContentSpot => _webDriver.FindElement(By.Id("ContentSpot"));
    public IWebElement txtNote => _webDriver.FindElement(By.Id("Note"));
    public IWebElement btnSaveContent => _webDriver.FindElement(By.Id("btnSaveCardInfo"));
    public IWebElement btnMainSaveContent => _webDriver.FindElement(By.Id("btnSaveCardContent"));

    public IWebElement btnSaves => _webDriver.FindElement(By.Id("btnSaveAContent"));
    public IWebElement container => _webDriver.FindElement(By.ClassName("sa-confirm-button-container"));
    public IWebElement btnYes => container.FindElement(By.CssSelector("button.confirm"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk
    {
        get
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("confirm")));
        }
    }

    public IWebElement table => _webDriver.FindElement(By.Id("lvsIndicators")).FindElement(By.TagName("table"));
    public List<IWebElement> rows => table?.FindElements(By.XPath(".//tbody/tr")).ToList() ?? new List<IWebElement>();

    public void ClickContinue()
    {
        btnContinue.Click();
    }

    public void ClickContinueDate()
    {
        btnContinueSelection.Click();
    }

    public void ClickSave()
    {
        btnSave.Click();
    }

    public void ClickSubmit()
    {
        btnSubmit.Click();
    }

    public void ClickContentPopUp()
    {
        contentPopUp.Click();
    }

    public void ClickYesPopUp()
    {
        btnYes.Click();
    }

    public void ClickOk()
    {
        btnClickOk.Click();
    }

    public void ProcessFlashCardDataSetSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSFlashCard();
            dropDownSector.SelectDropDownByIndex(retVal.FlashCardDataSector.SectorIndex);
            Utils.Sleep(1000);
            dropDownCategory.SelectDropDownByIndex(retVal.FlashCardDataSector.CategoryIndex);
            Utils.Sleep(1000);
            dropDownTable.SelectDropDownByIndex(retVal.FlashCardDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = rows.Count();
                //var rows = flashcard.rows;
                var rowIndexes = retVal.FlashCardDataSector.GetIndexArray();
                rowCount = rows.Count();
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = rows[item].FindElement(By.Name("SelIndiIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(3000);
            ClickContinue();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool isAlertPresent = wait.Until(ExpectedConditions.AlertIsPresent()) != null;
            int count = 2;
            while (count > 0)
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.AlertIsPresent());
                alert.Accept();
                count-=1;
                Console.WriteLine("An alert is present.");
            }
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessTimePeriodFlashCardSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSFlashCard();
            var perodTypeValz = readonlyInput;
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
                    OpenStartDateDrpDwn.Clear();
                    OpenStopDateDrpDwn.Clear();
                    OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    perodTypeValz.Click();
                    OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    perodTypeValz.Click();
                    break;
                case "Biannual":
                    break;
                default:
                    break;
            }
            Utils.Sleep(4000);
            btnContinueSelection.Click();
            Utils.Sleep(3000);
            var dataSetLink = driver.FindElement(By.LinkText("Content"));
            dataSetLink.Click();
            Utils.Sleep(3000);
            ProcessFlashCardContent(driver);
            Utils.Sleep(3000);
            btnMainSaveContent.Click();
            Utils.Sleep(3000);
            ClickOk();
            Utils.LogSuccess($"Create FlashCard", "CMS FlashCard");
            var btnOk = driver.WaitForElementToBeClickable(btnClickOk, 10);
            btnOk.Click();

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessFlashCardContent(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSFlashCard();
            var name = retVal.FlashCardDataSector?.Name;
            var title = retVal.FlashCardDataSector?.Title;
            txtBoxName.SendKeys(name);
            txtBoxTitle.SendKeys(title);
            btnSaveContent.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    #region Utility
    public static FlashCardDataSectorContainer ReadJsonCMSFlashCard()
    {
        try
        {
            string jsonFileName = "FCard.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "DataConsoleSelenium", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<FlashCardDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new FlashCardDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new FlashCardDataSectorContainer();
        }

    }

    #endregion
}
