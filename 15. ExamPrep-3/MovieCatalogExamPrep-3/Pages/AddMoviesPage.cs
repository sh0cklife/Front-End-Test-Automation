using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Pages
{
    public class AddMoviesPage : BasePage
    {
        public AddMoviesPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "Catalog/Add#add";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement AddMovieButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));
        public IWebElement MarkAsReadCheckBox => driver.FindElement(By.XPath("//input[@class='form-check-input']"));

        public IWebElement TitleErrorMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));
        public IWebElement DescriptionErrorMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void AddMovie(string title, string description)
        {
            TitleInput.Clear();
            TitleInput.SendKeys(title);
            DescriptionInput.Clear();
            DescriptionInput.SendKeys(description);
            AddMovieButton.Click();
        }
        public void AssertEmptyTitleMessage()
        {
            Assert.That(TitleErrorMessage.Text.Trim(), Is.EqualTo("The Title field is required."));
        }
        public void AssertEmptyDescriptionMessage()
        {
            Assert.That(DescriptionErrorMessage.Text.Trim(), Is.EqualTo("The Description field is required."));
        }
    }
}
