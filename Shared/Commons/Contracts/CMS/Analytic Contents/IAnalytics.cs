using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Commons.Contracts.CMS.Analytic_Contents;

public interface IAnalytics
{
    IWebElement dropDownSector { get; }
    IWebElement dropDownCategory { get; }
    IWebElement dropDownTable { get; }
    IWebElement btnContinue { get; }
    IWebElement btnContinueSelection { get; }
    IWebElement OpenStartDateDrpDwn { get; }
    IWebElement OpenStopDateDrpDwn { get; }
    IWebElement StartQuaterDrpDwn { get; }
    IWebElement StopQuaterDrpDwn { get; }
    IWebElement StartMonthtxtBox { get; }
    IWebElement StopMonthtxtBox { get; }
    IWebElement readonlyInput { get; }
    IWebElement footerDiv { get; }
    IWebElement contentPopUp { get; }
    IWebElement txtBoxName { get; }
    IWebElement txtBoxTitle { get; }
    IWebElement txtBoxSeries { get; }
    IWebElement dropDwnSeriesType { get; }
    IWebElement dropDwnContentSpot { get; }
    IWebElement txtNote { get; }
    IWebElement btnSaveContent { get; }
    IWebElement btnSaves { get; }
    IWebElement linkDivTxt { get; }
    IWebElement container { get; }
    IWebElement btnYes { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnSave { get; }
    IWebElement btnClose { get; }
    IWebElement btnAYes { get; }
    IWebElement btnCancel { get; }
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
    void ClickCMS(IWebDriver driver);
    bool ClickClear(IWebDriver driver);
    void ClickAnalyticCard(IWebDriver driver);
    bool ClickNew(IWebDriver driver);
    void ProcessDataSetSelector(IWebDriver driver);
    void ProcessTimePeriodSelector(IWebDriver driver);
    void ProcessAnalyticContent(IWebDriver driver);

}