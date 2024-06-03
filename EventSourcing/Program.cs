using EventSourcing.Events.StudentEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcing
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var studentDb = new StudentDatabase();

			var studentId = new Guid("6beaeff7-c463-4180-8155-e555f884ef7e");

			var studentCreated = new StudentCreated()
			{
				StudentId = studentId,
				FullName = "Anakin Skywalker",
				Email = "anakin.skywalker@jediknight.com",
				CreatedAtUtc = DateTime.UtcNow,
				DateOfBirth = new DateTime(1977, 5, 25)
			};

			studentDb.Append(studentCreated);
			Thread.Sleep(1000);

			var studentUpdated = new StudentUpdated()
			{
				StudentId = studentId,
				CreatedAtUtc = DateTime.UtcNow,
				FullName = "Darth Vader",
				Email = "darth.vader@empire.com"
			};
			studentDb.Append(studentUpdated);
			Thread.Sleep(1000);

			var studentEnrolled = new StudentEnrolled()
			{
				StudentId = studentId,
				CourseName = "Choking 101",
				CreatedAtUtc = DateTime.UtcNow
			};

			studentDb.Append(studentEnrolled);
			Thread.Sleep(1000);

			var studentEnrolled2 = new StudentEnrolled()
			{
				StudentId = studentId,
				CourseName = "Mind control 301",
				CreatedAtUtc = DateTime.UtcNow
			};
			studentDb.Append(studentEnrolled2);
			Thread.Sleep(1000);

			var studentUnEnrolled = new StudentUnEnrolled()
			{
				StudentId = studentId,
				CourseName = "Choking 101",
				CreatedAtUtc = DateTime.UtcNow
			};
			studentDb.Append(studentUnEnrolled);

			Student student = studentDb.Find(studentId);

			var studentFromView = studentDb.GetStudentView(studentId);

			Console.WriteLine(studentFromView);
		}
	}
}
