using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class HiddenMenuPage : BasePage
    {
        public HiddenMenuPage(IWebDriver driver) : base(driver) { }

        protected readonly By hamburgerMenuButton = By.Id("react-burger-menu-btn");
        protected readonly By logoutButon = By.XPath("//a[@data-test='logout-sidebar-link']");
        public void ClickHamburgerMenuButton()
        {
            Click(hamburgerMenuButton);
        }
        public void ClickLogoutButon()
        {
            Click(logoutButon);
        }
        public bool IsMenuOpen()
        {
            return FindElement(logoutButon).Displayed;
        }
    }
}
