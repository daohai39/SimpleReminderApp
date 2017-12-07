using System;
using NSubstitute;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
    [TestFixture]
    public class DataFileManagerTests
    {
        [Test]
        [Ignore("Not ready")]
        public void SaveDate_ByDefault_SaveAStringToFile()
        {
            var converter = Substitute.For <IStringConverter<Reminder>>();
            var dataFileManager = new DataFileManagerStub(converter);
            var sampleListReminder = new Reminder[] { MakeFakeReminder() };

            converter.ConvertToString(Arg.Any<Reminder>()).Returns("Content: fake content - Date: 24/11/1995");
            dataFileManager.SaveData(sampleListReminder);

            
        }

        [Test]
        public void LoadData_ByDefault_ReturnAListOfReminders()
        {
            var converter = Substitute.For<IStringConverter<Reminder>>();
            var dataFileManager = new DataFileManagerStub(converter);
            var expectedReminderLists = new Reminder[] { MakeFakeReminder() };

            converter.ConvertToObject(Arg.Is<string>(x => !string.IsNullOrWhiteSpace(x))).Returns(MakeFakeReminder());
            var result = dataFileManager.LoadData();

            Assert.That(result, Is.EquivalentTo(expectedReminderLists));
        }

        [Test]
        [Ignore("Not Ready")]
        public void WriteAllText()
        {
            
        }

        [Test]
        [Ignore("Not ready")]
        public void Copy()
        {
            
        }

        private Reminder MakeFakeReminder()
        {
            return new Reminder() {Content = "fake message", CreatedAt = DateTime.MinValue};
        }

        private class DataFileManagerStub : DataFileManager
        {
            public DataFileManagerStub(IStringConverter<Reminder> converter) : base(converter)
            {
            }

            public override string ReadFile(string path)
            {
                return "Content: fake content - Date: 24/11/1995";
            }

            public override void WriteFile(string path, string message)
            {   
            }
        }
    }
}
