using Commons.Contracts.Login;
using Commons;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Contracts.Category;

namespace NewCategory;

public class NewCategoryConsole
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _categoryService = serviceProvider.GetRequiredService<ICategory>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _categoryService.ClickDataCatalogCard(_driver);
            Utils.Sleep(3000);
            _categoryService.ClickCategoryCard(_driver);
            Utils.Sleep(3000);
            _categoryService.ClickNewDataCategoryButton(_driver);
            Utils.Sleep(3000);
            _driver.Dispose();
        }
    }
}
