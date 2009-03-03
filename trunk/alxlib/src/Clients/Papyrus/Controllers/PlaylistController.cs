using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alexandria.Core.Messaging;
using Papyrus.Forms;
using Papyrus.Views;

namespace Papyrus.Controllers
{
	public class PlaylistController
	{
		public PlaylistController()
		{
			form.Validating += new ViewActionCallback(ValidatingView);
		}

		private PlaylistForm form = new PlaylistForm();

		public void DisplayView()
		{
			

			form.Show();
		}

		private void ValidatingView(ViewAction action)
		{
			//if (action != null && action.IsValid)
			//{
			//}
		}
	}
}
