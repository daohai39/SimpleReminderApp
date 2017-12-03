using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
    [TestFixture]
    public class ReminderRepositoryTests
    {
        [Test]
        [Ignore("Better test with stub")]
        public void Insert_ByDefault_AddAReminderToList()
        {
            var repository = new ReminderRepository();
            var expectedReminder = new Reminder() { Content = "Fake content", CreatedAt = new DateTime(2017, 2, 12) };
            var expectedList = new List<Reminder> { expectedReminder};

            repository.Insert(new Reminder() { Content = "Fake content", CreatedAt = new DateTime(2017, 2, 12) });
                   
            Assert.That(repository.ReminderList, Is.EquivalentTo(expectedList));
            Assert.That(repository.ReminderList[repository.ReminderList.Count - 1], Is.EqualTo(expectedReminder));
        }

        [Test]
        public void Insert_ByDefault_AddAReminderToListUsingStubReminder()
        {
            var repository = MakeRepository();
            var expectedReminder = MakeReminder();
            var expectedList = new List<Reminder> {expectedReminder};

            repository.Insert(new FakeReminder() { ReturnValid = true});

            Assert.That(repository.ReminderList, Is.EquivalentTo(expectedList));
            Assert.That(repository.ReminderList[repository.ReminderList.Count - 1], Is.EqualTo(expectedReminder));
        }
        
        [Test]
        [Ignore("Better test with stub reminder")]
        public void Insert_InvalidReminder_ThrowsException()
        {
            var repository = new ReminderRepository();
            var invalidReminder = new Reminder() {Content = new string('a', 101), CreatedAt = DateTime.MinValue};

            var result = Assert.Throws<ArgumentException>(() => repository.Insert(invalidReminder));

            Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
        }

        [Test]
        public void Insert_InvalidStubReminder_ThrowsException()
        {
            var repository = MakeRepository();
            var invalidStubReminder = MakeReminder();
            invalidStubReminder.ReturnValid = false;

            var result = Assert.Throws<ArgumentException>(() => repository.Insert(invalidStubReminder));

            Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
        }
        

        [Test]
        public void Delete_ByDefault_RemoveAReminderFromList()
        {
            var repository = MakeRepository();
            var stubReminder = MakeReminder();
            var expectedList = new List<Reminder>() {stubReminder};
            var deletedReminder = new FakeReminder();
                                              
            repository.Insert(new FakeReminder());
            repository.Insert(deletedReminder);
            repository.Delete(deletedReminder);              
            var result = repository.ReminderList;

            Assert.That(result, Is.EquivalentTo(expectedList));
            Assert.That(result[result.Count-1], Is.EqualTo(stubReminder));
        }

        [Test]
        public void Delete_InvalidReminder_ThrowsException()
        {
            var repository = MakeRepository();
            var invalidStubReminder = MakeReminder();
            invalidStubReminder.ReturnValid = false;

            var result = Assert.Throws<ArgumentException>(() => repository.Delete(invalidStubReminder));

            Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
        }

        [Test]
        public void Delete_UnknowReminder_ThrowsException()
        {
            var repository = MakeRepository();
            var unkownStubReminder = MakeReminder();
            unkownStubReminder.IsEqual = false;

            repository.ReminderList.Add(new FakeReminder());
            var result = Assert.Throws<ArgumentException>(() => repository.Delete(unkownStubReminder));

            Assert.That(result.Message, Does.Contain("List does not have this reminder").IgnoreCase);
        }

        private ReminderRepository MakeRepository()
        {
            return new ReminderRepository();
        }

        private FakeReminder MakeReminder()
        {
            return new FakeReminder();
        }

        [Test]
        public void GetAll()
        {
            
        }
    }

    public class FakeReminder : Reminder
    {
        public bool ReturnValid { get; set; }
        public bool IsEqual { get; set; }

        public FakeReminder()
        {
            IsEqual = true;
            ReturnValid = true;
        }

        public override bool IsValid()
        {
            return ReturnValid;  
        }

        public override bool Equals(Object compareObject)
        {
            return compareObject != null && ((FakeReminder) compareObject).IsEqual;
        }

        public override int GetHashCode() => ReturnValid.GetHashCode();
    }

    public class FakeStringConverter : IStringConverter<Reminder>
    {
        public string ConvertToString(Reminder entity)
        {
            throw new System.NotImplementedException();
        }

        public Reminder ConvertToObject(string entityString)
        {
            throw new System.NotImplementedException();
        }
    }

    public class FakeConnection : IConnection<FileInfo>
    {
        public FileInfo Connection { get; }
        public bool IsConnect { get; }
        public bool Connect()
        {
            throw new System.NotImplementedException();
        }

        public bool Disconnect()
        {
            throw new System.NotImplementedException();
        }
    }
}

