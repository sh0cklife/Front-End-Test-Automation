using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace WebDriverWikiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new object of type ChromeDriver aka create Browser (Chrome)
            WebDriver driver = new ChromeDriver();

            //navigate to Wikipedia page
            driver.Url = "https://www.wikipedia.org/";

            //find the search field
            var searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.Click();

            //type Quality Assurance and press ENTER
            searchInput.SendKeys("Quality Assurance" + Keys.Enter);

            //get the page title for validation
            var currentPageTitle = driver.Title;

            //print the page title
            Console.WriteLine("Current page title is: " + currentPageTitle);

            if (currentPageTitle == "Quality assurance - Wikipedia")
            {
                Console.WriteLine("***** TEST PASSED *****");
            }
            else
            {
                Console.WriteLine("***** TEST FAILED *****");
            }

            //close the browser
            driver.Quit();

        }
    }
}
