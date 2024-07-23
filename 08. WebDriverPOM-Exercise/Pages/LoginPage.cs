using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;

namespace POMExercise.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By usernameField = By.XPath("//input[@data-test='username']");
        private readonly By passwordField = By.XPath("//input[@data-test='password']");
        private readonly By loginButton = By.XPath("//input[@data-test='login-button']");
        private readonly By errorMessage = By.CssSelector(".error-message-container.error");
        public LoginPage(IWebDriver driver) : base(driver) 
        {
        }
        public void InputUsername(string username)
        {
            Type(usernameField, username);
        }
        public void InputPassword(string password)
        {
            Type(passwordField, password);
        }
        public void ClickLoginButton()
        {
            Click(loginButton);
        }
        public string GetErrorMessage()
        {
            return GetText(errorMessage);
        }
        public void LoginUser(string username, string password)
        {
            InputUsername(username);
            InputPassword(password);
            ClickLoginButton();
            
        }
    }
}
