using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace Alexandria.Server
{
	public partial class AlexandriaService : ServiceBase
	{
		#region Constructors
		public AlexandriaService()
		{
			InitializeComponent();
		}
		#endregion

		#region Protected Methods
		protected override void OnStart(string[] args)
		{
			// TODO: Add code here to start your service.
		}

		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
		}
		#endregion
	}
}
