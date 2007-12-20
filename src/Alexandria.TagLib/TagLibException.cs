#region License (LGPL)
/***************************************************************************
    copyright            : (C) 2007 by Dan Poage
    email                : dan.poage@gmail.com
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
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Alexandria.TagLib
{
	[Serializable]
	public sealed class TagLibException : ApplicationException, ISerializable
	{
		#region Constructors
		public TagLibException(TagLibError error)
			: this()
		{
			this.error = error;
		}

		public TagLibException(TagLibError error, string message, Exception innerException)
			: this(message, innerException)
		{
			this.error = error;
		}

		public TagLibException(string message)
			: base(message)
		{
		}

		public TagLibException(Exception innerException)
			: base(string.Empty, innerException)
		{
		}

		public TagLibException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public TagLibException()
			: base()
		{
		}

		/// <summary>
		/// An exception in the Alexandria TagLib library
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination</param>
		private TagLibException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
		#endregion
		
		#region Private Fields
		private TagLibError error;
		#endregion
				
		#region Public Properties
		public TagLibError Error
		{
			get {return error;}
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
