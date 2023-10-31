using Commons.Contracts.CMS.Analytic_Contents;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using Newtonsoft.Json;

namespace Commons.Services.CMS.Analytics;

public class AnalyticsService : IAnalytics
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private readonly IWebDriver _webDriver;

    public AnalyticsService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public IWebElement dropDownSector => _webDriver.FindElement(By.Id("CSectorId"));
    public IWebElement dropDownCategory => _webDriver.FindElement(By.Id("CCategoryId"));
    public IWebElement dropDownTable => _webDriver.FindElement(By.Id("CTableId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnCSContinue"));
    public IWebElement btnContinueSelection => _webDriver.FindElement(By.Id("btnContSelection"));
    public IWebElement OpenStartDateDrpDwn => _webDriver.FindElement(By.Id("txtStartYear"));
    public IWebElement OpenStopDateDrpDwn => _webDriver.FindElement(By.Id("txtStopYear"));
    public IWebElement StartQuaterDrpDwn => _webDriver.FindElement(By.Id("StartQuarter"));
    public IWebElement StopQuaterDrpDwn => _webDriver.FindElement(By.Id("StopQuarter"));
    public IWebElement StartMonthtxtBox => _webDriver.FindElement(By.Id("txtStartMonth"));
    public IWebElement StopMonthtxtBox => _webDriver.FindElement(By.Id("txtStopMonth"));
    public IWebElement readonlyInput => _webDriver.FindElement(By.CssSelector("input[type='text'][aria-describedby='secname'][class='form-control p-1']"));
    public IWebElement footerDiv => _webDriver.FindElement(By.ClassName("modal-footer"));
    public IWebElement contentPopUp => _webDriver.FindElement(By.CssSelector("a[href='/shop/analytics/manage'][data-modal].item-button"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement txtBoxSeries => _webDriver.FindElement(By.Id("SeriesTitle"));
    public IWebElement dropDwnSeriesType => _webDriver.FindElement(By.Id("SeriesType"));
    public IWebElement dropDwnContentSpot => _webDriver.FindElement(By.Id("ContentSpot"));
    public IWebElement txtNote => _webDriver.FindElement(By.Id("Note"));
    public IWebElement btnSaveContent => _webDriver.FindElement(By.Id("btnSaveAContentInfo"));
    public IWebElement btnSaves => _webDriver.FindElement(By.Id("btnSaveAContent"));
    public IWebElement linkDivTxt => _webDriver.FindElement(By.LinkText("Analytic Detail"));
    public IWebElement container => _webDriver.FindElement(By.ClassName("sa-confirm-button-container"));
    public IWebElement btnYes => container.FindElement(By.CssSelector("button.confirm"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnAYes => _webDriver.FindElement(By.CssSelector("button.confirm"));
    public IWebElement btnCancel => _webDriver.FindElement(By.CssSelector("button.cancel"));
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


    public void ClickCMS(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Content Management"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool ClickClear(IWebDriver driver)
    {
        try
        {
            var btnClear = driver.FindElement(By.LinkText("Clear"));
            btnClear.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    public void ClickAnalyticCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Analytic Contents"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool ClickNew(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/analytics/new-content?sectorId=-1']"));
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
    public void ProcessDataSetSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSAnalytis();
            driver.WaitForElementToBeClickable(dropDownSector, 10);
                dropDownSector.SelectDropDownByIndex(retVal.AnalyticsDataSector.SectorIndex);
            Utils.Sleep(1000);
            driver.WaitForElementToBeClickable(dropDownCategory, 10);
            dropDownCategory.SelectDropDownByIndex(retVal.AnalyticsDataSector.CategoryIndex);
            Utils.Sleep(1000);
            driver.WaitForElementToBeClickable(dropDownTable, 10);
            dropDownTable.SelectDropDownByIndex(retVal.AnalyticsDataSector.TableIndex);
            Utils.Sleep(4000);
            ProcessTimePeriodSelector(driver);
            var indicatoros = table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = rows.Count();
                var rowIndexes = retVal.AnalyticsDataSector.GetIndexArray();
                rowCount = rows.Count();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", table);
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = rows[item].FindElement(By.Name("SelIndiIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount < 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(7000);
            ClickContinue();
            Utils.Sleep(2000);
            linkDivTxt.Clicks();
            Utils.Sleep(5000);
            ProcessAnalyticContent(driver);
            var waiter = driver.WaitForElementToBeClickable(btnSaves, 10);
            waiter.Click();
            Utils.Sleep(3000);
            var yesBtn = driver.WaitForElementToBeClickable(btnAYes, 10);
            yesBtn.Clicks();
            Utils.LogSuccess($"New Analytics", "CMS -> Analytics");
            var BtnOkay = driver.WaitForElementToBeClickable(btnClickOk, 15);
            Utils.Sleep(6000);
            BtnOkay.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessTimePeriodSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSAnalytis();
            var perodTypeValz = readonlyInput;
            var divClickValz = footerDiv;
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
                    divClickValz.Click();
                    stopyr.SendKeys(formattedendOfYear);
                    divClickValz.Click();
                    break;
                case "Monthly":
                    startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
                    stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
                    OpenStartDateDrpDwn.Clear();
                    OpenStopDateDrpDwn.Clear();
                    OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    divClickValz.Click();
                    OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    divClickValz.Click();
                    StartMonthtxtBox.Clear();
                    StopMonthtxtBox.Clear();
                    StartMonthtxtBox.SendKeys(retVal.AnalyticsDataSector.StartMonth);
                    divClickValz.Click();
                    Utils.Sleep(3000);
                    StopMonthtxtBox.SendKeys(retVal.AnalyticsDataSector.StoptMonth);
                    divClickValz.Click();
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", table);
                    break;
                case "Quarterly":
                    startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
                    stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
                    OpenStartDateDrpDwn.Clear();
                    OpenStopDateDrpDwn.Clear();
                    OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    divClickValz.Click();
                    Utils.Sleep(3000);
                    divClickValz.Click();
                    OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    divClickValz.Click();
                    StartQuaterDrpDwn.SelectDropDownByIndex(retVal.AnalyticsDataSector.StartQuaterIndex);
                    StopQuaterDrpDwn.SelectDropDownByIndex(retVal.AnalyticsDataSector.StoptQuaterIndex);
                    break;

                case "Semiannual":
                    break;
                case "Annually":
                    startYear = int.Parse(retVal.AnalyticsDataSector.StartDate);
                    stopYear = int.Parse(retVal.AnalyticsDataSector.StopDate);
                    OpenStartDateDrpDwn.Clear();
                    OpenStopDateDrpDwn.Clear();
                    OpenStartDateDrpDwn.SendKeys(startYear.ToString());
                    divClickValz.Click();
                    OpenStopDateDrpDwn.SendKeys(stopYear.ToString());
                    divClickValz.Click();
                    break;
                case "Biannual":
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessAnalyticContent(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSAnalytis();
            var name = retVal.AnalyticsDataSector?.Name;
            var title = retVal.AnalyticsDataSector?.Title;
            var titleSeries = retVal.AnalyticsDataSector?.SeriesTitle;
            var chatType = retVal.AnalyticsDataSector.ChartTypeIndex;
            var contentType = retVal.AnalyticsDataSector.ContentSpotIndex;
            var Note = retVal.AnalyticsDataSector?.Note;
            txtBoxName.SendKeys(name);
            txtBoxTitle.SendKeys(title);
            txtBoxSeries.SendKeys(titleSeries);
            dropDwnSeriesType.SelectDropDownByIndex(chatType);
            dropDwnContentSpot.SelectDropDownByIndex(contentType);
            txtNote.SendKeys(Note);
            btnSaveContent.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    #region Utility
    public static AnalyticsDataSectorContainer ReadJsonCMSAnalytis()
    {
        try
        {
            string jsonFileName = "Analytics.json";
            string jsonFilePath = Path.Combine(desktopPath, "DataConsoleSelenium", jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<AnalyticsDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new AnalyticsDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new AnalyticsDataSectorContainer();
        }

    }

    #endregion
}