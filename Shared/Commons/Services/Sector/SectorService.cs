using Contracts.Sectors;
using OpenQA.Selenium;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;

namespace Commons.Services.Sector;
public class SectorService : ISector
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private  int reqType = 0;

    private readonly IWebDriver _webDriver;
    public SectorService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public virtual IWebElement btnProcessSelected => _webDriver.FindElement(By.Id("btnReqSelect"));
    public virtual IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public virtual IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnSubmitRequest => _webDriver.FindElement(By.Id("btnSave"));
    public virtual IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public virtual IWebElement textMsgRes => _webDriver.FindElement(By.CssSelector("p[style='display: block;']"));
    public virtual IWebElement btnClickNew => _webDriver.FindElement(By.CssSelector("a.item-button[data-modal='']"));
    public IWebElement txtReson => _webDriver.FindElement(By.Id("txtReason"));
    public virtual IWebElement btnReqSelect => _webDriver.FindElement(By.Id("btnReqSelect"));
    public virtual IWebElement btnClickOk => _webDriver.FindElement(By.ClassName("confirm"));
    public virtual void EnterNameAndTitle(string userName, string password)
    {
        txtName.SendKeys(userName);
        txtTitle.SendKeys(password);
    }
    public virtual void ClickSubmit()
    {
        btnSubmit.Clicks();
    }
    public virtual void SubmitReq()
    {
        btnSubmitRequest.Clicks(); 
    }
    public virtual void ClickClose()
    {
        btnClose.Clicks();
    }
    public virtual void ClickNew()
    {
        btnClickNew.Clicks();
    }
    public virtual void ClickOk()
    {
        btnClickOk.Clicks();
    }
    public virtual void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReson.SendKeys(reason);
    }
    public bool ClickDataSet(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Datasets"));
            dataSetLink.Click();
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    public string CreateNewDataSectorSuccess(IWebDriver driver)
    {
        try
        {
            var secVal = ReadJsonFileCreateSector();
            ClickNew();
            Utils.Sleep(2000);
            EnterNameAndTitle(secVal.SectorField.Name, secVal.SectorField.Title);
            ClickSubmit();
            Utils.Sleep(3000);
            var teaxVal = textMsgRes.Text;
            Utils.Sleep(3000);
            var btnOkays = driver.WaitForElementToBeClickable(btnClickOk, 10);
            btnOkays.Click();
            Utils.LogSuccess($"Create {secVal.SectorField.Title}", "Sector");
            return teaxVal;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return string.Empty;
        }
    }
    public  bool ClickSector(IWebDriver driver)
    {
        try
        {
            var dataSetLinkSec = driver.FindElement(By.LinkText("Sectors"));
            dataSetLinkSec.Click();
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
    public string ClickRequestType(IWebDriver driver)
    {
        try
        {
            JsonFileReader lx = new();
            var retVals = lx.ReadJsonFileSelectCheckBoxes();
            reqType = retVals.CheckBoxNumbers.RequestType;
            Utils.Sleep(3000);
            IWebElement btn = null;
            switch (reqType)
            {
                case (int)RequestType.AuthorizationRequest:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    break;
                case (int)RequestType.UnAuthorization:
                    btn = driver.FindElement(By.LinkText("Unauthorize"));
                    break;
                case (int)RequestType.ArchiveRequest:
                    btn = driver.FindElement(By.LinkText("Archive"));
                    break;
                case (int)RequestType.UnarchiveRequest:
                    btn = driver.FindElement(By.LinkText("Unarchive"));
                    break;
                case (int)RequestType.ModificationRequest:
                    btn = driver.FindElement(By.LinkText("Modify"));
                    break;
                case (int)RequestType.PublicationRequest:
                    btn = driver.FindElement(By.LinkText("Publish"));
                    break;
                case (int)RequestType.UnpublicationRequest:
                    btn = driver.FindElement(By.LinkText("Unpublish"));
                    break;
                default:
                    btn = driver.FindElement(By.LinkText("Authorize"));
                    break;
            }
            if (btn != null)
            {
                btn.Click();
            }
            Utils.Sleep(2000);
            var wait = driver.WaitForElementToBeClickable(driver.FindElement(By.ClassName("table")), 10);;
            var tablex = driver.FindElement(By.ClassName("table"));
            var rowCount = 0;
            if (tablex != null)
            {
                var rows = tablex.FindElements(By.TagName("tr")).ToList(); ;
                var rowIndexes = retVals.CheckBoxNumbers.GetIndexArray();
                rowCount = rows.Count() - 1;
                foreach (var item in rowIndexes)
                {
                    IWebElement checkbox = rows[item].FindElement(By.Name("SelItemIds"));
                    checkbox.Click();
                    rowCount--;
                    if (rowCount <= 0)
                    {
                        break;
                    }
                }
            }
            Utils.Sleep(3000);
            return rowCount.ToString();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return string.Empty;
        }
    }
    public bool RequestInfBox(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            var RequestInforVal = ReadJsonFileForSelectCheckBoxesProcessNewRequest();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var waitBtnReqSelect = driver.WaitForElementToBeClickable(btnReqSelect, 10);  
            Utils.Sleep(4000);
            waitBtnReqSelect.Click();
            Utils.Sleep(3000);
            driver.WaitForElementToBeClickable(txtTitle, 5);
            EnterRequestInfo(RequestInforVal.RequestInformation.Title, RequestInforVal.RequestInformation.Reason);
            Utils.Sleep(3000);
            SubmitReq();
            Utils.Sleep(6000);
            var waitOkBtn = driver.WaitForElementToBeClickable(btnClickOk, 15);
            waitOkBtn.Click();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Sector");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    #region Utility
    public static DataSector ReadJsonFileCreateSector()
    {
        try
        {
            string jsonFileNameSec = "Sector.json";
            string jsonFilePathSec = Path.Combine(desktopPath,
                 "DataConsoleSelenium", jsonFileNameSec);
            if (File.Exists(jsonFilePathSec))
            {
                var jsonContent = File.ReadAllText(jsonFilePathSec);
                var retVal = JsonConvert.DeserializeObject<DataSector>(jsonContent);
                return retVal;
            }
            return new DataSector();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataSector();
        }
    }
    public static Request ReadJsonFileForSelectCheckBoxesProcessNewRequest()
    {
        try
        {
            string jsonFileNamex = "Sector.json";
            string jsonFilePathx = Path.Combine(desktopPath,
                "DataConsoleSelenium", jsonFileNamex);
            if (File.Exists(jsonFilePathx))
            {
                var jsonContent = File.ReadAllText(jsonFilePathx);
                var retVal = JsonConvert.DeserializeObject<Request>(jsonContent);
                return retVal;
            }
            return new Request();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new Request();
        }
    }
   
    #endregion

}