using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IModel
	{
		EventHandler<ModelChangedEventArgs> OnModelChanged { get; set; }
		EventHandler<ModelChangingEventArgs> OnModelChanging { get; set; }
	}
}
