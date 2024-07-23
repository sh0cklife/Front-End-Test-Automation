namespace POMExercise.Tests
{
    public class CartPageTests : BasePageTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(2);
            inventoryPage.ClickCartLink();
        }
        [Test]
        public void TestCartItemDisplayed()
        {
            Assert.True(cartPage.IsCartItemDisplayed(), "Error Message: Cart is empty");
        }

        [Test]
        public void TestClickCheckout()
        {
            cartPage.ClickCheckoutButton();
            Assert.That(checkoutPage.IsPageLoaded(), Is.True, "Error Message: Not navigated to checkout page");
        }
    }
}
