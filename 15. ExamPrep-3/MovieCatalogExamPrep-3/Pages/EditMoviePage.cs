using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Pages
{
    public class EditMoviePage : BasePage
    {
        public EditMoviePage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "Movie/Edit";
        public IWebElement EditTitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement EditDescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement EditMovieButton => driver.FindElement(By.XPath("//button[@class='btn warning']"));
        public IWebElement SuccessfullyEditedMovieMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public void EditMovie(string title, string description)
        {
            EditTitleInput.Clear();
            EditTitleInput.SendKeys(title);
            EditDescriptionInput.Clear();
            EditDescriptionInput.SendKeys(description);
            EditMovieButton.Click();
        }
        public void AssertEditMessage()
        {
            Assert.That(SuccessfullyEditedMovieMessage.Text.Trim(), Is.EqualTo("The Movie is edited successfully!"), "Movie was not deleted!");
        }
    }
}
