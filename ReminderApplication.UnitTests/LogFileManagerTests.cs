using System.IO;
using System.Text;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
    [TestFixture]
    public class LogFileManagerTests
    {
        [Test]
        [Ignore("Not avaiable at the moment")]
        public void WriteFile_ByDefault_CallsToTextWriter()
        {
           
        }

        [Test]
        public void WriteFile_WithTextWriterByDefault_CallsWriteLine()
        {
            var logFileManager = new LogFileManager();
            var expected = "Fake Message";
            var result = "";

            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                    logFileManager.WriteFile("Fake Message", streamWriter);
                result += Encoding.UTF8.GetString(stream.ToArray());
            }

            Assert.That(result, Does.Contain(expected).IgnoreCase);
        }

        [Test]
        [Ignore("Not avaiable at the moment")]
        public void ReadFile_ByDefault_CallsToTextReader()
        {
            
        }

        [Test]
        public void ReadFile_WithTextReaderByDefault_CallsReadLine()
        {
            var logFileManager = new LogFileManager();
            var expected = "Fake Message";
            var result = "";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("Fake Message")))
            {
                using (var streamReader = new StreamReader(stream))
                    result += logFileManager.ReadFile(streamReader);
            }

            Assert.That(result, Does.Contain(expected).IgnoreCase);
        }
    }
}
