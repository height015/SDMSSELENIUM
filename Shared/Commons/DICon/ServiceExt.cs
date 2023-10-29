using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Contracts.Sectors;
using Commons.Services.Sector;
using Commons.Contracts.Login;
using Commons.Services.Login;
using Contracts.Category;
using Commons.Services.Category;

namespace Commons;

public static class ServiceExt
{
    public static IServiceCollection AddExtDataServices(this IServiceCollection services)
    {

        services.AddScoped<IWebDriver>(provider => new ChromeDriver());

        // Register other services as needed
        services.AddTransient<ILogin, LoginService>();
        services.AddTransient<ISector, SectorService>();
        services.AddTransient<ICategory, CategoryService>();



        //SectorService : ISector



        return services;
    }

}

