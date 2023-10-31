using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Contracts.CMS.FContent;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using Newtonsoft.Json;

namespace Commons.Services.CMS.FeaturedContents;

public class FeaturedContentsService : IFeaturedContents
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private readonly IWebDriver _webDriver;
    public FeaturedContentsService(IWebDriver webDriver)
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
    public IWebElement txtBoxSeries => _webDriver.FindElement(By.Id("SubTitle"));
    public IWebElement dropDwnSeriesType => _webDriver.FindElement(By.Id("SeriesType"));
    public IWebElement dropDwnContentSpot => _webDriver.FindElement(By.Id("ContentSpot"));
    public IWebElement txtNote => _webDriver.FindElement(By.Id("Note"));
    public IWebElement btnSaveContent => _webDriver.FindElement(By.Id("btnSaveContentInfo"));
    public IWebElement btnSaves => _webDriver.FindElement(By.Id("btnAddFContent"));
    public IWebElement container => _webDriver.FindElement(By.ClassName("sa-confirm-button-container"));
    public IWebElement btnYes => container.FindElement(By.ClassName("confirm"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement SubTitle => _webDriver.FindElement(By.Id("SubTitle"));
    public IWebElement PreContent => _webDriver.FindElement(By.Id("txtShortContent"));
    //IWebElement txtShortContent = driver.FindElement(By.Id("txtShortContent"));
    public IWebElement shortContentBox => PreContent.FindElement(By.ClassName("ql-editor"));
    public IWebElement MainContents => _webDriver.FindElement(By.Id("txtContent"));
    public IWebElement mainContentBox => MainContents.FindElement(By.ClassName("ql-editor"));

    public IWebElement btnClickOk
    {
        get
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
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

    public void PopUpDataSet(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Dataset")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public void PopUpTimePeriod(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Time-Period")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    public void ClickFeaturedContentCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Featured Contents"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool ClickNewFContentCard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-contents/add-content?sectorId=-1']"));
            btnNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public void ProcessFContentDataSetSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSFeaturedContent();
            dropDownSector.SelectDropDownByIndex(retVal.FeaturedContentDataSector.SectorIndex);
            Utils.Sleep(1000);
            dropDownCategory.SelectDropDownByIndex(retVal.FeaturedContentDataSector.CategoryIndex);
            Utils.Sleep(1000);
            dropDownTable.SelectDropDownByIndex(retVal.FeaturedContentDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = rows.Count();
                var rowIndexes = retVal.FeaturedContentDataSector.GetIndexArray();
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
            while (isAlertPresent)
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.AlertIsPresent());
                alert.Accept();
                Console.WriteLine("An alert is present.");
            }
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessTimePeriodFContentSelector(IWebDriver driver)
    {
        try
        {
            var jsonFileReader = new JsonFileReader();
            var retVal = ReadJsonCMSFeaturedContent();
            var perodTypeValz = readonlyInput;
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
            ProcessFContent(driver);
            Utils.Sleep(3000);
            btnSaveContent.Click();
            Utils.LogSuccess($"Create Featured Contents", "CMS FeaturedContents");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessFContent(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSFeaturedContent();
            var name = retVal.FeaturedContentDataSector?.Name;
            var title = retVal.FeaturedContentDataSector?.Title;
            var titleSeries = retVal.FeaturedContentDataSector?.SubTitle;
            var chatType = retVal.FeaturedContentDataSector.ChartTypeIndex;
            var contentType = retVal.FeaturedContentDataSector.ContentSpotIndex;
            var ShortContent = retVal.FeaturedContentDataSector?.ShortContent;
            var MainContent = retVal.FeaturedContentDataSector?.MainContent;
            //var SubTiltles = retVal.FeaturedContentDataSector?.SubTitle;

            txtBoxName.SendKeys(name);
            txtBoxTitle.SendKeys(title);
            txtBoxSeries.SendKeys(titleSeries);
            dropDwnContentSpot.SelectDropDownByIndex(contentType);
            shortContentBox.SendKeys(ShortContent);
            mainContentBox.SendKeys(MainContent);
            btnSaveContent.Click();
            Utils.Sleep(3000);
            btnSaves.Click();
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => btnYes);
            dataSetLink.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }

    }

    #region Utility
    public static FeaturedContentDataSectorContainer ReadJsonCMSFeaturedContent()
    {
        try
        {
            string jsonFileName = "FContent.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "DataConsoleSelenium", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<FeaturedContentDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new FeaturedContentDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new FeaturedContentDataSectorContainer();
        }

    }

    #endregion
}
