﻿using NUnit.Framework;
using ReminderApplication;

namespace ReminderApplication.UnitTests
{
    [TestFixture]
    public class TextLoggerTests
    {
        [Test]
        public void LogError_ByDefault_CallsFileManager()
        {
            var mockFileManager = new FakeFileManager();
            var logger = new TextLogger(mockFileManager);
            var message = "Fake message";

            logger.LogError(message);

            Assert.That(mockFileManager.LastMessage, Does.Contain(message));
        }

        [Test]
        public void LogInfo_ByDefault_CallsFileManager()
        {
            var mockFileManager = new FakeFileManager();
            var logger = new TextLogger(mockFileManager);
            var message = "Fake info";

            logger.LogInfo(message);

            Assert.That(mockFileManager.LastMessage, Does.Contain(message));
        }

        [TestCase("Error","Fake Error Message", "Error : Fake Error Message")]
        [TestCase("Info","Fake Info Message", "Info : Fake Info Message")]
        public void Format_ByDefault_ReturnsAProbablyFormattedString(string messageType, string message, string expected)
        {
            var logger = new TextLogger();

            var result = logger.Format(messageType, message);

            Assert.That(result, Does.Contain(expected));
        }

    }

    public class FakeFileManager : IFileManager
    {
        public string LastMessage { get; set; }

        public void WriteFile(string message)
        {
            LastMessage = message;
        }

        public string ReadFile()
        {
            return LastMessage;
        }
    }
}