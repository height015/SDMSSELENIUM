using Contracts.Indicator;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Commons.Services.Indicator;

public class IndicatorService : IIndicator
{
    private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private const string jsonFileName = "Indicator.json";
    private static string jsonFilePath = Path.Combine(desktopPath,
        "DataConsoleSelenium", jsonFileName);
    private int reqType = -1;
    private readonly IWebDriver _webDriver;
    public IndicatorService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement dropDownCascadeSecor => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement dropDownCat => _webDriver.FindElement(By.Id("CategoryId"));
    public IWebElement dropDownTable => _webDriver.FindElement(By.Id("TableId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dataset/indicators/add']"));
    public IWebElement IndiComboTree => _webDriver.FindElement(By.CssSelector("div#comboTree846411InputWrapper input.comboTreeInputBox"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement checkBoxEmph => _webDriver.FindElement(By.Id("IsEmphasized"));
    public IWebElement displayOrder => _webDriver.FindElement(By.Id("DisplayOrder"));
    public IWebElement DisplayInChart => _webDriver.FindElement(By.Id("ChkGraphTitle"));
    public IWebElement txtGrapTit => _webDriver.FindElement(By.Id("GraphTitle"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.ClassName("confirm"));
    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));

    public virtual IWebElement btnBrowseFile => _webDriver.FindElement(By.Id("fileUpload"));
    public virtual IWebElement btnUpload => _webDriver.FindElement(By.Id("btn_upload"));
    public IWebElement btnUpdate => _webDriver.FindElement(By.Id("btnUpdateLine"));
    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table"));

    // Get all the rows in the table
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();
    public virtual IWebElement btnApply => _webDriver.FindElement(By.PartialLinkText("Apply"));

    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));

    public IWebElement chkBtnDisplayInChart => _webDriver.FindElement(By.Id("ChkGraphTitle"));

    public IWebElement txtDisplayInChart => _webDriver.FindElement(By.Id("GraphTitle"));
    public IWebElement btnUpdateModify => _webDriver.FindElement(By.Id("btnSaveLineItem"));
    public IWebElement comboTree => _webDriver.FindElement(By.ClassName("comboTreeDropDownContainer"));
    public IWebElement liTree => comboTree.FindElement(By.TagName("li"));

    public IWebElement txtTopLevelBox => _webDriver.FindElement(By.Id("topLevelInd"));
    public IList<IWebElement> boxSel => _webDriver.FindElements(By.CssSelector("span.comboTreeItemTitle.selectable"));
    public IWebElement dropDownCascadeSector => throw new NotImplementedException();
    public IWebElement btnReqSelect => _webDriver.FindElement(By.Id("btnReqSelect"));
    public IWebElement btnConFirm => _webDriver.FindElement(By.ClassName("confirm"));

    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);

    }
    public void ClickContinue()
    {
        btnContinue.Clicks();
    }

    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }

    public void ClickNew()
    {
        Newlink.Clicks();
    }

    public void ClickSave()
    {
        btnSave.Clicks();
    }

    public void ClickOk()
    {
        btnClickOk.Clicks();
    }
    public bool CreateNewReqIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            var RequestInforVal = ReadJsonFileForNewRequestIndicator();
            Utils.Sleep(3000);
            IWebElement overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var waitBtn = driver.WaitForElementToBeClickable(btnReqSelect, 10);
            Utils.Sleep(4000);
            waitBtn.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.RequestData.Title, RequestInforVal.RequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(8000);
            var btnOk = driver.WaitForElementToBeClickable(btnConFirm,15);
            btnOk.Click();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Indicator");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

    public void ClickIndicators(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Indicators"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public void IndicatorCataloguePopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileDataIndicator();
            Utils.Sleep(2000);
            dropDownCascadeSecor.SelectDropDownByIndex(retVal.IndicatorDataSelector.SectorIndex);
            Utils.Sleep(2000);
            dropDownCat.SelectDropDownByIndex(retVal.IndicatorDataSelector.CategoryIndex);
            Utils.Sleep(2000);
            dropDownTable.SelectDropDownByIndex(retVal.IndicatorDataSelector.TableIndex);
            Utils.Sleep(3000);
            ClickContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public void CreateNewDataIndicatorPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(2000);
            ClickNew();
            var retVal = ReadJsonFileNewDataIndicator();
            Utils.Sleep(4000);
            string textName = retVal.NewDataIndicator.Name;
            txtBoxName.SendKeys(textName);
            string textTit = retVal.NewDataIndicator.Title;
            txtBoxTitle.SendKeys(textTit);
            if (retVal.NewDataIndicator.EmboldenIndicatorTitle == true)
            {
                Utils.Sleep(3000);
                var switBox = checkBoxEmph;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
            }
            int dispOrd = retVal.NewDataIndicator.DisplayOrder;
            displayOrder.Clear();
            displayOrder.SendKeys(dispOrd.ToString());
            if (retVal.NewDataIndicator.DisplayInChart == true)
            {
                var switBox = DisplayInChart;
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                Utils.Sleep(3000);
                var texttoDispay = retVal.NewDataIndicator.TitleToDisplay;
                txtGrapTit.SendKeys(texttoDispay);
            }
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(3000);
            ClickOk();
            Utils.LogSuccess($"Create {textName}", "Indicator");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
        }
    }
    public void IndicatorUploadBulkFile(IWebDriver driver)
    {
        try
        {
            string fileName = "Indicator_Today.xlsx";
            string filePath = Path.Combine(desktopPath, "SeleniumTest", fileName);
            Utils.Sleep(3000);
            btnBrowseFile.SendKeys(filePath);
            btnUpload.Click();
            Utils.Sleep(3000);
            var buttonElem = driver.WaitForElementToBeClickable(btnConFirm, 10);
            if (buttonElem.Text == "Ok")
            {
                buttonElem.Click();
            }
            Utils.Sleep(3000);
            var retVal = ReadJsonBulkIndicator();
            bool modify = retVal.BulkIndicatorNewDataCon.Modify;
            var bulkTableNewDataList = retVal.BulkIndicatorNewDataCon.BulkIndicatorNewData;
            var tableRes = table;
            var rowCount = 0;
            if (modify)
            {
                if (tableRes != null)
                {
                    rowCount = rows.Count();
                    for (int item = 1; item < rowCount; item++)
                    {
                        IWebElement updateLink = rows[item].FindElement(By.LinkText("Modify"));
                        updateLink.Click();
                        Utils.Sleep(3000);
                        displayOrder.SendKeys(bulkTableNewDataList[item].DisplayOrder.ToString());
                        if (bulkTableNewDataList[item].DisplayInChart == true)
                        {
                            Utils.Sleep(3000);
                            var switBox = DisplayInChart;
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                            Utils.Sleep(2000);
                            txtGrapTit.SendKeys(bulkTableNewDataList[item].GraphTitle);
                        }
                        Utils.Sleep(2000);
                        driver.WaitForElementToBeClickable(btnConFirm, 10);
                        btnConFirm.Click();
                    }
                }
            }
            if (txtTopLevelBox != null)
            {
                txtTopLevelBox.Click();
                int indexToSelect = retVal.BulkIndicatorNewDataCon.indexToSelect;
                Utils.Sleep(3000);
                boxSel[indexToSelect].Click();
            }
            Utils.Sleep(3000);
            Utils.LogSuccess($"Bulk Indicator Creation", "Indicator");
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
        finally
        {
            Utils.Sleep(3000);
            btnSave.Click();
            Utils.Sleep(4000);
            var btnOk = driver.WaitForElementToBeClickable(btnConFirm, 15);
            btnOk.Click();
        }
    }

    #region Utility
    public NewDataIndicatorContainer ReadJsonFileNewDataIndicator()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<NewDataIndicatorContainer>(jsonContent);
                return retVal;
            }
            return new NewDataIndicatorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return new NewDataIndicatorContainer();
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
    private static BulkIndicatorNewDataContainer ReadJsonBulkIndicator()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkIndicatorNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkIndicatorNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new BulkIndicatorNewDataContainer();
        }

    }

    #endregion
}