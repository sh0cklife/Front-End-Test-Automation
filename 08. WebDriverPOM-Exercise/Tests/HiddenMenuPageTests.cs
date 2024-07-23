namespace POMExercise.Tests
{
    public class HiddenMenuPageTests : BasePageTests
    {
        [SetUp]
        public void Setup()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestOpenMenu()
        {
            hiddenPage.ClickHamburgerMenuButton();
            Assert.True(hiddenPage.IsMenuOpen(), "Hidden menu was not open");
        }

        [Test]
        public void TestLogout()
        {
            hiddenPage.ClickHamburgerMenuButton();
            hiddenPage.ClickLogoutButon();
            Assert.That(driver.Url.Equals("https://www.saucedemo.com/"), "User was not logged out");
        }
    }
}
