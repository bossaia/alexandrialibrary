using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Client.Views
{
	public interface IQueueView : IView
	{
		void ClearItems();
		void AddItem(QueueItem item);
	}
}
