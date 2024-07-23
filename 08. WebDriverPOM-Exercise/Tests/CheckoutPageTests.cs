namespace POMExercise.Tests
{
    public class CheckoutPageTests : BasePageTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(2);
            inventoryPage.ClickCartLink();
            cartPage.ClickCheckoutButton();
        }

        [Test]
        public void TestCheckoutPageLoaded()
        {
            Assert.True(checkoutPage.IsPageLoaded(), "Error Message: Checkout Page is not loaded");
        }

        [Test]
        public void TestContinueToNextStep()
        {
            checkoutPage.FillFirstName("Demo");
            checkoutPage.FillLastName("User");
            checkoutPage.FillPostalCode("1000");
            checkoutPage.ClickOnContinue();

            Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True, "Error Message: Not navigated to the correct page");
        }

        [Test]
        public void TestCompleteOrder()
        {
            checkoutPage.FillFirstName("Demo");
            checkoutPage.FillLastName("User");
            checkoutPage.FillPostalCode("1000");
            checkoutPage.ClickOnContinue();
            checkoutPage.ClickOnFinish();

            Assert.That(checkoutPage.IsCheckoutComplete(), Is.True, "Error Message: Checkout is not completed");
        }

    }
}
