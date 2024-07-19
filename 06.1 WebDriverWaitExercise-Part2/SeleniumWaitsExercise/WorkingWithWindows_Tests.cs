using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace SeleniumWaitsExercise
{
    internal class WorkingWithWindows_Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Dispose();
        }

        [Test]
        public void HandleMultipleWindows()
        {
            var clickHereButton = driver.FindElement(By.XPath("//*[@id='content']/div/a"));
            clickHereButton.Click();

            // Get all window handles - retrieve all the window handles currently opened by the web driver.
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Ensure there are at least two windows open
            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open");

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            //Verify the content of the new window
            string newWindowContent = driver.PageSource;
            Assert.IsTrue(driver.CurrentWindowHandle == windowHandles[1]);
            Assert.IsTrue(newWindowContent.Contains("New Window"), "Error message: The content of the new window is not as expected");


            // Log the content of the new window
            string path = Path.Combine(Directory.GetCurrentDirectory(), "windows.txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, "Window handle for new window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + newWindowContent + "\n\n");

            //Close the new window
            driver.Close();

            //Switch back to the original window
            driver.SwitchTo().Window(windowHandles[0]);
            Assert.IsTrue(driver.CurrentWindowHandle == windowHandles[0]);

            //verify the content of the original window
            string originalWindowContent = driver.PageSource;
            Assert.IsTrue(originalWindowContent.Contains("Opening a new window"), "Error message: The content of the original window is not as expected");

            // Log the content of the original window
            File.AppendAllText(path, "Window handle for original window: " + driver.CurrentWindowHandle + "\n\n");
            File.AppendAllText(path, "The page content: " + originalWindowContent + "\n\n");

        }

        [Test]
        public void HandleNoSuchWindowException()
        {
            var clickHereButton = driver.FindElement(By.XPath("//*[@id='content']/div/a"));
            clickHereButton.Click();

            // Get all window handles - retrieve all the window handles currently opened by the web driver.
            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            // Switch to the new window
            driver.SwitchTo().Window(windowHandles[1]);

            // close the window
            driver.Close();

            try
            {
                // attempt to switch back to the closed window
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException ex)
            {
                // log the exception
                string path = Path.Combine(Directory.GetCurrentDirectory(), "testLog.txt");
                File.AppendAllText(path, "NoSuchException caught: " + ex.Message + "\n\n");
                Assert.Pass("NoSuchWindowException was correctly handled.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected exeption was thrown: " + ex.Message);
            }
            finally
            {
                //switch back to the original window
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }
    }
}

