using Server;
using Server.Commands;
using Server.DataSaving;

namespace Eindopdracht_unit_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSavingNewAccount()
        {
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2"));
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient"));

            DataSaver.AddNewClient("TestClient", "1234");
            DataSaver.AddNewClient("TestClient2", "1234");
            Assert.IsTrue(Directory.Exists(Path.Combine(DataSaver.GetDirectory(), "TestClient")), "Client directory was not made");

            Assert.IsTrue(DataSaver.ClientExists("TestClient"), "Client folder was not found");
        }
        [TestMethod]
        public void TestSavingMessage()
        {
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2"));
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient"));

            DataSaver.AddNewClient("TestClient", "1234");
            DataSaver.AddNewClient("TestClient2", "1234");

            string time = DateTime.Now.ToString();

            DataSaver.WriteMessageFile("TestClient2", "TestClient", time, "Here is a message");

            Console.WriteLine(File.ReadAllText(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient")).ToString() + " - " + "TestClient2‎" + time + "‎Here is a message");
            Console.WriteLine(File.ReadAllText(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2")).ToString() + " - " + "TestClient2‎" + time + "‎Here is a message");

            Assert.IsTrue(File.Exists(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2")), "Chat file not made");
            Assert.IsTrue(File.Exists(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient")), "Chat file not made");
            Assert.IsTrue(String.Equals("TestClient2‎" + time + "‎Here is a message", File.ReadAllText(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient")).ToString()), "message saved incorrectly");
            Assert.IsTrue(String.Equals("TestClient2‎" + time + "‎Here is a message", File.ReadAllText(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2")).ToString()), "message saved incorrectly");

            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2"));
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient"));
        }
        [TestMethod]
        public void TestReadingMessage()
        {
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2"));
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient"));

            DataSaver.AddNewClient("TestClient", "1234");
            DataSaver.AddNewClient("TestClient2", "1234");

            string time = DateTime.Now.ToString();

            DataSaver.WriteMessageFile("TestClient2", "TestClient", time, "Here is a message");

            List<string[]> expectedAnswerList = new List<string[]>();
            expectedAnswerList.Add(new string[] {"TestClient2","","" });
            expectedAnswerList.Add(new string[] {"TestClient2",time,"Here is a message" });

            string[][] expectedAnswer = expectedAnswerList.ToArray();

            Console.WriteLine(expectedAnswer[0][0] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[0][0]);
            Console.WriteLine(expectedAnswer[0][1] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[0][1]);
            Console.WriteLine(expectedAnswer[0][2] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[0][2]);
            Console.WriteLine(expectedAnswer[1][0] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[1][0]);
            Console.WriteLine(expectedAnswer[1][1] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[1][1]);
            Console.WriteLine(expectedAnswer[1][2] + " - " + DataSaver.GetMessageFile("TestClient", "TestClient2")[1][2]);

            Assert.IsTrue(expectedAnswer[0][0] == DataSaver.GetMessageFile("TestClient", "TestClient2")[0][0], "wrong chat");
            Assert.IsTrue(expectedAnswer[0][1] == DataSaver.GetMessageFile("TestClient", "TestClient2")[0][1],"should be empty");
            Assert.IsTrue(expectedAnswer[0][2] == DataSaver.GetMessageFile("TestClient", "TestClient2")[0][2],"should be empty");
            Assert.IsTrue(expectedAnswer[1][0] == DataSaver.GetMessageFile("TestClient", "TestClient2")[1][0],"wrong sender");
            Assert.IsTrue(expectedAnswer[1][1] == DataSaver.GetMessageFile("TestClient", "TestClient2")[1][1],"wrong time");
            Assert.IsTrue(expectedAnswer[1][2] == DataSaver.GetMessageFile("TestClient", "TestClient2")[1][2],"wrong message");

            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient", "TestClient2"));
            File.Delete(Path.Combine(DataSaver.GetDirectory(), "TestClient2", "TestClient"));
        }
    }
}