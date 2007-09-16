using System;
using System.Collections.Generic;

using Alexandria.Console.Contexts;

namespace Alexandria.Console
{
	public static class ContextFactory
	{		
		private static Dictionary<string, Context> contexts = new Dictionary<string,Context>(StringComparer.InvariantCultureIgnoreCase);
		
		private static void AddContext(Context context)
		{
			if (!contexts.ContainsKey(context.Name))
				contexts.Add(context.Name, context);
		}
		
		public static bool IsContext(string name)
		{
			return Contexts.ContainsKey(name);
		}
		
		public static IDictionary<string, Context> Contexts
		{
			get
			{
				lock(contexts)
				{
					if (contexts.Count == 0)
					{
						AddContext(new PlaybackContext());
					}
					
					return contexts;
				}
			}
		}
		
		public static PlaybackContext PlaybackContext
		{
			//TODO: refactor this
			get { return Contexts["Playback"] as PlaybackContext; }
		}
	}
}
