using OpenQA.Selenium;

namespace Contracts.Sectors;
public interface ISector
{
    IWebElement txtName { get; }
    IWebElement txtTitle { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement textMsgRes { get; }
    IWebElement btnClickNew { get; }
    IWebElement btnClickOk { get; }
    IWebElement btnReqSelect { get; }
    IWebElement txtReson { get; }
    IWebElement btnProcessSelected { get; }
    IWebElement btnSubmitRequest { get; }
    void EnterNameAndTitle(string userName, string password);
    void EnterRequestInfo(string title, string reason);
    bool ClickNewRequest(IWebDriver driver);
    string ClickRequestType(IWebDriver driver);
    bool RequestInfBox(IWebDriver driver);
    void ClickSubmit();
    void ClickClose();
    void ClickNew();
    void ClickOk();
    void SubmitReq();

    public string CreateNewDataSectorSuccess(IWebDriver driver);

    public bool ClickDataSet(IWebDriver driver);
    bool ClickSector(IWebDriver driver);
}

