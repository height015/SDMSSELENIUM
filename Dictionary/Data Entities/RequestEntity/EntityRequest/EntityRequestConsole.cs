using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Login;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Sectors;

namespace EntityRequest;

public class EntityRequestConsole
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
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _entityService.ClickDataEntity(_driver);
            Utils.Sleep(3000);
            ClickNewRequest(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickRequestType(_driver);
            Utils.Sleep(3000);
            _entityService.CreateNewReqGenericPopUp(_driver);
            Utils.Sleep(3000);
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