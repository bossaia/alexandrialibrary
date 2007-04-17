using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class MetadataTag
	{
		#region Constructors
		public MetadataTag(string textValue)
		{
			type = MetadataTagType.Text;
			this.textValue = textValue;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}
				
		public MetadataTag(decimal numericValue)
		{
			type = MetadataTagType.Numeric;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = numericValue;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}
		
		public MetadataTag(DateTime dateValue)
		{
			type = MetadataTagType.Date;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = dateValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}
		
		public MetadataTag(TimeSpan intervalValue)
		{
			type = MetadataTagType.Interval;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = intervalValue;
			this.intervalValues = new List<TimeSpan>();
		}
		
		public MetadataTag(List<string> textValues)
		{
			type = MetadataTagType.TextList;
			this.textValue = string.Empty;
			this.textValues = textValues;
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}

		public MetadataTag(List<decimal> numericValues)
		{
			type = MetadataTagType.NumericList;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = numericValues;
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}

		public MetadataTag(List<DateTime> dateValues)
		{
			type = MetadataTagType.DateList;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = dateValues;
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = new List<TimeSpan>();
		}

		public MetadataTag(List<TimeSpan> intervalValues)
		{
			type = MetadataTagType.IntervalList;
			this.textValue = string.Empty;
			this.textValues = new List<string>();
			this.numericValue = 0M;
			this.numericValues = new List<decimal>();
			this.dateValue = DateTime.MinValue;
			this.dateValues = new List<DateTime>();
			this.intervalValue = TimeSpan.MinValue;
			this.intervalValues = intervalValues;
		}
		#endregion
		
		#region Private Fields
		private MetadataTagType type;
		private string textValue;
		private List<string> textValues;
		private decimal numericValue;
		private List<decimal> numericValues;
		private DateTime dateValue;
		private List<DateTime> dateValues;
		private TimeSpan intervalValue;
		private List<TimeSpan> intervalValues;
		#endregion
		
		#region Public Properties
		public MetadataTagType Type
		{
			get { return type; }
		}
		
		public string TextValue
		{
			get { return textValue; }
		}
		
		public IList<string> TextValues
		{
			get { return textValues; }
		}
		
		public decimal NumericValue
		{
			get { return numericValue; }
		}
		
		public IList<decimal> NumericValues
		{
			get { return numericValues; }
		}
		
		public DateTime DateValue
		{
			get { return dateValue; }
		}
		
		public IList<DateTime> DateValues
		{
			get { return dateValues; }
		}
		
		public TimeSpan IntervalValue
		{
			get { return intervalValue; }
		}
		
		public IList<TimeSpan> IntervalValues
		{
			get { return intervalValues; }
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			switch (type)
			{
				case MetadataTagType.Text:
					return textValue;
				case MetadataTagType.Numeric:
					return numericValue.ToString();
				case MetadataTagType.Date:
					return dateValue.ToShortDateString();
				case MetadataTagType.Interval:
					return intervalValue.ToString();
				default:
					return null;
			}
		}
		#endregion
	}	
}
