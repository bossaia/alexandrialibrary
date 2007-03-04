using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.BinaryReader
{
	public class StreamByteReader : AbstractByteReader
	{
		/** Creates a new instance of StreamByteReader */
		protected StreamByteReader(IdentificationFile theIDFile) : base(theIDFile)
		{
			//super(theIDFile);
		}
    
		/** Size of buffer to store the stream in */
		//private static int BUFFER_SIZE = 131072;

		/** Buffer to contain the contents of the stream. */
		//protected ByteBuffer buffer = null;
		protected System.IO.MemoryStream buffer = null;

		/** This will be non-null if the stream has been written to a temporary file. */
		protected System.IO.FileInfo tempFile = null;
    
		/**
		* Read stream into a <code>ByteBuffer</code> or temporary file.
		*
		* <p>This method allocates a buffer, and then attempts to read the stream into
		* it. If the buffer isn't big enough, the contents of it are transferred to a 
		* temporary file, and then the rest of the stream is appended to this file.
		*
		* <p>After this method has been called, the field <code>tempFile</code> is
		* <code>null</code> if the contents of the stream could fit into the buffer,
		* and is the created temporary file otherwise.
		*
		* @param inStream the stream to read in.
		* @throws java.io.IOException if there is an error writing to the temporary 
		* file
		*/
		protected void readStream()
		//InputStream inStream) //throws IOException
		{
			//TODO: figure out how to port this
			/*
			ReadableByteChannel c = Channels.newChannel(inStream);
			if (buffer == null)
			{
				buffer = new System.IO.MemoryStream(BUFFER_SIZE);
			}
			else
			{
				//TODO: figure out how to port this
				//buffer.clear();
			}
        
			
			//Not all bytes in the channel are available at once.
			//Loop until end of file and while the buffer has space
			
			int bytes = 0;
			while (bytes >= 0 && (buffer.Length < buffer.Capacity)) //.hasRemaining())
			{
				bytes = c.read(buffer);
			}
        
			// Sets limit to current position, then position to start of the buffer.			
			buffer.Capacity = (int)buffer.Position; //buffer.flip();
        
			if (buffer.Capacity == 0) // was buffer.limit
			{
				this.setErrorIdent();
				this.setIdentificationWarning("Zero-length file");
				return;
			}
        
			if (bytes != -1)
			{
				// Haven't got the whole file
				// Write it to a temporary file
				tempFile = writeToTempFile(buffer, c);
			}
			*/
		}
    
		/**
		* Write contents of <code>buffer</code> to a temporary file, followed by the remaining bytes
		* in <code>channel</code>.
		*
		* <p>The bytes are read from <code>buffer</code> from the current position to its limit.
		*
		* @param buffer contains the contents of the channel read so far
		* @param channel the rest of the channel
		* @throws java.io.IOException if there is a problem writing to the file
		* @return <code>File</code> object for the temporary file.
		*/
		static System.IO.FileInfo writeToTempFile()
		//ByteBuffer buffer, ReadableByteChannel channel) //throws IOException
		{
			//TODO: figure out how to port this
			/*
			File tempFile = java.io.File.createTempFile("droid",null);
			FileChannel fc = (new FileOutputStream(tempFile)).getChannel();
			ByteBuffer buf = ByteBuffer.allocate(8192);
			fc.write(buffer);
			buf.clear();
			for (;;)
			{
				if (channel.read(buf) < 0)
				{
					break;        // No more bytes to transfer
				}
				buf.flip();
				fc.write(buf);
				buf.compact();    // In case of partial write
			}
			fc.close();
			return tempFile;
			 */
			 return null;
		}
    
		/**
		* Get a byte from file
		* @param fileIndex position of required byte in the file
		* @return the byte at position <code>fileIndex</code> in the file
		*/
		public byte getByte(long fileIndex)
		{
			byte[] data = new byte[1];
			buffer.Read(data, (int)fileIndex, 1); //.get((int) fileIndex);
			return data[0];
		}
    
    
		/**
		* Gets the current position of the file marker.
		* @return the current position of the file marker
		*/
		public long getFileMarker()
		{
			return buffer.Position;
		}
    
    
		/**
		* Returns the number of bytes in the file
		*/
		public long getNumBytes()
		{
			return buffer==null ? 0 : buffer.Capacity; //limit();
		}
    
    
		/**
		* Position the file marker at a given byte position.
		*
		* <p>The file marker is used to record how far through the file
		* the byte sequence matching algorithm has got.
		*
		* @param markerPosition   The byte number in the file at which to position the marker
		*/
		public void setFileMarker(long markerPosition)
		{
			buffer.Seek(markerPosition, System.IO.SeekOrigin.Begin); //position((int) markerPosition);
		}
	}
}
