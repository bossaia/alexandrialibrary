using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Papyrus.Views;

namespace Papyrus.Forms
{
	public class FormBase : Form, IView
	{
		protected virtual void LoadForm()
		{
			LoadForm(new ViewAction());
		}

		protected virtual void LoadForm(ViewAction action)
		{
			if (action != null)
			{
				if (Loading != null)
					Loading(action);

				if (!action.IsRunning)
					return;

				if (Loaded != null)
					Loaded(action);
			}
		}

		protected virtual void ValidateForm()
		{
			ValidateForm(new ViewAction());
		}

		protected virtual void ValidateForm(ViewAction action)
		{
			if (action != null)
			{
				if (Validating != null)
					Validating(action);

				if (!action.IsRunning)
					return;

				if (Validated != null)
					Validated(action);
			}
		}

		protected virtual void AcceptForm()
		{
			AcceptForm(new ViewAction());
		}

		protected virtual void AcceptForm(ViewAction action)
		{
			if (action != null)
			{
				if (Accepting != null)
					Accepting(action);

				if (!action.IsRunning)
					return;

				if (Accepted != null)
					Accepted(action);
			}
		}

		protected virtual void CancelForm()
		{
			CancelForm(new ViewAction());
		}

		protected virtual void CancelForm(ViewAction action)
		{
			if (action != null)
			{
				if (Cancelling != null)
					Cancelling(action);

				if (!action.IsRunning)
					return;

				Close();

				if (Cancelled != null)
					Cancelled(action);
			}
		}

		#region IView Members

		public ViewActionCallback Loading { get; set; }

		public ViewActionCallback Loaded { get; set; }

		public ViewActionCallback Refreshing { get; set; }

		public ViewActionCallback Refreshed { get; set; }

		public new ViewActionCallback Validating { get; set; }

		public new ViewActionCallback Validated { get; set; }

		public ViewActionCallback Accepting { get; set; }

		public ViewActionCallback Accepted { get; set; }

		public ViewActionCallback Cancelling { get; set; }

		public ViewActionCallback Cancelled { get; set; }

		public virtual void Display()
		{
			Show();
		}

		public virtual void RefreshData()
		{
		}

		public virtual void ShowMessage(string title, string body)
		{
			MessageBox.Show(body, title);
		}

		#endregion
	}
}