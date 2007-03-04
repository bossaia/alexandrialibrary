using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Droid.BinaryReader;
using Alexandria.Droid.XmlReader;

namespace Alexandria.Droid.SignatureFile
{
	public class ByteSequence : SimpleElement
	{
		IList<SubSequence> SubSequences = new List<SubSequence>(); //ArrayList();
		string Reference = string.Empty;
		bool bigEndian = true;               // Assume a signature is big-endian unless we are told to the contrary.
		//int MaxOffset = 0;

		/* setters */
		public void addSubSequence(SubSequence sseq) { SubSequences.Add(sseq); }
		public void setSubSequences(IList<SubSequence> SubSequences) { this.SubSequences = SubSequences; }
		public void setReference(string theRef) { this.Reference = theRef; }
		public void setEndianness(string endianness) { this.bigEndian = !endianness.Equals("Little-endian"); }
		//public void setMaxOffset( String theMaxOffset ) { this.MaxOffset = Integer.parseInt(theMaxOffset); }
		public void setAttributeValue(string name, string value)
		{
			if (name.Equals("Reference"))
			{
				setReference(value);
			}
			else if (name.Equals("Endianness"))
			{
				setEndianness(value);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}

		/* getters */
		public IList<SubSequence> getSubSequences() { return SubSequences; }
		public int getNumSubSequences() { return SubSequences.Count; }
		public SubSequence getSubSequence(int theIndex) { return SubSequences[theIndex]; }
		public String getReference() { return Reference; }
		//public int getMaxOffset() { return MaxOffset; }



		/**
		 * checks whether the binary file specified by targetFile is compliant
		 * with this byte sequence
		 *
		 * @param targetFile   The binary file to be identified
		 */
		public bool isFileCompliant(IByteReader targetFile)
		{
			//System.out.println("Looking at new byte sequence with reference "+Reference);
			//initialise variables and start with the file marker at the beginning of the file
			bool isCompliant = true;
			bool reverseOrder = (string.Compare(getReference(), "EOFoffset", true) == 0) ? true : false; //(getReference().equalsIgnoreCase("EOFoffset")) ? true : false;
			int ssLoopStart = reverseOrder ? getNumSubSequences() - 1 : 0;
			int ssLoopEnd = reverseOrder ? -1 : getNumSubSequences();
			int searchDirection = reverseOrder ? -1 : 1;
			if (reverseOrder)
			{
				targetFile.SetFileMarker(targetFile.GetNumberOfBytes() - 1L);
			}
			else
			{
				targetFile.SetFileMarker(0L);
			}

			//check whether each subsequence in turn is compliant
			for (int iSS = ssLoopStart; (searchDirection * iSS < searchDirection * ssLoopEnd) & isCompliant; iSS += searchDirection)
			{
				bool isFixedStart = ((string.Compare(getReference(), "EOFoffset", true) == 0) || (string.Compare(getReference(), "BOFoffset", true) == 0)) ? true : false; //getReference().equalsIgnoreCase("EOFoffset") || getReference().equalsIgnoreCase("BOFoffset");
				if ((iSS == ssLoopStart) && (isFixedStart))
				{
					isCompliant = getSubSequence(iSS).isFoundAtStartOfFile(targetFile, reverseOrder, bigEndian); //, MaxOffset);
				}
				else
				{
					isCompliant = getSubSequence(iSS).isFoundAfterFileMarker(targetFile, reverseOrder, bigEndian);
				}
			}
			return isCompliant;
		}


		public String toString() { return Reference + "{" + SubSequences + "}"; }
	}
}
