using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Commons.Contracts.Dictionary.DataEntities;

public interface IDataEntities
{
    IWebElement txtName { get; }
    IWebElement txtShort { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement btnReqSelect { get; }
    void NewDataEntityEntry(string name, string shortName);
    void EnterRequestInfo(string title, string reason);
    IWebElement txtTitle { get; }
    IWebElement txtReason { get; }
    IWebElement btnSave { get; }
    void ClickSave();
    void ClickSubmit();
    void ClickOk();
    void ClickDictionary(IWebDriver driver);
    void ClickDataEntity(IWebDriver driver);
    bool ClickDataEntityNewRequest(IWebDriver driver);
    bool DataEntityEntry(IWebDriver driver);
    bool CreateNewReqGenericPopUp(IWebDriver driver);
}
