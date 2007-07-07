#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Reflection;

namespace Alexandria.Persistence
{
	public enum FactoryMapType
	{
		Constructor,
		Method
	}

	public class FactoryMap
	{
		#region Constructors
		public FactoryMap(FactoryAttribute attribute, ConstructorInfo constructor)
		{
			this.attribute = attribute;
			this.constructor = constructor;
			this.type = FactoryMapType.Constructor;
		}
		
		public FactoryMap(FactoryAttribute attribute, object factory, MethodInfo method)
		{
			this.attribute = attribute;
			this.factory = factory;
			this.method = method;
			this.type = FactoryMapType.Method;
		}
		#endregion
		
		#region Private Fields
		private FactoryAttribute attribute;
		private ConstructorInfo constructor;
		private object factory;
		private MethodInfo method;
		private FactoryMapType type;
		#endregion
		
		#region Public Properties
		public FactoryAttribute Attribute
		{
			get { return attribute; }
		}
		
		public ConstructorInfo Constructor
		{
			get { return constructor; }
		}
		
		public MethodInfo Method
		{
			get { return method; }
		}
		#endregion
		
		#region Public Methods
		public ParameterInfo[] GetParameters()
		{
			switch (type)
			{
				case FactoryMapType.Constructor:
					return constructor.GetParameters();
				case FactoryMapType.Method:
					return method.GetParameters();
				default:
					return null;
			}
		}
		
		public IRecord GetRecord(object[] parameters)
		{
			switch (type)
			{
				case FactoryMapType.Constructor:
					return (IRecord)constructor.Invoke(parameters);
				case FactoryMapType.Method:
					return (IRecord)method.Invoke(factory, parameters);
				default:
					return null;
			}
		}
		#endregion
	}
}
