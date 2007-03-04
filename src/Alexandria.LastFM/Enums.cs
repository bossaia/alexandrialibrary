using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.LastFM
{
	#region State
	public enum State
	{
		IDLE,
		NEED_HANDSHAKE,
		NEED_TRANSMIT,
		WAITING_FOR_REQ_STREAM,
		WAITING_FOR_HANDSHAKE_RESP,
		WAITING_FOR_RESP
	};
	#endregion
}
