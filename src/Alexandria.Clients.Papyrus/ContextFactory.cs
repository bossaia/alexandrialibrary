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

using Alexandria.Console.Contexts;

namespace Alexandria.Console
{
	public static class ContextFactory
	{		
		private static Dictionary<string, Context> contexts = new Dictionary<string,Context>(StringComparer.InvariantCultureIgnoreCase);
		private static Context activeContext;
		
		private static void AddContext(Context context)
		{
			if (!contexts.ContainsKey(context.Name))
				contexts.Add(context.Name, context);
		}
		
		public static bool IsContext(string name)
		{
			return Contexts.ContainsKey(name);
		}
		
		public static void SetActiveContext(string name)
		{
			if (IsContext(name))
			{
				foreach(Context context in contexts.Values)
					context.IsActive = (string.Compare(context.Name, name, true) == 0);
				
				activeContext = Contexts[name];
			}
		}
		
		public static Context ActiveContext
		{
			get { return activeContext; }
		}
		
		public static IDictionary<string, Context> Contexts
		{
			get
			{
				lock(contexts)
				{
					if (contexts.Count == 0)
					{
						AddContext(new DefaultContext());
						AddContext(new AudioContext());
						AddContext(new PlaylistContext());
					}
					
					return contexts;
				}
			}
		}
	}
}
