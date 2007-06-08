using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria
{
	public struct Version : IVersion, IComparable<Version>
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
		
		public Version(string version)
		{			
			numbers = new int[4];
			if (!string.IsNullOrEmpty(version))
			{
				string[] versionSplit = version.Split(new char[]{'.'}, 4);
				if (versionSplit != null && versionSplit.Length > 0)
				{
					for(int i=0;i<versionSplit.Length;i++)
					{
						int number;
						int.TryParse(versionSplit[i], out number);
						numbers[i] = number;
					}
				}
			}
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
			if (other != null)
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
			else return -1;
		}
		#endregion

		#region IComparable<Version> Members
		public int CompareTo(Version other)
		{
			for(int i = 0;i<numbers.Length;i++)
				if (this.numbers[i] != other.numbers[i])
					return (this.numbers[i] < other.numbers[i]) ? -1: 1;
			
			return 0;
		}
		#endregion
	}
}
