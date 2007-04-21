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

		public override int GetHashCode()
		{
			return numbers[0].GetHashCode() + numbers[1].GetHashCode() + numbers[2].GetHashCode() + numbers[3].GetHashCode();
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
	public struct MetadataItem<T>
	{
		#region Constructors
		public MetadataItem(string name, T data)
		{
			this.root = name;
			this.number = 0;
			this.name = name;			
			this.data = data;
		}
		
		public MetadataItem(string root, int number, T data)
		{
			this.root = root;
			this.number = number;
			this.name = number > 0 ? string.Format("{0}{1:n}", root, number) : root;
			this.data = data;
		}
		#endregion
		
		#region Private Fields
		private string root;
		private int number;
		private string name;
		private T data;
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		
		public int Number
		{
			get { return number; }
		}
		
		public bool IsPartOfList
		{
			get { return (number > 0); }
		}
		
		public T Data
		{
			get { return data; }
			set { data = value; }
		}
		
		public string DataName
		{
			get { return data.ToString(); }
		}
		#endregion
	}
	#endregion
}
