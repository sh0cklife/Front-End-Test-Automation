using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTables
{
    public class WebTablesTests
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
        public void TeardDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void WorkingWithTableElements()
        {
            //Locate the table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            ReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

            //Create a CSV file to save product information:
            //Path to save the CSV file
            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if (File.Exists(path)) { 
                File.Delete(path);
            }

            foreach (IWebElement row in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath(".//td")); //search in the current row "."

                foreach (var tData in tableData)
                {
                    string data = tData.Text;
                    string[] productInfo = data.Split("\n");

                    File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");
                }
            }

            Assert.IsTrue(File.Exists(path));
            Assert.IsTrue(new FileInfo(path).Length > 0);
        }
    }
}