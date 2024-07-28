using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumExerciseProject
{
    public class SummatorPage
    {
        private readonly AndroidDriver _driver;

        public SummatorPage(AndroidDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FirstInput => _driver.FindElement(MobileBy.Id("editText1"));
        public IWebElement SecondInput => _driver.FindElement(MobileBy.Id("editText2"));
        public IWebElement CalculateButton => _driver.FindElement(MobileBy.Id("buttonCalcSum"));
        public IWebElement ResultOutput => _driver.FindElement(MobileBy.Id("editTextSum"));

        public string Calculate(string input1, string input2)
        {
            ClearFields();

            FirstInput.SendKeys(input1);
            SecondInput.SendKeys(input2);
            CalculateButton.Click();

            return ResultOutput.Text;
        }

        public void ClearFields()
        {
            FirstInput.Clear();
            SecondInput.Clear();
        }
    }
}
