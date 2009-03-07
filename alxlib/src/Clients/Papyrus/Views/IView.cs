using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Views
{
	public interface IView
	{
		ViewActionCallback Loading { get; set; }
		ViewActionCallback Loaded { get; set; }
		ViewActionCallback Refreshing { get; set; }
		ViewActionCallback Refreshed { get; set; }
		ViewActionCallback Validating { get; set; }
		ViewActionCallback Validated { get; set; }
		ViewActionCallback Accepting { get; set; }
		ViewActionCallback Accepted { get; set; }
		ViewActionCallback Cancelling { get; set; }
		ViewActionCallback Cancelled { get; set; }
		void Display();
		void RefreshData();
		void ShowMessage(string title, string body);
	}
}
