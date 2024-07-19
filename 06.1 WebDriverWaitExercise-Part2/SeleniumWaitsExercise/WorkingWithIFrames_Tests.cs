using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWaitsExercise
{
    internal class WorkingWithIFrames_Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://codepen.io/pervillalva/full/abPoNLd");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void HandleIFramesByIndex()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // wait until the iFrame is available and switch to it by finding the first iframe
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));

            //click the dropdown button
            var dropDownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropDownBtn.Click();

            //select the links inside the dropdown menu
            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            //verify and print the link text
            foreach(var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();

        }

        [Test]
        public void HandleIFramesByID()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // wait until the iFrame is available and switch to it by finding the first iframe
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id("result")));

            //click the dropdown button
            var dropDownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropDownBtn.Click();

            //select the links inside the dropdown menu
            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            //verify and print the link text
            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }

        [Test]
        public void HandleIFramesByWebElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // locate the frame element
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));

            driver.SwitchTo().Frame(frameElement);

            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn"))).Click();

            //select the links inside the dropdown menu
            var dropDownLinks = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            //verify and print the link text
            foreach (var link in dropDownLinks)
            {
                Console.WriteLine(link.Text);
                Assert.IsTrue(link.Displayed, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }
    }
}
