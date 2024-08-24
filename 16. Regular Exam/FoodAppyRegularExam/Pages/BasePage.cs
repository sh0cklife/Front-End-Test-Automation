using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Pages
{
    public class BasePage
    {
        public IWebDriver driver;
        protected WebDriverWait wait;
        protected static string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement LoginLink => driver.FindElement(By.XPath("//a[text()='Log In']"));
        public IWebElement LogoutLink => driver.FindElement(By.XPath("//a[text()='Logout']"));
        public IWebElement AddFoodLink => driver.FindElement(By.XPath("//a[text()='Add Food']"));
        public IWebElement SearchBar => driver.FindElement(By.XPath("//input[@name='keyword']"));
        public IWebElement SearchButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary rounded-pill mt-5 col-2']"));
        public IWebElement MyProfileLink => driver.FindElement(By.XPath("//a[@class='nav-link mx-3']"));
    }
}
