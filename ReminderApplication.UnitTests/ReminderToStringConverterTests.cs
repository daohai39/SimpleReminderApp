using System;
using NUnit.Framework;

namespace ReminderApplication.UnitTests
{
	[TestFixture]
	public class ReminderToStringConverterTests
	{
		[Test]
		public void ConvertToString_ByDefault_ReturnsAString()
		{
			var converter = MakeConverter();
			var expectedContent = $"Test Reminder - {DateTime.MinValue.ToString()}";
			var reminder = new Reminder { Content = "Test Reminder", CreatedAt = DateTime.MinValue };

			var result = converter.ConvertToString(reminder);

			Assert.That(result, Is.EqualTo(expectedContent));
		}

		[Test]
		public void ConvertToString_NullObject_ThrowsException()
		{
			var converter = MakeConverter();
																										 
			var result = Assert.Throws<ArgumentException>(() => converter.ConvertToString(null));

			Assert.That(result.Message, Does.Contain("Reminder can not be null").IgnoreCase);
		}

		[Test]
		public void ConvertToObject_ByDefault_ReturnsAReminder()
		{
			var converter = MakeConverter();
			var content = "Test Reminder";
			var date = DateTime.MinValue; 

			var result = converter.ConvertToObject($"{content} - {date.ToString()}");
			Assert.That(result.Content, Does.Contain(content));
			Assert.That(result.CreatedAt, Is.EqualTo(date).Within(TimeSpan.FromMilliseconds(0.0)));
		}
									 
		[Test]
		public void ConvertToObject_EmptyString_ThrowsException()
		{
			var converter = MakeConverter();

			var result = Assert.Throws<ArgumentException>(() => converter.ConvertToObject(string.Empty));

			Assert.That(result.Message, Does.Contain("Content can not be empty").IgnoreCase);
		}

		[TestCase("Test Reminder - 11/28/2017 9:23:50 AM - 11/28/2017 9:23:50 AM ")]
		[TestCase("Test Reminder")] 
		public void ConvertToObject_InvalidString_ThrowsException(string invalidString)
		{
			var converter = MakeConverter();

			var result = Assert.Throws<ArgumentException>(() => converter.ConvertToObject(invalidString));

			Assert.That(result.Message, Does.Contain("Invalid string").IgnoreCase);
		}

		[TestCase("Test Reminder - Invalid Format")]
		[TestCase("Test Reminder - 11/28/2017 format")]
		[TestCase("Test Reminder - format 9:23:50 AM")]
		[TestCase("Test Reminder - 22/13/1999:23:50 AM")]
		public void ConvertToObject_InvalidDateTimeFormat_ThrowsException(string stringWithInvalidDateTimeFormat)
		{
			var converter = MakeConverter();

			var result = Assert.Throws<ArgumentException>(
					() => converter.ConvertToObject(stringWithInvalidDateTimeFormat));

			Assert.That(result.Message, Does.Contain("Invalid date format").IgnoreCase);
		}

		private ReminderToStringConverter MakeConverter()
		{
			return new ReminderToStringConverter();
		}
	}
}
