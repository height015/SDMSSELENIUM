using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace CMSUI;

public class Analytics
{

	private readonly IWebDriver _webDriver;

	public Analytics(IWebDriver webDriver)
	{
		_webDriver = webDriver;

	}

	public IWebElement dropDownSector => _webDriver.FindElement(By.Id("DSectorId"));
	public IWebElement dropDownCategory => _webDriver.FindElement(By.Id("DCategoryId"));
	public IWebElement dropDownTable => _webDriver.FindElement(By.Id("DTableId"));
	public IWebElement btnContinue => _webDriver.FindElement(By.Id("btnDSContinue"));


	public IWebElement btnContinueSelection => _webDriver.FindElement(By.Id("btnContSelection"));


	public IWebElement OpenStartDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStartPeriod"));
	public IWebElement OpenStopDateDrpDwn => _webDriver.FindElement(By.Id("txtAnnualStopPeriod"));

	public IWebElement readonlyInput => _webDriver.FindElement(By.CssSelector("input[type='text'][aria-describedby='secname'][readonly='']"));



	//IWebElement desiredDate = driver.FindElement(By.XPath("//td[text()='24']")); // Example XPath for the 24th day
	//desiredDate.Click();




	public IWebElement txtBoxName => _webDriver.FindElement(By.Id("Name"));
	public IWebElement txtBoxTitle => _webDriver.FindElement(By.Id("Title"));

	public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));

	public IWebElement btnSave => _webDriver.FindElement(By.Id("btnSave"));

	public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
	public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));

	public IWebElement table => _webDriver.FindElement(By.Id("lvsIndicators")).FindElement(By.TagName("table")) ?? null; 
	
	// Get all the rows in the table
	public List<IWebElement> rows => table.FindElements(By.XPath(".//tbody/tr")).ToList();

	public void ClickContinue()
	{
		btnContinue.Clicks();
	}

	public void ClickContinueDate()
	{
		btnContinueSelection.Clicks();
	}

	public void ClickSave()
	{
		btnSave.Clicks();
	}
	public void ClickSubmit()
	{
		btnSubmit.Clicks();
	}
	
	public void ClickOk()
	{
		btnClickOk.Clicks();
	}


}
