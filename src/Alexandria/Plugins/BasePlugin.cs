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

namespace Alexandria.Plugins
{
	public abstract class BasePlugin : IPlugin
	{
		#region IPlugin Members
		public Guid Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public string Name
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public string Description
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public Uri Path
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public Version Version
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public System.Reflection.Assembly Assembly
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool Enabled
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public IDictionary<string, ITool> Tools
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Initialize()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SaveSettings()
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
