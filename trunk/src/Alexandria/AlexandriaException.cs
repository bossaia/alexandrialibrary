using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace AlexandriaOrg.Alexandria
{	
	[Serializable]
	public class AlexandriaException : Exception, ISerializable
	{
		#region Private Fields
		private Subsystem subsystem = Subsystem.Unknown;
		#endregion
		
		#region Constructors
		public AlexandriaException() : base()
		{
		}
		
		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="subsystem">The subsystem of Alexandria where this exception occured</param>
		public AlexandriaException(Subsystem subsystem) : base()
		{
			this.subsystem = subsystem;
		}

		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="subsystem">The subsystem of Alexandria where this exception occured</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified</param>
		public AlexandriaException(Subsystem subsystem, Exception innerException) : base(null, innerException)
		{
			this.subsystem = subsystem;
		}
		
		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="subsystem">The subsystem of Alexandria where this exception occured</param>
		/// <param name="message">The message that describes this error</param>
		public AlexandriaException(Subsystem subsystem, string message) : base(message)
		{
			this.subsystem = subsystem;
		}
		
		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="subsystem">The subsystem of Alexandria where this exception occured</param>
		/// <param name="message">The message that describes the error</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified</param>
		public AlexandriaException(Subsystem subsystem, string message, Exception innerException) : base(message, innerException)
		{
			this.subsystem = subsystem;
		}
		
		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		public AlexandriaException(string message) : base(message)
		{
		}

		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="message">The message that describes the error</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified</param>
		public AlexandriaException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		/// <summary>
		/// An exception in the Alexandria system
		/// </summary>
		/// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown</param>
		/// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination</param>
		protected AlexandriaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the sub-system of Alexandria where this exception occured
		/// </summary>
		public Subsystem Subsystem
		{
			get {return subsystem;}
			protected set {subsystem = value;}
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
