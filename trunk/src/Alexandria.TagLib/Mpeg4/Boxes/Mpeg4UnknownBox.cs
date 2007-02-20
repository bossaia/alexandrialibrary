using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	/// <summary>
	/// This class is just used for clarity in saying, YES, this box IS unknown.
	/// </summary>
	public class Mpeg4UnknownBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4UnknownBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}
		#endregion
	}
}