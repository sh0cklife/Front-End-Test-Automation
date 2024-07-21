using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StudentRegistryPOM.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
