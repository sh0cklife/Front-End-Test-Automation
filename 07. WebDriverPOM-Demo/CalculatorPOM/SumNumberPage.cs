using OpenQA.Selenium;

namespace CalculatorPOM
{
    public class SumNumberPage
    {
        private readonly IWebDriver driver;

        public SumNumberPage(IWebDriver driver)
        {
            this.driver = driver;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public const string PageUrl = "https://d58319ae-d936-4ea0-a15e-f3e1167b62c6-00-1jmhojvqg7iap.picard.replit.dev/";

        public IWebElement FieldNum1 => driver.FindElement(By.Id("number1"));
        public IWebElement FieldNum2 => driver.FindElement(By.Id("number2"));
        public IWebElement CalcButton => driver.FindElement(By.Id("calcButton"));
        public IWebElement ResetButton => driver.FindElement(By.Id("resetButton"));
        public IWebElement ResultElement => driver.FindElement(By.XPath("//body//form//div[@id='result']"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }
        public void ResetForm()
        {
            ResetButton.Click();
        }
        public bool IsFormEmpty()
        {
            return FieldNum1.Text + FieldNum2.Text + ResultElement.Text == "";
        }
        public string AddNumber(string num1, string num2)
        {
            FieldNum1.SendKeys(num1);
            FieldNum2.SendKeys(num2);
            CalcButton.Click();
            return ResultElement.Text;
        }
    }
}
