using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace Commons.Contracts.CMS.FlashCards;
public interface IFlashCards
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
    IWebElement txtBoxSeries { get; }
    IWebElement dropDwnSeriesType { get; }
    IWebElement dropDwnContentSpot { get; }
    IWebElement txtNote { get; }
    IWebElement btnSaveContent { get; }
    IWebElement btnMainSaveContent { get; }
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
    void ProcessFlashCardDataSetSelector(IWebDriver driver);
    void ProcessTimePeriodFlashCardSelector(IWebDriver driver);
    void ProcessFlashCardContent(IWebDriver driver);
}
