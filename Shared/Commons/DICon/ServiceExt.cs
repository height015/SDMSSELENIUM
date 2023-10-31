using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Contracts.Sectors;
using Commons.Services.Sector;
using Commons.Contracts.Login;
using Commons.Services.Login;
using Contracts.Category;
using Commons.Services.Category;
using Contracts.Indicator;
using Commons.Services.Indicator;
using Contracts.Table;
using Commons.Services.Tables;
using Commons.Contracts.Dictionary.DataEntities;
using Commons.Services.Dictionary.DataEntity;
using Commons.Contracts.Dictionary.DataSource;
using Commons.Services.Dictionary.DataSource;
using Commons.Contracts.Dictionary.Frequency;
using Commons.Services.Dictionary.Frequency;
using Commons.Contracts.Dictionary.Units;
using Commons.Services.Dictionary.Units;
using Commons.Contracts.CMS.Analytic_Contents;
using Commons.Services.CMS.Analytics;
using Commons.Contracts.CMS.FContent;
using Commons.Services.CMS.FeaturedContents;
using Commons.Contracts.CMS.FlashCards;
using Commons.Services.CMS.FlashCard;
using Commons.Contracts.CMS.QuickFlash;
using Commons.Services.CMS.QuickFlash;

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
        services.AddTransient<IIndicator, IndicatorService>();
        services.AddTransient<ITables, TablesService>();
        services.AddTransient<IDataEntities, DataEntitiesService>();
        services.AddTransient<IDataSources, DataSourcesService>();
        services.AddTransient<IFrequency, FrequencyService>();
        services.AddTransient<IUnits, UnitsServices>();
        services.AddTransient<IAnalytics, AnalyticsService>();
        services.AddTransient<IFeaturedContents, FeaturedContentsService>();
        services.AddTransient<IFlashCards, FlashCardService>();
        services.AddTransient<IQuickFlashes, QuickFlashService>();

        return services;
    }

}

