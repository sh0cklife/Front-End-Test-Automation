using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) :base(driver)
        {
            
        }

        public string Url = BaseUrl + "User/Login";

        public IWebElement UserNameInput => driver.FindElement(By.XPath("//input[@name='Username']"));
        public IWebElement PasswordInput => driver.FindElement(By.XPath("//input[@name='Password']"));
        public IWebElement LoginButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
        public void Login(string username, string password)
        {
            UserNameInput.Clear();
            UserNameInput.SendKeys(username);
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
    }
}
