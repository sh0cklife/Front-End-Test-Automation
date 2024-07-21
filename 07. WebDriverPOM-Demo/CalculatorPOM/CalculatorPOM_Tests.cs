using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorPOM
{
    public class Tests
    {
        public IWebDriver driver;

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
        public void Test_Valid_Nubmers_V1()
        {
            var calculatorPage = new SumNumberPage(driver);

            calculatorPage.OpenPage();

            calculatorPage.FieldNum1.SendKeys("1");
            calculatorPage.FieldNum2.SendKeys("2");

            calculatorPage.CalcButton.Click();

            Assert.That(calculatorPage.ResultElement.Text, Is.EqualTo("Sum: 3"));
            Assert.AreEqual("Sum: 3", calculatorPage.ResultElement.Text);

            calculatorPage.ResetForm();
            var result = calculatorPage.IsFormEmpty();

            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Valid_Nubmers_V2()
        {
            var calculatorPage = new SumNumberPage(driver);

            calculatorPage.OpenPage();
            string calcResult = calculatorPage.AddNumber("1", "2");
            Assert.That(calcResult, Is.EqualTo("Sum: 3"));
        }

        [Test]
        public void Test_Valid_Nubmers_V3()
        {
            var sumpage = new SumNumberPage(driver);
            sumpage.OpenPage();
            var result = sumpage.AddNumber("1", "2");
            Assert.That(result, Is.EqualTo("Sum: 3"));
        }

        [Test]
        public void Test_Invalid_Numbers()
        {
            var sumpage = new SumNumberPage(driver);
            sumpage.OpenPage();
            var result = sumpage.AddNumber("hello", "2");
            Assert.That(result, Is.EqualTo("Sum: invalid input"));
        }

        [Test]
        public void Test_Form_Reset()
        {
            var calculatorPage = new SumNumberPage(driver);

            calculatorPage.OpenPage();

            string calcResult = calculatorPage.AddNumber("1", "2");
            Assert.That(calcResult, Is.EqualTo("Sum: 3"));

            calculatorPage.ResetForm();
            var result2 = calculatorPage.IsFormEmpty();
            Assert.IsTrue(result2);
            Assert.AreEqual("1st number", calculatorPage.FieldNum1.GetAttribute("placeholder"));
            Assert.AreEqual("2nd number", calculatorPage.FieldNum2.GetAttribute("placeholder"));
        }
    }
}