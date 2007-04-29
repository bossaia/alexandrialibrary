/***************************************************************************
    copyright            : (C) 2006 Novell, Inc.
    email                : abockover@novell.com
    based on             : Entagged#
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

namespace Alexandria.TagLib
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public sealed class SupportedMimeTypeAttribute : Attribute 
    {
		#region Constructors
		public SupportedMimeTypeAttribute(string mimeType)
		{
			this.mimeType = mimeType;
		}
		#endregion
		
		#region Private Fields
        private string mimeType;
        #endregion
    
		#region Public Properties
        public string MimeType
        {
            get {return mimeType;}
        }
        #endregion
    }
}
