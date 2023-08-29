
using OpenQA.Selenium;

namespace DataSetUiTest;

public class Indicator
{

    private readonly IWebDriver _webDriver;


    public Indicator(IWebDriver webDriver)
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
    public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));

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

    public void ClickOk()
    {
        btnClickOk.Clicks();
    }


}

