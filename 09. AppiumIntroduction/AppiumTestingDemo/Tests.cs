using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Internal;

namespace AppiumTestingDemo
{
    public class Tests
    {
        private AndroidDriver driver;
        private AppiumLocalService service;

        [SetUp]
        public void Setup()
        {
            service = new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();

            AppiumOptions options = new AppiumOptions();
            options.App = @"C:\Users\denni\Downloads\com.example.androidappsummator.apk";
            options.PlatformName = "Android";
            options.DeviceName = "Pixel 7 Demo";
            options.AutomationName = "UIAutomator2";

            driver = new AndroidDriver(service, options);

        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
            service.Dispose();
        }

        [Test]
        public void TestValidSum()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("5");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("5");

            var calcButton = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("10"), "Test failed: Calculation is incorrect");
        }

        [Test]
        public void TestInvalidSum()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("5");

            var calcButton = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/buttonCalcSum"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"), "Test failed: Calculation is incorrect");
        }
    }
}