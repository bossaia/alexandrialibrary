using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.BinaryReader
{
	public interface IByteReader
	{
		/* Setters for identification status */
		/** Set identification status to Positive */
		void SetPositiveIdentification();
		/** Set identification status to Tentative */
		void SetTentativeIdentification();
		/** Set identification status to No identification */
		void SetNoIdentification();
		/** Set identification status to Error */
		void SetErrorIdentification();

		/** Checks whether the file has yet been classified */
		bool IsClassified();

		/** Get classification of the file */
		int GetClassification();

		/**
		 * Set identification warning
		 * @param theWarning the warning message to use
		 */
		void SetIdentificationWarning(string theWarning);

		/** Get any warning message created when identifying this file */
		string GetIdentificationWarning();

		/**
		 * Add another hit to the list of hits for this file.
		 * @param theHit The <code>FileFormatHit</code> to be added
		 */
		void AddHit(FileFormatHit theHit);

		/**
		 * Remove a hit from the list of hits for this file.
		 * @param theIndex   Index of the hit to be removed
		 */
		void RemoveHit(int theIndex);

		/** Get number of file format hits */
		int GetNumberOfHits();

		/**
		 * Get a file format hit
		 * @param theIndex index of the <code>FileFormatHit</code> to get
		 * @return the hit associated with <code>theIndex</code>
		 */
		FileFormatHit GetHit(int theIndex);

		/** Get file path of the associated file */
		string GetFilePath();

		/** Get file name of the associated file */
		string GetFileName();


		/**
		 * Position the file marker at a given byte position.
		 *
		 * The file marker is used to record how far through the file
		 * the byte sequence matching algorithm has got.
		 *
		 * @param markerPosition   The byte number in the file at which to position the marker
		 */
		void SetFileMarker(long markerPosition);

		/**
		 * Gets the current position of the file marker.
		 * @return the current position of the file marker
		 */
		long GetFileMarker();

		/**
		 * Get a byte from file
		 * @param fileIndex position of required byte in the file
		 * @return the byte at position <code>fileIndex</code> in the file
		 */
		byte GetByte(long fileIndex);

		/**
		 * Returns the number of bytes in the file
		 */
		long GetNumberOfBytes();
	}
}
