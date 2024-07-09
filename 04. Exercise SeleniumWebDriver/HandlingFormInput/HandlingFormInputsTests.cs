using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInput
{
    public class HandlingFormInputsTests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit(); // NB! only quit is not enough to close all references
            driver.Dispose();
        }

        [Test]
        public void HandlingFormInputs()
        {
            //click [MY ACCOUNT] button
            var myAccountButton = driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2];
            myAccountButton.Click();

            //click [CONTINUE] button
            driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[3].Click();
            //driver.FindElement(By.LinkText("Continue")).Click();

            //Click Gender
            driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

            //Type First Name:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='firstname']")).SendKeys("Denis");

            //Type Last Name:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("Atanassov");

            //Type Date of Birth:
            driver.FindElement(By.Id("dob")).SendKeys("07/09/1992");

            //build random email address
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string email = ("denis" + randomNumber.ToString() + "@gmail.com");

            //Type email address
            driver.FindElement(By.Name("email_address")).SendKeys(email);

            //Type Company Name:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='company']")).SendKeys("TTEC");

            //Type Street Name:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']")).SendKeys("Apple 3");

            //Type Suburb Name:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='suburb']")).SendKeys("Nadezhda 2");

            //Type Postal Code:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='postcode']")).SendKeys("1220");

            //Type City:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='city']")).SendKeys("Sofia");

            //Type Province:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='state']")).SendKeys("Sofia-City");

            //Select from Country dropdown - Installed Selenium Support Nuget package
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            //Type Telephone Number:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='telephone']")).SendKeys("0888123456");

            //Click Newsletter checkbox:
            driver.FindElement(By.XPath("//input[@name='newsletter']")).Click();

            //Type Password:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='password']")).SendKeys("123456789");

            //Type Confirm Password:
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='confirmation']")).SendKeys("123456789");

            //Click [Continue] button
            driver.FindElements(By.XPath("//span[@class='ui-button-icon-primary ui-icon ui-icon-person']//following-sibling::span"))[1].Click();

            //Assert account creation success
            Assert.IsTrue(driver.PageSource.Contains("Your Account Has Been Created!"), "Account creation failed.");

            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!");

            //click [LOG OFF] button
            driver.FindElement(By.LinkText("Log Off")).Click();

            //click [CONTINUE] button
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Created successfully!");
        }
    }
}