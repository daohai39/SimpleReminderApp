using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
	[TestFixture]
	public class ReminderAppTests
	{
		[Test]
		public void Run_ByDefault_UpdateIsRunningState()
		{
		    var repository = Substitute.For<IRepository<Reminder>>();
		    var logger = Substitute.For<ILogger>();
		    var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
            var app = new ReminderApp(repository, logger, dataAccessor);

		    repository.Load(Arg.Is<IList<Reminder>>(x => x != null)).Returns(true);   
            app.Run();

		    Assert.That(app.IsRunning, Is.True);
		}

	    [Test]
	    public void Run_DataAccessorThrowsException_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor);

	        dataAccessor.LoadData().Returns(x => throw new Exception());
	        app.Run();

            logger.Received().LogError(Arg.Any<string>());
	    }

	    [Test]
	    public void Run_RepositoryThrowsException_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor);

	        repository.Load(Arg.Any<IList<Reminder>>()).Returns(x => throw new Exception());
            app.Run();

            logger.Received().LogError(Arg.Any<string>());
	    }

		[Test]
		public void Close_ByDefault_UpdateIsRunState()
		{
		    var repository = Substitute.For<IRepository<Reminder>>();
		    var logger = Substitute.For<ILogger>();
		    var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
		    var app = new ReminderApp(repository, logger, dataAccessor);

		    app.IsRunning = true;
		    app.Close();
                                                                       
            Assert.That(app.IsRunning, Is.False);
		}

	    [Test]
	    public void Close_RepositoryThrowsException_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor);

	        app.IsRunning = true;
            repository.GetAll().Returns(x => throw new Exception());
	        app.Close();

	        logger.Received().LogError(Arg.Any<string>());
	        Assert.That(app.IsRunning, Is.True);
        }

	    [Test]
	    public void Close_DataAccessorThrowsException_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor) {IsRunning = true};

	        dataAccessor.When( x => x.SaveData(Arg.Any<IList<Reminder>>()))
                        .Do(x => throw new Exception());
	        app.Close();

	        logger.Received().LogError(Arg.Any<string>());
	        Assert.That(app.IsRunning, Is.True);
        }

		[Test]
		public void AddReminder_ByDefault_CallLoggerAfterAddReminder()
		{
		    var repository = Substitute.For<IRepository<Reminder>>();
		    var logger = Substitute.For<ILogger>();
		    var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
		    var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

            app.AddReminder(new Reminder(){Content = "fake content", CreatedAt = DateTime.MinValue});

            logger.Received().LogInfo(Arg.Is<string>(x => x.Contains("Added new reminder:")));
        }

	    [Test]
	    public void AddReminder_ExceptionWasThrown_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

            repository.When(x => x.Insert(Arg.Any<Reminder>()))
               .Do(x => throw new Exception());
	        app.AddReminder(new Reminder() {Content = "fake content", CreatedAt = DateTime.MinValue});

            logger.Received().LogError(Arg.Any<string>());
        }

		[Test]
		public void DeleteReminder_ByDefault_CallLoggerAfterDeleteReminder()
		{
		    var repository = Substitute.For<IRepository<Reminder>>();
		    var logger = Substitute.For<ILogger>();
		    var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
		    var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

		    app.DeleteReminder(new Reminder() { Content = "fake content", CreatedAt = DateTime.MinValue });

		    logger.Received().LogInfo(Arg.Is<string>(x => x.Contains("Deleted reminder:")));

        }

	    [Test]
	    public void DeleteReminder_ExceptionWasThrown_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

	        repository.When(x => x.Delete(Arg.Any<Reminder>()))
	            .Do(x => throw new Exception());
            app.DeleteReminder(new Reminder() { Content = "fake content", CreatedAt = DateTime.MinValue });

	        logger.Received().LogError(Arg.Any<string>());
	    }

        [Test]
	    public void UpdateReminder_ByDefault_CallLoggerAfterUpdateReminder()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

	        app.UpdateReminder(
                new Reminder() { Content = "fake content 1", CreatedAt = DateTime.MinValue },
                new Reminder() { Content = "fake content 2", CreatedAt = DateTime.MinValue }
            );

	        logger.Received().LogInfo(Arg.Is<string>(x => x.Contains("Updated reminder:")));

        }

	    [Test]
	    public void UpdateReminder_ExceptionWasThrown_CallLogger()
	    {
	        var repository = Substitute.For<IRepository<Reminder>>();
	        var logger = Substitute.For<ILogger>();
	        var dataAccessor = Substitute.For<IDataAccess<Reminder>>();
	        var app = new ReminderApp(repository, logger, dataAccessor) { IsRunning = true };

	        repository.When(x => x.Update(Arg.Any<Reminder>(), Arg.Any<Reminder>()))
	            .Do(x => throw new Exception());
            app.UpdateReminder(
                new Reminder() { Content = "fake content", CreatedAt = DateTime.MinValue }, 
                new Reminder() { Content = "fake content", CreatedAt = DateTime.MinValue }
            );

	        logger.Received().LogError(Arg.Any<string>());
	    }
    }
}
