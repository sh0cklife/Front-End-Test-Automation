using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCenterExamPrep.Pages
{
    public class MyIdeasPage : BasePage
    {
        public MyIdeasPage(IWebDriver driver) : base(driver) { }

        public string Url = BaseUrl + "/Ideas/MyIdeas";

        public ReadOnlyCollection<IWebElement> IdeasCards => driver.FindElements(By.XPath("//div[@class='card mb-4 box-shadow']"));

        // Map the last created idea's view button
        public IWebElement ViewButtonLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Read')]"));
        // Map the last created idea's edit button
        public IWebElement EditButtonLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Edit')]"));
        // Map the last created idea's delete button
        public IWebElement DeleteButtonLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Delete')]"));
        // Map the last created idea's description
        public IWebElement DescriptionLastIdea => IdeasCards.Last().FindElement(By.XPath(".//p[@class='card-text']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

    }
}
