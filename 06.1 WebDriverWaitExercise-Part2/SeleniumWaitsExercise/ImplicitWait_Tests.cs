using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWaitsExercise
{
    public class ImplicitWait_Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Search_Product_with_Implicit_Wait()
        {
            //fill in the search field textbox
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
            // Click on the search icon
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                //click on BUY NOW
                driver.FindElement(By.LinkText("Buy Now")).Click();

                //Verify Test
                Assert.That(driver.PageSource.Contains("keyboard"), Is.True);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }

        [Test]
        public void Search_Product_Junk_ShouldThrowNoSuchElementException()
        {
            //fill in the search field textbox
            driver.FindElement(By.Name("keywords")).SendKeys("junk");

            // Click on the search icon
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                //TRY click on BUY NOW
                driver.FindElement(By.LinkText("Buy Now")).Click();
            }
            catch (NoSuchElementException ex)
            {
                //verify the exception for non-existing product
                Assert.Pass("Expected NoSuchElementException was thrown");
                Console.WriteLine("Timeout - " + ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception: " + ex.Message);
            }

        }
    }
}