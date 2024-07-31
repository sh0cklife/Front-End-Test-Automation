using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Runtime.CompilerServices;

namespace SelenoidTestProject
{
    [TestFixture("chrome", "126.0")]
    [TestFixture("firefox", "125.0")]

    public class Selenoid
    {
        private IWebDriver driver;
        private string browserType;
        private string browserVersion;

        public Selenoid(string browserType, string browserVersion)
        {
            this.browserType = browserType;
            this.browserVersion = browserVersion;
        }

        private DriverOptions GetOptions(string browserType, string browserVersion)
        {
            if (browserType == "chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.BrowserVersion = browserVersion;
                options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
                {
                    /* How to add test badge */
                    ["name"] = "Test badge...",
                    /* How to set session timeout */
                    ["sessionTimeout"] = "15m",
                    /* How to add "trash" button */
                    ["labels"] = new Dictionary<string, object>
                    {
                        ["manual"] = "true"
                    },

                    /* How to enable video recording */
                    ["enableVideo"] = true
                });
                return options;

            }
            else
            {
                FirefoxOptions options = new FirefoxOptions();
                options.BrowserVersion = browserVersion;
                options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
                {
                    /* How to add test badge */
                    ["name"] = "Test badge...",
                    /* How to set session timeout */
                    ["sessionTimeout"] = "15m",
                    /* How to add "trash" button */
                    ["labels"] = new Dictionary<string, object>
                    {
                        ["manual"] = "true"
                    },
                    /* How to enable video recording */
                    ["enableVideo"] = true
                });
                return options;
            }
        }

        [SetUp]
        public void Setup()
        {
            DriverOptions options = this.GetOptions(this.browserType, this.browserVersion);

            this.driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options);
            this.driver.Url = "https://en.wikipedia.org/";

            this.driver.Manage().Window.Maximize();
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        [TearDown]
        public void TearDown()
        {
            this.driver?.Close();
            this.driver?.Dispose();
        }

        [Test]
        public void Test_SearchInputField()
        {
            driver.FindElement(By.XPath("//input[@name='search']")).SendKeys("Quality Assurance");
            driver.FindElement(By.XPath("//*[@id='searchform']//button")).Click();

            var heading1 = driver.FindElement(By.Id("firstHeading"));
            Assert.That(heading1.Text, Is.EqualTo("Quality assurance"));

        }

        [Test]
        public void Test_HomeButtonLogo()
        {
            driver.FindElement(By.XPath("//input[@name='search']")).SendKeys("Quality Assurance");
            driver.FindElement(By.XPath("//*[@id='searchform']//button")).Click();

            driver.FindElement(By.CssSelector("body > div.vector-header-container > header > div.vector-header-start > a")).Click();

            var heading1 = driver.FindElement(By.Id("Welcome_to_Wikipedia"));
            Assert.That(heading1.Text, Is.EqualTo("Welcome to Wikipedia"));
            Assert.That(this.driver.Url, Is.EqualTo("https://en.wikipedia.org/wiki/Main_Page"));
        }


    }
}