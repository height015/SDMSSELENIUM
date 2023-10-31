using Commons.Contracts.Login;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Sectors;

namespace NewSector;

public class NewSectorConsole
{
    public static async Task Main(string[] args)
    {
        var classObj = new NewSectorConsole();
        var services = new ServiceCollection();
        services.AddExtDataServices();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        bool login = await _loginService.LoginSuccess();
       
        if (login)
        {
            _sectorService.ClickDataSet(_driver);
            Utils.Sleep(2000);
            _sectorService.ClickSector(_driver);
            Utils.Sleep(2000);
            _sectorService.CreateNewDataSectorSuccess(_driver);
            Utils.Sleep(3000);
            _driver.Quit();

        }

    }




   
}