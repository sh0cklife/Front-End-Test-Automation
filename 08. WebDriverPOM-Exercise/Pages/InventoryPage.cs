using OpenQA.Selenium;

namespace POMExercise.Pages
{
    public class InventoryPage : BasePage
    {
        public InventoryPage(IWebDriver driver) : base(driver) { }

        protected readonly By cartlink = By.CssSelector(".shopping_cart_link");
        protected readonly By productsPageTitle = By.ClassName("title");
        protected readonly By productItems = By.CssSelector(".inventory_item");

        public void AddToCartByIndex(int itemIndex)
        {
            var itemBuyIndexButton = By.XPath($"//div[@class='inventory_list']//div[@class='inventory_item'][{itemIndex}]//button");
            Click(itemBuyIndexButton);
        }
        public void AddToCartByName(string name)
        {
            var itemBuyNameButton = By.XPath($"//div[text() = '{name}']/ancestor::div[@class='inventory_item_description']//button");
            Click(itemBuyNameButton);
        }
        public void ClickCartLink()
        {
            Click(cartlink);
        }
        public bool InventoryPageItemsDisplayed()
        {
            return FindElements(productItems).Any();
        }
        public bool IsInventoryPageLoaded()
        {
            return GetText(productsPageTitle) == "Products" && driver.Url.Contains("inventory.html");
        }


    }
}
