using System.Net;
using Contracts.Category;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Commons.Services.Category;
public class CategoryService : ICategory
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
   private const string jsonFileName = "Category.json";
   private static string jsonFilePath = Path.Combine(desktopPath,
        "SeleniumTest", jsonFileName);
    private int reqType = -1;

    private readonly IWebDriver _webDriver;

    public CategoryService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table"));
    // Get all the rows in the table
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();
    public IWebElement catalogLink => _webDriver.FindElement(By.CssSelector("a[class='card'][href='/data-catalogs']"));
    public IWebElement dropDownBox => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public virtual IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

    public IWebElement btnSubmitReq => _webDriver.FindElement(By.Id("btnSave"));

    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReson => _webDriver.FindElement(By.Id("txtReason"));

    public virtual void EnterDataCategory(string title, string reason)
    {
        txtBoxName?.SendKeys(title);
        txtBoxTitle?.SendKeys(reason);
    }
    public void ClickCatalogCard()
    {
        catalogLink.Clicks();
    }
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public void ClickClose()
    {
        btnClose.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
    public virtual void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReson.SendKeys(reason);
    }
    public void ClickDataCatalogCard(IWebDriver driver)
    {
        try
        {
            var cartCard = driver.FindElement(By.LinkText("Datasets"));
            cartCard.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public  void ClickCategoryCard(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(3000);
            var cartCard = driver.FindElement(By.LinkText("Categories"));
            Utils.Sleep(3000);
            cartCard.Click();
            Utils.Sleep(5000);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForSelectOptionCatalogSelector();
            dropDownBox.SelectDropDownByIndex(retVal.CatalogueSelector.OptionToSelect);
            Utils.Sleep(3000);
            ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool ClickNewDataCategoryButton(IWebDriver driver)
    {
        try
        {
            var newDataCategoryButton = driver.FindElement(By.CssSelector("a.item-button[href*='/dataset/categories/add']"));
            Utils.Sleep(3000);
            newDataCategoryButton.Click();
            Utils.Sleep(2000);
            var retVal = ReadJsonFileForEnterNewDataCategory();
            EnterDataCategory(retVal.DataCategory.Name, retVal.DataCategory.Title);
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(5000);
            ClickOk();
            Utils.LogSuccess($"Create {retVal.DataCategory.Name}", "Category");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public bool CategoryRequestInfBox(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            var RequestInforVal = ReadJsonFileForSelectCheckBoxesProcessCatNewRequest();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IWebElement button = driver.FindElement(By.Id("btnReqSelect"));
            if (rows.Count > 10)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button);
            }
            //or Use this
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var dataSetLink = wait.Until(d => d.FindElement(By.Id("btnReqSelect")));
            //The UtilMethods.Sleep is Inportant so the Pop-Div is loaded to the  DOM
            Utils.Sleep(4000);
            button.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.CatRequestInformation.Title, RequestInforVal.CatRequestInformation.Reason);
            Utils.Sleep(2000);
            var submit = driver.WaitForElementToBeClickable(btnSubmitReq, 10);
            submit.Click();
            Utils.Sleep(3000);
            ClickOk();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Category");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    #region Utility
    private static DataCategoryContainer ReadJsonFileForEnterNewDataCategory()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataCategoryContainer>(jsonContent);
                return retVal;
            }
            return new DataCategoryContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataCategoryContainer();
        }
    }

    private static CatRequest ReadJsonFileForSelectCheckBoxesProcessCatNewRequest()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<CatRequest>(jsonContent);
                return retVal;
            }
            return new CatRequest();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new CatRequest();
        }
    }

    #endregion
}