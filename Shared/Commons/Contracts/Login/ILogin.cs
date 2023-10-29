using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    bool LoginSuccess();
}

