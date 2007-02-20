using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria.Droid.XmlReader;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class AnalysisController
	{
		#region Constructors
		public AnalysisController()
		{
			myFileCollection = new FileCollection() ;
		}
		#endregion
		    
		#region Private Fields
		//class variables:
		private ConfigFile myConfigFile = new ConfigFile();
		private FileCollection myFileCollection = new FileCollection();
		private SignatureFile.FFSignatureFile mySigFile;
		private bool myAnalysisCancelled = false;
		private int myNumCompletedFiles = 0;
		private bool isAnalysisRunning = false;
		/** output formats to be used for saving results at end of run */
		private String myOutFormats = string.Empty;
		/** base file name to be used for saving results at end of run */
		private String myOutFileName = string.Empty;
	    
		/**Latest version of signature file available , value is -1 if this hasn't been checked */
		private int sigFileLatestVersion = -1 ;
		#endregion
    
		#region Private Static Fields
		/**Date format to read/write dates to XML*/
		//private static string XML_DATE_FORMAT = "yyyy'-'MM'-'dd'T'HH:mm:ss";
	    
		/**Date format to display dates in application*/
		//private static string DISPLAY_DATE_FORMAT = "dd'-'MMM'-'yyyy" ;
	    
		/** Namespace for the xml file collection file */
		public static string FILE_COLLECTION_NS = "http://www.nationalarchives.gov.uk/pronom/FileCollection";
		/** Namespace for the xml configuration file */
		public static string CONFIG_FILE_NS = "http://www.nationalarchives.gov.uk/pronom/ConfigFile";
		/** Namespace for the xml file format signatures file */
		public static string SIGNATURE_FILE_NS = "http://www.nationalarchives.gov.uk/pronom/SignatureFile";	    
		#endregion
        
        #region Public Static Fields
	    //Application version
		public static string FFIT_VERSION = "V1.1.0";
    
		//File classification constants
		public static int FILE_CLASSIFICATION_POSITIVE = 1;
		public static int FILE_CLASSIFICATION_TENTATIVE = 2;
		public static int FILE_CLASSIFICATION_NOHIT = 3;
		public static int FILE_CLASSIFICATION_ERROR = 4;
		public static int FILE_CLASSIFICATION_NOTCLASSIFIED = 5;
		public static string FILE_CLASSIFICATION_POSITIVE_TEXT = "Positive";
		public static string FILE_CLASSIFICATION_TENTATIVE_TEXT = "Tentative";
		public static string FILE_CLASSIFICATION_NOHIT_TEXT = "Not identified";
		public static string FILE_CLASSIFICATION_ERROR_TEXT = "Error";
		public static string FILE_CLASSIFICATION_NOTCLASSIFIED_TEXT = "Not yet run";
	    
		//hit type constants
		public static int HIT_TYPE_POSITIVE_SPECIFIC = 10;
		public static int HIT_TYPE_POSITIVE_GENERIC = 11;
		public static int HIT_TYPE_TENTATIVE = 12;
		public static int HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC = 15;
		public static string HIT_TYPE_POSITIVE_SPECIFIC_TEXT = "Positive (Specific Format)";
		public static string HIT_TYPE_POSITIVE_GENERIC_TEXT = "Positive (Generic Format)";
		public static string HIT_TYPE_TENTATIVE_TEXT = "Tentative";
		public static string HIT_TYPE_POSITIVE_GENERIC_OR_SPECIFIC_TEXT = "Positive";
	    
		//Buffer size for reading random access files
		public static int FILE_BUFFER_SIZE = 100000000;
	    
		//default values
		public static int CONFIG_DOWNLOAD_FREQ = 30;
		public static string CONFIG_FILE_NAME = "DROID_config.xml";
		public static string FILE_LIST_FILE_NAME = "DROID_filecollection.xml";
		public static string PRONOM_WEB_SERVICE_URL = "http://www.nationalarchives.gov.uk/pronom/SignatureFile/proSignatureFileProvider.asp";
		public static string SIGNATURE_FILE_NAME = "DROID_SignatureFile.xml";
	    
		public static string LABEL_APPLICATION_VERSION = "DROIDVersion";
		public static string LABEL_DATE_CREATED = "DateCreated";
		#endregion
        
		#region Private Methods
		/**
		* Displays the contents of the binary file as a list of bytes.
		* This is only used for debugging purposes.
		* @param testFile   The binary file
		*/
		private void debugDisplayBinFile(System.IO.BinaryReader testFile, string testFileName) //ByteReader testFile)
		{
			System.Console.WriteLine("======================= "+ testFileName);
		    
			//printout testfile for debugging
			string allTheBytes = string.Empty;
		    
			//for(int i = 0; i<byteToTest.length; i++) {
			byte[] testFileBytes = new byte[1];
			for(int i = 0; i < 200; i++)
			{
				testFile.Read(testFileBytes, i, 1);
				allTheBytes += showByte(testFileBytes[0]);
				//allTheBytes += showByte() //testFile.getByte(i));
			}
		    
			System.Console.WriteLine(allTheBytes);
		}

		/**
		* Displays a byte in the same format as used by TNA.
		* @param theByte   The byte to display
		*/
		private string showByte(byte theByte)
		{
			string byteDisplay;
			byteDisplay = theByte.ToString("X"); //Integer.toHexString(theByte);
			if(byteDisplay.Length == 1) { byteDisplay = "0" + byteDisplay; }
			if(byteDisplay.Length > 2) { byteDisplay = byteDisplay.Substring(byteDisplay.Length - 2); }
			return byteDisplay;
		}
		#endregion
		
		#region Private Static Methods
		/**
		* Determines whether to launch FFIT in GUI mode
		* @param args   The run time arguments
		*/
		private static bool isGUI(string[] args)
		{
			if(args.Length == 0)        
				return true;        
			else        
				return false;        
		}

		/**
		* Determines whether to launch FFIT in command line mode
		* @param args   The run time arguments
		*/
		private static bool isCmdLine(string[] args)
		{
			if(args.Length > 0)        
				return true;
			else
				return false;
		}
		#endregion
		
		#region Public Methods
		/**
		* Reads the default configuration file, and loads the contents into memory.
		*/
		public void readConfiguration() //throws Exception {
		{
			readConfiguration(CONFIG_FILE_NAME);  //use default file name
		}

		/**
		* Reads a configuration file, and loads the contents into memory.
		* @param theFileName   The name of the configuration file to open
		*/
		public void readConfiguration(string theFileName) //throws Exception {
		{
			//if file doesn't exist, then warn user and create a new one using defaults
			if (!isFileFound(theFileName))
			{
				MessageDisplay.GeneralWarning("The expected configuration file " + theFileName + " was not found.\nA new one will be created using the configuration defaults.");
				myConfigFile = new ConfigFile();
				try
				{
					saveConfiguration(theFileName);
				}
				catch (System.IO.IOException)
				{
					MessageDisplay.GeneralWarning("Unable to save configuration updates");
				}
			}
			else
			{
				try
				{
					/*
					checkFile(theFileName);
		            
					//prepare for XML read
					messageDisplay.resetXMLRead();
		            
					//prepare to read in the XML file
					SAXParserFactory factory = SAXParserFactory.newInstance();
					factory.setNamespaceAware(true);
					SAXParser saxParser = factory.newSAXParser();
					XMLReader parser = saxParser.getXMLReader();
					SAXModelBuilder mb = new SAXModelBuilder();
					mb.setObjectPackage("FFIT");
					mb.setupNamespace(CONFIG_FILE_NS, true);
					parser.setContentHandler( mb );
		            
					//read in the XML file
					java.io.BufferedReader in = new java.io.BufferedReader(new java.io.InputStreamReader(new java.io.FileInputStream(theFileName),"UTF8"));
					parser.parse( new InputSource(in) );
					myConfigFile = (ConfigFile)mb.getModel();
					myConfigFile.setFileName(theFileName);
		            
					//let the user know the outcome if there were any problems
					int numXMLWarnings = messageDisplay.getNumXMLWarnings();
					if(numXMLWarnings > 0) {
						String successMessage = "The configuration file "+theFileName;
						successMessage += " contained "+numXMLWarnings+" warning(s)";
						messageDisplay.generalWarning(successMessage);
					}
					*/
				}
				catch (Exception e)
				{
					throw new Exception("Unable to read configuration file "+theFileName+"\nThe following error was encountered: "+e.ToString());
					//messageDisplay.generalWarning(e.toString());
					//System.exit(0);
				}
			}
		}

		/** Saves the current configuration to the default file name
		 */
		public void saveConfiguration() //throws IOException {
		{
			myConfigFile.saveConfiguration();
		}

		/** Saves the current configuration to file in XML format
		 *@param    filePath    Path of configuration file
		 */
		public void saveConfiguration(String filePath ) //throws IOException {
		{
			myConfigFile.setFileName(filePath);
			myConfigFile.saveConfiguration();
		}

		/**
		 *Saves the file list to file in XML format with or without the associated hits
		 *@param    filePath    Path of where to save file list
		 *@param    saveResults Save the file format hits as well
		 */
		public void saveFileList(String filePath , bool saveResults)
		{    
			try
			{
				//java.io.BufferedWriter out = new java.io.BufferedWriter(new java.io.OutputStreamWriter(new java.io.FileOutputStream(filePath),"UTF8"));
				System.IO.StreamWriter outWriter = new System.IO.StreamWriter(filePath, true, Encoding.UTF8);
				//new TextStream(filePath), Encoding.UTF8);
				writeXmlWithElements(outWriter, saveResults);
			}
			catch (System.IO.IOException)
			{
			}
		}
		#endregion
		
		#region Public Static Methods
		/**
		* Launches an instance of FFIT either with a GUI interface or in a command line environment,
		* depending on the run time arguments.
		* @param args   The run time arguments
		*/
		public static void Main(string[] args ) //throws Exception {
		{
			if(isGUI(args))
			{
				//GUI.FileIdentificationPane.launch(new AnalysisController()) ;		        
			}
			else if(isCmdLine(args))
			{
				//CommandLine.CmdController myCmdController ;
				//myCmdController = new CommandLine.CmdController(args);
			}
		    
		}

		/*
		* Created a date object with value of date in format yyyy-MM-ddTHH:mm:ss (e.g. 2005-02-24T12:35:23)
		* @param   XMLFormatDate   date in format yyyy-MM-ddTHH:mm:ss
		* @return  date with value set
		*/
		// was a java.util.Date
		public static DateTime parseXMLDate(string XMLFormatDate) //throws java.text.ParseException{
		{
			//SimpleDateFormat xmlDateFormat = new SimpleDateFormat(XML_DATE_FORMAT);
			
			try
			{
				//parse the date supplied  into a Date object
				return Convert.ToDateTime(XMLFormatDate); //xmlDateFormat.parse(XMLFormatDate) ;
		        
			}
			catch (InvalidCastException pe) //(java.text.ParseException pe){
			{
				throw pe ;
			}
		}

		/**
		* Creates an XML format date yyyy-MM-ddTHH:mm:ss from a date object.
		*
		* For example, 2005-02-24T12:35:23
		*
		*  @param  aDate   Date to represent
		*  @return Date in formatyyyy-MM-ddTHH:mm:ss (e.g. 2005-02-24T12:35:23)
		*/
		public static string writeXMLDate(DateTime aDate)
		{
			//SimpleDateFormat xmlDateFormat = new SimpleDateFormat(XML_DATE_FORMAT);			
			return aDate.ToString("s"); //xmlDateFormat.format(aDate);
		}

		/**
		* Creates a date in format dd-MMM-yyyy (e.g 18-Nov-2005)
		* @param  aDate   Date to represent
		* @return Date in format dd-MMM-yyyy (e.g 18-Nov-2005)
		*/
		public static String writeDisplayDate(DateTime aDate)
		{
			//SimpleDateFormat displayDateFormat = new SimpleDateFormat(DISPLAY_DATE_FORMAT);
			return string.Format("{0:dd-MMM-yyyy}", aDate); //displayDateFormat.format(aDate);
		}
		#endregion		
    
    
    
    

    /**
     * Write the XML to the file, using the new schema format with elements for most of the data.
     */
    private void writeXmlWithElements(System.IO.StreamWriter outWriter, bool saveResults)
    {
		//final java.io.BufferedWriter out, final boolean saveResults) throws IOException {

        outWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        outWriter.WriteLine();
        outWriter.Write("<FileCollection xmlns=\"" + FILE_COLLECTION_NS + "\">");
        outWriter.WriteLine();
        outWriter.Write("  <DROIDVersion>"+AnalysisController.getFFITVersion().Replace("&","&amp;")+"</DROIDVersion>");
        outWriter.WriteLine();
        outWriter.Write("  <SignatureFileVersion>"+(this.getSigFileVersion().ToString())+"</SignatureFileVersion>");
        outWriter.WriteLine();
        outWriter.Write("  <DateCreated>"+writeXMLDate(DateTime.Now)+"</DateCreated>");
        outWriter.WriteLine();
        
        //loop through file objects
        for (int n = 0; n < this.getNumFiles(); n++){
            
            IdentificationFile idFile = this.getFile(n) ;
            
            //create IdentificationFile element and its attributes
            outWriter.Write("  <IdentificationFile ");
            if (saveResults) {
                outWriter.Write("IdentQuality=\""+idFile.getClassificationText()+"\" ");
            }
            outWriter.Write(">");
            outWriter.WriteLine();
            outWriter.Write("    <FilePath>"+idFile.getFilePath().Replace("&","&amp;")+"</FilePath>");
            outWriter.WriteLine();
            if (saveResults && !string.Empty.Equals(idFile.getWarning()) ) {
                outWriter.Write("    <Warning>"+ idFile.getWarning().Replace("&","&amp;") + "</Warning>");
                outWriter.WriteLine();
            }
            
            //Add file format hits if required to do so
            if (saveResults)
            {
                //now create an FileFormatHit element for each hit
                for (int hitCounter = 0; hitCounter < idFile.getNumHits(); hitCounter++ )
                {
                    FileFormatHit formatHit = idFile.getHit(hitCounter);
                    outWriter.Write("    <FileFormatHit>");
                    outWriter.WriteLine();
                    outWriter.Write("      <Status>" + formatHit.GetHitTypeVerbose() + "</Status>");
                    outWriter.WriteLine();
                    outWriter.Write("      <Name>"+formatHit.GetFileFormatName().Replace("&","&amp;")+"</Name>");
                    outWriter.WriteLine();
                    if (formatHit.GetFileFormatVersion()!=null){
                        outWriter.Write("      <Version>"+formatHit.GetFileFormatVersion().Replace("&","&amp;")+"</Version>");
                        outWriter.WriteLine();
                    }
                    if (formatHit.GetFileFormatPUID()!=null){
                        outWriter.Write("      <PUID>"+formatHit.GetFileFormatPUID().Replace("&","&amp;")+"</PUID>");
                        outWriter.WriteLine();
                    }
                    if (!string.Empty.Equals(formatHit.GetHitWarning())){
                        outWriter.Write("      <IdentificationWarning>"
                                + formatHit.GetHitWarning().Replace("&","&amp;")+"</IdentificationWarning>");
                        outWriter.WriteLine();
                    }
                    outWriter.Write("    </FileFormatHit>");
                    outWriter.WriteLine();
                }//end file hit FOR
                
            }//end if (saveResults)
            
            //close IdentificationFile element
            outWriter.Write("  </IdentificationFile>");
            outWriter.WriteLine();
            
        }//end idFile FOR
        
        //close FileCollection element
        outWriter.Write("</FileCollection>");
        outWriter.WriteLine();
        
        outWriter.Flush();
        outWriter.Close();
    }    

    /**
     * Write the XML to the file, using the old schema format with attributes for most of the data
     */
    private void writeXmlWithAttributes(System.IO.StreamWriter outWriter, bool saveResults) //throws IOException {
    {
        /* out.write(getFileCollectionXMLJDOM(saveResults)); */
        outWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
        outWriter.Write("<FileCollection xmlns=\"" + FILE_COLLECTION_NS + "\" ");
        outWriter.Write(LABEL_APPLICATION_VERSION+"=\""+AnalysisController.getFFITVersion().Replace("&","&amp;")+"\" ");
        outWriter.Write("SigFileVersion=\""+(this.getSigFileVersion().ToString())+"\" ");
        
        outWriter.Write(LABEL_DATE_CREATED + "=\"" + writeXMLDate(DateTime.Now) + "\">\r\n");
        //outWriter.Write(LABEL_DATE_CREATED+"=\""+writeXMLDate(new java.util.Date())+"\">\r\n");
        
        
        //loop through file objects
        for (int n = 0 ; n < this.getNumFiles() ; n++){
            
            IdentificationFile idFile = this.getFile(n) ;
            
            //create IdentificationFile element and its attributes
            outWriter.Write("  <IdentificationFile ");
            outWriter.Write("Name=\""+idFile.getFilePath().Replace("&","&amp;")+"\" ");
            //If saving results then get the saving classification and warning message
            if (saveResults)
            {
                outWriter.Write("IdentQuality=\""+idFile.getClassificationText()+"\" ");
                outWriter.Write("Warning=\""+idFile.getWarning().Replace("&","&amp;")+"\" ");
            }
            outWriter.Write(">\r\n");
            
            
            //Add file format hits if required to do so
            if (saveResults)
            {
                //now create an FileFormatHit element for each hit
                for (int hitCounter = 0 ; hitCounter < idFile.getNumHits() ; hitCounter++ )
                {
                    FileFormatHit formatHit = idFile.getHit(hitCounter) ;
                    outWriter.Write("    <FileFormatHit ") ;
                    outWriter.Write("HitStatus=\"" + formatHit.GetHitTypeVerbose()+"\" ");
                    outWriter.Write("FormatName=\"" + formatHit.GetFileFormatName().Replace("&","&amp;")+"\" ");
                    if (formatHit.GetFileFormatVersion()!=null)
                    {
                        outWriter.Write("FormatVersion=\""+formatHit.GetFileFormatVersion().Replace("&","&amp;")+"\" ");
                    }
                    if (formatHit.GetFileFormatPUID()!=null)
                    {
                        outWriter.Write("FormatPUID=\""+formatHit.GetFileFormatPUID().Replace("&","&amp;")+"\" ");
                    }
                    if (formatHit.GetHitWarning()!=null)
                    {
                        outWriter.Write("HitWarning=\""+formatHit.GetHitWarning().Replace("&","&amp;")+"\" ");
                    }
                    outWriter.Write("/>\r\n") ;
                }//end file hit FOR
                
            }//end if (saveResults)
            
            //close IdentificationFile element
            outWriter.Write("  </IdentificationFile>\r\n");
            
        }//end idFile FOR
        
        //close FileCollection element
        outWriter.Write("</FileCollection>\r\n");
        
        outWriter.Flush();
        outWriter.Close();
    }
    
    
    /**
     *Creates a File Collection XML Document representing current file collection
     *Uses JDOM
     *
     *@param    saveResults Save the file format hits aswell
     *@return the xml document
     */
    private string getFileCollectionXMLJDOM(bool saveResults)
    {   
		//TODO: find the .NET equivalent to org.jdom.Element
		
		/*     
        org.jdom.Element file_collection = null ;
        IdentificationFile idFile = null ;
        FileFormatHit formatHit = null ;
        org.jdom.Element file = null ;
        org.jdom.Element hit = null ;
        
        //create the FileCollection element and attributes
        file_collection = new org.jdom.Element("FileCollection") ;
        file_collection.setAttribute(LABEL_APPLICATION_VERSION,AnalysisController.getFFITVersion());
        file_collection.setAttribute("SigFileVersion",Integer.toString(this.getSigFileVersion()));
        file_collection.setAttribute(LABEL_DATE_CREATED,writeXMLDate(new java.util.Date()));
        
        //loop through file objects
        for (int n = 0 ; n<  this.getNumFiles() ; n++){
            
            //create file element and attributes
            idFile = this.getFile(n) ;
            file = new org.jdom.Element("IdentificationFile") ;
            file.setAttribute("Name",idFile.getFilePath()) ;
            //If saving results then get the saving classification and warning message
            if (saveResults){
                file.setAttribute("IdentQuality",idFile.getClassificationText());
                file.setAttribute("Warning",idFile.getWarning());
            }
            //otherwise the file is unclassified and no warning if just saving the file list
            else{
                file.setAttribute("IdentQuality",FILE_CLASSIFICATION_NOTCLASSIFIED_TEXT);
                file.setAttribute("Warning","");
            }
            
            
            
            file_collection.addContent(file) ;
            
            //Only add file format hits if required to do so
            if (saveResults){
                //now create an Identification element for each hit
                for (int hitCounter = 0 ; hitCounter < idFile.getNumHits() ; hitCounter++ ){
                    
                    formatHit = idFile.getHit(hitCounter) ;
                    hit = new org.jdom.Element("FileFormatHit") ;
                    hit.setAttribute("HitStatus",formatHit.getHitTypeVerbose());
                    hit.setAttribute("FormatName",formatHit.getFileFormatName()) ;
                    if (formatHit.getFileFormatVersion()!=null){
                        hit.setAttribute("FormatVersion",formatHit.getFileFormatVersion()) ;
                    }
                    if (formatHit.getFileFormatPUID()!=null){
                        hit.setAttribute("FormatPUID",formatHit.getFileFormatPUID()) ;
                    }
                    if (formatHit.getHitWarning()!=null){
                        hit.setAttribute("HitWarning",formatHit.getHitWarning()) ;
                    }
                    file.addContent(hit) ;
                }//end file hit FOR
                
            }//end if (saveResults)
            
        }//end idFile FOR
        
        org.jdom.Document fileCollection = new org.jdom.Document(file_collection) ;
        
        //write it all to a String
        org.jdom.output.Format xmlFormat = org.jdom.output.Format.getPrettyFormat();
        org.jdom.output.XMLOutputter outputter = new org.jdom.output.XMLOutputter(xmlFormat) ;
        java.io.StringWriter writer = new java.io.StringWriter();
        
        try {
            outputter.output(fileCollection, writer);
            writer.close();
        }catch (java.io.IOException e){
            
        }
        return writer.toString() ;
        */
		  
		return null; 
    }
    
    /**
     * Writes the file collection to a CSV file
     * @param filePath  where to save the CSV file
     */
    public void exportFileCollectionAsCSV(string filePath)
    {
        //StringBuffer fileText = new StringBuffer() ;
        StringBuilder fileText = new StringBuilder();        
        
        //Initialise loop variables
        int fileCounter  = 0 ;
        int hitCounter  = 0 ;
        IdentificationFile idFile = null ;
        FileFormatHit hit = null ;
        
        //Write FFIT Version , Signature file version and date at top
        
        fileText.Append("DROID Version,\"" + getFFITVersion() + "\"") ;
        fileText.Append(",,SigFile Version,\"" + getSigFileVersion() + "\"") ;
        fileText.Append(",,Date Created,\"" + writeDisplayDate(DateTime.Now)  + "\"") ;
        fileText.Append("\n") ;
        
        //Write column headers
        fileText.Append("Status,File,Warning,PUID,Format,Version,Status,Warning\n");
        
        //Iterate through IdentifactionFile objects in file collection
        for (fileCounter = 0 ; fileCounter < getNumFiles() ; fileCounter++){
            idFile = getFile(fileCounter) ;
            
            //Write the identification classification , file name and warning(if it exists)
            fileText.Append("\"" + idFile.getClassificationText() + "\"" );
            fileText.Append(",") ;
            fileText.Append("\"" + idFile.getFilePath() + "\"" );
            fileText.Append(",") ;
            fileText.Append("\"" + idFile.getWarning() + "\"" );
            
            fileText.Append("\n") ; //Write new line
            
            //Iterate through file format hits for the identification file
            for (hitCounter = 0 ; hitCounter<idFile.getNumHits() ; hitCounter++){
                hit = idFile.getHit(hitCounter) ;
                //First three columns are empty as these are for file info
                fileText.Append(",,,") ;
                
                //Write the hit PUID,Format Name,Version,Hit Type and warning
                fileText.Append("\"" + hit.GetFileFormatPUID() + "\"" );
                
                fileText.Append(",") ;
                fileText.Append("\"" + hit.GetFileFormatName() + "\"" );
                
                fileText.Append(",") ;
                fileText.Append("\"" + hit.GetFileFormatVersion() + "\"" );
                
                fileText.Append(",") ;
                fileText.Append("\"" + hit.GetHitTypeVerbose() + "\"" );
                
                fileText.Append(",") ;
                fileText.Append("\"" + hit.GetHitWarning() + "\"" );
                
                fileText.Append("\n") ; //Write new line
            }
        }
        
        
        
        //Write the text to specified file
        try
        {
            //java.io.BufferedWriter out = new java.io.BufferedWriter(new java.io.FileWriter(filePath));
            System.IO.StreamWriter outWriter = new System.IO.StreamWriter(filePath);
            outWriter.Write(fileText.ToString());
            outWriter.Close();
        }
        catch (System.IO.IOException)
        {
            //TODO
        }
    }
    
    /**
     * Read in the default file collection file.
     */
    public void readFileCollection() //throws Exception {
    {
        readFileCollection(FILE_LIST_FILE_NAME);  //use default file name
    }
    
    /**
     * Read in a file collection file with the specified file name.
     * @param theFileName Name of the file collection to read in
     */
    public void readFileCollection(string theFileName)
    {
        try
        {
			/*
            checkFile(theFileName);
            
            //prepare for XML read
            MessageDisplay.resetXMLRead();
            
            //prepare to read in the XML file
            SAXParserFactory factory = SAXParserFactory.newInstance();
            factory.setNamespaceAware(true);
            SAXParser saxParser = factory.newSAXParser();
            XMLReader parser = saxParser.getXMLReader();
            SAXModelBuilder mb = new SAXModelBuilder();
            mb.setObjectPackage("FFIT");
            mb.setupNamespace(FILE_COLLECTION_NS, true);
            parser.setContentHandler( mb );
            
            //read in the XML file
            //specify the UTF8 encoding - otherwise will not interpret files with UTF8 characters correctly
            //java.io.BufferedReader in = new java.io.BufferedReader(new java.io.InputStreamReader(new java.io.FileInputStream(theFileName),"UTF8"));
            System.IO.StreamReader inReader = new System.IO.StreamReader(theFileName, Encoding.UTF8);
            parser.parse(new InputSource(inReader));
            myFileCollection = (FileCollection)mb.getModel();
            if (myFileCollection==null) {
                throw new Exception("The file "+theFileName+" doesn't specify any files.");
            }
            myFileCollection.setFileName(theFileName);
            
            //let the user know the outcome
            String successMessage = "The XML file "+theFileName;
            int numXMLWarnings = messageDisplay.getNumXMLWarnings();
            if(numXMLWarnings == 0) {
                successMessage += " was successfully loaded";
            } else if (numXMLWarnings == 1) {
                successMessage += " was loaded with 1 warning";
            } else {
                successMessage += " was loaded with "+numXMLWarnings+" warnings";
            }
            int numLoadedFiles = myFileCollection.getNumFiles();
            if(numLoadedFiles == 0) {
                successMessage += "\nNo files were read in";
            } else if (numLoadedFiles == 1) {
                successMessage += "\n1 file was read in";
            } else {
                successMessage += "\n"+numLoadedFiles+" files were read in";
            }
            messageDisplay.generalInformation(successMessage);
            
            int theFileSigFileVersion =  myFileCollection.getLoadedFileSigFileVersion();
            int theCurrentSigFileVersion = getSigFileVersion();
            if( (theFileSigFileVersion > 0) && (theCurrentSigFileVersion != theFileSigFileVersion)) {
                messageDisplay.generalWarning("The file was generated with signature file V"+theFileSigFileVersion+".  The current signature file is V"+theCurrentSigFileVersion);
            }
            */
            
        }
        catch (Exception e)
        {
            MessageDisplay.GeneralWarning(e.ToString());
            myFileCollection = null;
        }
    }
    
    
    /**
     * Reads and parses the signature file that is pointed to in the configuration file
     */
    public void readSigFile() //throws Exception {
    {
        string theSigFileName = myConfigFile.getSigFileName();
        readSigFile(theSigFileName, true);
    }
    
    
    /**
     * Reads in and parses the signature file
     * Updates the configuration file with this signature file
     * @param theSigFileName Name of the signature file
     */
    public void readSigFile(string theSigFileName) //throws Exception {
    {
        readSigFile(theSigFileName, true);
    }
    
    /**
     * Reads and parses the signature file
     * @param theSigFileName Name of the signature file
     * @param isConfigSave Flag to indicate whether the configuration file should be updated with this signature file.
     */
    public void readSigFile(string theSigFileName, bool isConfigSave) //throws Exception {
    {
        readSigFile(theSigFileName, isConfigSave, false);
    }
    
    /**
     * Reads and parses the signature file
     * @param theSigFileName Name of the signature file
     * @param isConfigSave Flag to indicate whether the configuration file should be updated with this signature file.
     * @param hideWarning gives the ability to hide the warning if a file failes to load
     */
    public void readSigFile(string theSigFileName, bool isConfigSave, bool hideWarning) //throws Exception {
    {
        
        try
        {
            //checks that the file exists, throws an error if it doesn't
            checkFile(theSigFileName);
            
            //store the name of the new signature file
            myConfigFile.setSigFile(theSigFileName);
            
            //prepare for XML read
            MessageDisplay.ResetXmlRead();
            
            //carry out XML read
            mySigFile = parseSigFile(theSigFileName);
            
            mySigFile.prepareForUse();
            
            String theVersion = mySigFile.getVersion();
            try
            {
                myConfigFile.setSigFileVersion(theVersion);
            }
            catch(Exception)
            {
            }
            
            //let the user know the outcome
            MessageDisplay.SetStatusText("Current signature file is V"+getSigFileVersion(), "Signature file V"+getSigFileVersion()+" has been checked");
            int numXMLWarnings = MessageDisplay.GetNumberOfXmlWarnings(); //.getNumXMLWarnings();
            if(numXMLWarnings > 0) {
                String successMessage = "The signature file "+theSigFileName+" was loaded with "+numXMLWarnings+" warnings";
                String cmdlineMessage = numXMLWarnings+" warnings were found";
                MessageDisplay.GeneralInformation(successMessage, cmdlineMessage);
            }
            
            if (isConfigSave)
            {
                //update the configuration file to contain the details of this signature file
                try
                {
                    saveConfiguration();
                }
                catch(System.IO.IOException)
                {
                    MessageDisplay.GeneralWarning("Unable to save configuration updates");
                }
            }            
        }
        catch (Exception e)
        {
            if(!hideWarning)
            {
                MessageDisplay.GeneralWarning(e.ToString());
            }
            mySigFile = null;
        }
    }
    
    /** Create a new signature file object based on a signature file
     * @param theFileName the file name
     */
    public SignatureFile.FFSignatureFile parseSigFile(string theFileName) //throws Exception{
    {
		//TODO: replace SAX logic with DOM
    
		/*
        SAXParserFactory factory = SAXParserFactory.newInstance();
        factory.setNamespaceAware(true);
        SAXParser saxParser = factory.newSAXParser();
        XMLReader parser = saxParser.getXMLReader();
        SAXModelBuilder mb = new SAXModelBuilder();
        mb.setupNamespace(SIGNATURE_FILE_NS, true);
        parser.setContentHandler( mb );
        
        //read in the XML file
        java.io.BufferedReader in = new java.io.BufferedReader(new java.io.InputStreamReader(new java.io.FileInputStream(theFileName),"UTF8"));
        parser.parse( new InputSource(in) );
        return (FFSignatureFile)mb.getModel();
		*/
		return null;
    }
    
    
    /**
     * Checks whether the signature file has been loaded.  If it hasn't, then
     * either exits the application or downloads a new signature file from
     * PRONOM web service.
     */
    public void checkSignatureFile()
    {        
        if (mySigFile == null)
        {
            if (MessageDisplay.ExitDueToMissigSignatureFile()) //.exitDueToMissigSigFile())
            {
                //System.exit(0);
            }
            else
            {
                this.downloadwwwSigFile();
                if(!XmlReader.PronomWebService.isCommSuccess) {
                    //failed to download signature file
                    //Message to warn user
                    string failureMessage = "Unable to connect to the PRONOM web service. Make sure that the following settings in your configuration file (DROID_config.xml) are correct:\n";
                    failureMessage += "    1- <SigFileURL> is the URL of the PRONOM web service.  This should be '"+ AnalysisController.PRONOM_WEB_SERVICE_URL + "'\n";
                    failureMessage += "    2- <ProxyHost> is the IP address of the proxy server if one is required\n";
                    failureMessage += "    3- <ProxyPort> is the port to use on the proxy server if one is required\n\n";
                    failureMessage += "Droid will now close down.";
                    //Warn the user that the connection failed
                    MessageDisplay.FatalError(failureMessage);
                    /*javax.swing.JOptionPane.showMessageDialog(this,failureMessage,"Web service connection error",javax.swing.JOptionPane.WARNING_MESSAGE) ;            */
            
                    //System.exit(0);
                }
            }
        }
    }
    
    /**
     * Checks that a file name corresponds to a file that exists and can be opened
     *
     * @param theFileName   file name
     */
    private void checkFile(string theFileName) //throws Exception  {
	{        
        //java.io.File file = new java.io.File(theFileName);
        System.IO.FileInfo file = new System.IO.FileInfo(theFileName);        
        
        if (!file.Exists)
        {
            throw new Exception("The file "+theFileName+" does not exist");
        }
        //else if (!file.CanRead?)
        //{
            //throw new Exception("The file "+theFileName+" cannot be read");
        //}
        else if (System.IO.Directory.Exists(theFileName)) //file.is .Is .isDirectory())
        {
            throw new Exception("The file " + theFileName + " is a directory");
        }
    }
    
    /**
     * Check whether a file exists
     * @param theFileName The full path of the file to check
     */
    public bool isFileFound(string theFileName)
    {
        //java.io.File file = new java.io.File(theFileName);
        System.IO.FileInfo file = new System.IO.FileInfo(theFileName);
        return file.Exists;
    }
    
    /**
     *Empties the file list
     */
    public void resetFileList(){
        myFileCollection.removeAll();
    }
    
    /**
     * Add files to list of files ready for identification.
     * Calls addFile(fileFolderName, false)
     *@param fileFolderName file or folder to add to
     */
    public void addFile(String fileFolderName) {
        addFile(fileFolderName, false);
    }
    
    /**
     * Add file to list of files ready for identification.
     * If the file is already in list, then does not add it.
     * If the file is a folder, then adds all files it contains.
     * If isRecursive is set to true, then it also searches recursively through any subfolders.
     *@param fileFolderName file or folder to add to
     *@param isRecursive whether or not to search folders recursively
     */
    public void addFile(String fileFolderName, bool isRecursive)
    {
        myFileCollection.addFile(fileFolderName, isRecursive);
    }
    
    /**
     *Remove file from the file list
     *@param theFileName the name of the file to remove
     */
    public void removeFile(String theFileName){
        myFileCollection.removeFile(theFileName);
    }
    
    public void removeFile(int theIndex){
        myFileCollection.removeFile(theIndex) ;
    }
    
    /**
     * Returns an identificationFile object based on its index in the list
     * @param theIndex   index of file in file collection
     */
    public IdentificationFile getFile(int theIndex){
        IdentificationFile theFile=null;
        try {
            theFile = myFileCollection.getFile(theIndex);
            //return myFileCollection.getFile(theIndex);
        }
        catch (Exception)
        {
        }
        return theFile;
    }
    
    /**
     * Returns the number of files in identification file list
     */
    public int getNumFiles(){
        int theNumFiles = 0;
        try
        {
            theNumFiles = myFileCollection.getNumFiles() ;
        }
        catch(Exception)
        {
        }
        return theNumFiles;
        //return myFileCollection.getNumFiles() ;
    }
    
    /**
     * Return the version of the currently loaded signature file
     */
    public int getSigFileVersion() {
        int theVersion = 0;
        try
        {
            theVersion = Convert.ToInt32(mySigFile.getVersion()); //Integer.parseInt(mySigFile.getVersion());
        }
        catch (Exception)
        {
        }
        return theVersion;
        //return Integer.parseInt(mySigFile.getVersion());
    }
    
    /** Return the current proxy host setting */
    public String getProxyHost() {
        return myConfigFile.getProxyHost();
    }
    
    /** Set the proxy host */
    public void setProxyHost(String value) {
        myConfigFile.setProxyHost(value);
    }
    
    /** Return the current proxy port setting */
    public int getProxyPort() {
        return myConfigFile.getProxyPort();
    }
    
    /** Set the proxy port number */
    public void setProxyPort(int value) {
        myConfigFile.setProxyPort(value.ToString()); //Integer.toString(value));
    }
    
    /**
     * Return the version of the FFIT application
     */
    public static String getFFITVersion() {
        String theVersion = FFIT_VERSION;
        
        //remove number after last .  This is a development version, not to be displayed in About box
        int theLastDot = theVersion.LastIndexOf(".");
        if (theLastDot>-1) {
            if(theVersion.IndexOf(".")<theLastDot) {
                theVersion = theVersion.Substring(0,theLastDot);
            }
        }
        return theVersion;
    }
    
    /**
     * Returns the number of files that have been analysed.
     */
    public int getNumCompletedFiles() {
        if(isAnalysisRunning) {
            return myNumCompletedFiles;
        } else {
            int theNumCompletedFiles = 0;
            for(int i = 0; i<getNumFiles(); i++) {
                if (myFileCollection.getFile(i).isClassified()) {
                    theNumCompletedFiles++;
                }
            }
            return theNumCompletedFiles;
        }
        
    }
    
    
    /**
     * Records the fact that analysis has finished and saves to file if a request has been made
     */
    public void setAnalysisComplete() {
        isAnalysisRunning = false;
        
        //save results if requested
        if (myOutFormats.IndexOf("XML")>-1 || myOutFormats.IndexOf("xml")>-1) {
            saveFileList(myOutFileName+".xml", true);
        }
        if(myOutFormats.IndexOf("CSV")>-1 || myOutFormats.IndexOf("csv")>-1) {
            exportFileCollectionAsCSV(myOutFileName+".csv");
        }
        
    }
    
    /**
     * Checks whether analysis has finished yet
     */
    public bool isAnalysisComplete() {
        return !isAnalysisRunning;
    }
    
    /**
     * Record start of anlaysis
     */
    public void setAnalysisStart() {
        myNumCompletedFiles = 0;
        isAnalysisRunning = true;
    }
    
    /**
     * Records the fact that a file has been anlaysed
     */
    public void incrNumCompletedFile() {
        myNumCompletedFiles++;
    }
    
    
    /**
     * Checks whether analysis has been cancelled by the user
     */
    public bool isAnalysisCancelled()
    {
        return myAnalysisCancelled;
    }
    
    /**
     * Cancel the analysis.  This will cause it to stop as soon as it has finished
     * the file it is working on.
     */
    public void cancelAnalysis() {
        myAnalysisCancelled = true;
    }
    
    /**
     * Launch the analysis thread on the files that have been listed and using
     * the signature file that has been opened.
     */
    public void runFileFormatAnalysis() {
        myAnalysisCancelled = false;
        myOutFileName = "";
        myOutFormats = "";
        new AnalysisThread(myFileCollection, mySigFile, this).Start();
    }
    
    /**
     * Launch the analysis thread on the files that have been listed and using
     * the signature file that has been opened.  Save results to file at the end of the run.
     * @param theOutFormats string containing formats for the output (code looks for CSV and XML in the string)
     * @param theOutFileName name of file to which to save results at end of run
     */
    public void runFileFormatAnalysis(String theOutFormats, String theOutFileName) {
        myAnalysisCancelled = false;
        myOutFileName = theOutFileName;
        myOutFormats = theOutFormats;
        new AnalysisThread(myFileCollection, mySigFile, this).Start();
    }
    
    /**
     *Access to the file collection
     *@return the current file collection
     */
    public FileCollection getFileCollection() {
        return myFileCollection ;
    }
    
    /** checks whether there is a signature file available through the PRONOM web service
     * which is a later version than the one currently loaded.
     */
    public bool isNewerSigFileAvailable()
    {
        int theLatestVersion ;
        try {
            theLatestVersion = Convert.ToInt32(PronomWebService.sendRequest(myConfigFile.getSigFileURL(), myConfigFile.getProxyHost(), myConfigFile.getProxyPort(), "getSignatureFileVersion", "Version"));
            sigFileLatestVersion = theLatestVersion ;
            MessageDisplay.SetStatusText("The latest signature file available is V"+sigFileLatestVersion);
        } catch(Exception e) {
            MessageDisplay.GeneralWarning("Unable to get signature file version from PRONOM website:\n"+e.Message);
            return false;
        }
        if(theLatestVersion>this.getSigFileVersion()) {
            return true;
        } else {
            return false;
        }
    }
    
    /** Download the latest signature file from the PRONOM web service, save it to file
     * An input flag determines whether or not to load it in to the current instance of FFIT
     * @param   theFileName file where to save signature file
     * @param   isLoadSigFile Flag indicating whether to load the signature file into the current instance of FFIT
     */
    public void downloadwwwSigFile(string theFileName, bool isLoadSigFile) {
        try
        {
            string theSigFileContent = PronomWebService.sendRequest(myConfigFile.getSigFileURL(), myConfigFile.getProxyHost(), myConfigFile.getProxyPort(),"getSignatureFile", "SignatureFile");
            
            try
            {
                //java.io.BufferedWriter out = new java.io.BufferedWriter(new java.io.FileWriter(theFileName));
				System.IO.StreamWriter outWriter = new System.IO.StreamWriter(theFileName);
                outWriter.Write(theSigFileContent);
                outWriter.Close();
                if (isLoadSigFile)
                {
                    try
                    {
                        myConfigFile.setDateLastDownload();
                        readSigFile(theFileName);
                    }
                    catch (Exception)
                    {
                        MessageDisplay.GeneralWarning("Unable to read in downloaded signature file");
                    }
                }
            }
            catch (Exception)
            {
                MessageDisplay.GeneralWarning("Unable to save downloaded signature file");
            }            
        }
        catch (Exception)
        {
            MessageDisplay.GeneralWarning("Unable to download signature file from the PRONOM web service");
        }        
    }
    
    /** Download the latest signature file from the PRONOM web service, save it to file
     * and load it in to the current instance of FFIT
     * @param   theFileName file where to save signature file
     */
    public void downloadwwwSigFile(String theFileName) {
        downloadwwwSigFile(theFileName, true) ;
    }
    
    /**
     *  Download the latest signature file from the PRONOM web service, save it to a DEFAULT location
     * and load it in to the current instance of FFIT
     * Saves to same folder as current signature file but as
     * FFIT_Signature_V[X].xml , where [X] is the version number of the signature file
     */
    public void downloadwwwSigFile(){
        
        try {
            int theLatestVersion = Convert.ToInt32(PronomWebService.sendRequest(myConfigFile.getSigFileURL(), myConfigFile.getProxyHost(), myConfigFile.getProxyPort(), "getSignatureFileVersion", "Version"));
            
            //java.io.File currentSigFile = new java.io.File(myConfigFile.getSigFileName()) ;
            System.IO.FileInfo currentSigFile = new System.IO.FileInfo(myConfigFile.getSigFileName());
            
            string currentPath = string.Empty;
            
            if (currentSigFile != null)
            {
                currentPath = //(currentSigFile.GetAbsoluteFile()).getParent() + System.IO.Path.PathSeparator; //java.io.File.separator;
                currentSigFile.DirectoryName + System.IO.Path.PathSeparator;
            }
            
            string newSigFileName  = currentPath
            + "DROID_SignatureFile_V"
            + theLatestVersion
            + ".xml" ;
            
            /*
        //Setup save file dialog
        javax.swing.JFileChooser fc = new javax.swing.JFileChooser() ;
        fc.addChoosableFileFilter(new FFIT.GUI.CustomFileFilter(FILE_COLLECTION_FILE_EXTENSTION,FILE_COLLECTION_FILE_DESCRIPTION));
        fc.setAcceptAllFileFilterUsed(false);
        fc.setDialogTitle("Save" + dialogTitle) ;
        //show file dialog
        int returnVal = fc.showSaveDialog(this);
             
        //Save file if user has chosen a file
        if (returnVal==javax.swing.JFileChooser.APPROVE_OPTION){
             
          //Get file user selected
           path = fc.getSelectedFile();
             
           //if no extension was specified add one
           if(!(path.getName().endsWith("." + FILE_COLLECTION_FILE_EXTENSTION))){
                path = new java.io.File(path.getParentFile(), path.getName() + "." + FILE_COLLECTION_FILE_EXTENSTION);
            }
             
           //if path exists check confirm with user if they want to overwrite
           if(path.exists()){
                int option = javax.swing.JOptionPane.showConfirmDialog(this, "The specified file exists, overwrite?");
                if(option != javax.swing.JOptionPane.YES_OPTION)return;
            }*/
            
            downloadwwwSigFile(newSigFileName) ;
        }
        catch (Exception)
        {
            MessageDisplay.GeneralWarning("Unable to download signature file from the PRONOM web service");
        }
        
    }
    
    /**
     * Checks whether a new signature file download is due
     * based on current date and settings in the configuration file
     */
    public bool isSigFileDownloadDue()
    {
        return myConfigFile.isDownloadDue();
    }
    
    /**
     * Returns the date current signature file was created
     * @return  date signature file was created
     */
    public string getSignatureFileDate()
    {
        string theDate = string.Empty;
        try
        {
            theDate = mySigFile.getDateCreated();
        }
        catch(Exception)
        {
        }
        
        if(theDate.Equals(string.Empty))
        {
            theDate = "No date given";
        }
        
        return theDate;
        //return mySigFile.getDateCreated() ;
    }
    
    /**
     * Returns the file path of the signature file
     * @return  signature file file path
     */
    public string getSignatureFileName()
    {
        return myConfigFile.getSigFileName() ;
    }
    
    /**
     * Gets the number of days after which user should be alerted for new signature file
     *@return number of days after which user should be alerted for new signature file
     */
    public int getSigFileCheckFreq(){
        return myConfigFile.getSigFileCheckFreq() ;
    }
    
    /** Updates the configuration parameter which records the interval after which
     * the signature file should be updated.
     * @param theFreq The number of days after which the user will be prompted to check for a newer signature file
     */
    public void setSigFileCheckFreq(String theFreq) {
        myConfigFile.setSigFileCheckFreq(theFreq);
    }
    
    /**
     * updates the DateLastDownload element of the configuration file and updates
     * the configuration file.  This is to be used whenever the user checks for
     * a signature file update, but one is not found
     */
    public void updateDateLastDownload() {
        //set the DateLastDownload to now
        myConfigFile.setDateLastDownload();
        //save to file
        try {
            saveConfiguration();
        } catch(System.IO.IOException) {
            MessageDisplay.GeneralWarning("Unable to save configuration updates");
        }
    }

	}
}
