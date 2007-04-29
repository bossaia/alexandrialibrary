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

using System;
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public class AsfHeaderObject : AsfObject
	{
		#region Constructors
		public AsfHeaderObject(AsfFile file, long position) : base(file, position)
		{
			if (!Guid.Equals(AsfGuid.AsfHeaderObject))
				throw new TagLibException(TagLibError.AsfObjectGuidIncorrect);
			
			uint childCount = file.ReadDWord();
			
			children = new List<AsfObject>((int)childCount);

			reserved = file.ReadBlock(2);
			foreach(AsfObject childObject in file.ReadObjects(childCount, file.Tell))
				children.Add(childObject);
			
			//children.AddRange(file.ReadObjects(childCount, file.Tell));
		}
		#endregion
		
		#region Private Fields
		private ByteVector reserved;
		private IList<AsfObject> children;
		#endregion
		
		#region Public Properties
		public IList<byte> Reserved
		{
			get { return reserved.Data; }
		}

		public IList<AsfObject> Children
		{
			get { return children; }
		}
		#endregion
		
		#region Public Methods
		public override ByteVector Render()
		{
			ByteVector output = new ByteVector();
			uint childCount = 0;
			foreach (AsfObject child in children)
				if (child.Guid != AsfGuid.AsfPaddingObject)
				{
					output += child.Render();
					childCount++;
				}

			int sizeDifference = (int)(output.Count + 30 - OriginalSize);

			if (sizeDifference != 0)
			{
				AsfPaddingObject obj = new AsfPaddingObject((uint)(sizeDifference > 0 ? 4096 : -sizeDifference));
				output += obj.Render();
				childCount++;
			}

			return Render(RenderDWord(childCount) + reserved + output);
		}

		public void AddObject(AsfObject obj)
		{
			children.Add(obj);
		}

		public void AddUniqueObject(AsfObject obj)
		{
			for (int i = 0; i < children.Count; i++)
				if (((AsfObject)children[i]).Guid == obj.Guid)
				{
					children[i] = obj;
					return;
				}

			children.Add(obj);
		}		
		#endregion
	}
}
