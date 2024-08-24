using FoodAppyRegularExam.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public Actions actions;

        public LoginPage loginPage;
        public AddFoodPage addFoodPage;
        public AllFoodsPage allFoodPage;
        public EditPage editFoodPage;
        public BasePage basePage;

        [OneTimeSetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");


            driver = new ChromeDriver(chromeOptions);
            actions = new Actions(driver);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            addFoodPage = new AddFoodPage(driver);
            allFoodPage = new AllFoodsPage(driver);
            editFoodPage = new EditPage(driver);
            basePage = new BasePage(driver);


            loginPage.OpenPage();
            loginPage.Login("denisqa", "123456");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public string GenerateRandomFood()
        {
            var random = new Random();
            return "Random Food: " + random.Next(1000, 10000);
        }

        public string GenerateRandomDescription()
        {
            var random = new Random();
            return "Random Description: " + random.Next(1000, 10000);
        }
    }
}
