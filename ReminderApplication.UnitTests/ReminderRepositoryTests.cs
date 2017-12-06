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
		public void Insert_ByDefault_AddAReminderToList()
		{
			var repository = MakeRepository();
			var expectedNewReminder = MakeFakeReminder();

			repository.Insert(expectedNewReminder);

			Assert.That(repository.ReminderList, Does.Contain(expectedNewReminder));
			Assert.That(repository.ReminderList[repository.ReminderList.Count -1 ], Is.EqualTo(expectedNewReminder));
		}

		
		[Test]
		public void Insert_InvalidReminder_ThrowsException()
		{
			var repository = MakeRepository();
			var invalidReminder = MakeFakeReminder();
			invalidReminder.Content = String.Empty;

			var result = Assert.Throws<ArgumentException>(() => repository.Insert(invalidReminder));

			Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
		}

		[Test]
		public void InsertAll_ByDefault_AddRemindersToList()
		{
			var repository = MakeRepository();
			var expectedListReminders = new [] {MakeFakeReminder(), MakeFakeReminder(), MakeFakeReminder()};
		    var expectedList = new List<Reminder>(expectedListReminders);
			
            repository.InsertAll(expectedListReminders);

            Assert.That(repository.ReminderList, Is.EquivalentTo(expectedList)); 
		}

	    [Test]
	    public void InsertAll_HasInvalidReminder_ThrowsException()
	    {
	        var repository = MakeRepository();
	        var invalidReminder = MakeFakeReminder();
	        invalidReminder.Content = String.Empty;
	        var expectedListReminders = new[] { MakeFakeReminder(), MakeFakeReminder(), MakeFakeReminder(), invalidReminder };

            var result = Assert.Throws<ArgumentException>(() => repository.InsertAll(expectedListReminders));

	        Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
	    }


        [Test]
		public void Delete_ByDefault_RemoveAReminderFromList()
		{
			var repository = MakeRepository(); 
			var fakeReminder = MakeFakeReminder();
			repository.ReminderList.Add(fakeReminder);

			repository.Delete(fakeReminder);

			Assert.That(repository.ReminderList, Does.Not.Contain(fakeReminder));
		}

		[Test]
		public void Delete_InvalidReminder_ThrowsException()
		{
			var repository = MakeRepository();
			var invalidReminder = MakeFakeReminder();
			var fakeReminder = MakeFakeReminder();
			invalidReminder.Content = String.Empty;
			repository.ReminderList.Add(fakeReminder);

			var result = Assert.Throws<ArgumentException>(() => repository.Delete(invalidReminder));

			Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
		}

		[Test]
		public void Delete_UnknowReminder_ThrowsException()
		{
			var repository = MakeRepository();
			var unknownReminder = MakeFakeReminder();
			unknownReminder.Content = "Unknow content";

			repository.ReminderList.Add(MakeFakeReminder());
			var result = Assert.Throws<ArgumentException>(() => repository.Delete(unknownReminder));

			Assert.That(result.Message, Does.Contain("List does not have this reminder").IgnoreCase);
		}

		[Test]
		public void Delete_IsCalledWhenListIsEmpty_ThrowsException()
		{
			var repository = MakeRepository();
			var fakeReminder = MakeFakeReminder();

			var result = Assert.Throws<NullReferenceException>(() => repository.Delete(fakeReminder));

			Assert.That(result.Message, Does.Contain("List is empty").IgnoreCase);
		}

		[Test]
		public void Update_ByDefault_UpdateAReminderWithAnother()
		{
			var repository = MakeRepository();
			var fakeReminder = MakeFakeReminder();
			var updatedReminder = MakeFakeReminder();
			updatedReminder.Content = "Updated content";
			repository.ReminderList.Add(fakeReminder);

			repository.Update(fakeReminder, updatedReminder);

			Assert.That(repository.ReminderList, Does.Not.Contain(fakeReminder));
			Assert.That(repository.ReminderList, Does.Contain(updatedReminder));
		}

		[Test]
		public void Update_IsCalledWhenListIsEmpty_ThrowsException()
		{
			var repository = MakeRepository();
			var fakeReminder = MakeFakeReminder();
			var updatedReminder = MakeFakeReminder();

			var result = Assert.Throws<NullReferenceException>(() => repository.Update(fakeReminder, updatedReminder));

			Assert.That(result.Message, Does.Contain("List is empty").IgnoreCase);
		}

		[Test]
		public void Update_InvalidReminder_ThrowsException()
		{
			var repository = MakeRepository();
			var fakeReminder = MakeFakeReminder();
			var invalidUpdatedReminder = MakeFakeReminder();
			invalidUpdatedReminder.Content = String.Empty;
			repository.ReminderList.Add(fakeReminder);

			var result = Assert.Throws<ArgumentException>(() => repository.Update(fakeReminder, invalidUpdatedReminder));

			Assert.That(result.Message, Does.Contain("Invalid reminder").IgnoreCase);
		}

		[Test]
		public void Update_UnknowReminder_ThrowsException()
		{
			var repository = MakeRepository();
			var fakeReminder = MakeFakeReminder();
			var unknowReminder = MakeFakeReminder();
			unknowReminder.Content = "Unknow Content";
			var updatedReminder = MakeFakeReminder();
			updatedReminder.Content = "Update Content";
			repository.ReminderList.Add(fakeReminder);

			var result = Assert.Throws<ArgumentException>(() => repository.Update(unknowReminder, updatedReminder));

			Assert.That(result.Message, Does.Contain("List does not have this reminder").IgnoreCase);
		}

		[Test]
		public void GetAll_ByDefault_ReturnRepositoryReminderList()
		{
			var repository = MakeRepository();
			var expected = repository.ReminderList;

			var result = repository.GetAll();

			Assert.That(result, Is.EqualTo(expected));
		}

	    [Test]
	    public void Load_ByDefault_ReplaceListWithANewOneAndReturnTrue()
	    {
	        var repository = MakeRepository();
	        var reminderList = new List<Reminder> {MakeFakeReminder()};

	        var result = repository.Load(reminderList);

	        Assert.That(result, Is.True);
            Assert.That(repository.ReminderList, Is.EquivalentTo(reminderList));
	    }

	    [Test]
	    public void Load_EmptyList_ReturnFalse()
	    {
	        var repository = MakeRepository();
	        List<Reminder> reminderList = null;

	        var result = repository.Load(reminderList);

	        Assert.That(result, Is.False);
	    }

	    [Test]
	    public void Load_ListContainsInvalidReminder_ReturnFalse()
	    {
	        var repository = MakeRepository();
	        var invalidReminderList = new List<Reminder> {MakeFakeReminder(), MakeFakeReminder()};
	        var invalidReminder = MakeFakeReminder();
            invalidReminder.Content = String.Empty;
	        invalidReminderList.Add(invalidReminder);

	        var result = repository.Load(invalidReminderList);

	        Assert.That(result, Is.False);

	    }

		private ReminderRepository MakeRepository() => new ReminderRepository();

		private Reminder MakeFakeReminder() => new Reminder() {Content = "fake content", CreatedAt = DateTime.Now};


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

