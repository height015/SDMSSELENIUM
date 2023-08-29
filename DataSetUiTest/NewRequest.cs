using System;
using OpenQA.Selenium;

namespace DataSetUiTest;

public class NewRequest
{

    private readonly IWebDriver _webDriver;

   
    public NewRequest(IWebDriver webDriver)
    {
        _webDriver = webDriver;

    }

    public IWebElement txtTitle => _webDriver.FindElement(By.Id("txtTitle"));

    public IWebElement txtReson => _webDriver.FindElement(By.Id("txtReason"));


    public IWebElement btnSubmit => _webDriver.FindElement(By.Id("btnSave"));


    public IWebElement table => _webDriver.FindElement(By.ClassName("table"));

    // Get all the rows in the table
    public List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList();


    public IWebElement btnProcessSelected => _webDriver.FindElement(By.Id("btnReqSelect"));


    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));


    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));


    public void EnterRequestInfo(string title, string reason)
    {
        txtTitle.SendKeys(title);
        txtReson.SendKeys(reason);
    }

    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }

    public void ClickClose()
    {
        btnClose.Clicks();
    }

    public void ProcessSelectedCheckedBoxes()
    {
        btnProcessSelected.Clicks();
    }

    public void ClickOk()
    {
        btnClickOk.Clicks();
    }


}

