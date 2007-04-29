/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

namespace Alexandria.TagLib
{
	public abstract class AsfObject
	{
		#region Constructors
		protected AsfObject(AsfFile file, long position)
		{
			if (file != null)
			{
				file.Seek(position);
				id = file.ReadGuid();
				size = file.ReadQWord();
			}
		}

		protected AsfObject(AsfGuid guid)
		{
			id = guid;
			//size = 0;
		}
		#endregion
   
		#region Private Fields
		private AsfGuid id;
		private long size;
		#endregion
      
		#region Protected Methods
		protected ByteVector Render(ByteVector data)
		{
			if (data == null)
				data = new ByteVector();
			return Guid.Render() + RenderQWord(data.Count + 24) + data;
		}

		#endregion
		
		#region Public Properties
		public AsfGuid Guid
		{
			get {return id;}
		}

		public long OriginalSize
		{
			get {return size;}
		}
		#endregion
		
		#region Public Methods
		public abstract ByteVector Render();

		public static ByteVector RenderUnicode(string value)
		{
			return ByteVector.FromString(value, StringType.UTF16LE) + ByteVector.FromShort(0);
		}

		[System.CLSCompliant(false)]
		public static ByteVector RenderDWord(uint value)
		{
			return ByteVector.FromUInt(value, false);
		}

		public static ByteVector RenderQWord(long value)
		{
			return ByteVector.FromLong(value, false);
		}

		public static ByteVector RenderWord(short value)
		{
			return ByteVector.FromShort(value, false);
		}
		#endregion
	}
}
