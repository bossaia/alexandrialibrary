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
	
	#region DataNode
	public struct DataNode<T>
	{
		#region Constructors
		public DataNode(string name, T value)
		{			
			this.number = -1;
			this.name = name;
			this.value = value;
			this.childNodes = new List<DataNode<T>>();
		}
		
		public DataNode(string name, IList<T> childValues)
		{	
			this.name = name;		
			this.number = -1;
			this.value = default(T);
			this.childNodes = new List<DataNode<T>>();
			
			if (childValues != null && childValues.Count > 0)
			{
				int i = 0;
				foreach(T value in childValues)
				{
					childNodes.Add(new DataNode<T>(name, i, value));
					i++;
				}
			}
		}
		
		private DataNode(string name, int number, T value)
		{
			this.name = name;
			this.number = number;
			this.value = value;
			this.childNodes = new List<DataNode<T>>();
		}
		#endregion
		
		#region Private Fields
		private string name;
		private int number;
		private T value;
		private List<DataNode<T>> childNodes;
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
		
		public bool HasChildren
		{
			get { return (this.childNodes != null && this.childNodes.Count > 0); }
		}
		
		public bool HasSiblings
		{
			get { return (number >= 0); }
		}
		
		public T Value
		{
			get { return this.value; }
			set { this.value = value; }
		}
		
		public IList<DataNode<T>> ChildNodes
		{
			get { return this.childNodes; }
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			if (default(T) == null && (object)value == null)
				return null;
			else return this.value.ToString();
		}
		#endregion
	}
	#endregion
}
