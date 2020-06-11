using OpenQA.Selenium;

namespace ChatAppAutomation.Pages
{
    public class Homepage:BasePageClass
    {
        private readonly IWebDriver _driver;

        public Homepage(IWebDriver Driver):base(Driver)
        {
            _driver = Driver;
        }

        private By Name = By.Id("X8712");
        private By Subject = By.Id("X7728");
        private By CreateChatButton = By.CssSelector(".X8501 input");
        private By EnterRoomButton = By.Id("X6668");


        public bool IsAt()
        {
            return IsAt(Name);
        }

        public void SignIn(string name)
        {
            _driver.FindElement(Name).SendKeys(name);
            _driver.FindElement(Subject).SendKeys("chat title");
            _driver.FindElement(CreateChatButton).Click();
            new ChatRoom(_driver).AcceptPopup();
        }

        public void JoinChat(string name)
        {
            _driver.FindElement(Name).SendKeys(name);
            _driver.FindElement(EnterRoomButton).Click();
        }
    }
}