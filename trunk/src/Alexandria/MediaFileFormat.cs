using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Alexandria
{
	public abstract class MediaFileFormat
	{
		#region Private Fields
		private string name;
		private System.Net.Mime.ContentType mime;
		private IList<FileExtension> allowedFileExtensions;
		private FileExtension defaultFileExtension;
		private bool supportsTags;
		#endregion
		
		#region Private Static Fields
		private static Dictionary<string, MediaFileFormat> manifest;
		#endregion
		
		#region Constructors
		protected MediaFileFormat(string name, System.Net.Mime.ContentType mime, bool supportsTags)
		{
			this.name = name;
			this.mime = mime;
			this.supportsTags = supportsTags;
		}
		
		protected MediaFileFormat(string name, System.Net.Mime.ContentType mime, bool supportsTags, IList<FileExtension> allowedFileExtensions) : this(name, mime, supportsTags)
		{
			this.allowedFileExtensions = allowedFileExtensions;
			if (allowedFileExtensions != null && allowedFileExtensions.Count > 0)
			{
				this.defaultFileExtension = allowedFileExtensions[0];
			}
		}

		protected MediaFileFormat(string name, System.Net.Mime.ContentType mime, bool supportsTags, IList<FileExtension> allowedFileExtensions, FileExtension defaultFileExtension) : this(name, mime, supportsTags)
		{
			this.allowedFileExtensions = allowedFileExtensions;
			this.defaultFileExtension = defaultFileExtension;
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
		}
		
		public System.Net.Mime.ContentType Mime
		{
			get {return mime;}
		}
		
		public bool SupportsTags
		{
			get {return supportsTags;}
		}
		
		public IList<FileExtension> AllowedFileExtensions
		{
			get {return allowedFileExtensions;}
			protected set {allowedFileExtensions = value;}
		}
		
		public FileExtension DefaultFileExtension
		{
			get {return defaultFileExtension;}
			protected set {defaultFileExtension = value;}
		}
		#endregion
		
		#region Public Static Methods
		public static void InitializeManifest()
		{
			manifest = new Dictionary<string,MediaFileFormat>();
			
			Assembly asm = Assembly.GetExecutingAssembly();
			foreach(Type type in asm.GetTypes())
			{
				if (type.BaseType != null && type.BaseType == typeof(MediaFileFormat))
				{
					ConstructorInfo ctorInfo = type.GetConstructor(System.Type.EmptyTypes);
					MediaFileFormat format = (MediaFileFormat)ctorInfo.Invoke(null);
					if (format != null && format.AllowedFileExtensions.Count > 0)
					{
						foreach(FileExtension ext in format.AllowedFileExtensions)
						{
							manifest.Add(ext.ToString().ToUpper(System.Globalization.CultureInfo.InvariantCulture), format);
						}
					}
				}
			}
		}
		
		public static Dictionary<string, MediaFileFormat> Manifest
		{
			get
			{
				if (manifest == null) InitializeManifest();
				
				return manifest;
			}
		}
		#endregion
	}
}
