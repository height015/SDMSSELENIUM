
using OpenQA.Selenium;

namespace Commons.Contracts.Dictionary.DataSource;

public interface IDataSources
{
    IWebElement txtName { get; }
    IWebElement txtShort { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement Newlink { get; }
    IWebElement btnSave { get; }
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    IWebElement txtTitle { get; }
    IWebElement txtReason { get; }

    void EnterRequestInfo(string title, string reason);
    void ClickNew();
    void NewDataUnitEntry(string name, string shortName);
    void ClickSubmit();
    void ClickSave();
    void ClickOk();
    void NewDataSourceEntry(string name, string shortName);
    void ClickDataSource(IWebDriver driver);
    bool DataSourceEntry(IWebDriver driver);
    bool CreateNewReqGenericPopUp(IWebDriver driver);
}


