using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogExamPrep_3.Pages
{
    public class WatchedMoviesPage : AllMoviesPage
    {
        public WatchedMoviesPage(IWebDriver driver) :base(driver)
        {
            
        }

        public override string Url => BaseUrl + "Catalog/Watched#watched";

        public IReadOnlyCollection<IWebElement> PageIndexesWatchedMovies => driver.FindElements(By.XPath("//a[@class='page-link']"));
        public IReadOnlyCollection<IWebElement> WatchedMoviesOnPage => driver.FindElements(By.XPath("//div[@class='col-lg-4']"));

        public override void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
