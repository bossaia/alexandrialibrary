#region License (MIT)
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
using System.Collections.Generic;
using Telesophy.Alexandria.Model;

namespace Alexandria.MusicDns
{
	internal static class PuidFactory
	{
		#region Private Constants
		private const string ID_TYPE = "PUID";		
		#endregion
		
		#region Private Static Fields
		private static readonly Version version = new Version(1, 0, 0, 0);
		#endregion
	
		#region Public Static Methods
		internal static IMetadataIdentifier CreatePuid(string value)
		{
			IMetadataIdentifier identifier = new MetadataIdentifier(value, ID_TYPE, version);
			return identifier;
		}
		#endregion
	}
}
