using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCenterExamPrep.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public string Url = BaseUrl + "/Users/Login";

        public IWebElement EmailInput => driver.FindElement(By.XPath("//input[@type='email']"));
        public IWebElement PasswordInput => driver.FindElement(By.XPath("//input[@type='password']"));
        public IWebElement SignInButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']"));

        public void Login(string email, string password)
        {
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignInButton.Click();
        }
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

    }
}
