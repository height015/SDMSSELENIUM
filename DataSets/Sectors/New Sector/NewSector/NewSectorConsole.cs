using Commons.Contracts.Login;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Sectors;

namespace NewSector;

public class NewSectorConsole
{
    public static void Main(string[] args)
    {
        var classObj = new NewSectorConsole();
        var services = new ServiceCollection();
        services.AddExtDataServices();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        bool login = _loginService.LoginSuccess();
       
        if (login)
        {
            _sectorService.ClickDataSet(_driver);
            _sectorService.ClickSector(_driver);
            _sectorService.CreateNewDataSectorSuccess(_driver);
        }

    }




   
}