using OpenQA.Selenium;

namespace Contracts.Table;
public interface ITables
{
    IWebElement dropDownCascadeSector { get; }
    IWebElement dropDownCat { get; }
    IWebElement btnContinue { get; }
    IWebElement Newlink { get; }
    IWebElement txtBoxName { get; }
    IWebElement txtBoxTitle { get; }
    IWebElement txtDescription { get; }
    IWebElement dropDownFeq { get; }
    IWebElement dropDownUnit { get; }
    IWebElement btnBrowseFile { get; }
    IWebElement btnUpload { get; }
    IWebElement checkBox1 { get; }
    IWebElement checkBox2 { get; }
    IWebElement checkBox3 { get; }
    IWebElement checkBox4 { get; }
    IWebElement checkBox5 { get; }
    IWebElement txtDataLab1 { get; }
    IWebElement txtDataLab2 { get; }
    IWebElement txtDataLab3 { get; }
    IWebElement txtDataLab4 { get; }
    IWebElement txtDataLab5 { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnSave { get; }
    IWebElement btnUpdate { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement txtTitle { get; }
    IWebElement txtReason { get; }
    IWebElement table { get; }
    IWebElement btnApply { get; }
    List<IWebElement> rows { get; }

    void EnterTableInfoData(string name, string titile, string desc);
    void EnterRequestInfo(string title, string reason);
    void Enable(IWebElement webElement);
    void ClickContinue();
    void ClickSave();
    void ClickSubmit();
    void ClickUpdate();
    void ClickNew();
    void ClickOk();
}
