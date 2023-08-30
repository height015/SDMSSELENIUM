﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WorkFlow.Reviews;

public class Authorization
{
	private readonly IWebDriver _webDriver;

	public Authorization(IWebDriver webDriver)
	{
		_webDriver = webDriver;

	}

	public IWebElement dropDownCat => _webDriver.FindElement(By.Id("ItemCategory"));
	public IWebElement dropDownType => _webDriver.FindElement(By.Id("ItemType"));

	public IWebElement btnSearch => _webDriver.FindElement(By.CssSelector("div.col-sm-1 a.item-button"));

	public IWebElement tblResult => _webDriver.FindElement(By.CssSelector("table.table-hover")) ?? null;

	public IList<IWebElement> rows => tblResult.FindElements(By.CssSelector("tbody tr"));


	//IList<IWebElement> rows = table.FindElements(By.CssSelector("tbody tr"));

	//int numberOfRows = rows.Count;

	public IWebElement btnReqInfo => _webDriver.FindElement(By.XPath("//button[contains(text(), 'Request Information')]"));
	public IWebElement btnRevInfo => _webDriver.FindElement(By.XPath("//button[contains(text(), 'Review Information')]"));


	public IWebElement txtRevComment => _webDriver.FindElement(By.Id("txtComment"));
	public IWebElement rdBtnApprove => _webDriver.FindElement(By.CssSelector("input[type='radio'][value='1'][name='rbAuthStatus']"));
	public IWebElement rdBtnDecline => _webDriver.FindElement(By.CssSelector("input[type='radio'][value='-1'][name='rbAuthStatus']"));

	public IWebElement btnSubmit => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
	public IWebElement btnClose => _webDriver.FindElement(By.CssSelector("button.btn.btn-secondary[data-dismiss='modal']"));
	public IWebElement btnClickOk => _webDriver.FindElement(By.CssSelector("button.confirm[style*='display: inline-block;'][style*='background-color: rgb(140, 212, 245);']"));

	public void EnterRevComment(string comment)
	{
		txtRevComment.SendKeys(comment);
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