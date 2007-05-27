using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IDataReadable
	{
		int NumberOfBytes { get; }
		int NumberOfSamples { get; }
		int SampleRate { get; }
		bool IsStereo { get; }
		[CLSCompliant(false)]
		IntPtr ReadData(uint length);
		void CleanupData(IntPtr buffer);
	}
}
