using Commons;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons.Contracts.Login;

namespace LoginAuthentication;



public class LoginConsole
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    public static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddExtDataServices();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        bool login = await _loginService.LoginSuccess();


    }

    //public bool LoginSuccess(IWebDriver driver)
    //{
    //    try
    //    {
    //        JsonFileReader jsonFileReader = new();

    //        var loginVal = jsonFileReader.ReadJsonFileSuccesLogin();

    //        string loginUrl = _URL + "/account/sign-in";
    //        driver.Navigate().GoToUrl(loginUrl);

    //        Sleep(3000);
    //        var loginPage = new LoginPage(driver);

    //        loginPage.EnterUserNameAndPassword(loginVal.LoginParameters.Username, loginVal.LoginParameters.Password);

    //        Sleep(3000);
    //        loginPage.ClickLogin();
    //        Sleep(3000);

    //        var url = _URL + "/dashboard";
    //        driver.Navigate().GoToUrl(url);
    //        Sleep(3000);
    //        return true;
    //    }

    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
    //        return false;
    //    }
    //}

    //public string LoginFailled(IWebDriver driver)
    //{
    //    try
    //    {
    //        JsonFileReader jsonFileReader = new();

    //        var loginVal = jsonFileReader.ReadJsonFileWrongLoginCredential();
    //        string loginUrl = _URL + "/account/sign-in";
    //        driver.Navigate().GoToUrl(loginUrl);

    //        var loginPage = new LoginPage(driver);

    //        loginPage.EnterUserNameAndPassword(loginVal.LoginFailedTest.Username, loginVal.LoginFailedTest.Password);



    //        loginPage.ClickLogin();

    //        var teaxVal = loginPage.divElementText;

    //        Sleep(3000);
    //        if (teaxVal != null)
    //        {
    //            string divText = teaxVal.Text;
    //            return divText;

    //        }

    //        return string.Empty;

    //    }

    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
    //        return string.Empty;
    //    }
    //}

    //public bool LoginEmptyUserName(IWebDriver driver)
    //{
    //    try
    //    {
    //        JsonFileReader jsonFileReader = new();
    //        var loginVal = jsonFileReader.ReadJsonFileEmptyUserNameLoginCredential();
    //        string loginUrl = _URL + "/account/sign-in";
    //        driver.Navigate().GoToUrl(loginUrl);

    //        var loginPage = new LoginPage(driver);

    //        loginPage.EnterUserNameAndPassword(loginVal.LoginEmptyUserName.Username, loginVal.LoginEmptyUserName.Password);
    //        loginPage.ClickLogin();

    //        Sleep(2000);
    //        return driver.Url == _URL + "/dashboard";
    //    }

    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
    //        return false;
    //    }
    //}

    //public bool LoginEmptyPassword(IWebDriver driver)
    //{
    //    try
    //    {
    //        string loginUrl = _URL + "/account/sign-in";
    //        driver.Navigate().GoToUrl(loginUrl);

    //        var loginPage = new LoginPage(driver);

    //        loginPage.EnterUserNameAndPassword("useradmin@xplugng.com", "Pasxw0rd");
    //        loginPage.ClickLogin();

    //        Sleep(2000);
    //        return driver.Url == _URL + "/dashboard";
    //    }

    //    catch (Exception ex)
    //    {
    //        Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
    //        return false;
    //    }
    //}

   
}
