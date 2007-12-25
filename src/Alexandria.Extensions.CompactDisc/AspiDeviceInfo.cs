using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Extensions.CompactDisc
{
	public class AspiDeviceInfo
	{
		public AspiDeviceInfo()
		{
		}
		
		private Uri path;
		private int bus;
		private int id;
		private int lun;
		private string manufacturer;
		private string model;
		private string revision;
		private string type;
		
		public Uri Path
		{
			get { return path; }
			set { path = value; }
		}
		
		public int Bus
		{
			get { return bus; }
			set { bus = value; }
		}
		
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		
		public int Lun
		{
			get { return lun; }
			set { lun = value; }
		}

		public string Manufacturer
		{
			get { return manufacturer; }
			set { manufacturer = value; }
		}

		public string Model
		{
			get { return model; }
			set { model = value; }
		}

		public string Revision
		{
			get { return revision; }
			set { revision = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public override string ToString()
		{
			return string.Format("ASPI:{0},{1},{2}", Bus, Id, Lun);
		}
	}
}
