using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Pages
{
    public class AllMoviesPage : BasePage
    {
        public AllMoviesPage(IWebDriver driver) : base(driver)
        {
            
        }

        public virtual string Url => BaseUrl + "Catalog/All#all";

        //find all pages
        public IReadOnlyCollection<IWebElement> PageIndexes => driver.FindElements(By.XPath("//a[@class='page-link']"));
        public IReadOnlyCollection<IWebElement> AllMoviesOnPage => driver.FindElements(By.XPath("//div[@class='col-lg-4']"));

        public IWebElement LastMovieTitle => AllMoviesOnPage.Last().FindElement(By.XPath(".//h2"));
        public IWebElement LastMovieEditButton => AllMoviesOnPage.Last().FindElement(By.XPath(".//a[@class='btn btn-outline-success']"));
        public IWebElement LastMovieDeleteButton => AllMoviesOnPage.Last().FindElement(By.XPath(".//a[@class='btn btn-danger']"));
        public IWebElement LastMovieMarkAsWatchedButton => AllMoviesOnPage.Last().FindElement(By.XPath(".//a[@class='btn btn-info']"));


        public virtual void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void NavigateToLastPage()
        {
            PageIndexes.Last().Click();
        }

    }
}
