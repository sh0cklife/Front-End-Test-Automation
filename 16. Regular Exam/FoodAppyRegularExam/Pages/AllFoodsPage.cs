using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Pages
{
    public class AllFoodsPage : BasePage
    {
        public AllFoodsPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl;

        public IReadOnlyCollection<IWebElement> AllFoodsOnPage => driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']"));
        public IWebElement LastFoodOnPage => AllFoodsOnPage.Last();
        public IWebElement LastFoodName => LastFoodOnPage.FindElement(By.XPath(".//h2[@class='display-4']"));
        public IWebElement LastFoodDescription => LastFoodOnPage.FindElement(By.XPath(".//p[@class='flex-lg-wrap']"));
        public IWebElement LastFoodImage => LastFoodOnPage.FindElement(By.XPath(".//img[@class='img-fluid rounded-circle']"));
        public IWebElement LastFoodEditButton => LastFoodOnPage.FindElement(By.XPath(".//a[text()='Edit']"));
        public IWebElement LastFoodDeleteButton => LastFoodOnPage.FindElement(By.XPath(".//a[text()='Delete']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        
    }
}
