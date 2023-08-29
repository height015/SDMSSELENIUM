using System;
using OpenQA.Selenium;

namespace DataSetUiTest;

public class CreateDataSector
{

    private readonly IWebDriver _webDriver;

   
    public CreateDataSector(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }


    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));
    public IWebElement txtTitle => _webDriver.FindElement(By.Id("Title"));
    public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
    public IWebElement textMsgRes => _webDriver.FindElement(By.CssSelector("p[style='display: block;']"));
    public IWebElement btnClickNew => _webDriver.FindElement(By.CssSelector("a.item-button[data-modal='']"));
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));


    public void EnterNameAndTitle(string userName, string password)
    {
        txtName.SendKeys(userName);
        txtTitle.SendKeys(password);
    }

    public void ClickSubmit()
    {
        btnSubmit.Clicks();
    }

    public void ClickClose()
    {
        btnClose.Clicks();
    }

    public void ClickNew()
    {
        btnClickNew.Clicks();
    }

    public void ClickOk()
    {
        btnClickOk.Clicks();
    }

   
}

