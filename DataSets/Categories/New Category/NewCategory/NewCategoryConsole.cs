using Commons.Contracts.Login;
using Commons;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Contracts.Category;

namespace NewCategory;

public class NewCategoryConsole
{
    static void Main(string[] args)
    {

        var classObj = new NewCategoryConsole();
        var services = new ServiceCollection();
        services.AddExtDataServices();
        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        var _categoryService = serviceProvider.GetRequiredService<ICategory>();
        bool login = _loginService.LoginSuccess();
        if (login)
        {
            _categoryService.ClickDataCatalogCard(_driver);
            Utils.Sleep(3000);
            _categoryService.ClickCategoryCard(_driver);
            Utils.Sleep(3000);
            _categoryService.ClickNewDataCategoryButton(_driver);
        }
    }
}
