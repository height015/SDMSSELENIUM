using Commons;
using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Dictionary.Frequency;
using Commons.Contracts.Dictionary.Units;
using Commons.Contracts.Login;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewUnit;

public class NewUnitConsole
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        //Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _entityService = serviceProvider.GetRequiredService<IDataEntities>();
        var _unitService = serviceProvider.GetRequiredService<IUnits>();
        bool login = await _loginService.LoginSuccess();

        if (login)
        {
            Utils.Sleep(3000);
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _unitService.ClickUnit(_driver);
            Utils.Sleep(3000);
            _unitService.ClickUnitNew(_driver);
            Utils.Sleep(3000);
            _unitService.UnitDataEntry(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }
}