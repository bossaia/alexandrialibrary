#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

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
