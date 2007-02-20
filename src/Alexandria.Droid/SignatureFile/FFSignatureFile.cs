using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid.SignatureFile
{
	public class FFSignatureFile : SimpleElement
	{
		String version = "";
		String dateCreated = "";
		FileFormatCollection FFcollection;
		InternalSignatureCollection intSigs;

		/* setters */
		public void setFileFormatCollection(FileFormatCollection coll) { this.FFcollection = coll; }
		public void setInternalSignatureCollection(InternalSignatureCollection col3) { this.intSigs = col3; }
		private void setVersion(String vers) { this.version = vers; }
		private void setDateCreated(String dc) { this.dateCreated = dc; }
		public void setAttributeValue(String name, String value)
		{
			if (name.Equals("Version"))
			{
				setVersion(value);
			}
			else if (name.Equals("DateCreated"))
			{
				setDateCreated(value);
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}

		/* getters */
		public int getNumInternalSignatures() { return this.intSigs.getInternalSignatures().Count; }
		public InternalSignature getInternalSignature(int theIndex)
		{
			return (InternalSignature)intSigs.getInternalSignatures()[theIndex];
		}
		public int getNumFileFormats() { return this.FFcollection.getFileFormats().Count; }
		public FileFormat getFileFormat(int theIndex) { return (FileFormat)FFcollection.getFileFormats()[theIndex]; }
		public String getVersion() { return version; }
		public String getDateCreated() { return dateCreated; }


		/**
		 * This method must be run after the signature file data has been read
		 * and before the FFSignatureFile class is used.
		 * It points internal signatures to the fileFormat objects they identify,
		 * and it ensures that the sequence fragments are in the correct order.
		 */
		public void prepareForUse()
		{
			this.setAllSignatureFileFormats();
			this.reorderAllSequenceFragments();
		}


		/**
		 * Points all internal signatures to the fileFormat objects they identify.
		 *
		 */
		private void setAllSignatureFileFormats()
		{
			for (int iFormat = 0; iFormat < this.getNumFileFormats(); iFormat++)
			{  //loop through file formats
				for (int iFileSig = 0; iFileSig < this.getFileFormat(iFormat).GetNumberOfInternalSignatures(); iFileSig++)
				{  //loop through internal signatures for each file format
					int iFileSigID = this.getFileFormat(iFormat).GetInternalSignatureId(iFileSig);
					//loop through all internal signatures to find one with a matching ID
					for (int iIntSig = 0; iIntSig < this.getNumInternalSignatures(); iIntSig++)
					{
						if (this.getInternalSignature(iIntSig).getID() == iFileSigID)
						{
							this.getInternalSignature(iIntSig).addFileFormat(this.getFileFormat(iFormat));
							break;
						}
					}
				}
			}
		}

		/**
		 * Run prepareSeqFragments on all subSequences within all ByteSequences within all internalSignatures.
		 *
		 */
		private void reorderAllSequenceFragments()
		{
			for (int iSig = 0; iSig < this.getNumInternalSignatures(); iSig++)
			{
				for (int iBS = 0; iBS < this.getInternalSignature(iSig).getNumByteSequences(); iBS++)
				{
					for (int iSS = 0; iSS < this.getInternalSignature(iSig).getByteSequence(iBS).getNumSubSequences(); iSS++)
					{
						this.getInternalSignature(iSig).getByteSequence(iBS).getSubSequence(iSS).prepareSeqFragments();
					}
				}
			}
		}


		/**
		 * Runs file identification for the binary file specified by targetFile
		 *
		 * @param targetFile   The binary file to be identified
		 */
		public void runFileIdentification(IByteReader targetFile)
		{

			//record all positive identifications
			for (int iSig = 0; iSig < this.getNumInternalSignatures(); iSig++)
			{
				InternalSignature internalSig = this.getInternalSignature(iSig);
				//System.out.println("============================ Running identification for signature ID = "+internalSig.getID()+ " ===========================");

				if (internalSig.isFileCompliant(targetFile))
				{
					//File matches this internal signature
					targetFile.SetPositiveIdentification();
					for (int i = 0; i < internalSig.getNumFileFormats(); i++)
					{
						FileFormatHit fileHit = new FileFormatHit(internalSig.getFileFormat(i), AnalysisController.HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC, internalSig.isSpecific(), "");
						targetFile.AddHit(fileHit);
					}
				}
			}

			//remove any hits for which there is a higher priority hit
			if (targetFile.GetNumberOfHits() > 1)
			{
				this.removeLowerPriorityHits(targetFile);
			}

			//carry out file extension checking
			this.checkExtension(targetFile);

			// if there are still no hits then classify as unidentified
			if (targetFile.GetNumberOfHits() == 0)
			{
				targetFile.SetNoIdentification();
			}
		}

		/**
		 * Remove any hits for which there is a higher priority hit
		 *
		 * @param targetFile   The binary file to be identified
		 */
		private void removeLowerPriorityHits(IByteReader targetFile)
		{
			//loop through specific hits and list any hits which these have priority over
			IList<int> hitsToRemove = new List<int>(); //ArrayList();
			for (int i = 0; i < targetFile.GetNumberOfHits(); i++)
			{
				for (int j = 0; j < targetFile.GetHit(i).GetFileFormat().GetNumberOfHasPriorityOver(); j++) //HasPriorityOver(); j++)
				{
					int formatID = targetFile.GetHit(i).GetFileFormat().GetHasPriorityOver(j); //getHasPriorityOver(j);
					for (int k = 0; k < targetFile.GetNumberOfHits(); k++)
					{ //loop through hits to find any for this file format
						if (targetFile.GetHit(k).GetFileFormat().GetId() == formatID)
						{
							hitsToRemove.Add(k); //Integer.toString(k));  //use string representation as ArrayList won't take integers
							break;
						}
					}
				}
			}
			//Create sorted array of indexes for hits to be removed
			int[] indexesOfHits = new int[hitsToRemove.Count];
			int numHitsToRemove = 0;
			for (int i = 0; i < hitsToRemove.Count; i++)
			{   //loop through unsorted list of hits to be removed
				int j = numHitsToRemove;
				//int indexOfHit =  Integer.parseInt((String)hitsToRemove[i]);
				int indexOfHit = hitsToRemove[i];
				while (j > 0 && indexesOfHits[j - 1] > indexOfHit)
				{
					indexesOfHits[j] = indexesOfHits[j - 1];
					--j;
				}
				indexesOfHits[j] = indexOfHit;
				++numHitsToRemove;
			}
			//Delete hits in decreasing index order, ignorinmg any repetitions
			for (int i = indexesOfHits.Length - 1; i >= 0; i--)
			{
				if (i == (indexesOfHits.Length - 1))
				{
					targetFile.RemoveHit(indexesOfHits[i]);
				}
				else if (indexesOfHits[i] != indexesOfHits[i + 1])
				{
					targetFile.RemoveHit(indexesOfHits[i]);
				}
			}

		}

		/**
		 * Determines the file extension
		 * If the file has got some positive hits, then check these against this extension
		 * If the file has not got any positive hits, then look for tentative hits
		 * based on the extension only.
		 *
		 * @param targetFile   The binary file to be identified
		 */
		private void checkExtension(IByteReader targetFile)
		{

			//work out if file has an extension
			bool hasExtension = true;
			int dotPos = targetFile.GetFileName().LastIndexOf(".");
			if (dotPos < 0)
			{
				hasExtension = false;
			}
			else if (dotPos == targetFile.GetFileName().Length - 1)
			{
				hasExtension = false;
			}
			else if (targetFile.GetFileName().LastIndexOf("/") > dotPos)
			{
				hasExtension = false;
			}
			else if (targetFile.GetFileName().LastIndexOf("\\") > dotPos)
			{
				hasExtension = false;
			}

			//
			if (hasExtension)
			{
				String fileExtension = targetFile.GetFileName().Substring(dotPos + 1);

				if (targetFile.GetNumberOfHits() > 0)
				{

					//for each file format which is a hit, check that it expects the given extension - if not give a warning
					for (int iHit = 0; iHit < targetFile.GetNumberOfHits(); iHit++)
					{
						if (!(targetFile.GetHit(iHit).GetFileFormat().HasMatchingExtension(fileExtension)))
						{
							targetFile.GetHit(iHit).SetIdentificationWarning(MessageDisplay.FILEEXTENSIONWARNING);
						}
					}//loop through hits

				}
				else
				{
					//no positive hits have been found, so search for tenative hits
					//loop through all file formats with no internal signature
					for (int iFormat = 0; iFormat < this.getNumFileFormats(); iFormat++)
					{
						if (this.getFileFormat(iFormat).GetNumberOfInternalSignatures() == 0)
						{
							if (this.getFileFormat(iFormat).HasMatchingExtension(fileExtension))
							{
								//add this as a tentative hit
								FileFormatHit fileHit = new FileFormatHit(this.getFileFormat(iFormat), AnalysisController.HIT_TYPE_TENTATIVE, false, "");
								targetFile.AddHit(fileHit);
								targetFile.SetTentativeIdentification();
							}
						}
					}//loop through file formats
				}
			}//end of if(hasExtension)
			else
			{
				//if the file does not have an extension then add warning to all its hits
				for (int iHit = 0; iHit < targetFile.GetNumberOfHits(); iHit++)
				{
					targetFile.GetHit(iHit).SetIdentificationWarning(MessageDisplay.FILEEXTENSIONWARNING);
				}
			}
		}
	}
}
