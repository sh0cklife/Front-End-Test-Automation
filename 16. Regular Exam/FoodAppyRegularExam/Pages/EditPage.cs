using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodAppyRegularExam.Pages
{
    public class EditPage : BasePage
    {
        public EditPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl + "Food/Edit";

        public IWebElement EditFoodNameInput => driver.FindElement(By.XPath("//input[@name='Name']"));
        public IWebElement EditDescriptionInput => driver.FindElement(By.XPath("//input[@name='Description']"));
        public IWebElement EditPictureInput => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement AddFoodButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-block fa-lg gradient-custom-2 mb-3']"));

        public void EditFood(string editName, string editDescription)
        {
            EditFoodNameInput.Clear();
            EditFoodNameInput.SendKeys(editName + "EDITED");
            EditDescriptionInput.Clear();
            EditPictureInput.SendKeys(editDescription + "EDITED");
            AddFoodButton.Click();
        }
    }
}
