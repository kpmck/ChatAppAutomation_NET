using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace ChatAppAutomation.Pages
{
    public class ChatRoom : BasePageClass
    {
        private readonly IWebDriver _driver;

        public ChatRoom(IWebDriver Driver):base(Driver)
        {
            _driver = Driver;
        }

        private By RoomCreatedOk = By.CssSelector(".X8501 input");
        private By ChatInputField = By.Id("X9225");
        private By ChatSendButton = By.Id("X1117");
        private By ChatMessage(string user, string message)
        {
            return By.XPath($"//*[text()='{user}']/parent::*[contains(text(),\"{message}\")][last()]");
        }

        public bool IsAt()
        {
            return IsAt(ChatInputField);
        }

        public bool ChatMessageExists(string user, string message)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(ChatMessage(user, message)));
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal void AcceptPopup()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(RoomCreatedOk)).Click();
            }
            catch (StaleElementReferenceException)
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(RoomCreatedOk)).Click();
            }
        }

        public void SendChat(string text)
        {
            _driver.FindElement(ChatInputField).SendKeys(text);
            _driver.FindElement(ChatSendButton).Click();
        }

    }
}
