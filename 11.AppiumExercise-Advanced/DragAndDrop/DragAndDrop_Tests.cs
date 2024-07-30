using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace DragAndDrop
{
    public class DragAndDrop_Tests
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
        public void Drag_And_Drop()
        {
            IWebElement viewsButton = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            viewsButton.Click();

            IWebElement dragAndDropButton = _driver.FindElement(MobileBy.AccessibilityId("Drag and Drop"));
            dragAndDropButton.Click();

            AppiumElement firstDot = _driver.FindElement(By.Id("drag_dot_1")); //NB! shorten id if doesn't work
            AppiumElement secondDot = _driver.FindElement(By.Id("drag_dot_2")); //NB! shorten id if doesn't work


            //javascript dictionary for the drag and drop function
            var scriptArgs = new Dictionary<string, object>
            {
                { "elementId", firstDot.Id },
                { "endX", secondDot.Location.X + (secondDot.Size.Width/2) },
                { "endY", secondDot.Location.Y + (secondDot.Size.Height/2) },
                { "speed", 2500 }
            };

            _driver.ExecuteScript("mobile: dragGesture", scriptArgs);

            // can't use the standart actions method here
            //var actions = new Actions(_driver);
            //var dragAndDrop = actions.DragAndDrop(firstDot, secondDot).Build();
            //dragAndDrop.Perform();

            var result = _driver.FindElement(MobileBy.Id("drag_result_text"));
            Assert.That(result.Text, Is.EqualTo("Dropped!"), "Drag and drop did not happen");
        }
    }
}