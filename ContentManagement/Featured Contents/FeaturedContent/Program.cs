using Commons.Contracts.CMS.Analytic_Contents;
using Commons.Contracts.Login;
using Commons;
using Contracts.Category;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons.Contracts.CMS.FContent;

namespace FeaturedContent;

public class Program
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _analyticsService = serviceProvider.GetRequiredService<IAnalytics>();
        var _fContentService = serviceProvider.GetRequiredService<IFeaturedContents>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _analyticsService.ClickCMS(_driver);
            Utils.Sleep(3000);
            _fContentService.ClickFeaturedContentCard(_driver);
            Utils.Sleep(3000);
            _fContentService.ClickNewFContentCard(_driver);
            Utils.Sleep(3000);
            _fContentService.PopUpDataSet(_driver);
            Utils.Sleep(3000);
            _fContentService.ProcessFContentDataSetSelector(_driver);
            Utils.Sleep(3000);
            _fContentService.PopUpTimePeriod(_driver);
            Utils.Sleep(3000);
            _fContentService.ProcessTimePeriodFContentSelector(_driver);
            Utils.Sleep(5000);
            _driver.Dispose();
        }
    }
}