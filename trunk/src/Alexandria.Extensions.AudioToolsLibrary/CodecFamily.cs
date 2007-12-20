using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.AudioToolsLibrary
{
	public enum CodecFamily
	{
		Lossy		 = 0, // Streamed, lossy data
		Lossless	 = 1, // Streamed, lossless data
		SequencedWav = 2, // Sequenced with embedded sound library
		Sequenced	 = 3 // Sequenced with codec-dependent sound library

	}
}
