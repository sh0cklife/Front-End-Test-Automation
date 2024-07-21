using OpenQA.Selenium;

namespace StudentRegistryPOM.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            //has everything from BasePage
        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/";

        public IWebElement StundetsCountElement => driver.FindElement(By.CssSelector("body > p > b"));
        public int StudentCount()
        {
            string studentsCountString = this.StundetsCountElement.Text;
            return int.Parse(studentsCountString);
        }
    }
}
