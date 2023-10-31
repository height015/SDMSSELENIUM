using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Commons.Contracts.Dictionary.DataSource;
using Newtonsoft.Json;

namespace Commons.Services.Dictionary.DataSource;

public class DataSourcesService : IDataSources
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static string jsonFileName = "DataSource.json";
    private static string jsonFilePath = Path.Combine(desktopPath,
        "DataConsoleSelenium", jsonFileName);
    private int reqType = -1;
    private readonly IWebDriver _webDriver;
    public DataSourcesService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtShort => _webDriver.FindElement(By.Id("ShortName"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk
    {
        get
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(15));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("confirm")));
        }
    }
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));
    public IWebElement table => _webDriver.FindElement(By.ClassName("table"));
    public List<IWebElement> rows => table?.FindElements(By.TagName("tr")).ToList() ?? new List<IWebElement>();
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));
    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);
    }
    public void ClickNew()
    {
        Newlink.Click();
    }
    public void NewDataUnitEntry(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }
    public void ClickSubmit()
    {
        btnSubmit.Click();
    }
    public void ClickSave()
    {
        btnSave.Click();
    }
    public void ClickOk()
    {
        btnClickOk.Click();
    }
    public void NewDataSourceEntry(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }
    public void ClickDataSource(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Sources"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool DataSourceEntry(IWebDriver driver)
    {
        try
        {
            var freqVal = ReadJsonFileDataSource();
            Utils.Sleep(5000);
            NewDataSourceEntry(freqVal.DataSource.Name, freqVal.DataSource.ShortName);
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(3000);
            ClickOk();
            Utils.LogSuccess($"Create {freqVal.DataSource.Name}", "Dictionary Data-Source");
            return true;
        }

        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public bool CreateNewReqGenericPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var retVals = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            reqType = retVals.CheckBoxNumbers.RequestType;
            var RequestInforVal = ReadJsonFileForNewRequestIndicator();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            if (rows.Count > 10)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");
            }
            //or Use this
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var waitBtn = driver.WaitForElementToBeClickable(driver.FindElement(By.Id("btnReqSelect")), 15); 
            Utils.Sleep(4000);
            waitBtn.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.RequestData.Title, RequestInforVal.RequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(4000);
            ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Dictionary Data-Source");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    #region Utility
    public static DataSourceContainer ReadJsonFileDataSource()
    {
        try
        {
            string jsonFileName = "DataSource.json";
            string jsonFilePath = Path.Combine(desktopPath, "SeleniumTest", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataSourceContainer retVal = JsonConvert.DeserializeObject<DataSourceContainer>(jsonContent);
                return retVal;
            }
            return new DataSourceContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataSourceContainer();
        }
    }
    private static IndicatorRequestDataContainer ReadJsonFileForNewRequestIndicator()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<IndicatorRequestDataContainer>(jsonContent);
                return retVal;
            }
            return new IndicatorRequestDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new IndicatorRequestDataContainer();
        }
    }
    #endregion
}




