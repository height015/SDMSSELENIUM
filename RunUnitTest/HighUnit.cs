using LoginUiTest;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RunUnitTest;

public class HighTest : IDisposable
{
    private readonly IWebDriver driver;

    private readonly LoginMain loginObj;

    private static readonly string _URL = "http://197.255.51.104:9008";


    public HighTest()
    {
        driver = new ChromeDriver();
        loginObj = new LoginMain();
    }

   
    [Fact]
    public void LoginSuccess_ValidCredentials_ReturnsTrue()
    {

        bool loginSuccess = loginObj.LoginSuccess(driver);
        Assert.True(loginSuccess.Equals(true));

    }

    [Fact]
    public void LoginFailed_Should_Return_String_On_Successful_Test()
    {
        string loginFailed = loginObj.LoginFailled(driver);
        Assert.True(loginFailed.Equals("No Member Login Information Found!"));
    }

    public void Dispose()
    {
        // Dispose of the driver after each test.
        driver.Quit();
        driver.Dispose();
    }
}
