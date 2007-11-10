using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IRecording
	{
		DateTime DateRecorded { get; set; }
		IList<IPerformance> Performances { get; }
		IList<IWork> Works { get; }
		IList<IRecording> Remixes { get; }
	}
}
