using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Droid.XmlReader;

namespace Alexandria.Droid.SignatureFile
{
	public class InternalSignatureCollection : SimpleElement
	{
		#region Private Fields
		IList<InternalSignature> intSigs = new List<InternalSignature>(); //ArrayList();
		#endregion

		#region Public Methods
		/* setters */
		public void addInternalSignature(InternalSignature iSig) { intSigs.Add(iSig); }
		public void setInternalSignatures(IList<InternalSignature> iSigs) { this.intSigs = iSigs; }

		/* getters */
		public IList<InternalSignature> getInternalSignatures() { return intSigs; }
		#endregion
	}
}
