using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SynchronizationExample
{
    public class WebDriverWaitsDemoTests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown() 
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void RedBoxInteraction()
        {
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/dynamic.html");
            IWebElement addButton = driver.FindElement(By.XPath("//input[@id='adder']"));
            addButton.Click();

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            //explicit wait
            IWebElement redBoxElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@id='box0']")));
            
            Assert.That(redBoxElement.Displayed, Is.True);
        }

        [Test]
        public void InputFieldInteration()
        {
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/dynamic.html");
            IWebElement revealButton = driver.FindElement(By.XPath("//input[@id='reveal']"));
            revealButton.Click();

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            //explicit wait
            IWebElement revealedInputElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='revealed']")));

            Assert.That(revealedInputElement.Displayed, Is.True);
        }

        [Test]
        public void ExplicitWait_WaitForTheElementToBeVisible_ByCondition()
        {
            //go to site
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

            //find the button and click it
            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            //set up WebDriverWait with a timeout of 10 seconds
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

            // wait until the element is visible (condition)
            IWebElement finishDiv = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));

            Assert.That(finishDiv.Displayed, Is.True);
        }

        [Test]
        public void ImplicitWait_ElementNotCreated()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            var finishDiv = driver.FindElement(By.XPath("//div[@class='example']//div[@id='finish']"));
            Assert.That(finishDiv.Displayed, Is.True);
        }

        [Test]
        public void PageLoadTimeout()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

            var startButton = driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button"));
            Assert.That(startButton.Displayed, Is.True);
        }

        [Test]
        public void AsyncJavaScripTimeout()
        {
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(60);
            string script = @"const start = new Date().getTime();
                              const delay = 45000;
                              while(new Date().getTime() < start + delay)
                               {
                                    //do something while waiting 45 seconds
                                }
                                console.log('45 seconds have passed')";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript(script);

        }

        [Test]
        public void IgnoreException_With_FluentWait()
        {
            //go to site
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");

            //find the button and click it
            driver.FindElement(By.XPath("//div[@class='example']//div[@id='start']//button")).Click();

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);

            IWebElement finishDiv = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='example']//div[@id='finish']")));
            
            Assert.That(finishDiv.Displayed, Is.True);
        }

        [Test]
        public void FluentWait_IgnoreException()
        {
            //go to site
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/2");

            //find the button and click it
            driver.FindElement(By.XPath("//*[@id='start']/button")).Click();

            //create the wait
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);

            //running the test without the line below throws NoSuchElementException
            //create the ignore exception
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            //create the condition
            IWebElement finishDiv = fluentWait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='finish']/h4")));

            Assert.That(finishDiv.Displayed, Is.True);
        }
    }
}