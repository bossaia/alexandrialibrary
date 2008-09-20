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

using Alexandria.Console.Commands;

namespace Alexandria.Console.Contexts
{
	public class DefaultContext : Context
	{
		public DefaultContext() : base(ContextConstants.Default)
		{
		}
	
		private void HandleContext(string option)
		{
			if (!string.IsNullOrEmpty(option))
			{
				if (ContextFactory.IsContext(option))
				{
					ContextFactory.SetActiveContext(option);
					Result = string.Format("Context changed to: {0}", option);
				}
				else Result = string.Format("{0} is not a valid context", option);
			}
			else
			{
				Result = string.Format("Active Context: {0}", ContextFactory.ActiveContext.Name);
			}
		}

		private void HandleStatus(string option)
		{
			System.Console.WriteLine("Alexandria Client Ready");
		}
	
		public override void HandleCommand(Command command, string option)
		{
			switch(command.Name)
			{
				case CommandConstants.Close:
					ContextFactory.ActiveContext.Close();
					break;
				case CommandConstants.Context:
					HandleContext(option);
					WriteResult();
					break;
				case CommandConstants.Status:
					if (IsActive) HandleStatus(option);
					break;
				default:
					break;
			}
		}
	}
}
