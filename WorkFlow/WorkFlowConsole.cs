﻿using System.Net;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WorkFlow.Reviews.Approvals;

public class WorkFlowConsole
{
	public static void Main(string[] args)
	{
		using (var driver = new ChromeDriver())
		{
			var loginObj = new LoginMain();

			var data = new WorkFlowConsole();

			bool login = loginObj.LoginSuccess(driver);

			if (login)
			{
				ClickWorkFlow(driver);
				Wait(3000);
				//ClickReviewAuthorization(driver);
				//Wait(3000);
				//CreateNewDataIndicatorPopUp(driver);
				//Wait(3000);
				//ClickWorkFlow(driver);

				ClickApprovalsAuthorization(driver);
				ClickIndicatorPopUp(driver);
				Wait(3000);
			}
		}

	}
	
	
	#region Reviews 

	public static void ClickWorkFlow(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Workflow Mgt."));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void ClickReviewAuthorization(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.LinkText("Authorization"));
			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}
	public static void CreateNewDataIndicatorPopUp(IWebDriver driver)
	{
		try
		{
			var auth = new WorkFlow.Reviews.Authorization(driver);

			Wait(2000);



			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonFileWorkFlowSelection();



			var dropdownCat = new SelectElement(auth.dropDownCat);

			dropdownCat.SelectByIndex(retVal.WorkFlowSelection.CategoryIndex);

			var sourceType = new SelectElement(auth.dropDownType);

			sourceType.SelectByIndex(retVal.WorkFlowSelection.SourceTypeIndex);


			var table = auth.tblResult;

			if (table != null)
			{
				var rows = auth.rows;
				var btnRow = retVal.WorkFlowSelection.RoleIndex;

				if (btnRow >= 0 && btnRow < rows.Count)
				{
					IWebElement desiredRow = rows[btnRow];
					//IWebElement viewLink = desiredRow.FindElement(By.CssSelector("a[title='View Detail']"));
					//viewLink.Click();

					IWebElement actionsButton = desiredRow.FindElement(By.CssSelector("button[data-toggle='dropdown']"));
					actionsButton.Click();
					Wait(2000);

					IWebElement RevBoxPopUp = desiredRow.FindElement(By.CssSelector("a[title='Review Item']"));
					RevBoxPopUp.Click();
					Wait(3000);

					var retCom = jsonFileReader.ReadJsonFileWorkFlowReview();
					auth.EnterRevComment(retCom.ReviewSelection.Comment);
					if (retCom.ReviewSelection.Status == true)
					{
						auth.rdBtnApprove.Click();
					}
					else
					{
						auth.rdBtnDecline.Click();

					}
					Wait(2000);
					auth.ClickSubmit();
					Wait(3000);
					auth.ClickOk();
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	#endregion

	#region Approvals   

	public static void ClickApprovalsAuthorization(IWebDriver driver)
	{
		try
		{
			var dataSetLink = driver.FindElement(By.CssSelector("a.card[href*='/workflow/processes/pending-approvals?reqType=1']"));

			dataSetLink.Click();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	public static void ClickIndicatorPopUp(IWebDriver driver)
	{
		try
		{
			var auth = new AuthorizationApp(driver);
			Wait(2000);
			JsonFileReader jsonFileReader = new();

			var retVal = jsonFileReader.ReadJsonFileWorkFlowSelection();
			var dropdownCat = new SelectElement(auth.dropDownCat);

			dropdownCat.SelectByIndex(retVal.WorkFlowSelection.CategoryIndex);
			var sourceType = new SelectElement(auth.dropDownType);
			sourceType.SelectByIndex(retVal.WorkFlowSelection.SourceTypeIndex);

			var table = auth.tblResult;
			if (table != null)
			{
				var rows = auth.rows;
				var btnRow = retVal.WorkFlowSelection.RoleIndex;
				if (btnRow >= 0 && btnRow < rows.Count)
				{
					IWebElement desiredRow = rows[btnRow];
					//IWebElement viewLink = desiredRow.FindElement(By.CssSelector("a[title='View Detail']"));
					//viewLink.Click();

					IWebElement actionsButton = desiredRow.FindElement(By.CssSelector("button[data-toggle='dropdown']"));
					actionsButton.Click();
					Wait(2000);
					IWebElement RevBoxPopUp = desiredRow.FindElement(By.CssSelector("a[title='Approve Item']"));
					RevBoxPopUp.Click();
					Wait(3000);

					var retCom = jsonFileReader.ReadJsonFileWorkFlowReview();
					auth.EnterRevComment(retCom.ReviewSelection.Comment);
					if (retCom.ReviewSelection.Status == true)
					{
						auth.rdBtnApprove.Click();
					}
					else
					{
						auth.rdBtnDecline.Click();
					}

					Wait(2000);
					auth.ClickSubmit();
					Wait(3000);
					auth.ClickOk();
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	#endregion
	
	private static void Wait(int time)
	{
		Thread.Sleep(time);
	}
}