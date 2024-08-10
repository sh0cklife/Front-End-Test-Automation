using IdeaCenterExamPrep.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IdeaCenterExamPrep.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public LoginPage loginPage;
        public CreateIdeaPage createIdeaPage;
        public MyIdeasPage myIdeasPage;
        public IdeasReadPage ideasReadPage;
        public IdeasEditPage ideasEditPage;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var ChromeOptions = new ChromeOptions();
            ChromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            ChromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(ChromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            createIdeaPage = new CreateIdeaPage(driver);
            myIdeasPage = new MyIdeasPage(driver);
            ideasReadPage = new IdeasReadPage(driver);
            ideasEditPage = new IdeasEditPage(driver);

            loginPage.OpenPage();
            loginPage.Login("denisqa@abv.bg", "123456");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();

        }

        public string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}