using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POMExercise.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExercise.Tests
{
    public class BasePageTests
    {
        protected IWebDriver driver;

        protected LoginPage loginPage;
        protected InventoryPage inventoryPage;
        protected CartPage cartPage;
        protected CheckoutPage checkoutPage;
        protected HiddenMenuPage hiddenPage;

        [SetUp]
        public void SetUp()
        {
            var chromeOption = new ChromeOptions();
            chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(chromeOption);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);
            hiddenPage = new HiddenMenuPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null) 
            {
                driver.Quit();
                driver.Dispose();
            }
            
        }

        protected void Login(string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.LoginUser(username, password);
        }
    }
}
