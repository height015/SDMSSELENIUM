using System.Net;
using LoginUiTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
				ClickAuthorization(driver);
				Wait(3000);

				Wait(3000);

				Wait(2000);

				Wait(2000);

			}







		}

	}

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

	public static void ClickAuthorization(IWebDriver driver)
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

			}



			//string textName = retVal.NewDataIndicator.Name;
			//auth.txtBoxName.SendKeys(textName);

			//string textTit = retVal.NewDataIndicator.Title;
			//auth.txtBoxTitle.SendKeys(textTit);

			//if (retVal.NewDataIndicator.EmboldenIndicatorTitle == true)
			//{

			//	Wait(3000);
			//	var switBox = inidi.checkBoxEmph;
			//	((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
			//	((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
			//}

			//int dispOrd = retVal.NewDataIndicator.DisplayOrder;
			//auth.displayOrder.Clear();
			//auth.displayOrder.SendKeys(dispOrd.ToString());

			//if (retVal.NewDataIndicator.DisplayInChart == true)
			//{
			//	var switBox = inidi.DisplayInChart;
			//	((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", switBox);
			//	((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", switBox);
			//	Wait(3000);
			//	var texttoDispay = retVal.NewDataIndicator.TitleToDisplay;
			//	inidi.txtGrapTit.SendKeys(texttoDispay);
			//}

			//Wait(3000);
			//inidi.ClickSubmit();
			//Wait(3000);
			//inidi.ClickOk();

		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
		}
	}

	private static void Wait(int time)
	{
		Thread.Sleep(time);
	}
}