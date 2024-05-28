using EventSourcing.Events.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Events.LicenseEvents
{
	public class LicenseCreated : BaseEvent
	{
		public LicenseCreated() 
		{
			LicenseId = Guid.NewGuid();
		}
		public override Guid StreamId => LicenseId;

		public Guid LicenseId { get; private set; }

		public string LicenseKey { get; set; }

		public Project AssociatedProject { get; set; }
	}
}
