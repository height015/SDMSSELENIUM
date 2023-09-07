﻿using System;
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


    public virtual IWebElement table => _webDriver.FindElement(By.ClassName("table")) ?? null;

    // Get all the rows in the table
    public virtual List<IWebElement> rows => table.FindElements(By.TagName("tr")).ToList() ?? null;


    public virtual IWebElement btnProcessSelected => _webDriver.FindElement(By.Id("btnReqSelect"));


    public virtual IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector(".sa-confirm-button-container .confirm"));


    public virtual IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));


    public virtual void EnterRequestInfo(string title, string reason)
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

