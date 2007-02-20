using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.BinaryReader;
using AlexandriaOrg.Alexandria.Droid.SignatureFile;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class AnalysisThread
	{
		//TODO: figure out how to port this
		//private System.Threading.Thread analysisThread; // = new System.Threading.Thread(
		
		private FileCollection myFileCollection;
		private FFSignatureFile mySigFile;
		private AnalysisController myAnalysisController;
		//parameters used to measure performance
		private int readTime = 0;
		private int algoTime = 0;

		/** Creates a new instance of AnalysisThread */
		public AnalysisThread(FileCollection theFileCollection, FFSignatureFile theSigFile, AnalysisController theAnalysisController)
		{
			myFileCollection = theFileCollection;
			mySigFile = theSigFile;
			myAnalysisController = theAnalysisController;
		}

		/** Runs the thread for file identification analysis */
		public void run()
		{

			//Let AnalysisController know that anlaysis has started
			myAnalysisController.setAnalysisStart();

			//DateTime startTime = new DateTime();

			for (int fileNum = 0; fileNum < myFileCollection.getNumFiles() && !myAnalysisController.isAnalysisCancelled(); fileNum++)
			{
				IdentificationFile idFile = myFileCollection.getFile(fileNum);
				String idFileName = idFile.getFilePath();

				DateTime startRead = new DateTime();
				IByteReader testFile = null;
				try
				{
					testFile = AbstractByteReader.newByteReader(idFile);
				}
				catch (System.OutOfMemoryException e) //OutOfMemoryError e)
				{
					testFile = AbstractByteReader.newByteReader(idFile, false);
					testFile.SetErrorIdentification();
					testFile.SetIdentificationWarning("The application ran out of memory while loading this file (" + e.ToString() + ")");
				}
				DateTime endRead = new DateTime();
				readTime += (endRead.TimeOfDay.Milliseconds - startRead.TimeOfDay.Milliseconds); // was getTime()

				if (!testFile.IsClassified())
				{

					//int id = theFile.getInternalSignature(0).getByteSequence(0).getSubSequence(1).getShift(16);
					//testFile.runFileIdentification(theFile);
					DateTime startAlgo = new DateTime();
					try
					{
						mySigFile.runFileIdentification(testFile);
					}
					catch (Exception e)
					{
						testFile.SetErrorIdentification();
						testFile.SetIdentificationWarning("Error during identification attempt: " + e.ToString());
					}
					DateTime endAlgo = new DateTime();
					algoTime += (endAlgo.TimeOfDay.Milliseconds - startAlgo.TimeOfDay.Milliseconds); // was getTime()


				}


				/************** print out results ***************/
				//display the hits
				debugDisplayHits(testFile, idFileName);

				//Record the fact that another file has completed
				myAnalysisController.incrNumCompletedFile();

			}
			//DateTime endTime = new DateTime();

			//Let AnalysisController know that anlaysis is complete
			myAnalysisController.setAnalysisComplete();

		}


		/**
		 * Print out the list of hits (for debugging purposes only
		 */
		private void debugDisplayHits(IByteReader testFile, String fileName) {
        
        System.Console.WriteLine("==================================");
        if (testFile.IsClassified()) {
            
            //display file classification and any warning
            System.Console.WriteLine("     " + fileName);
            if(testFile.GetIdentificationWarning().Length > 0)
                System.Console.WriteLine("with warning: "+testFile.GetIdentificationWarning());
            
            //display list of hits
            for(int ih=0; ih<testFile.GetNumberOfHits(); ih++) {
                String specificityDisplay = testFile.GetHit(ih).IsSpecific()?"specific":"generic";
                System.Console.WriteLine("          " + testFile.GetHit(ih).GetHitTypeVerbose() + " " + specificityDisplay + " hit for " + testFile.GetHit(ih).GetFileFormat().GetName() + "  [PUID: " + testFile.GetHit(ih).GetFileFormat().GetPuid()+ "]");
                if(testFile.GetHit(ih).GetHitWarning().Length > 0)
                {
                    System.Console.WriteLine("               WARNING: " + testFile.GetHit(ih).GetHitWarning());
                }
            }
        } else {
			System.Console.WriteLine("     " + fileName + " was not classified");
        }
        
    }
    
    public void Start()
    {
		//analysisThread.Start();
    }
    
	}
}
