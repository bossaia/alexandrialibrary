using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	#region DebugMessageSentHandler
	public delegate void DebugMessageSentHandler(string message);
	#endregion

	#region FrameCreator
	public delegate Id3v2Frame FrameCreator(ByteVector data, Id3v2FrameHeader header);
	#endregion
}
