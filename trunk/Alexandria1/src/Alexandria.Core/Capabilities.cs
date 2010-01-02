using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telesophy.Alexandria.Core
{
	public struct Capabilities
	{
		private bool usesFileSystem;
		private bool usesNetwork;
		
		public Capabilities(bool usesFileSystem, bool usesNetwork)
		{
			this.usesFileSystem = usesFileSystem;
			this.usesNetwork = usesNetwork;
		}
		
		public bool UsesFileSystem
		{
			get { return usesFileSystem; }
		}
		
		public bool UsesNetwork
		{
			get { return usesNetwork; }
		}
	}
}
