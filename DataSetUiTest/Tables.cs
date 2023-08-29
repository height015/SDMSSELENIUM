using System;
using OpenQA.Selenium;

namespace DataSetUiTest;

public class Tables
{

	private readonly IWebDriver _webDriver;

	public Tables(IWebDriver webDriver)
	{
		_webDriver = webDriver;

	}

    public IWebElement dropDownCascadeSecor => _webDriver.FindElement(By.Id("SectorId"));
    public IWebElement dropDownCat => _webDriver.FindElement(By.Id("CategoryId"));
    public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnContinue"));
    public IWebElement Newlink => _webDriver.FindElement(By.CssSelector("a[href^='/dataset/tables/add']"));
    public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
	public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));
	public IWebElement txtDescription => _webDriver.FindElement(By.Id("Description"));
	public IWebElement dropDownFeq => _webDriver.FindElement(By.Id("FrequencyId"));
	public IWebElement dropDownUnit => _webDriver.FindElement(By.Id("UnitId"));

	public IWebElement checkBox1 => _webDriver.FindElement(By.Id("ChkDataLabel1"));
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
	public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
	public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));

	public void EnterTableInfoData(string name, string titile, string desc)
	{
		txtBoxName.SendKeys(name);
		txtBoxTitle.SendKeys(titile);
        txtDescription.SendKeys(desc);

    }
    public void Enable(IWebElement webElement)
    {
        webElement.Clicks();
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
	public void ClickOk()
	{
		btnClickOk.Clicks();
	}


}

