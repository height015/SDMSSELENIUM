using Commons.Contracts.Login;
using Contracts.Indicator;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;

namespace Bulkindicator;

public class BulkIndicatorConsole
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        var _indicatorService = serviceProvider.GetRequiredService<IIndicator>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _sectorService.ClickDataSet(_driver);
            Utils.Sleep(3000);
            _indicatorService.ClickIndicators(_driver);
            Utils.Sleep(3000);
            _indicatorService.IndicatorCataloguePopUp(_driver);
            Utils.Sleep(3000);
            ClickIndicatorBulk(_driver);
            Utils.Sleep(3000);
            _indicatorService.IndicatorUploadBulkFile(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }

    public static void ClickIndicatorBulk(IWebDriver driver)
    {
        try
        {
            var tableBulk = driver.FindElement(By.LinkText("Bulk"));
            tableBulk.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

}