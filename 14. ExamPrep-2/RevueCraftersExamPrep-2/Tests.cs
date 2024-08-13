using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Text;

namespace RevueCraftersExamPrep_2
{
    public class Tests
    {
        private readonly static string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net";
        private IWebDriver driver;
        private Actions actions;
        private string LastCreatedTitle;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var ChromeOptions = new ChromeOptions();
            ChromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            ChromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(ChromeOptions);
            actions = new Actions(driver);
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl($"{BaseUrl}/Users/Login");
            var loginForm = driver.FindElement(By.XPath("//form[@method='post']"));
            actions.ScrollToElement(loginForm).Perform();

            driver.FindElement(By.Id("form3Example3")).Clear();
            driver.FindElement(By.Id("form3Example3")).SendKeys("denisqa@abv.bg");
            driver.FindElement(By.Id("form3Example4")).Clear();
            driver.FindElement(By.Id("form3Example4")).SendKeys("123456");

            driver.FindElement(By.CssSelector(".btn")).Click();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test, Order(1)]
        public void CreateRevueWithInvalidData()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/Create");

            var formCard = driver.FindElement(By.CssSelector(".card-body"));
            actions.ScrollToElement(formCard).Perform();

            var titleInput = driver.FindElement(By.Id("form3Example1c"));
            titleInput.Clear();
            titleInput.SendKeys("");

            var descriptionInput = driver.FindElement(By.Id("form3Example4cd"));
            descriptionInput.Clear();
            descriptionInput.SendKeys("");

            driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/Create"), "user should remain on the same page with same URL");

            //var errorMessage = driver.FindElement(By.XPath("//div[contains(@class, 'card-body')]//li"));
            var errorMessage = driver.FindElement(By.CssSelector(".card-body li"));
            Assert.That(errorMessage.Text, Is.EqualTo("Unable to create new Revue!"), "The error message for invalid data when creating Revue is not there");

        }

        [Test, Order(2)]
        public void TestCreateRandomTitleRevue()
        {

            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/Create");

            var formCard = driver.FindElement(By.CssSelector(".card-body"));
            actions.ScrollToElement(formCard).Perform();

            var titleInput = driver.FindElement(By.Id("form3Example1c"));
            titleInput.Clear();
            LastCreatedTitle = $"Test Title: {GenerateRandomString(6)}" ;
            titleInput.SendKeys(LastCreatedTitle);

            var descriptionInput = driver.FindElement(By.Id("form3Example4cd"));
            descriptionInput.Clear();
            var newRevueDescription = GenerateRandomString(40);
            descriptionInput.SendKeys(newRevueDescription);

            driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"), "User was not redirected to My Revues Page.");

            var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
            var lastRevueTitle = revues.Last().FindElement(By.CssSelector(".text-muted")).Text;
            Assert.That(lastRevueTitle, Is.EqualTo(LastCreatedTitle), "The last Revue is not present on the screen.");
        }

        [Test, Order(3)]
        public void TestSearchRevueByTitle()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");

            var searchField = driver.FindElement(By.Id("keyword"));
            actions.ScrollToElement(searchField).Perform();

            searchField.SendKeys(LastCreatedTitle);
            driver.FindElement(By.Id("search-button")).Click();

            var searchResutRevueTitle = driver.FindElement(By.CssSelector(".text-muted")).Text;
            Assert.That(searchResutRevueTitle, Is.EqualTo(LastCreatedTitle), "The search resulting Revue is not present on the screen.");

        }


        [Test, Order(4)]
        public void TestEditLastCreated()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");

            var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
            Assert.That(revues.Count(), Is.AtLeast(1), "There are no Revues");

            var lastRevue = revues.Last();
            actions.ScrollToElement(lastRevue).Perform();

            driver.FindElement(By.XPath($"//div[text()='{LastCreatedTitle}']/..//a[text()='Edit']")).Click();

            var formCard = driver.FindElement(By.CssSelector(".card-body"));
            actions.ScrollToElement(formCard).Perform();

            var titleInput = driver.FindElement(By.Id("form3Example1c"));
            titleInput.Clear();
            LastCreatedTitle = GenerateRandomString(6) + " updated";
            titleInput.SendKeys(LastCreatedTitle);

            driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"), "User was not redirected to My Revues Page.");

            var revuesResult = driver.FindElements(By.CssSelector(".card.mb-4"));
            var lastRevueTitle = revuesResult.Last().FindElement(By.CssSelector(".text-muted")).Text;
            Assert.That(lastRevueTitle, Is.EqualTo(LastCreatedTitle), "The last Revue is not present on the screen.");

        }

        [Test, Order(5)]
        public void TestDeleteLastCreated()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");

            var revues = driver.FindElements(By.CssSelector(".card.mb-4"));
            Assert.That(revues.Count(), Is.AtLeast(1), "There are no Revues");

            var lastRevue = revues.Last();
            actions.ScrollToElement(lastRevue).Perform();

            driver.FindElement(By.XPath($"//div[text()='{LastCreatedTitle}']/..//a[text()='Delete']")).Click();

            var currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo($"{BaseUrl}/Revue/MyRevues"), "User was not redirected to My Revues Page.");

            var revuesResult = driver.FindElements(By.CssSelector(".card.mb-4"));
            Assert.That(revuesResult.Count(), Is.LessThan(revues.Count()), "The number of Revues did not decrease");

            var lastRevueTitle = revuesResult.Last().FindElement(By.CssSelector(".text-muted")).Text;
            Assert.That(lastRevueTitle, !Is.EqualTo(LastCreatedTitle), "The last Revue is not present on the screen.");

        }

        [Test, Order(6)]
        public void TestNonExistingRevue()
        {
            driver.Navigate().GoToUrl($"{BaseUrl}/Revue/MyRevues");

            var searchField = driver.FindElement(By.Id("keyword"));
            actions.ScrollToElement(searchField).Perform();

            searchField.SendKeys("non-existing-revue");

            driver.FindElement(By.Id("search-button")).Click();

            var noResultsMessage = driver.FindElement(By.CssSelector(".text-muted")).Text;

            Assert.That(noResultsMessage, Is.EqualTo("No Revues yet!"));
        }

        public static string GenerateRandomString(int length)
        {
            char[] chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            if (length <= 0)
            {
                throw new ArgumentException("Length must be greater than zero.", nameof(length));
            }

            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }



    }
}