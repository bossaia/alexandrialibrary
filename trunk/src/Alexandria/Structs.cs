using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria
{
	#region Version
	public struct Version : IComparable<Version>
	{
		#region Constructors
		public Version(int majorNumber, int minorNumber, int buildNumber, int revisionNumber)
		{
			numbers = new int[4];
			numbers[0] = majorNumber;
			numbers[1] = minorNumber;
			numbers[2] = buildNumber;
			numbers[3] = revisionNumber;	
		}
		#endregion
		
		#region Private Fields
		private int[] numbers;// = new int[4];
		#endregion
				
		#region Public Properties
		public int MajorNumber
		{
			get { return numbers[0]; }
		}
		
		public int MinorNumber
		{
			get { return numbers[1]; }
		}
		
		public int BuildNumber
		{
			get { return numbers[2]; }
		}
		
		public int RevisionNumber
		{
			get { return numbers[3]; }
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj != null && obj.GetType() == typeof(Version))
			{
				return (this.CompareTo((Version)obj) == 0);
			}
			else return false;
		}
		#endregion

		#region IComparable<Version> Members
		public int CompareTo(Version other)
		{
			for(int i = 0; i < numbers.Length; i++)
				if (this.numbers[i] > other.numbers[i])
					return 1;
				else if (this.numbers[i] < other.numbers[i]) 
					return -1;
			return 0;
		}
		#endregion
	}
	#endregion
	
	#region MetadataItem
	public struct MetadataItem
	{
		#region Constructors
		public MetadataItem(IDictionary<string, MetadataItem> subItems, IDictionary<string, object> tags)
		{
			this.subItems = subItems;
			this.tags = tags;
		}
		#endregion
		
		#region Private Fields
		private IDictionary<string, MetadataItem> subItems;
		private IDictionary<string, object> tags;
		#endregion
	
		#region Public Properties
		public IDictionary<string, MetadataItem> SubItems
		{
			get { return subItems; }
		}
		
		public IDictionary<string, object> Tags
		{
			get { return tags; }
		}
		#endregion
	
		#region Public Methods
		public void InsertTag<T>(string name, T value)
		{
			if (!this.tags.ContainsKey(name))
			{
				this.tags.Add(name, value);
			}			
		}
		
		public T ReadTag<T>(string name)
		{
			if (this.tags.ContainsKey(name))
			{
				object obj = this.tags[name];
				if (obj is T)
					return (T)this.tags[name];
				else throw new KeyNotFoundException();
			}
			else throw new KeyNotFoundException();
		}
		
		public void UpdateTag<T>(string name, T value)
		{
			if (this.tags.ContainsKey(name))
			{
				object obj = this.tags[name];
				if (obj is T)
					this.tags[name] = value;
				else throw new KeyNotFoundException();
			}
			else throw new KeyNotFoundException();
		}
		#endregion
	}
	#endregion
}
