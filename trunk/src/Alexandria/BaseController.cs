using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseController<M, V> : IController<M , V> where M: IModel where V: IView
	{
		public BaseController(IController parent, M model, V view)
		{
			this.parent = parent;
			this.model = model;
			this.view = view;
		}
		
		private M model;
		private V view;
		private IController parent;
		private IList<IController> children = new List<IController>();

		#region IController<M,V> Members
		public M Model
		{
			get { return model; }
		}

		public V View
		{
			get { return view; }
		}
		#endregion

		#region IController Members
		IModel IController.Model
		{
			get { return model; }
		}

		IView IController.View
		{
			get { return view; }
		}

		public IController Parent
		{
			get { return parent; }
		}

		public IList<IController> Children
		{
			get { return children; }
		}

		public virtual void ReceiveMessage(object sender, IMessage message)
		{
			if (sender == null)
				throw new ArgumentNullException("sender");
			
			foreach (IController child in children)
			{
				if (!sender.Equals(child))
					child.ReceiveMessage(this, message);
			}
			
			if (!sender.Equals(parent))
				parent.ReceiveMessage(this, message);
		}

		public virtual void ReceiveAsync(IAsyncResult result)
		{
		}
		#endregion
	}
}
