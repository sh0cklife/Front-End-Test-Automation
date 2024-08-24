using FoodAppyRegularExam.Pages;
using OpenQA.Selenium;

namespace FoodAppyRegularExam.Tests
{
    public class FoodyAppTests : BaseTest
    {
        private string lastFoodName;
        private string lastFoodDescription;

        [Test, Order(1)]
        public void AddFoodWithInvalidDataTest()
        {
            addFoodPage.OpenPage();
            addFoodPage.AddFoodReview("", "");
            addFoodPage.AssertEmptyDescription();
            addFoodPage.AssertEmptyName();
        }

        [Test, Order(2)]
        public void AddFoodWithValidDataTest()
        {
            lastFoodName = GenerateRandomFood();
            lastFoodDescription = GenerateRandomDescription();

            addFoodPage.OpenPage();
            addFoodPage.AddFoodReview(lastFoodName, lastFoodDescription);

            Assert.That(driver.Url, Is.EqualTo("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/"));
            Assert.That(allFoodPage.LastFoodName.Text.Trim(), Is.EqualTo(lastFoodName));
        }

        [Test, Order(3)]
        public void EditLastAddedFoodTest()
        {
            allFoodPage.OpenPage();
            actions.ScrollToElement(allFoodPage.LastFoodEditButton).Perform();
            allFoodPage.LastFoodEditButton.Click();

            editFoodPage.EditFood(lastFoodName, lastFoodDescription);

            string actualTitle = allFoodPage.LastFoodName.Text.Trim();
            string expectedTitle = lastFoodName;

            try
            {
                Assert.AreEqual(actualTitle, expectedTitle, "Title should remain unchanged due to incomplete functionality.");
                Console.WriteLine("Title remains unchanged as expected.");
            }
            catch (AssertionException)
            {
                Console.WriteLine($"Assertion failed: Expected title to remain '{actualTitle}', but found '{expectedTitle}' instead.");
                throw;
            }

        }

        [Test, Order(4)]
        public void SearchForFoodTitleTest()
        {
            basePage.SearchBar.SendKeys(lastFoodName);
            basePage.SearchButton.Click();

            var result = driver.FindElement(By.XPath("//h2[@class='display-4']"));
            
            Assert.That(lastFoodName, Is.EqualTo(result.Text));
            
        }

        [Test, Order(5)]
        public void DeleteLastAddedFoodTest()
        {
            allFoodPage.OpenPage();

            var initialCount = allFoodPage.AllFoodsOnPage.Count();
            Console.WriteLine(initialCount);

            actions.ScrollToElement(allFoodPage.LastFoodDeleteButton).Perform();
            allFoodPage.LastFoodDeleteButton.Click();

            var afterCount = allFoodPage.AllFoodsOnPage.Count();
            Console.WriteLine(afterCount);

            Assert.That(afterCount < initialCount, "Deletion not successfull");

        }

        [Test, Order(6)]
        public void SearchForDeletedFoodTest()
        {
            allFoodPage.OpenPage();
            basePage.SearchBar.SendKeys(lastFoodName);
            basePage.SearchButton.Click();

            var result = driver.FindElement(By.XPath("//h2[@class='display-4']"));
            var button = driver.FindElement(By.XPath("//a[@class='btn btn-primary btn-xl rounded-pill mt-5']"));

            Assert.That(result.Text, Is.EqualTo("There are no foods :("));
            Assert.IsTrue(button.Displayed, "The addButton is not visible on the page");
        }
    }
}