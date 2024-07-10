using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DropdownManipulations
{
    public class DropdownManipulations_Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }


        [Test]
        public void ExtractInformationBasedOnDropDownOption()
        {
            // create txt file
            string path = System.IO.Directory.GetCurrentDirectory() + "/manufacturers.txt";

            // map dropdown menu
            SelectElement dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

            IList<IWebElement> options = dropdown.Options; // can be var too

            List<string> optionsAsString = new List<string>();
            foreach (var option in options) 
            {
                optionsAsString.Add(option.Text);
            }

            optionsAsString.RemoveAt(0); // remove the template option

            foreach (var option in optionsAsString)
            {
                dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));
                dropdown.SelectByText(option);
                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {option} has no products");
                }
                else
                {
                    IWebElement productsTable = driver.FindElement(By.ClassName("productListingData"));

                    File.AppendAllText(path, $"\n\n The manufacturer {option} products are listen below -- \n");

                    IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

                    foreach (var row in tableRows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }

        }
    }
}