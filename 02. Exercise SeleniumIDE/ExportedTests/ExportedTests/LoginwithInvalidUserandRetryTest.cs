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
public class LoginwithInvalidUserandRetryTest {
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
  public void loginwithInvalidUserandRetry() {
    // Test name: Login with Invalid User and Retry
    // Step # | name | target | value
    // 1 | open | / | 
    driver.Navigate().GoToUrl("https://www.saucedemo.com/");
    // 2 | type | css=*[data-test="username"] | demo_user
    driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).SendKeys("demo_user");
    // 3 | type | css=*[data-test="password"] | secret_sauce
    driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).SendKeys("secret_sauce");
    // 4 | click | css=*[data-test="login-button"] | 
    driver.FindElement(By.CssSelector("*[data-test=\"login-button\"]")).Click();
    // 5 | storeText | css=*[data-test="error"] | errorMessage
    vars["errorMessage"] = driver.FindElement(By.CssSelector("*[data-test=\"error\"]")).Text;
    // 6 | if | ${errorMessage}==="Epic sadface: Username and password do not match any user in this service" | 
    if ((Boolean) js.ExecuteScript("return (arguments[0]===\'Epic sadface: Username and password do not match any user in this service\')", vars["errorMessage"])) {
      // 7 | echo | WrongUserName | 
      Console.WriteLine("WrongUserName");
            // 8 | type | css=*[data-test="username"] | standard_user
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).Clear();
            driver.FindElement(By.CssSelector("*[data-test=\"username\"]")).SendKeys("standard_user");
            // 9 | type | css=*[data-test="password"] | secret_sauce
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).Clear();
            driver.FindElement(By.CssSelector("*[data-test=\"password\"]")).SendKeys("secret_sauce");
      // 10 | click | css=*[data-test="login-button"] | 
      driver.FindElement(By.CssSelector("*[data-test=\"login-button\"]")).Click();
      // 11 | assertText | css=*[data-test="title"] | Products
      Assert.That(driver.FindElement(By.CssSelector("*[data-test=\"title\"]")).Text, Is.EqualTo("Products"));
      // 12 | echo | LoginSuccess | 
      Console.WriteLine("LoginSuccess");
      // 13 | end |  | 
    }
    // 14 | close |  | 
    driver.Close();
  }
}
