using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Commons.Contracts.CMS.QuickFlash;

public interface IQuickFlashes
{
    IWebElement dropDownSector { get; }
    IWebElement dropDownCategory { get; }
    IWebElement dropDownTable { get; }
    IWebElement btnContinue { get; }
    IWebElement btnContinueSelection { get; }
    IWebElement OpenStartDateDrpDwn { get; }
    IWebElement OpenStopDateDrpDwn { get; }
    IWebElement readonlyInput { get; }
    IWebElement contentPopUp { get; }
    IWebElement txtBoxName { get; }
    IWebElement txtBoxTitle { get; }
    IWebElement dropDwnArrow { get; }
    IWebElement dropDwonArrDir { get; }
    IWebElement txtValue { get; }
    IWebElement txtExplanation { get; }
    IWebElement btnSaves { get; }
    IWebElement container { get; }
    IWebElement btnYes { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnSave { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    void ClickContinue();
    void ClickContinueDate();
    void ClickSave();
    void ClickSubmit();
    void ClickContentPopUp();
    void ClickYesPopUp();
    void ClickOk();
    void ProcessQFlashDataSetSelector(IWebDriver driver);
    void ProcessTimePeriodQFlashSelector(IWebDriver driver);
    void ProcessNewQFlash(IWebDriver driver);
}

