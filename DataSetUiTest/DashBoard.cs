using System;
using OpenQA.Selenium;

namespace DataSetUiTest;

public class DashBoard
{
    private readonly IWebDriver _webDriver;

    public DashBoard(IWebDriver webDriver)
    {
        _webDriver = webDriver;

    }

    public IWebElement txtName => _webDriver.FindElement(By.Id("Name"));

    public IWebElement txtEmail => _webDriver.FindElement(By.Name("txtUsername"));

    public IWebElement txtPassword => _webDriver.FindElement(By.Id("txtPassword"));


    public IWebElement btnLogin => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

    public IWebElement divElementText => _webDriver.FindElement(By.ClassName("alert-danger"));

 
}

