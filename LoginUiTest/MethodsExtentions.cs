using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LoginUiTest;

public static class MethodsExtentions
{
    public static void EnterText(this IWebElement element, string value)
    {
        element.SendKeys(value);
    }

    public static void Clicks(this IWebElement element)
    {
        element.Click();
    }

    public static void SelectDropDown(this IWebElement element, string value)
    {
        new SelectElement(element).SelectByValue(value);
    }
}

