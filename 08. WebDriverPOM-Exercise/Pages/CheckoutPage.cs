using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class CheckoutPage : BasePage
    {
        public CheckoutPage(IWebDriver driver) : base(driver) { }

        protected readonly By firstNameInput = By.XPath("//input[@data-test='firstName']");
        protected readonly By lastNameInput = By.XPath("//input[@data-test='lastName']");
        protected readonly By postalCodeInput = By.XPath("//input[@data-test='postalCode']");
        protected readonly By continueButton = By.XPath("//input[@data-test='continue']");
        protected readonly By finishButton = By.CssSelector("#finish");
        protected readonly By completeHeader = By.ClassName("complete-header");

        public void FillFirstName(string firstName)
        {
            Type(firstNameInput, firstName);
        }
        public void FillLastName(string lastName)
        {
            Type(lastNameInput, lastName);
        }
        public void FillPostalCode(string postalCode)
        {
            Type(postalCodeInput, postalCode);
        }
        public void ClickOnContinue()
        {
            Click(continueButton);
        }
        public void ClickOnFinish()
        {
            Click(finishButton);
        }
        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") || driver.Url.Contains("checkout-step-two.html");
        }
        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
