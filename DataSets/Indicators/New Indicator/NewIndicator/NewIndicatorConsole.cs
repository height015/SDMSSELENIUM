using Commons.Contracts.Login;
using Contracts.Category;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Indicator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewIndicator;

public class NewIndicatorConsole
{
    public static async Task Main(string[] args)
    {
        var classObj = new NewIndicatorConsole();
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
            _indicatorService.CreateNewDataIndicatorPopUp(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }
}