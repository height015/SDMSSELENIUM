﻿using Commons.Contracts.Login;
using Contracts.Sectors;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Commons;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewRequest;
public class RequestConsole
{
   public static void Main(string[] args)
    {
        var classObj = new RequestConsole();
        var services = new ServiceCollection();
        services.AddExtDataServices();

        // Build the service provider
        var serviceProvider = services.BuildServiceProvider();
        var _driver = serviceProvider.GetRequiredService<IWebDriver>();
        var _loginService = serviceProvider.GetRequiredService<ILogin>();
        var _sectorService = serviceProvider.GetRequiredService<ISector>();
        bool login = _loginService.LoginSuccess();
        if (true)
        {
            _sectorService.ClickDataSet(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickSector(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickNewRequest(_driver);
            Utils.Sleep(3000);
            _sectorService.ClickRequestType(_driver);
            Utils.Sleep(3000);
            _sectorService.RequestInfBox(_driver);
        }
    }


}