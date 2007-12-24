using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	public enum TaskStatus
	{
		Unknown = 0,
		Pending,
		Running,
		Paused,
		Cancelled,
		Completed,
	}
}
