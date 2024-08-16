using MovieCatalogExamPrep_3.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MovieCatalogExamPrep_3
{
    public class BaseTest
    {
        public IWebDriver driver;

        public LoginPage loginPage; 
        public AddMoviesPage addMoviesPage;
        public AllMoviesPage allMoviesPage;
        public EditMoviePage editMoviePage;
        public WatchedMoviesPage watchedMoviesPage;
        public DeletePage deletePage;

        [OneTimeSetUp]
        public void Setup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            loginPage = new LoginPage(driver);
            addMoviesPage = new AddMoviesPage(driver);
            allMoviesPage = new AllMoviesPage(driver);
            editMoviePage = new EditMoviePage(driver);
            watchedMoviesPage = new WatchedMoviesPage(driver);
            deletePage = new DeletePage(driver);


            loginPage.OpenPage();
            loginPage.Login("denisqa@abv.bg", "123456");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public string GenerateRandomTitle()
        {
            var random = new Random();
            return "TITLE: " + random.Next(1000, 10000);
        }

        public string GenerateRandomDescription()
        {
            var random = new Random();
            return "DESCRIPTION: " + random.Next(1000, 10000);
        }

    }
}