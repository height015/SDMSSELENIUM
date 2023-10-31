using Commons;
using Commons.Contracts.Login;
using Contracts.Indicator;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace IndicatorRequest;
public class IndicatorRequestConsole
{
    public static async Task Main(string[] args)
    {
        var classObj = new IndicatorRequestConsole();
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
            ClickNewRequest(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickRequestType(_driver);
            Utils.Sleep(3000);
            _indicatorService.CreateNewReqIndicatorPopUp(_driver);
            Utils.Sleep(3000);
            ClickClose(_driver);
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

    public static void ClickClose(IWebDriver driver)
    {
        try
        {
            var closeBtn = driver.FindElement(By.CssSelector("a[href='/dataset/indicators']"));
            closeBtn.Click();
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

}