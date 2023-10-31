using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Commons.Contracts.Dictionary.Units;

public interface IUnits
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

    void EnterRequestInfo(string title, string reason);
    void ClickNew();
    void NewDataUnitEntry(string name, string shortName);
    void ClickSubmit();
    void ClickSave();
    void ClickOk();
    void ClickUnit(IWebDriver driver);
    bool ClickUnitNew(IWebDriver driver);
    bool UnitDataEntry(IWebDriver driver);
    bool CreateNewReqGenericPopUp(IWebDriver driver);
}