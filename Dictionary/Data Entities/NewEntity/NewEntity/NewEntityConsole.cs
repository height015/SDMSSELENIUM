using Commons.Contracts.Login;
using Commons;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons.Contracts.Dictionary.DataEntities;

namespace NewEntity;

public class NewEntityConsole
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _entityService = serviceProvider.GetRequiredService<IDataEntities>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _entityService.ClickDataEntity(_driver);
            Utils.Sleep(3000);
            _entityService.ClickDataEntityNewRequest(_driver);
            Utils.Sleep(3000);
            _entityService.DataEntityEntry(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }

    }
}