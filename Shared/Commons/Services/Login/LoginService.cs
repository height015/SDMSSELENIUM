using Commons.Contracts.Login;
using OpenQA.Selenium;

namespace Commons.Services.Login;

public class LoginService : ILogin
{
    private static readonly string _URL = "http://197.255.51.104:9035";

    private readonly IWebDriver _webDriver;

    public LoginService(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
    public IWebElement txtEmail => _webDriver.FindElement(By.Id("txtUsername"));
    public IWebElement txtPassword => _webDriver.FindElement(By.Id("txtPassword"));
    public IWebElement btnLogin => _webDriver.FindElement(By.XPath("//input[@type='submit']"));
    public IWebElement divElementText => _webDriver.FindElement(By.ClassName("alert-danger"));
    public void EnterUserNameAndPassword(string userName, string password)
    {
        txtEmail.SendKeys(userName);
        txtPassword.SendKeys(password);
    }
    public void ClickLogin()
    {
        btnLogin.Clicks();
    }

    public bool LoginSuccess()
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSuccesLogin();
            string loginUrl = _URL + "/account/sign-in";
            _webDriver.Navigate().GoToUrl(loginUrl);
            Utils.Sleep(3000);
            EnterUserNameAndPassword(loginVal.LoginParameters.Username, loginVal.LoginParameters.Password);
            Utils.Sleep(3000);
            ClickLogin();
            Utils.Sleep(3000);
            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }
}

