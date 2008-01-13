#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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

namespace Telesophy.Alexandria.Persistence
{
	public class Command<Model> : ICommand<Model>
	{
		#region Constructors
		public Command(IMap<Model> map, CommandType type, string text)
		{
			this.map = map;
			this.type = type;
			this.text = text;
		}
		#endregion
		
		#region Private Fields
		private string text;
		private CommandType type;
		private IMap<Model> map;
		private List<ICommand> additionalCommands = new List<ICommand>();
		#endregion
	
		#region ICommand Members
		public string Text
		{
			get { return text; }
		}

		public CommandType Type
		{
			get { return type; }
		}
		#endregion
	
		#region ICommand<Model> Members
		public IList<ICommand> AdditionalCommands
		{
			get { return additionalCommands; }
		}

		public IMap<Model> Map
		{
			get { return map; }
		}
		#endregion
	}
}
