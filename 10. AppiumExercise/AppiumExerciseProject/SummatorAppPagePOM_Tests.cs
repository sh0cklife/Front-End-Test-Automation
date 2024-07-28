using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AppiumExerciseProject
{
    public class SummatorAppPagePOM_Tests
    {
        private AndroidDriver _driver;
        private AppiumLocalService _appiumLocalService;
        private SummatorPage _summatorPage;

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
            _summatorPage = new SummatorPage(_driver);

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
            var result = _summatorPage.Calculate("5", "5");
            Assert.That(result, Is.EqualTo("10"));
        }

        [Test]
        public void Test_InvalidData()
        {
            _summatorPage.ClearFields();
            _summatorPage.CalculateButton.Click();

            Assert.That(_summatorPage.ResultOutput.Text, Is.EqualTo("error"));
        }

        [Test]
        public void Test_InvalidData_FirstInputOnly()
        {
            _summatorPage.ClearFields();
            _summatorPage.FirstInput.SendKeys("5");
            _summatorPage.CalculateButton.Click();

            Assert.That(_summatorPage.ResultOutput.Text, Is.EqualTo("error"));
        }

        [Test]
        public void Test_InvalidData_SecondInputOnly()
        {
            _summatorPage.ClearFields();
            _summatorPage.SecondInput.SendKeys("5");
            _summatorPage.CalculateButton.Click();

            Assert.That(_summatorPage.ResultOutput.Text, Is.EqualTo("error"));
        }

        [Test]
        [TestCase("10", "10", "20")]
        [TestCase("1", "1", "2")]
        [TestCase("1000", "1000", "2000")]
        [TestCase("0", "10", "10")]
        [TestCase("5.5", "4.5", "10.0")]
        public void Test_ValidDataParametrized(string input1, string input2, string expectedResult)
        {
            var result = _summatorPage.Calculate(input1, input2);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
