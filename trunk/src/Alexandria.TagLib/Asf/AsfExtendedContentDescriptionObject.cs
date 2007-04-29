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
	public class AsfExtendedContentDescriptionObject : AsfObject
	{
		#region Constructors
		public AsfExtendedContentDescriptionObject(AsfFile file, long position) : base(file, position)
		{
			if (file != null)
			{
				if (!Guid.Equals(AsfGuid.AsfExtendedContentDescriptionObject))
					throw new TagLibException(TagLibError.AsfObjectGuidIncorrect);

				if (OriginalSize < 26)
					throw new TagLibException(TagLibError.AsfObjectSizeTooSmall);

				short count = file.ReadWord();

				for (short i = 0; i < count; i++)
				{
					AsfContentDescriptor descriptor = new AsfContentDescriptor(file);
					descriptors.Add(descriptor);
				}
			}
		}

		public AsfExtendedContentDescriptionObject() : base(AsfGuid.AsfExtendedContentDescriptionObject)
		{
		}
		#endregion
		
		#region Private Fields
		private List<AsfContentDescriptor> descriptors = new List<AsfContentDescriptor>();
		#endregion
		
		#region Public Properties
		public IList<AsfContentDescriptor> Descriptors
		{
			get { return descriptors; }
		}
		#endregion
		
		#region Public Methods
		public override ByteVector Render()
		{
			ByteVector output = new ByteVector();
			short count = 0;

			foreach (AsfContentDescriptor descriptor in descriptors)
			{
				count++;
				output += descriptor.Render();
			}

			return Render(RenderWord(count) + output);
		}

		public void RemoveDescriptors(string name)
		{
			for (int i = descriptors.Count - 1; i >= 0; i--)
				if (name == descriptors[i].Name)
					descriptors.RemoveAt(i);
		}

		public IList<AsfContentDescriptor> GetDescriptorsByName(string name)
		{
			List<AsfContentDescriptor> list = new List<AsfContentDescriptor>();

			foreach (AsfContentDescriptor descriptor in descriptors)
				if (descriptor.Name == name)
					list.Add(descriptor);

			return list;
		}

		//public void AddDescriptor(AsfContentDescriptor descriptor)
		//{
			//descriptors.Add(descriptor);
		//}

		public void SetDescriptorsByName(string name, params AsfContentDescriptor[] descriptors)
		{
			int i;
			for (i = 0; i < this.descriptors.Count; i++)
				if (name == ((AsfContentDescriptor)this.descriptors[i]).Name)
					break;

			RemoveDescriptors(name);

			this.descriptors.InsertRange(i, descriptors);
		}
		#endregion
	}
}
