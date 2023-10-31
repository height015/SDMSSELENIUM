using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Commons.Contracts.CMS.FContent;

public interface IFeaturedContents
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
    IWebElement btnSaves { get; }
    IWebElement container { get; }
    IWebElement btnYes { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnSave { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    IWebElement SubTitle {  get; }
    IWebElement PreContent {  get; }
    IWebElement MainContents { get; }
    IWebElement mainContentBox { get; }
    IWebElement shortContentBox { get; }
    void ClickContinue();
    void ClickContinueDate();
    void ClickSave();
    void ClickSubmit();
    void ClickContentPopUp();
    void ClickYesPopUp();
    void ClickOk();
    void PopUpDataSet(IWebDriver driver);
    void PopUpTimePeriod(IWebDriver driver);
    void ClickFeaturedContentCard(IWebDriver driver);
    bool ClickNewFContentCard(IWebDriver driver);
    void ProcessFContentDataSetSelector(IWebDriver driver);
    void ProcessTimePeriodFContentSelector(IWebDriver driver);
    void ProcessFContent(IWebDriver driver);
}

