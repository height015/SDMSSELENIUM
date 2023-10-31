using Commons.Contracts.CMS.QuickFlash;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json;
using System.Globalization;

namespace Commons.Services.CMS.QuickFlash;
public class QuickFlashService : IQuickFlashes
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private readonly IWebDriver _webDriver;

    public QuickFlashService(IWebDriver webDriver)
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
    public IWebElement dropDwnArrow => _webDriver.FindElement(By.Id("ArrowType"));
    public IWebElement dropDwonArrDir => _webDriver.FindElement(By.Id("ArrowDirection"));
    public IWebElement txtValue => _webDriver.FindElement(By.Id("Value"));
    public IWebElement txtExplanation => _webDriver.FindElement(By.Id("Explanation"));
    public IWebElement btnSaves => _webDriver.FindElement(By.Id("btnSaveQFlash"));
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
    public void ProcessQFlashDataSetSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSQFlash();
            dropDownSector.SelectDropDownByIndex(retVal.QFlashDataSector.SectorIndex);
            Utils.Sleep(1000);
            dropDownCategory.SelectDropDownByIndex(retVal.QFlashDataSector.CategoryIndex);
            Utils.Sleep(1000);
            dropDownTable.SelectDropDownByIndex(retVal.QFlashDataSector.TableIndex);
            Utils.Sleep(4000);
            var indicatoros = table;
            var rowCount = 0;
            if (indicatoros != null)
            {
                var rowCounts = rows.Count();
                //var rows = Fcontent.rows;
                var rowIndexes = retVal.QFlashDataSector.GetIndexArray();
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
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessTimePeriodQFlashSelector(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSQFlash();
            var perodTypeValz = readonlyInput;
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
            ProcessNewQFlash(driver);
            Utils.Sleep(3000);
            ClickOk();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ProcessNewQFlash(IWebDriver driver)
    {
        try
        {
            var retVal = ReadJsonCMSQFlash();
            var name = retVal.QFlashDataSector?.Name;
            var title = retVal.QFlashDataSector?.Title;
            var arrType = retVal.QFlashDataSector?.ArrowType;
            var arrDirec = retVal.QFlashDataSector?.ArrowDirection;
            var value = retVal.QFlashDataSector.Value;
            var Note = retVal.QFlashDataSector?.Note;
            txtBoxName.SendKeys(name);
            txtBoxTitle.SendKeys(title);
            dropDwnArrow.SelectDropDownByIndex(arrType.Value);
            dropDwonArrDir.SelectDropDownByIndex(arrDirec.Value);
            txtValue.SendKeys(value.ToString());
            txtExplanation.SendKeys(Note);
            btnSaves.Click();
            Utils.Sleep(3000);
            ClickOk();
            Utils.Sleep(3000);
            Utils.LogSuccess($"Create QuickFlashes", "CMS QuickFlashe");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }

    }

    #region Utility
    private static QFlashDataSectorContainer ReadJsonCMSQFlash()
    {
        try
        {
            string jsonFileName = "QFlashData.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "DataConsoleSelenium", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<QFlashDataSectorContainer>(jsonContent);
                return retVal;
            }
            return new QFlashDataSectorContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new QFlashDataSectorContainer();
        }

    }

    #endregion
}