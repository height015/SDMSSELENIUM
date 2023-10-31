using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Dictionary.DataSource;
using Commons.Contracts.Login;
using Commons;
using Contracts.Sectors;
using OpenQA.Selenium;
using Microsoft.Extensions.DependencyInjection;

namespace DataSourceRequest;

public class DataSourceConsole
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
            ClickNewRequest(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickRequestType(_driver);
            _dataSourceService.CreateNewReqGenericPopUp(_driver);
            Utils.Sleep(4000);
            _driver.Dispose();
        }
    }
    public static bool ClickNewRequest(IWebDriver driver)
    {
        try
        {
            var dataSetLinkNewReq = driver.FindElement(By.CssSelector("a.item-button[href*='/workflow/requests/requests?reqType=1'][onclick*='showLoader()']"));
            dataSetLinkNewReq.Click();
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