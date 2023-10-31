using OpenQA.Selenium;

namespace Commons.Contracts.Dictionary.Frequency;

public interface IFrequency
{
    IWebElement Newlink { get; }
    IWebElement txtName { get; }
    IWebElement txtShort { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement btnSave { get; }
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    IWebElement txtTitle { get; }
    IWebElement txtReason { get; }

    void ClickNew();
    void NewDataFrequencyEntry(string name, string shortName);
    void ClickSubmit();
    void ClickOk();
    void EnterRequestInfo(string title, string reason);
    void NewDataUnitEntry(string name, string shortName);
    void ClickSave();
    void ClickFrequency(IWebDriver driver);
    bool NewFrequency(IWebDriver driver);
    bool DataEntryFrequency(IWebDriver driver);
    bool CreateNewReqGenericPopUp(IWebDriver driver);
}
