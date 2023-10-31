using Commons.Contracts.Dictionary.DataEntities;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Newtonsoft.Json;
using System.Net;

namespace Commons.Services.Dictionary.DataEntity;

public class DataEntitiesService : IDataEntities
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static string jsonFileName = "DataEntity.json";
   private static string jsonFilePath = Path.Combine(desktopPath, "DataConsoleSelenium", jsonFileName);
    private int reqType = -1;
    private readonly IWebDriver _webDriver;

    public DataEntitiesService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtShort => _webDriver.FindElement(By.Id("ShortName"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnReqSelect => _webDriver.FindElement(By.Id("btnReqSelect"));

    public IWebElement btnClickOk
    {
        get
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(15));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("confirm")));
        }
    }

    public void NewDataEntityEntry(string name, string shortName)
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
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));

    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);
    }
    public void ClickSave()
    {
        btnSave.Clicks();
    }
    public void ClickDictionary(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Dictionaries"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void ClickDataEntity(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Data Entities"));
            dataSetLink.Click();

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool ClickDataEntityNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/data-entities/add']"));
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

    public bool DataEntityEntry(IWebDriver driver)
    {
        try
        {
            var freqVal = ReadJsonFileDataEntity();
            Utils.Sleep(3000);
            NewDataEntityEntry(freqVal.DataEntities.Name, freqVal.DataEntities.ShortName);
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(3000);
            ClickOk();
            Utils.LogSuccess($"Create {freqVal.DataEntities.Name}", "Dictionaty Data-Entities");
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
            var RequestInforVal = ReadJsonFileForNewRequestIndicator();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
           
            //or Use this
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var dataSetLink = driver.WaitForElementToBeClickable(btnReqSelect,10);
            dataSetLink.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.RequestData.Title, RequestInforVal.RequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(8000);
            ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Dictionary Data-Entities");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    #region Utility
    public static DataEntitiesContainer ReadJsonFileDataEntity()
    {
        try
        {
           
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataEntitiesContainer>(jsonContent);
                return retVal;
            }
            return new DataEntitiesContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataEntitiesContainer();
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