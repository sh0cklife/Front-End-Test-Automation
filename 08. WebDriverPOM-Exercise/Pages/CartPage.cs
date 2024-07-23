using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        protected readonly By cartItem = By.CssSelector(".cart_item");

        protected readonly By checkOutButton = By.CssSelector("#checkout");

        public bool IsCartItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckoutButton()
        {
            Click(checkOutButton);
        }
    }
}
