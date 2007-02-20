using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4AppleElementaryStreamDescriptor : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4AppleElementaryStreamDescriptor(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			//WARNING: this was changed from accessing the Data property directy to instead
			// use LoadBoxData which returns a reference to the underlying data.  This change
			// was required to avoid accessing the virtual methods that the Data property uses
			// Everything should still work but if it doesn't then this change is the culprit...			
		
			decoderConfiguration = new ByteVector();

			uint length;

			// This box contains a ton of information.
			int offset = 0;

			// This is a safe alternative to the Data property
			// it will be a reference to the same underlying structure and so should work identically to Data
			ByteVector boxData = LoadBoxData();
			
			// Elementary Stream Descriptor Tag
			if (boxData[offset++] == 3)
			{
				// We have a descriptor tag. Check that it'field at least 20 long.
				if ((length = ReadLength(boxData, offset)) < 20)
				{
					TagLibDebugger.Debug("TagLib.Mpeg4.AppleElementaryStreamDescriptor () - Could not read data. Too small.");
					return;
				}
				offset += 4;

				streamId = boxData.Mid(offset, 2).ToShort();
				offset += 2;

				streamPriority = boxData[offset++];
			}
			else
			{
				// The tag wasn'type found, so the next two byte are the ID, and
				// after that, business as usual.
				streamId = boxData.Mid(offset, 2).ToShort();
				offset += 2;
			}

			// Verify that the next data is the Decoder Configuration Descriptor
			// Tag and escape if it won'type work out.
			if (boxData[offset++] != 4)
			{
				TagLibDebugger.Debug("TagLib.Mpeg4.AppleElementaryStreamDescriptor () - Could not identify decoder configuration descriptor.");
				return;
			}

			// Check that it'field at least 15 long.
			if ((length = ReadLength(boxData, offset)) < 15)
			{
				TagLibDebugger.Debug("TagLib.Mpeg4.AppleElementaryStreamDescriptor () - Could not read data. Too small.");
				return;
			}
			offset += 4;

			// Read a lot of good info.
			objectTypeId = boxData[offset++];
			streamType = boxData[offset++];
			bufferSize = boxData.Mid(offset, 3).ToUInt();
			offset += 3;
			maximumBitrate = boxData.Mid(offset, 4).ToUInt();
			offset += 4;
			averageBitrate = boxData.Mid(offset, 4).ToUInt();
			offset += 4;

			// Verify that the next data is the Decoder Specific Descriptor
			// Tag and escape if it won'type work out.
			if (boxData[offset++] != 5)
			{
				TagLibDebugger.Debug("TagLib.Mpeg4.AppleElementaryStreamDescriptor () - Could not identify decoder specific descriptor.");
				return;
			}

			// The rest of the info is decoder specific.
			length = ReadLength(boxData, offset);
			offset += 4;
			decoderConfiguration = boxData.Mid(offset, (int)length);
		}
		#endregion
		
		#region Private Fields
		private short streamId;
		private byte streamPriority;
		private byte objectTypeId;
		private byte streamType;
		private uint bufferSize;
		private uint maximumBitrate;
		private uint averageBitrate;
		private ByteVector decoderConfiguration;
		#endregion

		#region Private Static Methods
		/*
		private uint ReadLength(int offset, long X)
		{
			byte b;
			int end = offset + 4;
			uint length = 0;

			do
			{
				b = Data[offset++];
				length = (uint)(length << 7) | (uint)(b & 0x7f);
			} while ((b & 0x80) != 0 && offset <= end);

			return length;
		}
		*/
		
		/// <summary>
		/// A safe version of the original ReadLength that can be called from the constructor
		/// </summary>
		/// <param name="data">The data to read</param>
		/// <param name="offset">The offset</param>
		/// <returns>The length as an unsigned integer</returns>
		private static uint ReadLength(ByteVector data, int offset)
		{
			byte b;
			int end = offset + 4;
			uint length = 0;

			do
			{
				b = data[offset++];
				length = (uint)(length << 7) | (uint)(b & 0x7f);
			} while ((b & 0x80) != 0 && offset <= end);

			return length;
		}
		#endregion
		
		#region Public Properties
		public short StreamId
		{
			get { return streamId; }
		}
		
		public byte StreamPriority
		{
			get { return streamPriority; }
		}
		
		public byte ObjectTypeId
		{
			get { return objectTypeId; }
		}
		
		public byte StreamType
		{
			get { return streamType; }
		}

		//was BufferSizeDb
		[System.CLSCompliant(false)]
		public uint BufferSize
		{
			get { return bufferSize; }
		}
		
		[System.CLSCompliant(false)]
		public uint MaximumBitrate
		{
			get { return maximumBitrate / 1000; }
		}
		
		[System.CLSCompliant(false)]
		public uint AverageBitrate
		{
			get { return averageBitrate / 1000; }
		}
		
		public ByteVector DecoderConfig
		{
			get { return decoderConfiguration; }
		}
		#endregion
	}
}
