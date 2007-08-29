using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class ModelChangedEventArgs : EventArgs
	{
		public ModelChangedEventArgs(IModel model)
		{
			this.model = model;
		}
		
		private IModel model;
		
		public IModel Model
		{
			get { return model; }
		}
	}
}
