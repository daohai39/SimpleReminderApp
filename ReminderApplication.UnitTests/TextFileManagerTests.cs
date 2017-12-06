using System.IO;
using System.Text;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
    [TestFixture]
    public class TextFileManagerTests
    {
        [Test]
        [Ignore("Not avaiable at the moment")]
        public void WriteFile_ByDefault_CallsToTextWriter()
        {
           
        }

        [Test]
        public void WriteFile_WithTextWriterByDefault_CallsWriteLine()
        {
            var textFileManager = new TextFileManager();
            var expected = "Fake Message";
            var result = "";

            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                    textFileManager.WriteFile("Fake Message", streamWriter);
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
            var textFileManager = new TextFileManager();
            var expected = "Fake Message";
            var result = "";

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("Fake Message")))
            {
                using (var streamReader = new StreamReader(stream))
                    result += textFileManager.ReadFile(streamReader);
            }

            Assert.That(result, Does.Contain(expected).IgnoreCase);
        }

        [Test]
        [Ignore("Not avaiable")]
        public void WriteAllText_ByDefault_CreateTheFileThenWriteTheFileWithTheMessageOverrideIfExist()
        {
        }

        [Test]
        [Ignore("Not avaiable")]
        public void Copy_ByDefault_CopyAnExistFileToANewFileOverrideIfAllowed()
        {
            
        }
    }
}
