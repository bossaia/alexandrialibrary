using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Alexandria.AlbumArtDownloader
{
	public class Script
	{
		#region Constructors
		internal Script(ArtDownloader a, Type thetype)
		{
			lock (this)
			{
				_Name = thetype.Name;
				artdownloader = a;

				getThumbs = thetype.GetMethod("GetThumbs", BindingFlags.Static | BindingFlags.Public);
				getResult = thetype.GetMethod("GetResult", BindingFlags.Static | BindingFlags.Public);

				PropertyInfo nameinfo = thetype.GetProperty("SourceName", BindingFlags.Static | BindingFlags.Public);
				_Name = nameinfo.GetValue(null, null).ToString();

				nameinfo = thetype.GetProperty("SourceVersion", BindingFlags.Static | BindingFlags.Public);
				_Version = nameinfo.GetValue(null, null).ToString();

				try
				{
					nameinfo = thetype.GetProperty("SourceCreator", BindingFlags.Static | BindingFlags.Public);
					_Creator = nameinfo.GetValue(null, null).ToString();
				}
				catch
				{
					_Creator = "";
				}


				if (getThumbs == null || getResult == null)
					throw new Exception("Script must implement GetThumbs() And GetResult()");

				//group = a.mainForm.AddGroup(this);
				//listcol = a.mainForm.AddCol(this);
			}

		}
		#endregion
		
		#region Private Fields
		//public ListViewGroup group;
		//public ColumnHeader listcol;
		private ArtDownloader artdownloader;		
		private string _Name;
		private string _Version;
		private string _Creator;
		private bool _Enabled = true;
		private int _SortPosition;
		private MethodInfo getThumbs, getResult;
		#endregion

		#region Public Properties
		public string Name
		{
			get { return _Name; }
		}
		
		public string Version
		{
			get	{ return _Version; }
		}
		public string Creator
		{
			get { return _Creator; }
		}

		public bool Enabled
		{
			get	{ return _Enabled; }
			set { _Enabled = value;	}
		}

		public int SortPosition
		{
			get { return _SortPosition; }
			set	{ _SortPosition = value; }
		}
		#endregion

		#region Internal Methods
		internal void GetThumbs(ScriptTask t, string artist, string album)
		{
			try
			{
				getThumbs.Invoke(null, new object[] { t, artist, album });
			}
			catch (Exception e)
			{
				if (e.GetType() == typeof(System.Reflection.TargetInvocationException))
				{
					throw e.InnerException;
				}
			}
		}
		
		internal object GetResult(object data)
		{
			try
			{
				return getResult.Invoke(null, new object[] { data });
			}
			catch (Exception e)
			{
				if (e.GetType() == typeof(System.Reflection.TargetInvocationException))
				{
					throw e.InnerException;
				}
			}
			return null;
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return Name;
		}
		#endregion
	}
}
