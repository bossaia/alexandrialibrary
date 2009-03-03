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
		protected virtual void Accept()
		{
			//ViewAction action = new ViewAction(ViewActionType.Validate);

			//if (Validating != null)
			//    Validating(action);

			//if (!action.IsValid)
			//    return;

			//if (Validated != null)
			//    Validated(action);

			//action.Type = ViewActionType.Accept;

			//if (Accepting != null)
			//    Accepting(action);

			//if (!action.IsValid)
			//    return;

			//if (action.IsValid)
			//    Close();

			//if (Accepted != null)
			//    Accepted(action);
		}

		protected virtual void btnCancel_Click(object sender, EventArgs e)
		{
			//ViewAction cancel = new ViewAction(ViewActionType.Cancel);

			//if (OnCancel != null)
			//{
			//    OnCancel(cancel);
			//}

			//if (cancel.IsValid)
				//Close();
		}

		#region IView Members

		public ViewActionCallback Refreshing
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ViewActionCallback Refreshed
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public new ViewActionCallback Validating
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public new ViewActionCallback Validated
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ViewActionCallback Accepting
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ViewActionCallback Accepted
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ViewActionCallback Cancelling
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ViewActionCallback Cancelled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}
