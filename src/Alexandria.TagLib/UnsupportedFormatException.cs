/***************************************************************************
    copyright            : (C) 2005-2006 Novell, Inc.
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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AlexandriaOrg.Alexandria.TagLib
{
	[Serializable]
    public class UnsupportedFormatException : Exception, ISerializable 
    {
		#region Constructors
        public UnsupportedFormatException(string message) : base(message) 
        {
        }
        
        public UnsupportedFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        public UnsupportedFormatException() : base() 
        {
        }
        
   		protected UnsupportedFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
		#endregion

		#region ISerializable Members
		/// <summary>
		/// Get object data for serialization of this exception
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination</param>
		[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		#endregion
    }
}
