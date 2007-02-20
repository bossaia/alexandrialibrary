using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid.SignatureFile
{
	public class InternalSignature : SimpleElement
	{
		IList<ByteSequence> ByteSequences = new List<ByteSequence>(); //ArrayList();
		int intSigID;
		string Specificity;
		IList<FileFormat> FileFormatList = new List<FileFormat>(); //ArrayList();

		/* setters */
		public void addByteSequence(ByteSequence bs) { ByteSequences.Add(bs); }
		public void setByteSequences(IList<ByteSequence> ByteSequences) { this.ByteSequences = ByteSequences; }
		public void addFileFormat(FileFormat theFileFormat) { FileFormatList.Add(theFileFormat); }
		public void setID(string theIntSigID)
		{
			this.intSigID = Convert.ToInt32(theIntSigID); //Integer.parseInt(theIntSigID);
		}
		public void setSpecificity(string Specificity) { this.Specificity = Specificity; }
		public void setAttributeValue(string name, String value)
		{
			if (name.Equals("ID"))
			{
				setID(value);
			}
			else if (name.Equals("Specificity"))
			{
				setSpecificity(value);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}

		/* getters */
		public IList<ByteSequence> getByteSequences() { return ByteSequences; }
		public int getNumFileFormats() { return FileFormatList.Count; }
		public FileFormat getFileFormat(int theIndex) { return (FileFormat)FileFormatList[theIndex]; } //.get(theIndex); }
		public ByteSequence getByteSequence(int theByteSeq) { return (ByteSequence)this.getByteSequences()[theByteSeq]; } //.get(theByteSeq); }
		public int getNumByteSequences() { return this.ByteSequences.Count; } //.size(); }
		public int getID() { return intSigID; }
		public string getSpecificity() { return Specificity; }
		public bool isSpecific()
		{
			return (string.Compare(Specificity, "specific", true) == 0); //Specificity.equalsIgnoreCase("specific");
		}


		/**
		 * Indicates whether the file is compliant with this internal signature
		 *
		 * @param targetFile   the binary file to be identified
		 */
		public bool isFileCompliant(IByteReader targetFile)
		{
			//initialise variable
			bool isCompliant = true;
			//check each byte sequence in turn - stop as soon as one is found to be non-compliant
			for (int i = 0; (i < this.ByteSequences.Count) & isCompliant; i++)  //.size()) & isCompliant; i++)
			{
				isCompliant = this.getByteSequence(i).isFileCompliant(targetFile);
			}
			return isCompliant;
		}


		public override string ToString() { return intSigID + "(" + Specificity + ")" + ByteSequences; }
	}
}
