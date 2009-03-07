using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Views
{
	public class ViewAction
	{
		public ViewAction()
		{
		    IsValid = true;
			IsRunning = true;
		}

		public ViewAction(bool isValid)
		{
		    IsValid = isValid;
			IsRunning = true;
		}

		private IList<string> messages = new List<string>();

		public bool IsValid { get; set; }
		public bool IsRunning { get; private set; }
		public Uri Path { get; set; }
		public object Content { get; set; }
		public IList<string> Messages { get { return messages; } }

		public void Stop()
		{
			IsRunning = false;
		}
	}
}
