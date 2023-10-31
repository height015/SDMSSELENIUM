namespace Commons.Contracts.Login;

using OpenQA.Selenium;

public interface ILogin
{
    IWebElement txtEmail { get; }
    IWebElement txtPassword { get; }
    IWebElement btnLogin { get; }
    IWebElement divElementText { get; }

    void EnterUserNameAndPassword(string userName, string password);
    void ClickLogin();
    Task<bool> LoginSuccess();
        
        
        
}

