using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public interface ILinkType : IResource
	{
		string SubjectMask { get; set; }
		string ValueMask { get; set; }
		string SequenceMask { get; set; }
	}
}
