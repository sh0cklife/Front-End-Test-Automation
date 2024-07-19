using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeleniumWaitsExercise
{
    internal class WorkingWithAlerts_Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        //Implement a test method to handle a basic JavaScript alert.
        //Click the button to trigger the alert.
        //Switch to the alert, accept it, and verify the result message.
        [Test]
        public void HandlingBasicJavaScriptAlerts()
        {
            //click on the [Click for JS Alert] button
            driver.FindElement(By.XPath("//button[@onclick='jsAlert()']")).Click();

            //define and switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Or alert text is not as expected");

            //accept the alert / click OK
            alert.Accept();

            // Verify the result message
            var result = driver.FindElement(By.Id("result"));
            Assert.That(result.Text, Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void HandlingJavaScriptConfirmAlerts()
        {
            //click on the [Click for JS Alert] button
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();

            //define and switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Or alert text is not as expected");

            //accept the alert / click OK
            alert.Accept();

            // Verify the result message
            var result = driver.FindElement(By.Id("result"));
            Assert.That(result.Text, Is.EqualTo("You clicked: Ok"));

            // trigger the alert again
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();

            // switch to the alert
            alert = driver.SwitchTo().Alert();

            //Dismiss the alert
            alert.Dismiss();

            //verify the result message
            Assert.That(result.Text, Is.EqualTo("You clicked: Cancel"));

        }

        [Test]
        public void HandlingJavaScriptPromptAlerts()
        {
            //click on the [Click for JS Alert] button
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();

            //define and switch to the alert
            IAlert alert = driver.SwitchTo().Alert();

            //verify the alert text
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Or alert text is not as expected");

            //accept the alert / click OK
            alert.SendKeys("Test-123");
            alert.Accept();

            // Verify the result message
            var result = driver.FindElement(By.Id("result"));
            Assert.That(result.Text, Is.EqualTo("You entered: Test-123"));

            // trigger the alert again
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();

            // switch to the alert
            alert = driver.SwitchTo().Alert();

            //Dismiss the alert
            alert.SendKeys("Test-123");
            alert.Dismiss();

            //verify the result message
            Assert.That(result.Text, Is.EqualTo("You entered: null"));

        }
    }
}
