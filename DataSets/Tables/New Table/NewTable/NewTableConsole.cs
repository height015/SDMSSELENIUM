using Commons.Contracts.Login;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Table;

namespace NewTable;
public class NewTableConsole
{
    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        var _tableService = serviceProvider.GetRequiredService<ITables>();
        bool login = await _loginService.LoginSuccess();
        if (login)
        {
            _sectorService.ClickDataSet(_driver);
            Utils.Sleep(3000);
            ClickTableCard(_driver);
            Utils.Sleep(3000);
            _tableService.TableCatalogueSelectorPopUp(_driver);
            Utils.Sleep(3000);
            _tableService.TableCreateNewPopUp(_driver);
            Utils.Sleep(6000);
            _driver.Dispose();

        }

    }
    public static void ClickTableCard(IWebDriver driver)
    {
        try
        {
            var tableCard = driver.FindElement(By.LinkText("Tables"));
            tableCard.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

}