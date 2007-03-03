using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	public class Disc
	{
		#region Constructors
		public Disc(CDTableOfContents toc)
		{
			this.toc = toc;

			this.id = Disc.GetDiscId(toc);
		}
		#endregion
		
		#region Private Fields
		private ulong id = 0;
		private string title = string.Empty;
		private string primaryArtist = string.Empty;
		private string label = string.Empty;
		private string yearReleased = string.Empty;
		private string copywrongNotice = string.Empty;
		private CDTableOfContents toc = null;
		#endregion
				
		#region Public Properties
		
		#region Toc
		public CDTableOfContents Toc
		{
			get {return toc;}
		}
		#endregion
		
		#region Id
		public ulong Id
		{
			get {return id;}
		}
		#endregion
		
		#endregion

		#region Static Methods

		#region Private Static Methods

		#region GetTocSum
		/// <summary>
		/// Get the sum of all tracks in seconds; this is used by CalculateDiscId()
		/// </summary>
		/// <returns>an integer</returns>
		/// <remarks>For backward compatibility this algorithm must not change.
		/// This algorithm is based on the freedb.org reference at www.freedb.org/....
		/// </remarks>
		private static ulong GetTocSum(ulong n)
		{
			ulong sum = 0;

			while (n > 0)
			{
				sum = sum + (n % 10);
				n = n / 10;
			}

			return sum;
		}
		#endregion

		#endregion
		
		#region Public static Methods
		
		#region GetDiscId
		/// <summary>
		/// Gets the id for this disc based on the total length of all tracks
		/// </summary>
		/// <returns>The disc id as an unsigned long integer</returns>
		/// <remarks>For backward compatibility this algorithm must not change.
		/// This algorithm is based on the freedb.org reference at www.freedb.org/....
		/// </remarks>
		public static ulong GetDiscId(CDTableOfContents toc)
		{
			int numberOfTracks = toc.NumberOfTracks -1;
		
			int index = 0;
			ulong offset = 0, sum = 0;
			//int other = 0xff;	// = 255
			
			for(index = 0; index < toc.NumberOfTracks; index++)
			{
				sum = sum + GetTocSum(Convert.ToUInt64((toc.Minutes[index] * 60) + toc.Seconds[index]));
			}
			
			offset = 
				Convert.ToUInt64(
				((toc.Minutes[numberOfTracks] * 60) + toc.Seconds[numberOfTracks]) -
				((toc.Minutes[0] * 60) + toc.Seconds[0])
				);
			
			return Convert.ToUInt64( ((sum % 0xff) << 24) | (offset << 8) | Convert.ToUInt64(toc.NumberOfTracks) );
			
			//return (ulong)( ((sum % 0xFF) << 24) | (offset << 8) | ((ulong)toc.NumberOfTracks) );
		}
		#endregion
		
		#endregion
		
		#endregion
	}
}
