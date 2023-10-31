using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Contracts.Dictionary.Units;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Net;
using Newtonsoft.Json;

namespace Commons.Services.Dictionary.Units;
public class UnitsServices : IUnits
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    private static string jsonFileName = "Unit.json";
    private static string jsonFilePath = Path.Combine(desktopPath, "DataConsoleSelenium", jsonFileName);
    private readonly IWebDriver _webDriver;
    public UnitsServices(IWebDriver webDriver)
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


    public void ClickUnit(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Units"));
            dataSetLink.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }

    public bool ClickUnitNew(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a[href^='/dictionary/units/add']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(3000);
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
            var freqVal = ReadJsonFileDataUnit();
            Utils.Sleep(3000);
            NewDataUnitEntry(freqVal.DataUnit.Name, freqVal.DataUnit.ShortName);
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(3000);
            ClickOk();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public bool CreateNewReqGenericPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var retVals = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            var reqType = retVals.CheckBoxNumbers.RequestType;
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
            var waitBtnReqSelect = driver.WaitForElementToBeClickable(driver.FindElement(By.Id("btnReqSelect")), 10);
            Utils.Sleep(4000);
            waitBtnReqSelect.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.RequestData.Title, RequestInforVal.RequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(8000);
            ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Dictionary -> Units");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
    #region Utility
    public static DataUnitContainer ReadJsonFileDataUnit()
    {
        try
        {
            string jsonFileName = "Unit.json";
            string jsonFilePath = Path.Combine(desktopPath, "DataConsoleSelenium", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataUnitContainer retVal = JsonConvert.DeserializeObject<DataUnitContainer>(jsonContent);
                return retVal;
            }
            return new DataUnitContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new DataUnitContainer();
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
            var message = ex.Message;
            return new IndicatorRequestDataContainer();
        }
    }

    #endregion

}