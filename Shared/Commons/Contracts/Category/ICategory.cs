using OpenQA.Selenium;

namespace Contracts.Category;
public interface ICategory
{
    IWebElement table { get; }
    List<IWebElement> rows { get; }
    IWebElement catalogLink { get; }
    IWebElement dropDownBox { get; }
    IWebElement btnContinue { get; }
    IWebElement txtBoxName { get; }
    IWebElement txtBoxTitle { get; }
    IWebElement btnSubmit { get; }
    IWebElement btnClose { get; }
    IWebElement btnClickOk { get; }
    IWebElement txtTitle { get; }
    IWebElement btnSubmitReq { get; }
    void EnterRequestInfo(string title, string reason);
    void EnterDataCategory(string title, string reason);
    void ClickCatalogCard();
    void ClickContinue();
    void ClickSubmit();
    void ClickClose();
    void ClickOk();
    void ClickDataCatalogCard(IWebDriver driver);
    void ClickCategoryCard(IWebDriver driver);
    bool ClickNewDataCategoryButton(IWebDriver driver);
    bool ClickNewRequest(IWebDriver driver);
    bool CategoryRequestInfBox(IWebDriver driver);
}
