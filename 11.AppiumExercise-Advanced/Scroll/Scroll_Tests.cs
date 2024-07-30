using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Scroll
{
    public class Scroll_Tests
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

            var androidOptions = new AppiumOptions();
            androidOptions.App = @"C:\Users\denni\Downloads\ApiDemos-debug.apk";
            androidOptions.DeviceName = "TestPhone";
            androidOptions.PlatformName = "Android";
            androidOptions.AutomationName = "UIAutomator2";
            androidOptions.PlatformVersion = "14"; // NB!

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();
        }

        [Test]
        public void Test1()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            ScrollToText("Lists");

            IWebElement listButton = _driver.FindElement(MobileBy.AccessibilityId("Lists"));
            Assert.That(listButton, Is.Not.Null, "The Lists element is not present!");

            listButton.Click();

            IWebElement arraysButton = _driver.FindElement(MobileBy.AccessibilityId("09. Array (Overlay)"));
            Assert.That(arraysButton, Is.Not.Null, "Arrays button is not present!");
        }

        private void ScrollToText(string text)
        {
            _driver.FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector()" +
                                                            ".scrollable(true))" +
                                                            ".scrollIntoView(new UiSelector()" +
                                                            ".text(\"" + text + "\"))"));
        }
    }
}