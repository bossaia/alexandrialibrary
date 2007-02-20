using System;
using System.Collections;
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4Box
	{
		#region Constructors
		public Mpeg4Box(Mpeg4BoxHeader header, Mpeg4Box parent)
		{
			// Initialize everything.
			this.header = header;
			this.parent = parent;
			//this.data     = null;
			//this.children = new ArrayList();
			//this.loadChildrenStarted = false;
			
			// This was necessary after refactoring the File, DataPosition and DataSize
			// properties so that they are no longer virtual
			if (header != null && file == null)
			{
				this.file = header.File;
			}
		}

		// Do the same as above, but accept a box type.
		public Mpeg4Box(ByteVector type, Mpeg4Box parent) : this(new Mpeg4BoxHeader(type), parent)
		{
		}

		// Who needs a parent? I'm all grown up.
		public Mpeg4Box(ByteVector type) : this(type, null)
		{
		}
		#endregion
		
		#region Private Fields
		private Mpeg4BoxHeader header;
		private Mpeg4Box parent;
		private ByteVector data;
		private bool loadChildrenStarted;
		//private ArrayList children;
		private List<Mpeg4Box> children = new List<Mpeg4Box>();
		private Mpeg4File file;
		#endregion
		
		#region Private Properties
		// If the file is readable and the box can have children and there is 
		// enough space to read the data, get the first child box by reading the
		// file.
		private Mpeg4Box FirstChild
		{
			get
			{
				if (HasChildren && this.File != null && (GetType() == typeof(Mpeg4FileBox) || DataSize >= 8))
					return Mpeg4Box.Create(File, DataPosition, this);
				return null;
			}
		}
		#endregion
		
		#region Private Methods
		// If the file is readable and the box can have children and there is 
		// enough space to read the data, get the next box by reading from the end
		// of the first box.
		private Mpeg4Box NextChild(Mpeg4Box child)
		{
			if (HasChildren && this.File != null && child.NextBoxPosition >= DataPosition && child.NextBoxPosition < NextBoxPosition)
				return Mpeg4Box.Create(File, child.NextBoxPosition, this);
			return null;
		}
		#endregion

		#region Protected Properties
		[CLSCompliant(false)]
		protected ulong HeaderDataSize
		{
			get { return this.header.DataSize; }
		}
		#endregion
		
		#region Protected Methods
		/// <summary>
		/// A safe method to use for initializing the box data from a constructor
		/// </summary>
		/// <param name="data">The data to initialize this box with</param>
		/// <remarks>
		/// When getting or setting data outside of a constructor it is safe to use the Data property
		/// </remarks>
		protected void InitializeBoxData(ByteVector data)
		{
			this.data = data;
		}
		
		// Render a box with the "data" before its content.
		protected virtual ByteVector Render(ByteVector data)
		{
			bool freeFound = false;

			ByteVector output = new ByteVector();

			// If we have children, render them if they aren'type "free", otherwise
			// render the box'field data.
			if (HasChildren)
				foreach (Mpeg4Box box in Children)
					if (box.GetType() == typeof(Mpeg4IsoFreeSpaceBox))
						freeFound = true;
					else
						output += box.Render();
			else
				output += Data;

			// If there was a free, don'type take it away, and let meta be a special case.
			if (freeFound || BoxType == "meta")
			{
				long sizeDifference = (long)DataSize - (long)output.Count;

				// If we have room for free space, add it so we don'type have to resize the file.
				if (header.DataSize != 0 && sizeDifference >= 8)
					output += (new Mpeg4IsoFreeSpaceBox((ulong)sizeDifference, this)).Render();
				// If we're getting bigger, get a lot bigger so we might not have to again.
				else
					output += (new Mpeg4IsoFreeSpaceBox(2048, this)).Render();
			}

			// Adjust the header'field data size to match the content.
			header.DataSize = (ulong)(data.Count + output.Count);

			// Render the full box.
			return header.Render() + data + output;
		}				
		
		protected ByteVector LoadBoxData()
		{
			LoadData(this.header.DataPosition, (int)this.header.DataSize);
			return data;
		}
		
		[CLSCompliant(false)]
		protected ByteVector LoadBoxData(long dataPosition, ulong dataSize)
		{
			LoadData(dataPosition, (int)dataSize);
			return data;
		}
		
		protected ByteVector LoadBoxData(int dataPositionOffset, int dataSizeOffset)
		{
			long dataPosition = this.header.DataPosition + dataPositionOffset;
			
			int dataSize = ((int)this.header.DataSize) + dataSizeOffset;
			
			LoadData(dataPosition, dataSize);
			return data;
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Is the box valid?
		/// </summary>
		public virtual bool IsValid
		{
			get { return header.IsValid; }
		}

		/// <summary>
		/// The file associated with this box.
		/// </summary>
		/// <remarks>
		/// This property used to be virtual but it was refactored for FxCop compliance
		/// The only class that had an override to this property was Mpeg4FileBox
		/// and that class has also been refactored to work with this change
		/// </remarks>
		public Mpeg4File File
		{
			get { return file; }			 
			protected set { file = value; }
		}		

		/// <summary>
		/// The box'field parent box.
		/// </summary>
		public Mpeg4Box Parent
		{
			get { return parent; } set { parent = value; }
		}

		// The box type.
		public virtual ByteVector BoxType
		{
			get { return header.BoxType; }
		}

		// The total size of the box.
		[System.CLSCompliant(false)]
		public virtual ulong BoxSize
		{
			get { return header.BoxSize; }
		}
		
		/// <summary>
		/// The size of the non-header data.
		/// </summary>		
		[System.CLSCompliant(false)]
		protected virtual ulong DataSize
		{
			get { return header.DataSize; }
		}

		/// <summary>
		/// The stream position of the box'field data.
		/// </summary>
		protected virtual long DataPosition
		{
			get { return header.DataPosition; }
		}

		// Whether or not the box can have children.
		public virtual bool HasChildren
		{
			get { return false; }
		}

		// The stream position of the next box.
		public virtual long NextBoxPosition
		{
			get { return header.NextBoxPosition; }
		}

		// All child boxes of this box.
		public IList<Mpeg4Box> Children
		{
			get
			{
				LoadChildren();
				return children; //(Mpeg4Box[])children.ToArray(typeof(Mpeg4Box));
			}
		}

		// The handler used for this box.
		public Mpeg4IsoHandlerBox Handler
		{
			get
			{
				Mpeg4Box box = this;

				// Look in all parent boxes.
				while (box != null)
				{
					// Handlers will be contained in "meta" and "mdia" boxes.
					if (box.BoxType == "mdia" || box.BoxType == "meta")
					{
						// See if you can find a handler, and return if you can.
						Mpeg4IsoHandlerBox handler = (Mpeg4IsoHandlerBox)box.FindChild(typeof(Mpeg4IsoHandlerBox));
						if (handler != null)
							return handler;
					}

					// Check the parent next.
					box = box.Parent;
				}

				// Failure.
				return null;
			}
		}

		// The data contained within this box.
		public virtual ByteVector Data
		{
			get
			{
				LoadData();
				return data;
			}
			set
			{
				data = value;
			}
		}
		#endregion
		
		#region Public Methods
		// Render the complete box including children.
		public virtual ByteVector Render()
		{
			return Render(new ByteVector());
		}

		// Overwrite the box'field header with a new header incorporating a size
		// change.
		public virtual void OverwriteHeader(long sizeChange)
		{
			// If we don'type have a header we can'type do anything.
			if (header == null || header.Position < 0)
			{
				TagLibDebugger.Debug("Box.OverWriteHeader() - No header to overwrite.");
				return;
			}

			// Make sure this alteration won'type screw up the reading of children.
			LoadChildren();

			// Save the header'field original position and size.
			long position = header.Position;
			long oldSize = header.HeaderSize;

			// Update the data size.
			header.DataSize = (ulong)((long)header.DataSize + sizeChange);

			// Render the header onto the file.
			File.Insert(header.Render(), position, oldSize);
		}

		// Find the first child with a given box type.
		public Mpeg4Box FindChild(ByteVector type)
		{
			foreach (Mpeg4Box child in Children)
				if (child.BoxType == type)
					return child;

			return null;
		}

		// Find the first child with a given System.Type.
		public Mpeg4Box FindChild(System.Type type)
		{
			foreach (Mpeg4Box child in Children)
				if (child.GetType() == type)
					return child;

			return null;
		}

		// Recursively find the first child with a given box type giving 
		// preference to the current depth.
		public Mpeg4Box FindChildDeep(ByteVector type)
		{
			foreach (Mpeg4Box child in Children)
				if (child.BoxType == type)
					return child;

			foreach (Mpeg4Box child in Children)
			{
				Mpeg4Box success = child.FindChildDeep(type);
				if (success != null)
					return success;
			}

			return null;
		}

		// Recursively find the first child with a given System.Type giving
		// preference to the current depth.
		public Mpeg4Box FindChildDeep(System.Type type)
		{
			foreach (Mpeg4Box child in Children)
				if (child.GetType() == type)
					return child;

			foreach (Mpeg4Box child in Children)
			{
				Mpeg4Box success = child.FindChildDeep(type);
				if (success != null)
					return success;
			}

			return null;
		}

		// Add a child to this box.
		public void AddChild(Mpeg4Box child)
		{
			children.Add(child);
			child.Parent = this;
		}

		// Remove a child from this box.
		public void RemoveChild(Mpeg4Box child)
		{
			children.Remove(child);
			child.Parent = null;
		}

		// Remove all children with a given box type.
		public void RemoveChildren(ByteVector type)
		{
			Mpeg4Box box;
			while ((box = FindChild(type)) != null)
				RemoveChild(box);
		}

		// Remove all children with a given System.Type.
		public void RemoveChildren(System.Type type)
		{
			Mpeg4Box box;
			while ((box = FindChild(type)) != null)
				RemoveChild(box);
		}

		// Detach the current box from its parent.
		public void RemoveFromParent()
		{
			if (Parent != null)
				Parent.RemoveChild(this);
		}

		// Remove all children from this box.
		public void ClearChildren()
		{
			foreach (Mpeg4Box box in children)
				box.Parent = null;
			children.Clear();
		}

		// Replace a child with a new one.
		public void ReplaceChild(Mpeg4Box oldChild, Mpeg4Box newChild)
		{
			int index = children.IndexOf(oldChild);

			if (index >= 0)
			{
				children[index] = newChild;
				oldChild.Parent = null;
				newChild.Parent = this;
			}
			else
				AddChild(newChild);
		}

		// Replace this box with another one.
		public void ReplaceWith(Mpeg4Box box)
		{
			if (Parent != null)
				Parent.ReplaceChild(this, box);
		}

		// Load this box'field children as well as their children. 
		public void LoadChildren()
		{
			if (!HasChildren || children.Count != 0 || loadChildrenStarted)
				return;

			loadChildrenStarted = true;

			Mpeg4Box box = FirstChild;
			while (box != null)
			{
				box.LoadChildren();
				children.Add(box);
				box = NextChild(box);
			}
		}

		// Load the data stored in this box.
		public void LoadData()
		{
			if (data == null && this.File != null && this.File.Mode != FileAccessMode.Closed)
			{
				File.Seek(DataPosition);
				data = File.ReadBlock((int)DataSize);
			}
		}
		
		/// <summary>
		/// The 'safe' LoadData that can be called from constructors without accessing virtual properties
		/// </summary>
		/// <param name="dataPosition">The position in the data to seek to</param>
		/// <param name="dataSize">The size of the block of data to load</param>
		[CLSCompliant(false)]
		public void LoadData(long dataPosition, int dataSize)
		{
			if (data == null && this.File != null && this.File.Mode != FileAccessMode.Closed)
			{
				File.Seek(dataPosition);
				data = File.ReadBlock(dataSize);
			}			
		}
		#endregion
		
		#region Public Static Methods
		// Create a box by reading the file and add it to "parent".
		public static Mpeg4Box Create(Mpeg4File file, long position, Mpeg4Box parent)
		{
			// Read the box header.
			Mpeg4BoxHeader header = new Mpeg4BoxHeader(file, position);

			// If we're not even valid, quit.
			if (!header.IsValid)
				return null;

			// IF we're in a SampleDescriptionBox and haven'type loaded all the
			// entries, try loading an appropriate entry.
			if (parent.BoxType == "stsd" && parent.Children.Count < ((Mpeg4IsoSampleDescriptionBox)parent).EntryCount)
			{
				Mpeg4IsoHandlerBox handler = parent.Handler;
				if (handler != null && handler.HandlerType == "soun")
					return new Mpeg4IsoAudioSampleEntry(header, parent);
				else
					return new Mpeg4IsoSampleEntry(header, parent);
			}

			//
			// A bunch of standard items.
			//

			if (header.BoxType == "moov")
				return new Mpeg4IsoMovieBox(header, parent);

			if (header.BoxType == "mvhd")
				return new Mpeg4IsoMovieHeaderBox(header, parent);

			if (header.BoxType == "mdia")
				return new Mpeg4IsoMediaBox(header, parent);

			if (header.BoxType == "minf")
				return new Mpeg4IsoMediaInformationBox(header, parent);

			if (header.BoxType == "stbl")
				return new Mpeg4IsoSampleTableBox(header, parent);

			if (header.BoxType == "stsd")
				return new Mpeg4IsoSampleDescriptionBox(header, parent);

			if (header.BoxType == "stco")
				return new Mpeg4IsoChunkOffsetBox(header, parent);

			if (header.BoxType == "co64")
				return new Mpeg4IsoChunkLargeOffsetBox(header, parent);

			if (header.BoxType == "trak")
				return new Mpeg4IsoTrackBox(header, parent);

			if (header.BoxType == "hdlr")
				return new Mpeg4IsoHandlerBox(header, parent);

			if (header.BoxType == "udta")
				return new Mpeg4IsoUserDataBox(header, parent);

			if (header.BoxType == "meta")
				return new Mpeg4IsoMetaBox(header, parent);

			if (header.BoxType == "ilst")
				return new Mpeg4AppleItemListBox(header, parent);

			if (header.BoxType == "data")
				return new Mpeg4AppleDataBox(header, parent);

			if (header.BoxType == "esds")
				return new Mpeg4AppleElementaryStreamDescriptor(header, parent);

			if (header.BoxType == "free" || header.BoxType == "skip")
				return new Mpeg4IsoFreeSpaceBox(header, parent);

			if (header.BoxType == "mean" || header.BoxType == "name")
				return new Mpeg4AppleAdditionalInfoBox(header, parent);

			// If we still don'type have a tag, and we're inside an ItemLisBox, load
			// lthe box as an AnnotationBox (Apple tag item).
			if (parent.GetType() == typeof(Mpeg4AppleItemListBox))
				return new Mpeg4AppleAnnotationBox(header, parent);

			// Nothing good. Go generic.
			return new Mpeg4UnknownBox(header, parent);
		}		
		#endregion
	}
}
