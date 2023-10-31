using Commons.Contracts.CMS.Analytic_Contents;
using Commons.Contracts.CMS.FContent;
using Commons.Contracts.Login;
using Commons;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons.Contracts.CMS.FlashCards;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FlashCard;

public class FlashCardConsole
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _analyticsService = serviceProvider.GetRequiredService<IAnalytics>();
        var _flashService = serviceProvider.GetRequiredService<IFlashCards>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _analyticsService.ClickCMS(_driver);
            Utils.Sleep(3000);
            ClickFlashCard(_driver);
            Utils.Sleep(3000);
            ClickNewFcard(_driver);
            Utils.Sleep(3000);
            PopUpDataSet(_driver);
            Utils.Sleep(3000);
            _flashService.ProcessFlashCardDataSetSelector(_driver);
            Utils.Sleep(3000);
            PopUpTimePeriod(_driver);
            Utils.Sleep(3000);
            _flashService.ProcessTimePeriodFlashCardSelector(_driver);
            Utils.Sleep(5000);
            _driver.Dispose();
        }

    }
    public static void ClickFlashCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Flash Cards"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickNewFcard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/shop/f-cards/add-content?sectorId=-1']"));
            btnNewReq.Click();
            Utils.Sleep(2000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }
    public static void PopUpDataSet(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Dataset")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static void PopUpTimePeriod(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Time-Period")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }


}