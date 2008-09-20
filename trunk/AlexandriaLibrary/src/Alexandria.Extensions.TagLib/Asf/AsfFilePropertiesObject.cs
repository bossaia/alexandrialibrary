/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;

namespace Alexandria.TagLib
{
	public class AsfFilePropertiesObject : AsfObject
	{
		#region Constructors
		public AsfFilePropertiesObject(AsfFile file, long position) : base(file, position)
		{
			if (file != null)
			{
				if (!Guid.Equals(AsfGuid.AsfFilePropertiesObject))
					throw new TagLibException(TagLibError.AsfObjectGuidIncorrect);

				if (OriginalSize < 104)
					throw new TagLibException(TagLibError.AsfObjectSizeTooSmall);

				fileId = file.ReadGuid();
				fileSize = file.ReadQWord();
				creationDate = file.ReadQWord();
				dataPacketsCount = file.ReadQWord();
				playDuration = file.ReadQWord();
				sendDuration = file.ReadQWord();
				preRoll = file.ReadQWord();
				flags = file.ReadDWord();
				minimumDataPacketSize = file.ReadDWord();
				maximumDataPacketSize = file.ReadDWord();
				maximumBitrate = file.ReadDWord();
			}
		}
		#endregion
		
		#region Private Fields
		private AsfGuid fileId;
		private long fileSize;
		private long creationDate;
		private long dataPacketsCount;
		private long playDuration;
		private long sendDuration;
		private long preRoll;
		private uint flags;
		private uint minimumDataPacketSize;
		private uint maximumDataPacketSize;
		private uint maximumBitrate;
		#endregion
		
		#region Public Properties
		public AsfGuid FileId
		{
			get { return fileId; }
		}
		
		public long FileSize
		{
			get { return fileSize; }
		}
		
		public DateTime CreationDate
		{
			get { return new DateTime(creationDate); }
		}
		
		public long DataPacketsCount
		{
			get { return dataPacketsCount; }
		}
		
		public TimeSpan PlayDuration
		{
			get { return new TimeSpan(playDuration); }
		}
		
		public TimeSpan SendDuration
		{
			get { return new TimeSpan(sendDuration); }
		}

		public long PreRoll
		{
			get { return preRoll; }
		}

		[System.CLSCompliant(false)]
		public uint Flags
		{
			get { return flags; }
		}

		[System.CLSCompliant(false)]
		public uint MinimumDataPacketSize
		{
			get { return minimumDataPacketSize; }
		}
		
		[System.CLSCompliant(false)]
		public uint MaximumDataPacketSize
		{
			get { return maximumDataPacketSize; }
		}
		
		[System.CLSCompliant(false)]
		public uint MaximumBitrate
		{
			get { return maximumBitrate; }
		}
		#endregion
		
		#region Public Methods
		public override ByteVector Render()
		{
			ByteVector output = new ByteVector();

			output += fileId.Render();
			output += RenderQWord(fileSize);
			output += RenderQWord(creationDate);
			output += RenderQWord(dataPacketsCount);
			output += RenderQWord(playDuration);
			output += RenderQWord(sendDuration);
			output += RenderQWord(preRoll);
			output += RenderDWord(flags);
			output += RenderDWord(minimumDataPacketSize);
			output += RenderDWord(maximumDataPacketSize);
			output += RenderDWord(maximumBitrate);

			return Render(output);
		}
		#endregion
	}
}
