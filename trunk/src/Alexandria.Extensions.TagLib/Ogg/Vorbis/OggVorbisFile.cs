/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : oggfile.cpp from TagLib
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
using System.Collections;

namespace Alexandria.TagLib
{
   [SupportedMimeTypeAttribute("taglib/ogg")]
   [SupportedMimeTypeAttribute("application/ogg")]
   [SupportedMimeTypeAttribute("application/x-ogg")]
   [SupportedMimeTypeAttribute("audio/vorbis")]
   [SupportedMimeTypeAttribute("audio/x-vorbis")]
   [SupportedMimeTypeAttribute("audio/x-vorbis+ogg")]
   [SupportedMimeTypeAttribute("audio/ogg")]
   [SupportedMimeTypeAttribute("audio/x-ogg")]
	public class OggVorbisFile : OggFile
   {
      //////////////////////////////////////////////////////////////////////////
      // private properties
      //////////////////////////////////////////////////////////////////////////
		private OggXiphComment comment;
		private OggVorbisProperties properties;
      
		private static byte [] vorbis_comment_header_id = {0x03, (byte)'v', (byte)'o', (byte)'r', (byte)'b', (byte)'i', (byte)'s'};
      
      
      //////////////////////////////////////////////////////////////////////////
      // public methods
      //////////////////////////////////////////////////////////////////////////
		public OggVorbisFile (string file, ReadStyle properties_style) : base (file)
		{
			//comment = null;
			//properties = null;
         
			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				return;
			}
         
			Read(properties_style);
         
			Mode = FileAccessMode.Closed;
		}
      
		public OggVorbisFile(string file) : this(file, ReadStyle.Average)
		{
		}
      
		public override void Save()
		{
			ClearPageData(); // Force re-reading of the file.

			ByteVector v = vorbis_comment_header_id;

			FindTag(TagTypes.Xiph, true);
            
			v.Add(comment.Render());

			SetPacket(1, v);

			base.Save();
		}
      
		public override TagLib.Tag FindTag(TagTypes type, bool create)
		{
			if (type == TagTypes.Xiph)
			{
				if (comment == null && create)
					comment = new OggXiphComment();
            
				return comment;
			}         
			else return null;
		}
      
      
      //////////////////////////////////////////////////////////////////////////
      // public properties
      //////////////////////////////////////////////////////////////////////////
      public override Tag Tag {get {return FindTag (TagTypes.Xiph, true);}}
      
      public override AudioProperties AudioProperties {get {return properties;}}      
      
      //////////////////////////////////////////////////////////////////////////
      // private methods
      //////////////////////////////////////////////////////////////////////////
      private void Read (ReadStyle propertiesStyle)
      {
         ByteVector comment_header_data = GetPacket (1);

         if (comment_header_data.Mid (0, 7) != vorbis_comment_header_id)
         {
            TagLibDebugger.Debug ("Vorbis.File.Read() - Could not find the Vorbis comment header.");
            SetValid (false);
            return;
         }

         comment = new OggXiphComment(comment_header_data.Mid(7));

         if(propertiesStyle != ReadStyle.None)
            properties = new OggVorbisProperties(this, propertiesStyle);
      }
   }
}
