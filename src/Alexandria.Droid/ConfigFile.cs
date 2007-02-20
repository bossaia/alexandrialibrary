using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Droid
{
	public class ConfigFile : XmlReader.SimpleElement
	{
		#region Private Fields
	    /** The full path of the configuration file */
		private string myFileName = AnalysisController.CONFIG_FILE_NAME;
		/** The full path of the signature file */
		private string mySigFileName = AnalysisController.SIGNATURE_FILE_NAME;
		/** The version of the signature file referred to by mySigFileName */
		private int mySigFileVersion = 0;
		/** The full URL for the PRONOM web service */
		private string mySigFileURL = AnalysisController.PRONOM_WEB_SERVICE_URL;
		/** Proxy server (IP address) */
		private String myProxyHost = "";
		/** Proxy server port */
		private int myProxyPort = 0;
		/** Time interval (in days) after which to check whether a newer signature file exists */
		private int myDownloadFreq = AnalysisController.CONFIG_DOWNLOAD_FREQ;
		/** Date of last signature file download */
		private DateTime myLastDownloadDate;
		#endregion
    
		#region Public Methods
		
		#region Setters
		/** Set the configuration file name */
		public void setFileName(String theFileName) {
			myFileName = theFileName;
		}
	    
		/** set the signature file name */
		public void setSigFile(String theFileName) {
			this.mySigFileName = theFileName;
		}
	    
		/** Set the signature file version */
		public void setSigFileVersion(String theVersion) {
			try
			{
				this.mySigFileVersion = Convert.ToInt32(theVersion); //Integer.parseInt(theVersion);
			}
			catch(Exception)
			{
			}
		}
	    
		/** Set the signature file version */
		public void setSigFileVersion(int theVersion) {
			this.mySigFileVersion = theVersion;
		}
	    
		/** Set the URL for PRONOM web service */
		public void setSigFileURL(String theURL) {
			this.mySigFileURL = theURL;
		}
	    
		/** Set the Proxy Server name (IP address) */
		public void setProxyHost(String theProxyHost) {
			this.myProxyHost = theProxyHost;
		}
	    
		/** Set the proxy server port */
		public void setProxyPort(String theProxyPort) {
			if(theProxyPort.Trim().Length > 0) {
				try
				{
					this.myProxyPort = Convert.ToInt32(theProxyPort); //Integer.parseInt(theProxyPort);
				}
				catch(System.FormatException) // NumberFormatException
				{
					//the port number is not translatable to an integer
					this.myProxyPort = 0;
					MessageDisplay.GeneralWarning("Unable to read the proxy server port settings\nMake sure that the <ProxyPort> element in the configuration file is an integer.");
				}
			}
		}
	    
		/** Set the interval (in days) after which to check for newer signature file */
		public void setSigFileCheckFreq(String theFreq)
		{
			try
			{
				this.myDownloadFreq = Convert.ToInt32(theFreq); //Integer.parseInt(theFreq);
			}
			catch(Exception)
			{
				MessageDisplay.GeneralWarning("Unable to read the signature file download frequency\nMake sure that the <SigFileCheckFreq> element in the configuration file is an integer number of days.\nThe default value of " + Convert.ToString(AnalysisController.CONFIG_DOWNLOAD_FREQ) + " days will be used");
				//Integer.toString(AnalysisController.CONFIG_DOWNLOAD_FREQ) + " days will be used");
			}
		}
	    
		/** Set the date of the last signature file download
		* @param theDate the date to use entered as a string
		*/
		public void setDateLastDownload(string theDate)
		{
			//java.text.DateFormat df;
			try
			{
				this.myLastDownloadDate = AnalysisController.parseXMLDate(theDate);
			}
			catch (InvalidCastException) //ParseException pe)
			{
				try
				{ 
					//df = java.text.DateFormat.getDateTimeInstance(java.text.DateFormat.MEDIUM, java.text.DateFormat.MEDIUM);
					//this.myLastDownloadDate = df.parse(theDate);
					this.myLastDownloadDate = Convert.ToDateTime(theDate);
				}
				catch(InvalidCastException) // ParseException e1)
				{
					try
					{
						//df = java.text.DateFormat.getDateTimeInstance(java.text.DateFormat.FULL, java.text.DateFormat.FULL);
						this.myLastDownloadDate = Convert.ToDateTime(theDate); //df.parse(theDate);
					}
					catch (InvalidCastException) //ParseException e2)
					{
						this.myLastDownloadDate = DateTime.MinValue;
					}
				}
			}
		}
	    
		/** Set the date of the last signature file download to now */
		public void setDateLastDownload()
		{
			//java.util.Date now = new java.util.Date();
			DateTime now = DateTime.Now;
			
			try
			{
				this.myLastDownloadDate = now;
			}
			catch(Exception)
			{
				this.myLastDownloadDate = DateTime.Now;
			}
		}
		#endregion
    
		#region Getters
		/** Get the name of the configuration file */
		public String getFileName() { return myFileName; }
	    
		/** Get the name of the signature file */
		public String getSigFileName() { return mySigFileName; }
	    
		/** Get the version of the current signature file */
		public int getSigFileVersion() { return mySigFileVersion; }
	    
		/** Get the URL for the PRONOM web service */
		public String getSigFileURL() { return mySigFileURL; }
	    
		/** Get the proxy server IP address */
		public String getProxyHost() { return myProxyHost; }
	    
		/** Get the proxy server port */
		public int getProxyPort() { return myProxyPort; }
	    
		/** Get the interval in days after which to check whether a newer signature file exists */
		public int getSigFileCheckFreq() { return myDownloadFreq; }
	    
		/** Get the date of the last signature file download */
		public DateTime getLastDownloadDate() { return myLastDownloadDate; }
		#endregion
    
		/** Check whether a newer signature file is available on the PRONOM web service */
		public bool isDownloadDue()
		{
			if (myLastDownloadDate == DateTime.MinValue) //null)
			{
				return true;
			}
			DateTime theNow = DateTime.Now;
			long elapsedTime = (theNow.Ticks - myLastDownloadDate.TimeOfDay.Ticks); //.getTime());
			long theThreshold = myDownloadFreq*24L*3600L*1000L;
        
	        if(elapsedTime > theThreshold)
	        {
				return true;
			}
			else
			{
				return false;
			}
		}
    
		/** Saves the current configuration to file in XML format
		*/
		public void saveConfiguration() //throws IOException
		{
			System.IO.FileStream stream = new System.IO.FileStream(myFileName, System.IO.FileMode.OpenOrCreate);
			//System.IO.BufferedStream outStream = new System.IO.BufferedStream(new System.IO.StreamWriter(new System.IO.FileStream(myFileName, System.IO.FileMode.Open), Encoding.UTF8));
			System.IO.StreamWriter outStream = new System.IO.StreamWriter(stream, Encoding.UTF8);
			//myFileName, Encoding.UTF8);
			outStream.Write(getConfigurationXMLJDOM());
			outStream.Close();
			//java.io.BufferedWriter out = new java.io.BufferedWriter(new java.io.OutputStreamWriter(new java.io.FileOutputStream(myFileName),"UTF8"));
			//out.write(getConfigurationXMLJDOM());
			//out.close();
		}
    
		/**
		*Creates a Configuration XML Document representing the current configuration
		*Uses JDOM
		*@return the xml document
		*/
		private string getConfigurationXMLJDOM()
		{
			/*
			//create all elements that will be used
			Namespace ns = Namespace.getNamespace(AnalysisController.CONFIG_FILE_NS);
			org.jdom.Element config_file = new org.jdom.Element("ConfigFile", ns);
			org.jdom.Element sig_file = new org.jdom.Element("SigFile", ns);
			org.jdom.Element sig_file_version = new org.jdom.Element("SigFileVersion", ns);
			org.jdom.Element sig_file_url = new org.jdom.Element("SigFileURL", ns);
			org.jdom.Element proxy_host = new org.jdom.Element("ProxyHost", ns);
			org.jdom.Element proxy_port = new org.jdom.Element("ProxyPort", ns);
			org.jdom.Element sig_file_check_freq = new org.jdom.Element("SigFileCheckFreq", ns);
			org.jdom.Element date_last_download = new org.jdom.Element("DateLastDownload", ns);
        
			//populate the elements
			sig_file.setText(getSigFileName());
			sig_file_version.setText(Integer.toString(getSigFileVersion()));
			sig_file_url.setText(getSigFileURL());
			proxy_host.setText(getProxyHost());
			int aProxyPort = getProxyPort();
			if(aProxyPort>0)
			{
				proxy_port.setText(Integer.toString(aProxyPort));
			}
			else
			{
				proxy_port.setText("");
			}
			sig_file_check_freq.setText(Integer.toString(getSigFileCheckFreq()));
			try
			{
				date_last_download.setText(FFIT.AnalysisController.writeXMLDate(getLastDownloadDate()));
			}
			catch (Exception e)
			{
				date_last_download.setText("");
			}
			config_file.addContent(sig_file);
			config_file.addContent(sig_file_version);
			config_file.addContent(sig_file_url);
			config_file.addContent(proxy_host);
			config_file.addContent(proxy_port);
			config_file.addContent(sig_file_check_freq);
			config_file.addContent(date_last_download);
        
			org.jdom.Document theJDOMdocument = new org.jdom.Document(config_file) ;
        
			//write it all to a String
			org.jdom.output.Format xmlFormat = org.jdom.output.Format.getPrettyFormat();
			org.jdom.output.XMLOutputter outputter = new org.jdom.output.XMLOutputter(xmlFormat) ;
			java.io.StringWriter writer = new java.io.StringWriter();
        
			try
			{
				outputter.output(theJDOMdocument, writer);
				writer.close();
			}
			catch (System.IO.IOException e) // java.io.IOException e){
            {
			}
			return writer.toString();
			*/
			return null;
		}
		#endregion
	}
}
