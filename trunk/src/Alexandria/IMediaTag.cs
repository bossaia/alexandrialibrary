using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaTag
	{
		IMediaTagFormat Format { get; }
		IIdentifier Id { get; }
		MetadataMap<string, string> StringItems { get; }
		MetadataMap<string, int> IntegerItems { get; }
		MetadataMap<string, double> DoubleItems { get; }
		MetadataMap<string, decimal> DecimalItems { get; }
		MetadataMap<string, DateTime> DateItems { get; }
	}
}
