using Contracts.Table;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace Commons.Services.Tables;

public class TablesService : ITables
{

    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    public static string jsonFileName = "Tables.json";
    public static string jsonFilePath = Path.Combine(desktopPath,
        "DataConsoleSelenium", jsonFileName);
    private int reqType = -1;

    private readonly IWebDriver _webDriver;
    public TablesService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public virtual IWebElement dropDownCascadeSecor => _webDriver.FindElement(By.Id("SectorId"));
    public virtual IWebElement dropDownCat => _webDriver.FindElement(By.Id("CategoryId"));
    public virtual IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public virtual IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dataset/tables/add']"));
    public virtual IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
    public virtual IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
    public virtual IWebElement txtDescription => _webDriver.FindElement(By.Id("Description"));
    public virtual IWebElement dropDownFeq => _webDriver.FindElement(By.Id("FrequencyId"));
    public virtual IWebElement dropDownUnit => _webDriver.FindElement(By.Id("UnitId"));
    public virtual IWebElement btnBrowseFile => _webDriver.FindElement(By.Id("fileUpload"));
    public virtual IWebElement btnUpload => _webDriver.FindElement(By.Id("btn_upload"));
    public virtual IWebElement checkBox1 => _webDriver.FindElement(By.Id("ChkDataLabel1"));
    public IWebElement checkBox2 => _webDriver.FindElement(By.Id("ChkDataLabel2"));
    public IWebElement checkBox3 => _webDriver.FindElement(By.Id("ChkDataLabel3"));
    public IWebElement checkBox4 => _webDriver.FindElement(By.Id("ChkDataLabel4"));
    public IWebElement checkBox5 => _webDriver.FindElement(By.Id("ChkDataLabel5"));

    public IWebElement txtDataLab1 => _webDriver.FindElement(By.Id("DataLabel1"));
    public IWebElement txtDataLab2 => _webDriver.FindElement(By.Id("DataLabel2"));
    public IWebElement txtDataLab3 => _webDriver.FindElement(By.Id("DataLabel3"));
    public IWebElement txtDataLab4 => _webDriver.FindElement(By.Id("DataLabel4"));
    public IWebElement txtDataLab5 => _webDriver.FindElement(By.Id("DataLabel5"));

    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

    public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));

    public IWebElement btnUpdate => _webDriver.FindElement(By.Id("btnUpdateLine"));


    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.ClassName("confirm"));
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));
    public IWebElement txtReason => _webDriver.FindElement(By.Id("txtReason"));
    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table")) ?? null;
    public virtual IWebElement btnApply => _webDriver.FindElement(By.PartialLinkText("Apply"));

    // Get all the rows in the table
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();

    public IWebElement dropDownCascadeSector => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement btnConFirm => _webDriver.FindElement(By.ClassName("confirm"));

    public virtual void EnterTableInfoData(string name, string titile, string desc)
    {
        txtBoxName.SendKeys(name);
        txtBoxTitle.SendKeys(titile);
        txtDescription.SendKeys(desc);

    }

    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReason.SendKeys(reason);

    }
    public void Enable(IWebElement webElement)
    {
        webElement.Clicks();
    }

    public void ClickContinue()
    {
        btnContinue.Clicks();
    }

    public void ClickSave()
    {
        btnSave.Clicks();
    }
    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }

    public void ClickUpdate()
    {
        btnUpdate.Clicks();
    }

    public void ClickNew()
    {
        Newlink.Clicks();
    }
    public void ClickOk()
    {
        btnClickOk.Clicks();
    }

    public void TableCatalogueSelectorPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(1000);
            JsonFileReader jsonFileReader = new();
            var retVal = jsonFileReader.ReadJsonFileForTableDataSector();
            dropDownCascadeSecor.SelectDropDownByIndex(retVal.TableDataSelector.DataSectorIndexToSelect);
            Utils.Sleep(5000);
            dropDownCat.SelectDropDownByIndex(retVal.TableDataSelector.DataCategoryIndexToSelect);
            Utils.Sleep(3000);
            ClickContinue();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public void TableCreateNewPopUp(IWebDriver driver)
    {
        try
        {
            Utils.Sleep(1000);
            ClickNew();
            Utils.Sleep(4000);
            JsonFileReader jsonFileReader = new();
            var retVal = ReadJsonFileForNewDataTable();
            driver.WaitForElementToBeClickable(txtBoxName, 10);
            EnterTableInfoData(retVal.TableNewData.Name, retVal.TableNewData.Title, retVal.TableNewData.Description);
            var dropFreq = ReadJsonFileForTableFrequency();
            dropDownFeq.SelectDropDownByIndex(dropFreq.TableFrequency.OptionToSelect);
            var dropUnit = ReadJsonFileForTableUnit();
            dropDownUnit.SelectDropDownByIndex(dropUnit.TableUnit.OptionToSelect);
            var switchBoxControl = ReadJsonFileNewDataTableSettings();
            var txtBoxVal = ReadJsonFileDataTableTxt();
            string textData1;
            string textData2;
            string textData3;
            string textData4;
            string textData5;
            if (switchBoxControl.DataTableSetting.Data1 == true)
            {
                textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                string readonlyAttribute = txtDataLab1.GetAttribute("readonly");
                textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                txtDataLab1.SendKeys(textData1);
                if (readonlyAttribute != null)
                {
                    var switBox = checkBox1;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData1 = txtBoxVal.DataTableTxtVal.Txt1;
                    txtDataLab1.SendKeys(textData1);
                }
            }

            if (switchBoxControl.DataTableSetting.Data2 == true)
            {
                textData2 = txtBoxVal.DataTableTxtVal.Txt2;
                string readonlyAttribute = txtDataLab2.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = checkBox2;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    Utils.Sleep(2000);
                    textData2 = txtBoxVal.DataTableTxtVal.Txt2;
                    txtDataLab2.SendKeys(textData2);
                }
            }

            if (switchBoxControl.DataTableSetting.Data3 == true)
            {
                textData3 = txtBoxVal.DataTableTxtVal.Txt3;
                string readonlyAttribute = txtDataLab3.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = checkBox3;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData3 = txtBoxVal.DataTableTxtVal.Txt3;
                    txtDataLab3.SendKeys(textData3);
                }
            }

            if (switchBoxControl.DataTableSetting.Data4 == true)
            {
                textData4 = txtBoxVal.DataTableTxtVal.Txt4;
                string readonlyAttribute = txtDataLab4.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = checkBox4;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData4 = txtBoxVal.DataTableTxtVal.Txt4;
                    txtDataLab4.SendKeys(textData4);
                }
            }

            if (switchBoxControl.DataTableSetting.Data5 == true)
            {
                textData5 = txtBoxVal.DataTableTxtVal.Txt5;
                string readonlyAttribute = txtDataLab5.GetAttribute("readonly");
                if (readonlyAttribute != null)
                {
                    var switBox = checkBox5;
                    Utils.Sleep(3000);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
                    textData5 = txtBoxVal.DataTableTxtVal.Txt5;
                    txtDataLab5.SendKeys(textData5);
                }
            }
            Utils.Sleep(3000);
            ClickSubmit();
            Utils.Sleep(4000);
            driver.WaitForElementToBeClickable(btnClickOk, 10);
            ClickOk();
            Utils.LogSuccess($"Create", "Tables");

        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public bool TableCreateNewReqPopUp(IWebDriver driver)
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var retVals = jsonFileReader.ReadJsonFileSelectCheckBoxes();
            reqType = retVals.CheckBoxNumbers.RequestType;
            var RequestInforVal = ReadJsonFileForNewRequestTable();
            Utils.Sleep(3000);
            var overlappingDiv = driver.FindElement(By.CssSelector(".col-7.text-right"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.display='none';", overlappingDiv);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            Utils.Sleep(3000);
            var btn = driver.WaitForElementToBeClickable(driver.FindElement(By.Id("btnReqSelect")), 10);
            Utils.Sleep(4000);
            btn.Click();
            Utils.Sleep(3000);
            EnterRequestInfo(RequestInforVal.TableRequestData.Title, RequestInforVal.TableRequestData.Reason);
            Utils.Sleep(2000);
            ClickSave();
            Utils.Sleep(4000);
            var OkBtn = driver.WaitForElementToBeClickable(btnClickOk,20);
            OkBtn.Click();
            string enumString = Enum.GetName(typeof(RequestType), reqType);
            Utils.LogSuccess(enumString, "Tables");
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public void TableUploadBulkFile(IWebDriver driver)
    {
        try
        {
            string fileName = "Table_Template.xlsx";
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
            var retVal = ReadJsonBulkTabe();
            bool applyAll = retVal.BulkTableNewDataCon.ApplyAll;
            var bulkTableNewDataList = retVal.BulkTableNewDataCon.BulkTableNewData;
            var tableRes = table;
            var rowCount = 0;
            if (tableRes != null && applyAll == false)
            {
                rowCount = rows.Count();
                for (int item = 1; item < rowCount; item++)
                {
                    var updateLink = rows[item].FindElement(By.LinkText("Update"));
                    updateLink.Click();
                    Utils.Sleep(3000);
                    dropDownFeq.SelectDropDownByIndex(bulkTableNewDataList[item].FreqIndexToSelect);
                    dropDownUnit.SelectDropDownByIndex(bulkTableNewDataList[item].UnitIndexToSelect);
                    ClickUpdate();
                    Utils.Sleep(2000);
                    var buttonElemt = driver.WaitForElementToBeClickable(btnConFirm, 10);
                    if (buttonElemt.Text == "Ok")
                    {
                        buttonElemt.Click();
                    }
                }
            }
            else if (applyAll)
            {
                var updateLink = rows[1].FindElement(By.LinkText("Update"));
                updateLink.Click();
                Utils.Sleep(3000);
                dropDownFeq.SelectDropDownByIndex(bulkTableNewDataList[1].FreqIndexToSelect);
                dropDownUnit.SelectDropDownByIndex(bulkTableNewDataList[1].UnitIndexToSelect);
                ClickUpdate();
                Utils.Sleep(2000);
                var buttonElemt = driver.WaitForElementToBeClickable(btnConFirm, 10);
                if (buttonElemt.Text == "Ok")
                {
                    buttonElemt.Click();
                }
                var applyBtn = btnApply;
                Utils.Sleep(3000);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", applyBtn);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", applyBtn);
                Utils.Sleep(3000);
                var buttonElemts = driver.WaitForElementToBeClickable(btnConFirm, 10);
                if (buttonElemts.Text == "Ok")
                {
                    buttonElemts.Click();
                }
            }
            Utils.Sleep(4000);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
            ClickSave();
            Utils.Sleep(3000);
            var buttonElemx = driver.WaitForElementToBeClickable(btnConFirm, 10);
            buttonElemx.Click();
            Utils.LogSuccess($"Bulk Table Creation", "Tables");
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

    #region Utility
    private static TableNewDataContainer ReadJsonFileForNewDataTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                TableNewDataContainer retVal = JsonConvert.DeserializeObject<TableNewDataContainer>(jsonContent);
                return retVal;
            }
            return new TableNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableNewDataContainer();
        }
    }

    private static TableFrequencyContainer ReadJsonFileForTableFrequency()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableFrequencyContainer>(jsonContent);
                return retVal;
            }
            return new TableFrequencyContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableFrequencyContainer();
        }
    }
    private static TableUnitContainer ReadJsonFileForTableUnit()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableUnitContainer>(jsonContent);
                return retVal;
            }
            return new TableUnitContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableUnitContainer();
        }
    }
    private static DataTableSettingContainer ReadJsonFileNewDataTableSettings()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<DataTableSettingContainer>(jsonContent);
                return retVal;
            }
            return new DataTableSettingContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataTableSettingContainer();
        }
    }
    private static DataTableTxtValContainer ReadJsonFileDataTableTxt()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableTxtValContainer retVal = JsonConvert.DeserializeObject<DataTableTxtValContainer>(jsonContent);
                return retVal;
            }
            return new DataTableTxtValContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new DataTableTxtValContainer();
        }
    }
    private static TableRequestDataContainer ReadJsonFileForNewRequestTable()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<TableRequestDataContainer>(jsonContent);
                return retVal;
            }
            return new TableRequestDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new TableRequestDataContainer();
        }
    }

    public static BulkTableNewDataContainer ReadJsonBulkTabe()
    {
        try
        {
            string jsonFileName = "Tables.json";
            string jsonFilePath = Path.Combine(desktopPath,
                "DataConsoleSelenium", jsonFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                var retVal = JsonConvert.DeserializeObject<BulkTableNewDataContainer>(jsonContent);
                return retVal;
            }
            return new BulkTableNewDataContainer();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return new BulkTableNewDataContainer();
        }
    }

    #endregion
}