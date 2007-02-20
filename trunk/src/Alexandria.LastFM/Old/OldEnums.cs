using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	#region StationModes
	public enum StationModes
	{
		Personal,
		Loved,
		Neighbor
	}
	#endregion

	#region CommandKeywords
	public enum CommandKeywords
	{
		Love,
		Skip,
		Ban,
		RecordToProfile,
		NoRecordToProfile
	}
	#endregion
}
