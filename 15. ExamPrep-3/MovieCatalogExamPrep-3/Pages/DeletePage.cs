using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Pages
{
    public class DeletePage : BasePage
    {
        public DeletePage(IWebDriver driver) : base(driver)
        {
            
        }

        public IWebElement YesButton => driver.FindElement(By.XPath("//button[@type='submit' and text()='Yes']"));
        public IWebElement SuccessfullyDeletedMovieMessage => driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public void AssertMessage()
        {
            Assert.That(SuccessfullyDeletedMovieMessage.Text.Trim(), Is.EqualTo("The Movie is deleted successfully!"));
        }
    }
}
