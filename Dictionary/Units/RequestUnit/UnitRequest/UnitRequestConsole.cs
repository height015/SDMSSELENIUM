using Commons;
using Commons.Contracts.Dictionary.DataEntities;
using Commons.Contracts.Dictionary.Units;
using Commons.Contracts.Login;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace UnitRequest;

public class UnitRequestConsole
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
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            Utils.Sleep(3000);
            _entityService.ClickDictionary(_driver);
            Utils.Sleep(3000);
            _unitService.ClickUnit(_driver);
            Utils.Sleep(3000);
            ClickNewRequest(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickRequestType(_driver);
            _unitService.CreateNewReqGenericPopUp(_driver);
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
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

}