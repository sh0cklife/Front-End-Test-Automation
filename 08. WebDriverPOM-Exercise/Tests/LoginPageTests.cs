using POMExercise.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExercise.Tests
{
    public class LoginPageTests : BasePageTests
    {
        [Test]
        public void TestLoginWithValidCredentials()
        {
            Login("standard_user", "secret_sauce");

            //var inventoryPage = new InventoryPage(driver);
            Assert.That(inventoryPage.IsInventoryPageLoaded(), Is.True, "The Inventory Page is NOT Loaded after success LOGIN");
        }

        [Test]
        public void TestLoginWithInvalidCredentials()
        {
            Login("invalid_username", "secret_sauce");

            //var loginPage = new LoginPage(driver);
            string error = loginPage.GetErrorMessage();
            Assert.That(error.Contains("Epic sadface: Username and password do not match any user in this service"), "Error message is not as expected!");
        }

        [Test]
        public void TestLoginWithLockedOutUser()
        {
            Login("locked_out_user", "secret_sauce");

            //var loginPage = new LoginPage(driver);
            string error = loginPage.GetErrorMessage();
            Assert.That(error.Contains("Epic sadface: Sorry, this user has been locked out."), "Error message is not as expected!");
        }
    }
}
