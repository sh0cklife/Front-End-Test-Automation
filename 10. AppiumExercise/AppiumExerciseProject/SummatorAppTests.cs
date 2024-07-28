using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace AppiumExerciseProject
{
    public class SummatorAppTests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;

        [OneTimeSetUp]
        public void Setup()
        {
            _appiumLocalService = new AppiumServiceBuilder()
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();
            _appiumLocalService.Start();

            var androidOptions = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                DeviceName = "Pixel 7 Demo",
                App = @"C:\\Users\\denni\\Downloads\\com.example.androidappsummator.apk"
            };

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
        }

        [OneTimeTearDown]
        public void Teardown() 
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
            
        }

        [Test]
        public void Test_ValidData()
        {
            IWebElement firstInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("5");

            IWebElement secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("2");

            IWebElement calculateButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calculateButton.Click();

            IWebElement resultOutput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultOutput.Text, Is.EqualTo("7"));
        }

        [Test]
        public void Test_InvalidData()
        {
            IWebElement secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("2");

            IWebElement calculateButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calculateButton.Click();

            IWebElement resultOutput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultOutput.Text, Is.EqualTo("error"));
        }

        [Test]
        public void Test_InvalidData_FillOnlyFirstInput()
        {
            IWebElement firstInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("5");

            IWebElement secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();

            IWebElement calculateButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calculateButton.Click();

            IWebElement resultOutput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultOutput.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("1", "1", "2")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "10", "10")]
        [TestCase("5.5", "4.5", "10.0")]
        public void Test_ValidDataParametrized(string input1, string input2, string expectedResult)
        {
            IWebElement firstInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys(input1);

            IWebElement secondInput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys(input2);

            IWebElement calculateButton = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calculateButton.Click();

            IWebElement resultOutput = _driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));
            Assert.That(resultOutput.Text, Is.EqualTo(expectedResult));
        }
    }
}