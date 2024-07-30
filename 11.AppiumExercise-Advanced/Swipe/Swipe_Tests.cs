using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;

namespace Swipe
{
    public class Swipe_Tests
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
                App = @"C:\Users\denni\Downloads\ApiDemos-debug.apk",
                DeviceName = "TestPhone",
                PlatformName = "Android",
                AutomationName = "UIAutomator2",
                PlatformVersion = "14" // NB!
            };

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
        public void SwipeTest()
        {
            IWebElement viewButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewButton.Click();

            IWebElement galleryButton = _driver.FindElement(MobileBy.AccessibilityId("Gallery"));
            galleryButton.Click();

            IWebElement photosButton = _driver.FindElement(MobileBy.AccessibilityId("1. Photos"));
            photosButton.Click();

            //1. click on the first photo
            var firstPhoto = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[0]; // can be used only By when used with class name?

            //2. move mouse 200pixels left
            var actions = new Actions(_driver);
            var swipe = actions.ClickAndHold(firstPhoto).MoveByOffset(-200, 0).Release().Build();
            swipe.Perform();

            var thirdPhoto = _driver.FindElements(MobileBy.ClassName("android.widget.ImageView"))[2];

            Assert.That(thirdPhoto, Is.Not.Null, "Third image is not visible");
        }
    }
}