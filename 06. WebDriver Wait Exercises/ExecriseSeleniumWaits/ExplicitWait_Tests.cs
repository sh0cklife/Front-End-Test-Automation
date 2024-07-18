using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ExerciseSeleniumWait
{
    public class ExplicitWait_Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");

            //The initial implicit wait ensures that the driver will wait up to 10 seconds for elements to appear during the initial setup phase.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void SearchProduct_Keyboard_AddToCart()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            // set the implicit wait from 10 to 0 before using explicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                //wait object with timeout set to 10 seconds
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                //wait for the driver to identify the Buy Now link using the LinkText Property
                IWebElement buyNow = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));
                //IWebElement buyNowLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Buy Now")));


                //Set the implicit wait back to 10 seconds
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                buyNow.Click();

                string productName = driver.FindElement(By.XPath("//form[@name='cart_quantity']//a/strong")).Text;

                //Assertions
                Assert.IsTrue(productName == "Microsoft Internet Keyboard PS/2");
                

            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception" + ex.Message);
            }
        }

        [Test]
        public void SearchProduct_Junk()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement buyNow = wait.Until(e => e.FindElement(By.LinkText("Buy Now")));
                buyNow.Click();
                Assert.Fail("The 'Buy Now' link was found for a non-existing product");

            }
            catch (WebDriverException)
            {
                // Expected exception for non-existing product
                Assert.Pass("Expected WebDriverTimeoutException was thrown.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }
            finally
            {
                //reset the implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}