using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data;

namespace WikiNUnitProject
{
    public class WikipediaUITests
    {
        private ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void HomePage_Title()
        {
            Assert.That(driver.Title, Is.EqualTo("Wikipedia"));

        }

        [Test]
        public void QualityAssurancePage_Title()
        {
            driver.FindElement(By.Id("searchInput")).SendKeys("Quality Assurance" + Keys.Enter);
            var searchInput = driver.FindElement(By.Id("searchInput"));

            Assert.That(driver.Title, Is.EqualTo("Quality assurance - Wikipedia"));
 
        }
    }
}