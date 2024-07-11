using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CalculatorProject
{
    public class Calculator_Tests
    {
        WebDriver driver;
        IWebElement textBoxNumber1;
        IWebElement textBoxNumber2;
        IWebElement dropdownOperation;
        IWebElement calcButton;
        IWebElement resetButton;
        IWebElement divResult;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [SetUp]
        public void Setup()
        {
            textBoxNumber1 = driver.FindElement(By.Id("number1"));
            textBoxNumber2 = driver.FindElement(By.XPath("//input[@id='number2']"));
            dropdownOperation = driver.FindElement(By.XPath("//label[@for='operation']//following-sibling::select"));
            calcButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            divResult = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void PerformTestLogic(string firstNumber, string operation, string secondNumber, string expected)
        {
            //click reset button
            resetButton.Click();
            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxNumber1.SendKeys(firstNumber);
            }
            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxNumber2.SendKeys(secondNumber);
            }
            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropdownOperation).SelectByText(operation);
            }
            calcButton.Click();

            Assert.That(divResult.Text, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("5", "+ (sum)", "10", "Result: 15")]
        [TestCase("5", "+ (sum)", "5", "Result: 10")]
        [TestCase("5", "+ (sum)", "15", "Result: 20")]
        [TestCase("0", "+ (sum)", "5", "Result: 5")]
        public void Test1(string firstNumber, string operation, string secondNumber, string expected)
        {
            PerformTestLogic(firstNumber, operation, secondNumber, expected);
        }
    }
}