using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Pages
{
    public class AddFoodPage : BasePage
    {
        public AddFoodPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "Food/Add";

        public IWebElement FoodNameInput => driver.FindElement(By.XPath("//input[@name='Name']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//input[@name='Description']"));
        public IWebElement PictureInput => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement AddFoodButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

        //The Name field is required.
        public IWebElement FoodNameErrorMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[0];
        //The Description field is required.
        public IWebElement FoodDescriptionErorrMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[1];

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void AddFoodReview(string foodName, string foodDescription)
        {
            FoodNameInput.Clear();
            FoodNameInput.SendKeys(foodName);
            DescriptionInput.Clear();
            DescriptionInput.SendKeys(foodDescription);
            AddFoodButton.Click();
        }

        public void AssertEmptyName()
        {
            Assert.That(FoodNameErrorMessage.Text.Trim(), Is.EqualTo("The Name field is required."));
        }
        public void AssertEmptyDescription()
        {
            Assert.That(FoodDescriptionErorrMessage.Text.Trim(), Is.EqualTo("The Description field is required."));
        }
    }
}
