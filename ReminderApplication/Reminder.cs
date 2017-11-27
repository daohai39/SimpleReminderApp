using System;

namespace ReminderApplication
{
	public class Reminder
	{
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }

		public override string ToString()
		{
		    return $"Reminder - Content: {Content} - Date: {CreatedAt.ToString()}";
		}
	}
}