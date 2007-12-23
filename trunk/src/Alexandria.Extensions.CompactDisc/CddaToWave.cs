using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Alexandria;

namespace Alexandria.CompactDiscTools
{
	public class CddaToWave : ConsoleTool
	{
		#region Constructors
		public CddaToWave(string path) : base(path)
		{
		}
		#endregion
		
		#region Protected Methods
		protected override void Execute()
		{
			//base.Execute();
		}
		#endregion
		
		#region Public Methods
		public void Run()
		{
			string path = string.Empty;
			if (Parameters != null && Parameters.Count > 0)
				path = Parameters[0];
			
			Process proc = Process.Start(string.Format("cdda2wave.exe {0}", path));
		} 
		#endregion
	}
}
