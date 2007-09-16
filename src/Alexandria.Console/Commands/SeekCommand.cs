using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class SeekCommand : Command
	{
		public SeekCommand() : base("Seek")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			int hours = 0, minutes = 0, seconds = 0;
			
			if (!string.IsNullOrEmpty(option))
			{
				string[] parts = option.Split(new char[] { ':' }, 3);
				if (parts.Length > 1)
				{
					if (parts.Length > 2)
					{
						hours = Convert.ToInt32(parts[0]);
						minutes = Convert.ToInt32(parts[1]);
						seconds = Convert.ToInt32(parts[2]);
					}
					else
					{
						minutes = Convert.ToInt32(parts[0]);
						seconds = Convert.ToInt32(parts[1]);
					}
				}
				else seconds = Convert.ToInt32(option);
			}

			TimeSpan position = new TimeSpan(hours, minutes, seconds);
			ContextFactory.PlaybackContext.Player.Seek((int)position.TotalMilliseconds);
			ContextFactory.PlaybackContext.WriteStatus("Stream Position", position.ToString());
			return context;
		}
	}
}
