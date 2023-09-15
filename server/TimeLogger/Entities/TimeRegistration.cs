using System;

namespace TimeLogger.Entities
{
	public class TimeRegistration
	{
		public uint Id { get; set; }
		public uint ProjectId { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
	}
}
