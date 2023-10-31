using System.Net;
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

    public async Task<bool> LoginSuccess()
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var loginVal = jsonFileReader.ReadJsonFileSuccesLogin();
            var Url = await ValidGateWay();
            string loginUrl = Url + "/account/sign-in";
            _webDriver.Navigate().GoToUrl(loginUrl);
            Utils.Sleep(3000);
            EnterUserNameAndPassword(loginVal.LoginParameters.Username, loginVal.LoginParameters.Password);
            Utils.Sleep(3000);
            ClickLogin();
            Utils.Sleep(5000);
            return true;
        }

        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            return false;
        }
    }

    public async Task<string> ValidGateWay()
    {
        try
        {
            JsonFileReader jsonFileReader = new();
            var JsonObj = jsonFileReader.ReadJsonFileURL();
            var url = JsonObj.URLParameters.UseHttps ?
                $"https://{JsonObj.URLParameters.Url}:{JsonObj.URLParameters.Port}" 
                : 
                $"http://{JsonObj.URLParameters.Url}:{JsonObj.URLParameters.Port}";

            if (IPAddress.TryParse(JsonObj.URLParameters.Url, out IPAddress ipAddress))
            {
                if (await IsWebServiceAccessible(url))
                {
                    return url;
                }
                Utils.LogE("GateWay -> ", $"{url} not Accessible", $"{JsonObj.URLParameters.Url}:{JsonObj.URLParameters.Port}");
                Console.WriteLine($"GateWay -> {url} not Accessible");
                throw new Exception($"GateWay -> {url} not Accessible");
            }
            else
            {
                Utils.LogE("IpAddress -> ", $"{JsonObj.URLParameters.Url}:{JsonObj.URLParameters.Port} is Invalid", "Please Check Again");
                Console.WriteLine("Invalid IP address");
                _webDriver.Dispose();
                throw new Exception("Invalid Ip Address" + ipAddress);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} and {ex.InnerException} and {ex.Message}");
            _webDriver.Dispose();
            return string.Empty;

        }
    }
    public static async Task<bool> IsWebServiceAccessible(string ipAddress)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(ipAddress);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }

}

