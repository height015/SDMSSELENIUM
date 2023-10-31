using Commons.Contracts.Login;
using Commons;
using Contracts.Category;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons.Contracts.CMS.Analytic_Contents;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AnalyticsCMS;

public class AnalyticsConsole
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        var _categoryService = serviceProvider.GetRequiredService<ICategory>();
        var _analyticsService = serviceProvider.GetRequiredService<IAnalytics>();        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            
            _analyticsService.ClickCMS(_driver);
            Utils.Sleep(3000);
            _analyticsService.ClickAnalyticCard(_driver);
            Utils.Sleep(3000);
            _analyticsService.ClickNew(_driver);
            Utils.Sleep(3000);
            PopUpDataSet(_driver);
            Utils.Sleep(3000);
            _analyticsService.ProcessDataSetSelector(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }
    public static void PopUpDataSet(IWebDriver driver)
    {
        try
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var dataSetLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Dataset - Period Selector")));
            dataSetLink.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

}