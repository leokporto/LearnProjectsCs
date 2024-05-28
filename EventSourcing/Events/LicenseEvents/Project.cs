namespace EventSourcing.Events.LicenseEvents
{
	public class Project
	{
		public Project(int id, string name, int year, int projectNumber)
		{
			Id = id;
			Name = name;
			Year = year;
			ProjectNumber = projectNumber;
		}

		public int Id { get; }

		public string Name { get; set; }

		public int Year
		{
			get;
		}

		public int ProjectNumber { get; }

		public string ProjectKey => $"{Year}.{ProjectNumber}";
	}
}