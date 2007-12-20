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
   public class AsfPaddingObject : AsfObject
   {
      //////////////////////////////////////////////////////////////////////////
      // private properties
      //////////////////////////////////////////////////////////////////////////
      private long size;
      
      
      //////////////////////////////////////////////////////////////////////////
      // public methods
      //////////////////////////////////////////////////////////////////////////
      public AsfPaddingObject(AsfFile file, long position) : base (file, position)
      {
         if (!Guid.Equals(AsfGuid.AsfPaddingObject))
			throw new TagLibException(TagLibError.AsfObjectGuidIncorrect);
         
         if (OriginalSize < 24)
			throw new TagLibException(TagLibError.AsfObjectSizeTooSmall);
         
         size = OriginalSize;
      }

	   [System.CLSCompliant(false)]
      public AsfPaddingObject (uint size) : base (AsfGuid.AsfPaddingObject)
      {
         this.size = size;
      }
      
      public override ByteVector Render ()
      {
         return Render (new ByteVector ((int) (size - 24)));
      }
      
      //////////////////////////////////////////////////////////////////////////
      // public properties
      //////////////////////////////////////////////////////////////////////////
      public long Size {get {return size;} set {size = value;}}
   }
}
