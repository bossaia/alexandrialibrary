using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	public class CDTableOfContents
	{
		#region Constructors
		public CDTableOfContents(int numberOfTracks, List<int> minutes, List<int> seconds, List<int> frames)
		{
			this.numberOfTracks = numberOfTracks;
			this.minutes = minutes;
			this.seconds = seconds;
			this.frames = frames;
		}

		public CDTableOfContents(int numberOfTracks, int[] minutes, int[] seconds, int[] frames)
		{
			this.numberOfTracks = numberOfTracks;

			foreach (int minute in minutes)
			{
				if (this.minutes.Count < numberOfTracks) this.minutes.Add(minute);
			}

			foreach (int second in seconds)
			{
				if (this.seconds.Count < numberOfTracks) this.seconds.Add(second);
			}

			foreach (int frame in frames)
			{
				if (this.frames.Count < numberOfTracks) this.frames.Add(frame);
			}
		}
		#endregion

		#region Private Fields
		private int numberOfTracks = 0;
		private List<int> minutes = new List<int>();
		private List<int> seconds = new List<int>();
		private List<int> frames = new List<int>();
		#endregion

		#region Public Properties

		#region NumberOfTracks
		public int NumberOfTracks
		{
			get { return numberOfTracks; }
		}
		#endregion

		#region Minutes
		public List<int> Minutes
		{
			get { return minutes; }
		}
		#endregion

		#region Seconds
		public List<int> Seconds
		{
			get { return seconds; }
		}
		#endregion

		#region Frames
		public List<int> Frames
		{
			get { return frames; }
		}
		#endregion

		#endregion
	}
}