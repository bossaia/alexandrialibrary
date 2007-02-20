using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	[Serializable]
	public sealed class TagLibException : AlexandriaException, ISerializable
	{
		#region Private Fields
		private TagLibError error;
		#endregion
		
		#region Constructors
		public TagLibException(TagLibError error) : this()
		{
			this.error = error;
		}

		public TagLibException(TagLibError error, string message, Exception innerException) : this(message, innerException)
		{
			this.error = error;
		}

		public TagLibException(string message) : base(Subsystem.TagEngine, message)
		{
		}

		public TagLibException(Exception innerException) : base(Subsystem.TagEngine, innerException)
		{
		}

		public TagLibException(string message, Exception innerException) : base(Subsystem.TagEngine, message, innerException)
		{
		}

		public TagLibException() : base(Subsystem.TagEngine)
		{
		}
		
		/// <summary>
		/// An exception in the Alexandria TagLib library
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination</param>
		private TagLibException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Subsystem = Subsystem.TagEngine;
		}
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
