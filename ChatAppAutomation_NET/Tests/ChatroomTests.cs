using ChatAppAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ChatAppAutomation.Tests
{

    [TestFixture]
    public class ChatRoomTests : BaseTestClass
    {

        private static string applicationPath = "https://www.chatzy.com";
        private string firstUser = "George Harris";
        private string secondUser = "Janet Solis";

        //Instantiate both chat drivers
        private IWebDriver firstDriver;
        private IWebDriver secondDriver;

        //Instantiate page models for both chat drivers
        private ChatRoom firstUserChat;
        private ChatRoom secondUserChat;

        private Homepage firstHomePage;
        private Homepage secondHomePage;


        [SetUp]
        public void SetUp()
        {
            //Initialize first chat driver and accompanying page models
            firstDriver = new ChromeDriver();
            drivers.Add(firstDriver);

            firstUserChat = new ChatRoom(firstDriver);
            firstHomePage = new Homepage(firstDriver);

            firstDriver.Navigate().GoToUrl(applicationPath);
        }

        [Test]
        public void Validate_User_Starts_Chat_Notification()
        {
            firstHomePage.SignIn(firstUser);
            Assert.IsTrue(firstUserChat.ChatMessageExists(firstUser, "started the chat"));
        }

        [Test]
        public void Validate_User_Joined_Chat_Notification()
        {
            //Initialize the second chat driver and accompanying page models
            secondDriver = new ChromeDriver();
            drivers.Add(secondDriver);
            secondUserChat = new ChatRoom(secondDriver);
            secondHomePage = new Homepage(secondDriver);

            //Navigate to the first user's chat and log in
            Assert.IsTrue(firstHomePage.IsAt(), "First user's homepage was not loaded.");
            firstHomePage.SignIn(firstUser);
            Assert.IsTrue(firstUserChat.IsAt(), "first user's chat room was not loaded.");

            //Grab the first user's chat link and use it to log the second chat user in
            var chatRoomUrl = firstDriver.Url;
            secondDriver.Navigate().GoToUrl(chatRoomUrl);
            Assert.IsTrue(secondHomePage.IsAt(), "Second user's homepage was not loaded.");
            secondHomePage.JoinChat(secondUser);
            Assert.IsTrue(secondUserChat.IsAt(), "Second user's chat room was not loaded.");

            //Verify both chats notify second user's arrival
            Assert.IsTrue(firstUserChat.ChatMessageExists(secondUser, "joined the chat"));
            Assert.IsTrue(secondUserChat.ChatMessageExists(secondUser, "joined the chat"));
        }

        [Test]
        public void Validate_Users_Chat_Messages()
        {
            firstHomePage.IsAt();

            //Initialize the second driver and accompanying page models
            secondDriver = new ChromeDriver();
            drivers.Add(secondDriver);
            secondUserChat = new ChatRoom(secondDriver);
            secondHomePage = new Homepage(secondDriver);

            //Sign in as the first user
            firstHomePage.SignIn(firstUser);
            firstUserChat.IsAt();

            //Sign in as the second user using the first user's chat url
            var chatRoomUrl = firstDriver.Url;
            secondDriver.Navigate().GoToUrl(chatRoomUrl);
            secondHomePage.IsAt();
            secondHomePage.JoinChat(secondUser);
            secondUserChat.IsAt();

            //Send chat as first user and verify second user sees the chat notification
            firstUserChat.SendChat("Hello Janet");
            Assert.IsTrue(secondUserChat.ChatMessageExists(firstUser, "Hello Janet"));

            //Send response as second user and verify first user sees the chat notification
            secondUserChat.SendChat("Hi George, what's up?");
            Assert.IsTrue(firstUserChat.ChatMessageExists(secondUser, "Hi George, what's up?"));
        }
    }
}
