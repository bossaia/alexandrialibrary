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
