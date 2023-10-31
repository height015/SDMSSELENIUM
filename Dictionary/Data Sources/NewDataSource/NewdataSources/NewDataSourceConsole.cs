using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Login;
using Commons;
using Contracts.Sectors;
using OpenQA.Selenium;
using Microsoft.Extensions.DependencyInjection;
using Commons.Contracts.Dictionary.DataSource;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewdataSources;

public class NewDataSourceConsole
{
   public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        //Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        var _entityService = serviceProvider.GetRequiredService<IDataEntities>();
        var _dataSourceService = serviceProvider.GetRequiredService<IDataSources>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _dataSourceService.ClickDataSource(_driver);
            Utils.Sleep(4000);
            ClickDataSourceNewRequest(_driver);
            Utils.Sleep(3000);
            _dataSourceService.DataSourceEntry(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }

    }

    public static bool ClickDataSourceNewRequest(IWebDriver driver)
    {
        try
        {
            var newReqBtn = driver.FindElement(By.PartialLinkText("New"));
            newReqBtn.Click();
            Utils.Sleep(3000);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
            return false;
        }
    }

}