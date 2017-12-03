using System;

namespace ReminderApplication
{
	public class Reminder
	{
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }

		public virtual bool IsValid()
		{
			return ValidContent(Content) && ValidCreatedAt(CreatedAt);
		}

		public override string ToString()
		{
			return $"Reminder - Content: {Content} - Date: {CreatedAt.ToString()}";
		}

		public override bool Equals(Object compareObject)
		{
			if (!(compareObject is Reminder))
				return false;
			var reminder = compareObject as Reminder;
			return reminder.Content.Equals(Content) && reminder.CreatedAt.Equals(CreatedAt);
		}

		public override int GetHashCode()
		{
			var hash = 23;
			hash += Content.GetHashCode() * 17;
			hash += CreatedAt.GetHashCode() * 17;
			return hash;
		}

		private bool ValidContent(string content) => !String.IsNullOrWhiteSpace(content) && content.Length < 100;

		private bool ValidCreatedAt(DateTime createdAt) => true;
	}
}