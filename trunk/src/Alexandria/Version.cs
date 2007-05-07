using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria
{
	public struct Version : IVersion
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
		public string Name
		{
			get { return string.Format("{0}.{1}.{2}.{3}", MajorNumber, MinorNumber, BuildNumber, RevisionNumber); }
		}
		
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

		#region IComparable<IVersion> Members
		public int CompareTo(IVersion other)
		{
			if (this.MajorNumber > other.MajorNumber)
				return 1;
			else if (this.MajorNumber < other.MajorNumber)
				return -1;

			if (this.MinorNumber > other.MinorNumber)
				return 1;
			else if (this.MinorNumber < other.MinorNumber)
				return -1;

			if (this.BuildNumber > other.BuildNumber)
				return 1;
			else if (this.BuildNumber < other.BuildNumber)
				return -1;

			if (this.RevisionNumber > other.RevisionNumber)
				return 1;
			else if (this.RevisionNumber < other.RevisionNumber)
				return -1;
								
			return 0;
		}
		#endregion
	}
}
