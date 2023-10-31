using Commons.Contracts.CMS.Analytic_Contents;
using Commons.Contracts.CMS.FlashCards;
using Commons.Contracts.CMS.QuickFlash;
using Commons.Contracts.Login;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;


namespace QuickFlash;

public class Program
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
        var _quickFlashService = serviceProvider.GetRequiredService<IQuickFlashes>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _analyticsService.ClickCMS(_driver);
            Utils.Sleep(3000);
            ClickQuickFlashCard(_driver);
            Utils.Sleep(3000);
            ClickNewQuickFlashCard(_driver);
            Utils.Sleep(3000);
            _quickFlashService.ProcessNewQFlash(_driver);
        }
    }

    public static void ClickQuickFlashCard(IWebDriver driver)
    {
        try
        {
            var dataSetLink = driver.FindElement(By.LinkText("Quick Flashes"));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }
    public static bool ClickNewQuickFlashCard(IWebDriver driver)
    {
        try
        {
            var btnNewReq = driver.FindElement(By.XPath("//a[contains(@href, '/shop/q-flashes/add')]"));
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
}