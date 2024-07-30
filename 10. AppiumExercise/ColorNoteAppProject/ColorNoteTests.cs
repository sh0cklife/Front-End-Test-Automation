using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace ColorNoteAppProject
{
    public class ColorNoteTests
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
                App = @"C:\Users\denni\Downloads\Notepad.apk"
            };
            androidOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);

            _driver = new AndroidDriver(_appiumLocalService, androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            try
            {
                var skipTutorialButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/btn_start_skip"));
                skipTutorialButton.Click();
            }
            catch (NoSuchElementException){}
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _driver?.Quit();
            _driver?.Dispose();
            _appiumLocalService?.Dispose();

        }

        [Test, Order(1)]
        public void Test_CreateNewNote()
        {
            IWebElement addNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            addNoteButton.Click();

            IWebElement createNoteTest = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createNoteTest.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test_1");

            IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement createdNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));

            Assert.That(createdNote, Is.Not.Null, "Note was not created");
            Assert.That(createdNote.Text, Is.EqualTo("Test_1"));
            
        }

        [Test, Order(2)]
        public void Test_Edit_CreatedNote()
        {
            IWebElement addNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            addNoteButton.Click();

            IWebElement createNoteTest = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createNoteTest.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Test_2");

            IWebElement backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement note = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));
            note.Click();

            IWebElement editButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_btn"));
            editButton.Click();

            noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.Clear();
            
            noteTextField.SendKeys("Edited_Test_2");

            backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement editedNote = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/title"));
            Assert.That(editedNote.Text, Is.EqualTo("Edited_Test_2"));

        }

        [Test, Order(3)]
        public void Test_Delete_CreatedNote()
        {
            IWebElement addNoteButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/main_btn1"));
            addNoteButton.Click();

            IWebElement createNoteTest = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Text\")"));
            createNoteTest.Click();

            IWebElement noteTextField = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/edit_note"));
            noteTextField.SendKeys("Note_For_Delete");

            var backButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/back_btn"));
            backButton.Click();
            backButton.Click();

            IWebElement note = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Note_For_Delete\")"));
            note.Click();


            IWebElement menuButton = _driver.FindElement(MobileBy.Id("com.socialnmobile.dictapps.notepad.color.note:id/menu_btn"));
            menuButton.Click();

            IWebElement deleteButton = _driver.FindElement(MobileBy.AndroidUIAutomator("new UiSelector().text(\"Delete\")"));
            deleteButton.Click();

            IWebElement areYouSureYES = _driver.FindElement(MobileBy.Id("android:id/button1"));
            areYouSureYES.Click();

            var deletedNote = _driver.FindElements(MobileBy.XPath("//android.widget.ListView[@text='Note_For_Delete']"));
            Assert.That(deletedNote, Is.Empty);

        }
    }
}