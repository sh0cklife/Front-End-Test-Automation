using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace StudentRegistryPOM.Pages
{
    public class ViewStudentsPage : BasePage
    {
        public ViewStudentsPage(IWebDriver driver) : base(driver)
        {

        }

        public override string PageUrl => "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:82/students";

        public ReadOnlyCollection<IWebElement> StudentListItems => driver.FindElements(By.CssSelector("body > ul > li"));

        public string[] GetRegisteredStudents()
        {
            var studentElements = this.StudentListItems.Select(student => student.Text).ToArray();
            return studentElements;
        }
    }
}
