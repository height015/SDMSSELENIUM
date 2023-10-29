using Commons;
using Commons.Contracts.Login;
using Contracts.Category;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CategoryRequest;

public class CategoryRequestConsole
{
    public static void Main(string[] args)
    {

        var classObj = new CategoryRequestConsole();
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
            Utils.Sleep(3000);
            _categoryService.ClickDataCatalogCard(_driver);
            Utils.Sleep(3000);
            _categoryService.ClickCategoryCard(_driver);
            _categoryService.ClickNewRequest(_driver);
            _sectorService.ClickRequestType(_driver);
            Utils.Sleep(3000);
            _categoryService.CategoryRequestInfBox(_driver);
        }
    }
}