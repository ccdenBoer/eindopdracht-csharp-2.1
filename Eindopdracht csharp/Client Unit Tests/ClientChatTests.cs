using Eindopdracht_csharp;

namespace Client_Unit_Tests
{
    [TestClass]
    public class ClientChatTests
    {
        [TestMethod]
        public void AddMessageTest()
        {
            ChatScreen chatScreen = new ChatScreen("testChat", true);
            chatScreen.Show();

            string time = DateTime.Now.ToString();
            string chatName = "testChat";
            chatScreen.AddMessage("testChat", time, "message 1", true);
            chatScreen.AddMessage("testChat", time, "message 2", false);
            chatScreen.AddMessage("testChat", time, "message 3", true);

            var items = chatScreen.GetChatViewItems();

            Assert.IsNotNull(items, "no items were added");

            
            Assert.AreEqual(3, chatScreen.GetTotalMessages(), "not all messages were added");

            string word = items[0].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length-1));
            word = items[1].ToString().Substring(15);
            Assert.AreEqual<string>("message 3", word.Substring(0, word.Length - 1));
            word = items[2].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[3].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length - 1));
            word = items[4].ToString().Substring(15);
            Assert.AreEqual<string>("message 1", word.Substring(0, word.Length - 1));
            word = items[5].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[6].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length - 1));
            word = items[7].ToString().Substring(15);
            Assert.AreEqual<string>("message 2", word.Substring(0, word.Length - 1));
            word = items[8].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            time = DateTime.Now.ToString();

            chatScreen.AddMessage("testChat", time, "long message 1, a loooooooooooooooooooooooooooooooooooooooooooooooooooooong word", true);
            chatScreen.AddMessage("testChat", time, "long message 2, a slight shorter word, but there are a lot more of the so it kinda balances out don't ya think?", false);
            chatScreen.AddMessage("testChat", time, "long message 3, this one will be a coooooooommmmmmmmmmbiiiiiiiiiiiiinnnnnnnnnnnnnnnation of the last 2 messages so that both will be tested at the same time", true);

            Assert.AreEqual(6, chatScreen.GetTotalMessages(), "not all messages were added");

            items = chatScreen.GetChatViewItems();
            word = items[0].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length - 1));
            word = items[1].ToString().Substring(15);
            Assert.AreEqual<string>("long message 3, this one will be a", word.Substring(0, word.Length - 1));
            word = items[2].ToString().Substring(15);
            Assert.AreEqual<string>("coooooooommmmmmmmmmbiiiiiiiiiiiiinnnnn-", word.Substring(0, word.Length - 1));
            word = items[3].ToString().Substring(15);
            Assert.AreEqual<string>("nnnnnnnnnnation", word.Substring(0, word.Length - 1));
            word = items[4].ToString().Substring(15);
            Assert.AreEqual<string>("of the last 2 messages so that both", word.Substring(0, word.Length - 1));
            word = items[5].ToString().Substring(15);
            Assert.AreEqual<string>("will be tested at the same time", word.Substring(0, word.Length - 1));
            word = items[6].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[7].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length - 1));
            word = items[8].ToString().Substring(15);
            Assert.AreEqual<string>("long message 1, a", word.Substring(0, word.Length - 1));
            word = items[9].ToString().Substring(15);
            Assert.AreEqual<string>("looooooooooooooooooooooooooooooooooooo-", word.Substring(0, word.Length - 1));
            word = items[10].ToString().Substring(15);
            Assert.AreEqual<string>("ooooooooooooooooong", word.Substring(0, word.Length - 1));
            word = items[11].ToString().Substring(15);
            Assert.AreEqual<string>("word", word.Substring(0, word.Length - 1));
            word = items[12].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[22].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - " + chatName, word.Substring(0, word.Length - 1));
            word = items[23].ToString().Substring(15);
            Assert.AreEqual<string>("long message 2, a slight shorter word,", word.Substring(0, word.Length - 1));
            word = items[24].ToString().Substring(15);
            Assert.AreEqual<string>("but there are a lot more of the so it", word.Substring(0, word.Length - 1));
            word = items[25].ToString().Substring(15);
            Assert.AreEqual<string>("kinda balances out don't ya think?", word.Substring(0, word.Length - 1));
            word = items[26].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

        }

        [TestMethod]
        public void UpdateChatHistoryTest()
        {
            ChatScreen chatScreen = new ChatScreen("testChat", true);
            chatScreen.Show();
            string time = DateTime.Now.ToString();
            List<string[]> chatHistory = new List<string[]>();
            chatHistory.Add(new string[] {"testChat", "", "" });
            chatHistory.Add(new string[] {"testChat", time, "message 1" });
            chatHistory.Add(new string[] {"notTestChat", time, "message 2" });
            chatHistory.Add(new string[] {"testChat", time, "message 3" });

            chatScreen.UpdateChatHistory(chatHistory.ToArray());

            Assert.AreEqual(3, chatScreen.GetTotalMessages(), "not all messages were added");

            var items = chatScreen.GetChatViewItems();

            string word = items[0].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - testChat", word.Substring(0, word.Length - 1));
            word = items[1].ToString().Substring(15);
            Assert.AreEqual<string>("message 1", word.Substring(0, word.Length - 1));
            word = items[2].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[3].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - You", word.Substring(0, word.Length - 1));
            word = items[4].ToString().Substring(15);
            Assert.AreEqual<string>("message 2", word.Substring(0, word.Length - 1));
            word = items[5].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));

            word = items[6].ToString().Substring(15);
            Assert.AreEqual<string>(time + " - testChat", word.Substring(0, word.Length - 1));
            word = items[7].ToString().Substring(15);
            Assert.AreEqual<string>("message 3", word.Substring(0, word.Length - 1));
            word = items[8].ToString().Substring(15);
            Assert.AreEqual<string>("", word.Substring(0, word.Length - 1));
        }
    }
}