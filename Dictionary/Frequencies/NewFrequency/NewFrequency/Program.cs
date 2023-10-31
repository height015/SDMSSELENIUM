using Commons;
using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Dictionary.Frequency;
using Commons.Contracts.Login;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace NewFrequency;

public class Program
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
        var _frequencyService = serviceProvider.GetRequiredService<IFrequency>();

        //IFrequency
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _frequencyService.ClickFrequency(_driver);
            Utils.Sleep(4000);
            _frequencyService.NewFrequency(_driver);
            Utils.Sleep(3000);
            _frequencyService.DataEntryFrequency(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }
}