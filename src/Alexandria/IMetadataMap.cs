using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataMap
	{
		IDictionary<string, string> AllItems { get; }
		IDictionary<string, MetadataItem<string>> StringItems { get; }
		IDictionary<string, MetadataItem<int>> IntegerItems { get; }
		IDictionary<string, MetadataItem<double>> DoubleItems { get; }
		IDictionary<string, MetadataItem<decimal>> DecimalItems { get; }
		IDictionary<string, MetadataItem<DateTime>> DateItems { get; }
		IDictionary<string, MetadataItem<TimeSpan>> IntervalItems { get; }
		IDictionary<string, MetadataItem<bool>> BooleanItems { get; }
		IDictionary<string, string> GetAllSubItems(string root);
		IDictionary<string, MetadataItem<string>> GetStringSubItems(string root);
		IDictionary<string, MetadataItem<int>> GetIntegerSubItems(string root);
		IDictionary<string, MetadataItem<double>> GetDoubleSubItems(string root);
		IDictionary<string, MetadataItem<decimal>> GetDecimalSubItems(string root);
		IDictionary<string, MetadataItem<DateTime>> GetDateSubItems(string root);
		IDictionary<string, MetadataItem<TimeSpan>> GetIntervalSubItems(string root);
		IDictionary<string, MetadataItem<bool>> GetBooleanSubItems(string root);
	}
}
