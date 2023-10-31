using Commons.Contracts.Login;
using Contracts.Indicator;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Contracts.Table;

namespace BulkTable;

public class BulkTableConsole
{
    public static async Task  Main(string[] args)
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
            #region Tables
            _sectorService.ClickDataSet(_driver);
            ClickTableCard(_driver);
            _tableService.TableCatalogueSelectorPopUp(_driver);
            Utils.Sleep(3000);
            ClickTableBulk(_driver);
            Utils.Sleep(3000);
            _tableService.TableUploadBulkFile(_driver);
            Utils.Sleep(3000);
            #endregion
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
    public static void ClickTableBulk(IWebDriver driver)
    {
        try
        {
            var tableBulk = driver.FindElement(By.LinkText("Bulk"));
            tableBulk.Click();
            Utils.Sleep(3000);
        }
        catch (Exception ex)
        {
            Utils.LogE(ex.StackTrace, ex.Source, ex.Message);
        }
    }

}