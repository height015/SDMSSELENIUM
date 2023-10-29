using OpenQA.Selenium;

namespace Contracts.Indicator;
public interface IIndicator
{
    IWebElement dropDownCascadeSector { get; }
    IWebElement dropDownCat { get; }
    IWebElement dropDownTable { get; }
    IWebElement btnContinue { get; }
    IWebElement Newlink { get; }
    IWebElement IndiComboTree { get; }
    IWebElement txtBoxName { get; }
    IWebElement txtBoxTitle { get; }
    IWebElement checkBoxEmph { get; }
    IWebElement displayOrder { get; }
    IWebElement DisplayInChart { get; }
    IWebElement txtGrapTit { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement btnSave { get; }
    IWebElement btnBrowseFile { get; }
    IWebElement btnUpload { get; }
    IWebElement btnUpdate { get; }
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    IWebElement btnApply { get; }
    IWebElement txtTitle { get; }
    IWebElement txtReason { get; }
    IWebElement chkBtnDisplayInChart { get; }
    IWebElement txtDisplayInChart { get; }
    IWebElement btnUpdateModify { get; }
    IWebElement comboTree { get; }
    IWebElement liTree { get; }
    IWebElement txtTopLevelBox { get; }
    IList<IWebElement> boxSel { get; }
    void EnterRequestInfo(string title, string reason);
    void ClickContinue();
    void ClickSubmit();
    void ClickNew();
    void ClickSave();
    void ClickOk();
}
