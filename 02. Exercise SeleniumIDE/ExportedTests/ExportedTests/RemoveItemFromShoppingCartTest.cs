// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class RemoveItemFromShoppingCartTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  [SetUp]
  public void SetUp() {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  protected void TearDown() {
    driver.Quit();
  }
  [Test]
  public void removeItemFromShoppingCart() {
    // Test name: Remove Item From Shopping Cart
    // Step # | name | target | value
    // 1 | open | / | 
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");
    // 2 | type | css=*[data-test="username"] | standard_user
    driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).SendKeys("standard_user");
    // 3 | type | css=*[data-test="password"] | secret_sauce
    driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).SendKeys("secret_sauce");
    // 4 | click | css=*[data-test="login-button"] | 
    driver.FindElement(By.CssSelector("*[data-test=\"login-button\"]")).Click();
    // 5 | click | css=*[data-test="add-to-cart-sauce-labs-backpack"] | 
    driver.FindElement(By.CssSelector("*[data-test=\"add-to-cart-sauce-labs-backpack\"]")).Click();
    // 6 | click | css=*[data-test="shopping-cart-link"] | 
    driver.FindElement(By.CssSelector("*[data-test=\"shopping-cart-link\"]")).Click();
    // 7 | verifyText | css=*[data-test="inventory-item-name"] | Sauce Labs Backpack
    Assert.That(driver.FindElement(By.CssSelector("*[data-test=\"inventory-item-name\"]")).Text, Is.EqualTo("Sauce Labs Backpack"));
    // 8 | click | css=*[data-test="remove-sauce-labs-backpack"] | 
    driver.FindElement(By.CssSelector("*[data-test=\"remove-sauce-labs-backpack\"]")).Click();
    // 9 | assertElementNotPresent | css=*[data-test="remove-sauce-labs-backpack"] | 
    {
      var elements = driver.FindElements(By.CssSelector("*[data-test=\"remove-sauce-labs-backpack\"]"));
      Assert.True(elements.Count == 0);
    }
    // 10 | close |  | 
    driver.Close();
  }
}
