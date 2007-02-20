using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid.SignatureFile
{
	public class SubSequence : SimpleElement
	{
		#region Private Fields
		int Position;
		int minSeqOffset = 0;
		int maxSeqOffset = 0;
		int minFragLength;
		string Sequence;
		
		//        ShiftFunction shift;
		long[] ShiftFunction = new long[256];
		IList<LeftFragment> LeftFragments = new List<LeftFragment>(); //ArrayList();
		IList<RightFragment> RightFragments = new List<RightFragment>(); //ArrayList();
		byte[] byteSequence;
		IList<LeftFragment> orderedLeftFragments = new List<LeftFragment>(); //ArrayList();
		IList<RightFragment> orderedRightFragments = new List<RightFragment>(); //ArrayList();
		#endregion
		
		#region Private Static Fields
		static bool showProgress = false;
		#endregion

		#region Public Method
		/* setters */
		public void addLeftFragment(LeftFragment lf) { LeftFragments.Add(lf); }
		public void addRightFragment(RightFragment lf) { RightFragments.Add(lf); }
		public void setPosition(int Position) { this.Position = Position; }
		public void setShift(Shift theShift)
		{
			int theShiftByte = theShift.getShiftByte();
			if (theShiftByte >= 0 && theShiftByte < 256)
			{
				this.ShiftFunction[theShiftByte] = theShift.getShiftValue();
			}
		}
		public void setDefaultShift(String theValue)
		{
			for (int i = 0; i < 256; i++)
			{
				this.ShiftFunction[i] = Convert.ToInt64(theValue); //Long.parseLong(theValue);
			}
		}
		public void setSequence(String seq) {
        this.Sequence = seq;
        int seqLength = seq.Length/2;
        if(2*seqLength != seq.Length) {
            System.Console.WriteLine("A problem - sequence of odd length was found: "+seq);
        }
        byteSequence = new byte[seqLength];
        for(int i=0; i<seqLength; i++) {
            int byteVal = Convert.ToInt32(seq.Substring(2*i, 2*(i+1)), 16); //Integer.parseInt(seq.substring(2*i, 2*(i+1)),16);
			byteSequence[i] = (byteVal > byte.MaxValue) ? (byte)(byteVal - 256) : (byte)byteVal;
            //Byte.MAX_VALUE)?(byte)(byteVal-256):(byte)byteVal;
        }
    }
		public void setMinSeqOffset(int theOffset)
		{
			this.minSeqOffset = theOffset;
			if (this.maxSeqOffset < this.minSeqOffset)
			{
				this.maxSeqOffset = this.minSeqOffset;
			}
		}
		public void setMaxSeqOffset(int theOffset)
		{
			this.maxSeqOffset = theOffset;
			if (this.maxSeqOffset < this.minSeqOffset)
			{
				this.maxSeqOffset = this.minSeqOffset;
			}
		}
		public void setMinFragLength(int theLength) { this.minFragLength = theLength; }
		public void setAttributeValue(string name, string value)
		{
			if (name.Equals("Position"))
			{
				setPosition(Convert.ToInt32(value)); //Integer.parseInt(value));
			}
			else if (name.Equals("SubSeqMinOffset"))
			{
				setMinSeqOffset(Convert.ToInt32(value)); //Integer.parseInt(value));
			}
			else if (name.Equals("SubSeqMaxOffset"))
			{
				setMaxSeqOffset(Convert.ToInt32(value)); //Integer.parseInt(value));
			}
			else if (name.Equals("MinFragLength"))
			{
				setMinFragLength(Convert.ToInt32(value)); //Integer.parseInt(value));
			}
			else
			{
				MessageDisplay.UnknownAttributeWarning(name, this.GetElementName());
			}
		}

		/* getters */
		public int getNumFragmentPositions(bool leftFrag)
		{
			if (leftFrag)
			{
				return this.orderedLeftFragments.Count; //.size();
			}
			else
			{
				return this.orderedRightFragments.Count; //.size();
			}
		}
		
		public int getNumAlternativeFragments(bool leftFrag, int thePosition)
		{
			if (leftFrag)
			{
				return (this.orderedLeftFragments.Count - (thePosition - 1)); //this.orderedLeftFragments. //[thePosition - 1]. //.get(thePosition - 1)).size();
			}
			else
			{
				return (this.orderedRightFragments.Count - (thePosition - 1)); //((ArrayList)this.orderedRightFragments.get(thePosition - 1)).size();
			}
		}
		
		public SideFragment getFragment(bool leftFrag, int thePosition, int theIndex)
		{
			if (leftFrag)
			{
				return this.orderedLeftFragments[thePosition - 1]; //(sideFragment)((ArrayList)this.orderedLeftFragments.get(thePosition - 1)).get(theIndex);
			}
			else
			{
				return this.orderedRightFragments[thePosition - 1]; //(sideFragment)((ArrayList)this.orderedRightFragments.get(thePosition - 1)).get(theIndex);
			}
		}
		public long getShift(byte theByteValue)
		{
			int inValue = (int)theByteValue;
			if (inValue >= 0)
			{
				return this.ShiftFunction[inValue];
			}
			else
			{
				return this.ShiftFunction[inValue + 256];
			}
		}
		public String getSequence() { return Sequence; }
		public byte getByte(int theIndex) { return byteSequence[theIndex]; }
		public int getNumBytes() { return byteSequence.Length; }
		public IList<LeftFragment> getLeftFragments() { return LeftFragments; }
		public IList<RightFragment> getRightFragments() { return RightFragments; }
		public LeftFragment getRawLeftFragment(int theIndex) { return LeftFragments[theIndex]; }
		public RightFragment getRawRightFragment(int theIndex) { return RightFragments[theIndex]; }
		public int getPosition() { return Position; }
		public int getMinSeqOffset() { return minSeqOffset; }
		public int getMaxSeqOffset() { return maxSeqOffset; }
		public int getMinFragLength() { return minFragLength; }



		/**
		 * Re-orders the left and right sequence fragments in increasing position order
		 * this method must be after the signature file has been parsed and
		 * before running any file identifications
		 *
		 */
		public void prepareSeqFragments()
		{

			/* Left fragments */
			//Determine the number of fragment subsequences there are
			int numFrags = 0;
			for (int i = 0; i < LeftFragments.Count; i++)
			{
				int currentPosition = this.getRawLeftFragment(i).getPosition();
				if (currentPosition > numFrags) { numFrags = currentPosition; }
			}

			#region OLD CODE
			/*
			//initialise all necessary fragment lists (one for each position)
			for (int i = 0; i < numFrags; i++)
			{ //loop through fragment positions
				List alternativeFragments = new ArrayList();
				orderedLeftFragments.add(alternativeFragments);
			}

			//Add fragments to new structure
			for (int i = 0; i < LeftFragments.size(); i++)
			{  //loop through all fragments
				int currentPosition = this.getRawLeftFragment(i).getPosition();
				((ArrayList)orderedLeftFragments.get(currentPosition - 1)).add(this.getRawLeftFragment(i));
			}
			*/
			#endregion
			
			#region NEW CODE
			for (int i = 0; i < numFrags; i++)
			{
				orderedLeftFragments.Add(null);
			}
			
			for (int i = 0; i < LeftFragments.Count + numFrags; i++)
			{
				int currentPosition = this.getRawLeftFragment(i).getPosition();
				orderedLeftFragments[currentPosition - 1] = this.getRawLeftFragment(i);
			}
			#endregion

			//clear out unecessary info
			this.LeftFragments = null;

			/* Right fragments */
			//Determine the number of fragment subsequences there are
			numFrags = 0;
			for (int i = 0; i < RightFragments.Count; i++)
			{
				int currentPosition = this.getRawRightFragment(i).getPosition();
				if (currentPosition > numFrags) { numFrags = currentPosition; }
			}

			#region OLD CODE
			/*
			//initialise all necessary fragment lists (one for each position)
			for (int i = 0; i < numFrags; i++)
			{ //loop through fragment positions
				List alternativeFragments = new ArrayList();
				orderedRightFragments.add(alternativeFragments);
			}

			//Add fragments to new structure
			for (int i = 0; i < RightFragments.size(); i++)
			{  //loop through all fragments
				int currentPosition = this.getRawRightFragment(i).getPosition();
				((ArrayList)orderedRightFragments.get(currentPosition - 1)).add(this.getRawRightFragment(i));
			}
			*/
			#endregion
			
			#region NEW CODE
			for (int i = 0; i < numFrags; i++)
			{
				orderedRightFragments.Add(null);
			}
			
			for (int i = 0; i < RightFragments.Count; i++)
			{
				int currentPosition = this.getRawRightFragment(i).getPosition();
				orderedRightFragments[currentPosition - 1] = this.getRawRightFragment(i);
			}
			#endregion

			//clear out unecessary info
			this.RightFragments = null;

		}


		/**
		 * Searches for this subsequence after the current file marker position in the file.
		 * Moves the file marker to the end of this subsequence.
		 *
		 * @param targetFile   the binary file to be identified
		 * @param reverseOrder  true if file is being searched from right to left
		 * @param bigEndian    True iff our parent signature is big-endian
		 */
		public bool isFoundAfterFileMarker(IByteReader targetFile, bool reverseOrder, bool bigEndian) {
        
        int searchDirection = reverseOrder?-1:1;
        //get the current file marker
        long startPosInFile = targetFile.GetFileMarker();
        //Add the minimum offset before start of sequence and update the file marker accordingly
        startPosInFile = startPosInFile + (long)(searchDirection * this.getMinSeqOffset());
        if (targetFile.GetNumberOfBytes() < startPosInFile)
        {
            // We're looking for a sequence of bytes at an offset which is longer than the file itself
            return false;
        }
        targetFile.SetFileMarker(startPosInFile);
        //start searching for main sequence after the minimum length of the relevant fragments
        startPosInFile = startPosInFile + (long)(searchDirection * this.getMinFragLength());
        long numFileBytes = reverseOrder?(startPosInFile+1):(targetFile.GetNumberOfBytes() - startPosInFile);
        int numSeqBytes = this.getNumBytes();
        bool subSeqFound = false;
        bool endOfFileReached = false;
        while((!subSeqFound) & (!endOfFileReached)) {
            if((long)numSeqBytes> numFileBytes) {
                endOfFileReached = true;
            } else {
                //compare sequence with file contents directly at fileMarker position
                bool missMatchFound = false;
                int byteLoopStart = reverseOrder?numSeqBytes-1:0;
                int byteLoopEnd   = reverseOrder?0:numSeqBytes-1;
                long tempFileMarker = startPosInFile;
                for(int iByte=byteLoopStart; (!missMatchFound) && (iByte<=numSeqBytes-1) && (iByte>=0); iByte+=searchDirection) {
                    missMatchFound = (byteSequence[iByte] != targetFile.GetByte(tempFileMarker));
                    if(!missMatchFound && showProgress) { System.Console.WriteLine("FLOATING SEQ: A match was found for " + this.getByte(iByte).ToString()); }
                    tempFileMarker+=searchDirection;
                }
                if(!missMatchFound) { //subsequence was found at position fileMarker in the file
                    //Now search for fragments between original fileMarker and startPosInFile
                    if(reverseOrder) {
                        long rightFragEnd;
                        long[] rightFragEndArray = bytePosForRightFragments(targetFile, startPosInFile+1, targetFile.GetFileMarker(), 1, 0, bigEndian);
                        if(rightFragEndArray.Length == 0) {
                            missMatchFound = true;
                        } else {
                            rightFragEnd = rightFragEndArray[0];
                            long leftFragEnd;
                            long[] leftFragEndArray = bytePosForLeftFragments(targetFile, 0, startPosInFile - numSeqBytes, -1, 0, bigEndian);
                            if(leftFragEndArray.Length == 0) {
                                missMatchFound = true;
                            } else {
                                leftFragEnd = leftFragEndArray[0];
                                targetFile.SetFileMarker(leftFragEnd-1L);
                                subSeqFound = true;
                            }
                        }
                        
                    } else {  //search is in forward direction
                        long leftFragEnd;
                        long[] leftFragEndArray = bytePosForLeftFragments(targetFile, targetFile.GetFileMarker(), startPosInFile-1L, -1, 0, bigEndian);
                        if(leftFragEndArray.Length == 0) {
                            missMatchFound = true;
                        } else {
                            leftFragEnd = leftFragEndArray[0];
                            long rightFragEnd;
                            long[] rightFragEndArray = bytePosForRightFragments(targetFile, startPosInFile + numSeqBytes, targetFile.GetNumberOfBytes()-1L, 1, 0, bigEndian);
                            if(rightFragEndArray.Length == 0) {
                                missMatchFound = true;
                            } else {
                                rightFragEnd = rightFragEndArray[0];
                                targetFile.SetFileMarker(rightFragEnd+1L);
                                subSeqFound = true;
                            }
                        }
                    }
                    
                }
                if(missMatchFound) {
                    if (startPosInFile+(long)(searchDirection*numSeqBytes)>=targetFile.GetNumberOfBytes()) {
                        endOfFileReached = true;
                    } else {
                        long numShiftBytes = this.getShift(targetFile.GetByte(startPosInFile+(long)(searchDirection*numSeqBytes))); //reverseOrder?-1:1;   // add shift function for the byte at [fileMarker+(searchDirection*numSeqBytes)] in file
                        numFileBytes -= (numShiftBytes>0)?numShiftBytes:-numShiftBytes;
                        startPosInFile += numShiftBytes;
                        
                        if((startPosInFile<0L) || (startPosInFile>=targetFile.GetNumberOfBytes())) {
                            endOfFileReached = true;
                        }
                    }
                }
            }
        }
        return subSeqFound;
    }


		/**
		 * Searches for this subsequence at the start of the current file.
		 * Moves the file marker to the end of this subsequence.
		 *
		 * @param targetFile   the binary file to be identified
		 * @param reverseOrder  true if file is being searched from right to left
		 * @param bigEndian    True iff our parent signature is big-endian
		 */
		public bool isFoundAtStartOfFile(IByteReader targetFile, bool reverseOrder, bool bigEndian)
		{
			int searchDirection = reverseOrder ? -1 : 1;
			int minSeqOffset = this.getMinSeqOffset();
			int maxSeqOffset = this.getMaxSeqOffset();
			long[] startPosInFile = new long[1];
			startPosInFile[0] = reverseOrder ? targetFile.GetNumberOfBytes() - minSeqOffset - 1 : minSeqOffset;
			bool subseqFound = true;
			bool leftFrag = true;

			if (reverseOrder) { leftFrag = false; }

			//match intial fragment
			if (reverseOrder)
			{
				startPosInFile = bytePosForRightFragments(targetFile, 0, startPosInFile[0], -1, (maxSeqOffset - minSeqOffset), bigEndian);
			}
			else
			{
				startPosInFile = bytePosForLeftFragments(targetFile, startPosInFile[0], targetFile.GetNumberOfBytes() - 1, 1, (maxSeqOffset - minSeqOffset), bigEndian);
			}
			int numOptions = startPosInFile.Length;
			if (numOptions == 0)
			{
				subseqFound = false;
			}
			else
			{
				for (int i = 0; i < numOptions; i++)
				{
					startPosInFile[i] += (long)searchDirection;
				}
			}

			//match main sequence
			if (subseqFound)
			{
				//move startPosInFile according to min offset of last fragment looked at
				int minOffset = 0;
				int maxOffset = 0;
				if (this.getNumFragmentPositions(leftFrag) > 0)
				{
					minOffset = this.getFragment(leftFrag, 1, 0).getMinOffset();
					maxOffset = this.getFragment(leftFrag, 1, 0).getMaxOffset();
					for (int i = 0; i < numOptions; i++)
					{
						startPosInFile[i] += (long)(minOffset * searchDirection);
					}
				}

				//add new possible values for startPosInFile to allow for difference between maxOffset and minOffset
				int offsetRange = maxOffset - minOffset;
				if (offsetRange > 0)
				{
					long[] newStartPosInFile = new long[numOptions * (offsetRange + 1)];
					for (int i = 0; i <= offsetRange; i++)
					{
						for (int j = 0; j < numOptions; j++)
						{
							newStartPosInFile[j + i * numOptions] = startPosInFile[j] + (long)(i * searchDirection);
						}
					}
					//Arrays.sort(newStartPosInFile);
					System.Array.Sort(newStartPosInFile);
					
					int newNumOptions = 1;
					for (int i = 1; i < numOptions * (offsetRange + 1); i++)
					{
						if (newStartPosInFile[i] > newStartPosInFile[newNumOptions - 1])
						{
							newStartPosInFile[newNumOptions] = newStartPosInFile[i];
							newNumOptions++;
						}
					}
					//now copy these back to the startPosInFile array (sorted in searchDirection)
					numOptions = newNumOptions;
					if (searchDirection > 1)
					{
						System.Array.Copy(newStartPosInFile, 0, startPosInFile, 0, numOptions);						
					}
					else
					{
						//reverse order copy
						for (int i = 0; i < numOptions; i++)
						{
							startPosInFile[i] = newStartPosInFile[numOptions - 1 - i];
						}
					}

				}

				//check that the end of the file is not going to be reached
				int numSeqBytes = this.getNumBytes();
				long numBytesInFile = targetFile.GetNumberOfBytes();
				if (reverseOrder)
				{
					//cutoff if startPosInFile is too close to start of file
					for (int i = 0; i < numOptions; i++)
					{
						if (startPosInFile[i] < ((long)numSeqBytes - 1L))
						{
							numOptions = i;
						}
					}
				}
				else
				{
					//cutoff if startPosInFile is too close to end of file
					for (int i = 0; i < numOptions; i++)
					{
						if (startPosInFile[i] > (numBytesInFile - (long)numSeqBytes))
						{
							numOptions = i;
						}
					}
				}

				/*
				long refNumTries = (long)(maxOffset - minOffset) + 1L;  //this is the expected number of attempts to get a match
				//correct the number of attempts for each starting position depending on whether the end of the file may be reached
				long[] maxNumTries = new long[numOptions];
				if(reverseOrder) {
					 for(int i=0; i<numOptions; i++) {
						 long tempNumTries = (startPosInFile[i]+1L) - (long)numSeqBytes + 1L;
						 maxNumTries[i] = tempNumTries<refNumTries ? tempNumTries : refNumTries;
					 }
				} else {
					for(int i=0; i<numOptions; i++) {
						long tempNumTries = (numBytesInFile - startPosInFile[i]) - numSeqBytes + 1L;
						 maxNumTries[i] = tempNumTries<refNumTries ? tempNumTries : refNumTries;
					}
				}
				 */

				for (int iOption = 0; iOption < numOptions; iOption++)
				{
					//compare sequence with file contents directly at fileMarker position
					int byteLoopStart = reverseOrder ? numSeqBytes - 1 : 0;
					int byteLoopEnd = reverseOrder ? 0 : numSeqBytes - 1;
					long tempFileMarker = startPosInFile[iOption];
					bool provSeqMatch = true;

					//check whether the file and signature sequences match
					for (int iByte = byteLoopStart; (provSeqMatch) && (iByte <= numSeqBytes - 1) && (iByte >= 0); iByte += searchDirection)
					{
						provSeqMatch = (byteSequence[iByte] == targetFile.GetByte(tempFileMarker));
						tempFileMarker += searchDirection;
					}

					if (!provSeqMatch)
					{
						//no match
						startPosInFile[iOption] = -2L;
					}
					else
					{
						//success: a match was found - update the startPosInFile
						startPosInFile[iOption] = tempFileMarker;
					}
				}


				//check the startPosInFile array: remove -2 values, reorder and remove duplicates
				System.Array.Sort(startPosInFile, 0, numOptions);
				int newNumOptions2 = 0;
				long[] newStartPosInFile2 = new long[numOptions];
				if (numOptions > 0)
				{
					if (startPosInFile[0] >= -1L)
					{
						newStartPosInFile2[0] = startPosInFile[0];
						newNumOptions2 = 1;
					}
				}
				for (int i = 1; i < numOptions; i++)
				{
					if (startPosInFile[i] > startPosInFile[i - 1])
					{
						newStartPosInFile2[newNumOptions2] = startPosInFile[i];
						newNumOptions2++;
					}
				}

				if (newNumOptions2 == 0)
				{
					subseqFound = false;
				}
				else
				{
					numOptions = newNumOptions2;
					if (searchDirection < 0)
					{
						//for right to left search direction, reorder in reverse
						for (int iOption = 0; iOption < numOptions; iOption++)
						{
							startPosInFile[iOption] = newStartPosInFile2[numOptions - 1 - iOption];
						}
					}
					else
					{
						//for left to right search direction, copy over as is
						System.Array.Copy(newStartPosInFile2, 0, startPosInFile, 0, numOptions);
					}
				}
			}


			//match remaining sequence fragment
			long newValueStartPosInFile = 0L;
			if (subseqFound)
			{

				long[] newArrayStartPosInFile;
				if (reverseOrder)
				{
					int i = 0;
					subseqFound = false;
					while (i < numOptions && !subseqFound)
					{
						newArrayStartPosInFile = bytePosForLeftFragments(targetFile, 0L, startPosInFile[i], -1, 0, bigEndian);
						if (newArrayStartPosInFile.Length == 0)
						{
							subseqFound = false;
						}
						else
						{
							subseqFound = true;
							newValueStartPosInFile = newArrayStartPosInFile[0] - 1L;  //take away -1???
						}
						i++;
					}
				}
				else
				{
					int i = 0;
					subseqFound = false;
					while (i < numOptions && !subseqFound)
					{
						newArrayStartPosInFile = bytePosForRightFragments(targetFile, startPosInFile[i], targetFile.GetNumberOfBytes() - 1L, 1, 0, bigEndian);
						if (newArrayStartPosInFile.Length == 0)
						{
							subseqFound = false;
						}
						else
						{
							subseqFound = true;
							newValueStartPosInFile = newArrayStartPosInFile[0] + 1L;  //take away +1????
						}
						i++;
					}
				}
			}

			//update the file marker
			if (subseqFound)
			{
				targetFile.SetFileMarker(newValueStartPosInFile);
			}

			return subseqFound;

		}

		/**
		 * Searches for the left fragments of this subsequence between the given byte
		 * positions in the file.  Either returns the last byte taken up by the
		 * identified sequences or returns -2 if no match was found
		 *
		 * @param targetFile   the binary file to be identified
		 * @param leftBytePos     left-most byte position of allowed search window on file
		 * @param rightBytePos     right-most byte position of allowed search window on file
		 * @param searchDirection  1 for a left to right search, -1 for right to left
		 * @param offsetRange  range of possible start positions in the direction of searchDirection
		 * @param bigEndian    True iff our parent signature is big-endian
		 */
		private long[] bytePosForLeftFragments(IByteReader targetFile, long leftBytePos, long rightBytePos,
				int searchDirection, int offsetRange, bool bigEndian) {
        bool leftFrag = true;
        long startPos = rightBytePos;
        int posLoopStart = 1;
        int numFragPos = this.getNumFragmentPositions(leftFrag);
        if (searchDirection == 1) {
            startPos = leftBytePos;
            posLoopStart = numFragPos;
        }
        
        //now set up the array so that it can potentially hold all possibilities
        int totalNumOptions = offsetRange+1;
        for(int iFragPos = 1; iFragPos <= numFragPos; iFragPos++) {
            totalNumOptions = totalNumOptions * this.getNumAlternativeFragments(leftFrag,iFragPos);
        }
        long[] markerPos = new long[totalNumOptions];
        for(int iOffset = 0; iOffset<=offsetRange; iOffset++) {
            markerPos[iOffset] = startPos + iOffset*searchDirection;
        }
        int numOptions = 1 + offsetRange;
        
        
        bool seqNotFound = false;
        for(int iFragPos = posLoopStart; (!seqNotFound) && (iFragPos <= numFragPos) && (iFragPos>=1) ; iFragPos-=searchDirection) {
            int numAltFrags = this.getNumAlternativeFragments(leftFrag,iFragPos);
            long[] tempEndPos = new long[numAltFrags*numOptions]; //array to store possible end positions after this fragment position has been examined
            System.Console.WriteLine(tempEndPos.Length);
            
            int numEndPos = 0;
            for(int iOption = 0; iOption < numOptions; iOption ++) {
                //will now look for all matching alternative sequence at the current end positions
                for(int iAlt = 0; iAlt< numAltFrags; iAlt++) {
                    long tempFragEnd;
                    if(searchDirection ==1) {
                        tempFragEnd = this.endBytePosForSeqFrag(targetFile, markerPos[iOption], rightBytePos, true, searchDirection, iFragPos, iAlt, bigEndian);
                    } else {
                        tempFragEnd = this.endBytePosForSeqFrag(targetFile, leftBytePos, markerPos[iOption], true, searchDirection, iFragPos, iAlt, bigEndian);
                    }
                    if(tempFragEnd > -1L) { // amatch has been found
                        tempEndPos[numEndPos] = tempFragEnd +searchDirection;
                        numEndPos += 1;
                    }
                }
            }
            if(numEndPos == 0) {
                seqNotFound = true;
            } else {
                numOptions = 0;
                for(int iOption = 0; iOption < numEndPos; iOption++) {
                    //eliminate any repeated end positions
                    bool addEndPos = true;
                    for(int iMarker = 0; iMarker<numOptions; iMarker++) {
                        if(markerPos[iMarker] == tempEndPos[iOption]) {
                            addEndPos = false;
                            break;
                        }
                    }
                    if(addEndPos) {
                        markerPos[numOptions] = tempEndPos[iOption];
                        numOptions++;
                    }
                }
            }
        }
        
        //prepare array to be returned
        if (seqNotFound) {
            // no possible positions found, return 0 length array
            long[] outArray = new long[0];
            return outArray;
            
        } else {
            // return ordered array of possibilities
            long[] outArray = new long[numOptions];
            
            //convert values to negative temporarily so that reverse sort order can be obtained for a right to left search direction
            if(searchDirection<0) {
                for(int iOption = 0; iOption < numOptions; iOption++) {
                    markerPos[iOption] = - markerPos[iOption];
                }
            }
            
            //sort the values in the array
            System.Array.Sort(markerPos,0,numOptions);
            
            //convert values back to positive now that a reverse sort order has been obtained
            if(searchDirection<0) {
                for(int iOption = 0; iOption < numOptions; iOption++) {
                    markerPos[iOption] = - markerPos[iOption];
                }
            }
            
            //copy to a new array which has precisely the correct length
            System.Array.Copy(markerPos,0,outArray,0,numOptions);
            
            //correct the value
            for(int iOption = 0; iOption < numOptions; iOption++) {
                outArray[iOption] -= (long)searchDirection;
            }
            
            return outArray;
        }
        
    }



		/**
		 * Searches for the right fragments of this subsequence between the given byte
		 * positions in the file.  Either returns the last byte taken up by the
		 * identified sequences or returns -2 if no match was found
		 *
		 * @param targetFile   the binary file to be identified
		 * @param leftBytePos     left-most byte position of allowed search window on file
		 * @param rightBytePos     right-most byte position of allowed search window on file
		 * @param searchDirection  1 for a left to right search, -1 for right to left
		 * @param offsetRange  range of possible start positions in the direction of searchDirection
		 * @param bigEndian    True iff our parent signature is big-endian
		 */
		private long[] bytePosForRightFragments(IByteReader targetFile, long leftBytePos, long rightBytePos,
				int searchDirection, int offsetRange, bool bigEndian)
		{
			bool leftFrag = false;
			long startPos = leftBytePos;
			int posLoopStart = 1;
			int numFragPos = this.getNumFragmentPositions(leftFrag);
			if (searchDirection == -1)
			{
				startPos = rightBytePos;
				posLoopStart = numFragPos;
			}

			//now set up the array so that it can potentially hold all possibilities
			int totalNumOptions = offsetRange + 1;
			for (int iFragPos = 1; iFragPos <= numFragPos; iFragPos++)
			{
				totalNumOptions = totalNumOptions * this.getNumAlternativeFragments(leftFrag, iFragPos);
			}
			long[] markerPos = new long[totalNumOptions];
			for (int iOffset = 0; iOffset <= offsetRange; iOffset++)
			{
				markerPos[iOffset] = startPos + iOffset * searchDirection;
			}
			int numOptions = 1 + offsetRange;

			bool seqNotFound = false;
			for (int iFragPos = posLoopStart; (!seqNotFound) && (iFragPos <= numFragPos) && (iFragPos >= 1); iFragPos += searchDirection)
			{
				int numAltFrags = this.getNumAlternativeFragments(leftFrag, iFragPos);
				long[] tempEndPos = new long[numAltFrags * numOptions]; //array to store possible end positions after this fragment position has been examined
				int numEndPos = 0;
				for (int iOption = 0; iOption < numOptions; iOption++)
				{
					//will now look for all matching alternative sequence at the current end positions
					for (int iAlt = 0; iAlt < numAltFrags; iAlt++)
					{
						long tempFragEnd;
						if (searchDirection == -1)
						{
							tempFragEnd = this.endBytePosForSeqFrag(targetFile, leftBytePos, markerPos[iOption], false, searchDirection, iFragPos, iAlt, bigEndian);
						}
						else
						{
							tempFragEnd = this.endBytePosForSeqFrag(targetFile, markerPos[iOption], rightBytePos, false, searchDirection, iFragPos, iAlt, bigEndian);
						}
						if (tempFragEnd > -1)
						{ // amatch has been found
							tempEndPos[numEndPos] = tempFragEnd + searchDirection;
							numEndPos += 1;
						}
					}
				}
				if (numEndPos == 0)
				{
					seqNotFound = true;
				}
				else
				{
					numOptions = 0;
					for (int iOption = 0; iOption < numEndPos; iOption++)
					{
						//eliminate any repeated end positions
						bool addEndPos = true;
						for (int iMarker = 0; iMarker < numOptions; iMarker++)
						{
							if (markerPos[iMarker] == tempEndPos[iOption])
							{
								addEndPos = false;
								break;
							}
						}
						if (addEndPos)
						{
							markerPos[numOptions] = tempEndPos[iOption];
							numOptions++;
						}
					}
				}
			}

			//prepare array to be returned
			if (seqNotFound)
			{
				// no possible positions found, return 0 length array
				long[] outArray = new long[0];
				return outArray;

			}
			else
			{
				// return ordered array of possibilities
				long[] outArray = new long[numOptions];

				//convert values to negative temporarily so that reverse sort order can be obtained for a right to left search direction
				if (searchDirection < 0)
				{
					for (int iOption = 0; iOption < numOptions; iOption++)
					{
						markerPos[iOption] = -markerPos[iOption];
					}
				}

				//sort the values in the array
				System.Array.Sort(markerPos, 0, numOptions);

				//convert values back to positive now that a reverse sort order has been obtained
				if (searchDirection < 0)
				{
					for (int iOption = 0; iOption < numOptions; iOption++)
					{
						markerPos[iOption] = -markerPos[iOption];
					}
				}

				//copy to a new array which has precisely the correct length
				System.Array.Copy(markerPos, 0, outArray, 0, numOptions);
				//System.arraycopy(markerPos, 0, outArray, 0, numOptions);

				//correct the value
				for (int iOption = 0; iOption < numOptions; iOption++)
				{
					outArray[iOption] -= (long)searchDirection;
				}

				return outArray;
			}

		}






		/**
		 * searches for the specified fragment sequence
		 * between the leftmost and rightmost byte positions that are given.
		 * returns the end position of the found sequence or -1 if it is not found
		 *
		 * @param   targetFile  The file that is being reviewed for identification
		 * @param   leftBytePos leftmost position in file at which to search
		 * @param   rightBytePos    rightmost postion in file at which to search
		 * @param   leftFrag    flag to indicate whether looking at left or right fragments
		 * @param   searchDirection    direction in which search is carried out (1 for left to right, -1 for right to left)
		 * @param   fragPos     position of left/right sequence fragment to use
		 * @param   fragIndex   index of fragment within the position (where alternatives exist)
		 * @param   bigEndidan  True iff out parent signature is big-endian
		 */
		private long endBytePosForSeqFrag(IByteReader targetFile, long leftEndBytePos, long rightEndBytePos,
				bool leftFrag, int searchDirection, int fragPos, int fragIndex, bool bigEndian)
		{
			long startPosInFile;
			long lastStartPosInFile;
			long endPosInFile = -1L;
			long searchDirectionL = (long)searchDirection;
			int numBytes;
			int minOffset;
			int maxOffset;

			// read in values
			numBytes = this.getFragment(leftFrag, fragPos, fragIndex).getNumBytes();
			if (leftFrag && (searchDirection == -1))
			{
				minOffset = this.getFragment(leftFrag, fragPos, fragIndex).getMinOffset();
				maxOffset = this.getFragment(leftFrag, fragPos, fragIndex).getMaxOffset();
			}
			else if (!leftFrag && (searchDirection == 1))
			{
				minOffset = this.getFragment(leftFrag, fragPos, fragIndex).getMinOffset();
				maxOffset = this.getFragment(leftFrag, fragPos, fragIndex).getMaxOffset();
			}
			else if (fragPos < this.getNumFragmentPositions(leftFrag))
			{
				minOffset = this.getFragment(leftFrag, fragPos + 1, 0).getMinOffset();
				maxOffset = this.getFragment(leftFrag, fragPos + 1, 0).getMaxOffset();
			}
			else
			{
				minOffset = 0;
				maxOffset = 0;
			}

			// set up start and end positions for searches taking into account min and max offsets
			if (searchDirection == -1)
			{
				startPosInFile = rightEndBytePos - (long)minOffset;
				long lastStartPosInFile1 = leftEndBytePos + (long)numBytes - 1L;
				long lastStartPosInFile2 = rightEndBytePos - (long)maxOffset;
				lastStartPosInFile = (lastStartPosInFile1 < lastStartPosInFile2) ? lastStartPosInFile2 : lastStartPosInFile1;
			}
			else
			{
				startPosInFile = leftEndBytePos + (long)minOffset;
				long lastStartPosInFile1 = rightEndBytePos - (long)numBytes + 1L;
				long lastStartPosInFile2 = leftEndBytePos + (long)maxOffset;
				lastStartPosInFile = (lastStartPosInFile1 < lastStartPosInFile2) ? lastStartPosInFile1 : lastStartPosInFile2;
			}


			//keep searching until either the sequence fragment is found or until the end of the search area has been reached.
			//compare sequence with file contents directly at fileMarker position
			bool subSeqFound = false;
			while ((!subSeqFound) && ((searchDirectionL) * (lastStartPosInFile - startPosInFile) >= 0L))
			{
				bool missMatchFound = false;
				int byteLoopStart;
				if (searchDirection == -1)
				{
					byteLoopStart = numBytes - 1;
				}
				else
				{
					byteLoopStart = 0;
				}
				
				SideFragment fragment = this.getFragment(leftFrag, fragPos, fragIndex);
				long tempFileMarker = startPosInFile;
				for (int i = (searchDirection == 1) ? 0 : fragment.getNumByteSeqSpecifiers() - 1; !missMatchFound && 0 <= i && i < fragment.getNumByteSeqSpecifiers(); i += searchDirection)
				{
					missMatchFound = !fragment.getByteSeqSpecifier(i).matchesByteSequence(targetFile, tempFileMarker, searchDirection, bigEndian);
					if (!missMatchFound)
					{
						tempFileMarker += searchDirection * fragment.getByteSeqSpecifier(i).getNumBytes();
					}
				}
				if (!missMatchFound)
				{ //subsequence fragment was found in the file
					subSeqFound = true;
					endPosInFile = tempFileMarker - searchDirectionL;
				}
				else
				{
					startPosInFile += searchDirectionL;
				}
			}
			return endPosInFile;  //this is -1 unless subSeqFound = true
		}





		public String toString() { return Position + " seq=<" + Sequence + ">" + "LLL" + orderedLeftFragments + "LLL" + "RRR" + orderedRightFragments + "RRR"; }
		#endregion
	}
}
