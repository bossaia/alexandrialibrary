using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ILinkType : IResource
	{
		bool IsSequential { get; }
		//string SubjectMask { get; set; }
		//string ObjectMask { get; set; }
		//string SequenceMask { get; set; }
	}

	public interface ILinkType<X, Y> : ISubjectLinkType<X>, IObjectLinkType<Y>
		where X : IEntityType
		where Y : IEntityType
	{
		ILink<X, Y> CreateLink(IEntity<X> subject, IEntity<Y> obj);
		ILink<X, Y> CreateLink(IEntity<X> subject, IEntity<Y> obj, int sequence);
	}
}
