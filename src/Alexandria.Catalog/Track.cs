using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Catalog
{
	public class Track
	{
		#region Private Fields
		private string name;
		private Artist artist;
		private Song song;
		private Album album;
		private uint length;
		private uint milliseconds;
		private uint number;
		#endregion
		
		#region Constructors
		public Track()
		{
		}
		
		public Track(string id) : this()
		{
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		
		public IResource Artist
		{
			get {return artist;}
			set {artist = value;}
		}
		
		public IResource Song
		{
			get {return song;}
			set {song = value;}
		}
		
		public IResource Album
		{
			get {return album;}
			set {album = value;}
		}

		[System.CLSCompliant(false)]
		public uint Length
		{
			get {return length;}
			set {length = value;}
		}

		[System.CLSCompliant(false)]
		public uint Milliseconds
		{
			get {return milliseconds;}
			set {milliseconds = value;}
		}

		[System.CLSCompliant(false)]
		public uint Number
		{
			get {return number;}
			set {number = value;}
		}
		#endregion
	}
}
