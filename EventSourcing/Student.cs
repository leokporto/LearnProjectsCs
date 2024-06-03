using EventSourcing.Events.Base;
using EventSourcing.Events.StudentEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
	public class Student
	{
		public Guid Id { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public List<string> EnrolledCourses { get; set; } = new List<string>();

		public DateTime DateOfBirth { get; set; }

		private void Apply(StudentCreated studentCreated)
		{
			Id = studentCreated.StudentId;
			FullName = studentCreated.FullName;
			Email = studentCreated.Email;
			DateOfBirth = studentCreated.DateOfBirth;
		}

		private void Apply(StudentUpdated updated)
		{
			FullName = updated.FullName;
			Email = updated.Email;
		}

		private void Apply(StudentEnrolled enrolled)
		{
			if (!EnrolledCourses.Contains(enrolled.CourseName))
			{
				EnrolledCourses.Add(enrolled.CourseName);
			}
		}

		private void Apply(StudentUnEnrolled unEnrolled)
		{
			if (EnrolledCourses.Contains(unEnrolled.CourseName))
			{
				EnrolledCourses.Remove(unEnrolled.CourseName);
			}
		}

		public void Apply(BaseEvent evt)
		{
			switch (evt)
			{
				case StudentCreated studentCreated:
					Apply(studentCreated);
					break;
				case StudentUpdated studentUpdated:
					Apply(studentUpdated);
					break;
				case StudentEnrolled studentEnrolled:
					Apply(studentEnrolled);
					break;
				case StudentUnEnrolled studentUnEnrolled:
					Apply(studentUnEnrolled);
					break;
			}
		}

		public override string ToString()
		{
			var enrolledCourses = string.Join(", ", EnrolledCourses);
			string result = $"Student: {FullName} ({Email})\nBirth: {DateOfBirth}\nCourses enrolled: {enrolledCourses}";
			return result;
		}
	}
}
