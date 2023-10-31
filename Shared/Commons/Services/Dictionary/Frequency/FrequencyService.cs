using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Commons.Contracts.Dictionary.Frequency;
using Newtonsoft.Json;

namespace Commons.Services.Dictionary.Frequency;

public class FrequencyService : IFrequency
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static string jsonFileName = "Frequency.json";
    private static string jsonFilePath = Path.Combine(desktopPath, "DataConsoleSelenium", jsonFileName);
    private int reqType = -1;

    private readonly IWebDriver _webDriver;
    public FrequencyService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
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

    public void ClickNew()
    {
        Newlink.Click();
    }

    public void NewDataFrequencyEntry(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }

    public void ClickSubmit()
    {
        btnSubmit.Click();
    }

    public void ClickOk()
    {
        btnClickOk.Click();
    }

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

    public void NewDataUnitEntry(string name, string shortName)
    {
        txtName.SendKeys(name);
        txtShort.SendKeys(shortName);
    }

    public void ClickSave()
    {
        btnSave.Click();
    }

    public void ClickFrequency(IWebDriver driver)
    {
        try
        {
            var freqLink = driver.FindElement(By.LinkText("Frequencies"));
            freqLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool NewFrequency(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/frequencies/add']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public bool DataEntryFrequency(IWebDriver driver)
    {
        try
        {


            var freqVal = ReadJsonFileDataFrequency();
            Utils.Sleep(3000);
            NewDataFrequencyEntry(freqVal.DataFrequency.Name, freqVal.DataFrequency.ShortName);
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(3000);
            ClickOk();
            Utils.LogSuccess($"Create {freqVal.DataFrequency.Name}", "Dictionary Frequency");
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
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var waitBtnReqSelect= driver.WaitForElementToBeClickable(driver.FindElement(By.Id("btnReqSelect")), 10);
            Utils.Sleep(4000);
            waitBtnReqSelect.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.RequestData.Title, RequestInforVal.RequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(8000);
            ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Frequency");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    #region Utility
    public static DataFrequencyContainer ReadJsonFileDataFrequency()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataFrequencyContainer>(jsonContent);
                return retVal;
            }
            return new DataFrequencyContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataFrequencyContainer();
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
