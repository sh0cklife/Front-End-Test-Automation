using OpenQA.Selenium;

namespace StudentRegistryPOM.Pages
{
    public class AddStudentPage : BasePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver)
        {

        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/add-student";

        public IWebElement NameField => driver.FindElement(By.Id("name"));
        public IWebElement EmailField => driver.FindElement(By.Id("email"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//form//button[@type='submit']"));

        
        public IWebElement ErrorMessage => driver.FindElement(By.CssSelector("body > div"));

        public void AddStudent(string name, string email)
        {
            this.NameField.SendKeys(name);
            this.EmailField.SendKeys(email);
            this.AddButton.Click();
        }
        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
            //Cannot add student. Name and email fields are required!
        }

    }
}
