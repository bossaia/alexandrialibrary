using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;

namespace AlexandriaOrg.Alexandria.Droid.XmlReader
{
	public class PronomWebService
	{
	    /** flag to indicate whether a successful communication was established via the web service */
		public static bool isCommSuccess = false;
    
		/** Exposes the PRONOM web service.  This is the only method required to call a
		 * web service.  The URL of the web service must be provided.  The web server
		 * name or IP address is then extracted from the URL.
		 * @param theWebServiceURL  Full URL to webservice
		 * @param theMethodName     Name of the web service method.  The suffix "request"
		 * is appended for the SOAP request and the suffix "response" is appended for the
		 * SOAP response.
		 * @param theXMLWrapper     Name of XML Element which wraps the result inside the web service response
		 */
		public static String sendRequest(string theWebServiceURL, string theProxyHost, int theProxyPort, string theMethodName, string theXMLWrapper) //throws Exception{		
		{
			//assume that the communication fails.  If it succeeds, the flag will be reset at the end
			isCommSuccess = false;
		    
			String theResult = "";
		    
			//set up the URL
			//URL myUrl = null;
			Uri myUrl = null;
			try {
				myUrl = new Uri(theWebServiceURL); //URL(theWebServiceURL);
			}
			catch (Exception e) //MalformedURLException e)
			{
				throw new //WSException("Invalid URL for PRONOM web services" + theWebServiceURL +"\n"+e.getMessage());
				AlexandriaException("Invalid URL for PRONOM web services" + theWebServiceURL + "\n" + e.Message);
			}
		    
		    
			try {
				// Create and send a message
				System.Web.Services.Description.Message aMessage = new System.Web.Services.Description.Message();
		        
				//If a proxy server is required for connecting to web service, then record its settings
				try {
					if(theProxyHost.Length > 0 && theProxyPort > 0)
					{
						//TODO: figure out how to port this
						/*
						org.apache.soap.transport.http.SOAPHTTPConnection aSOAPConnection = new org.apache.soap.transport.http.SOAPHTTPConnection();
						aSOAPConnection.setProxyHost(theProxyHost);
						aSOAPConnection.setProxyPort(theProxyPort);
						aMessage.setSOAPTransport(aSOAPConnection);
						*/
					}
				}
				catch (Exception e)
				{
					throw new //WSException("Error while creating the proxy settings" +"\n"+e.getMessage());
					AlexandriaException("Error while creating the proxy settings" + "\n" + e.Message);
				}
		        
				//send the SOAP message to webservice
		        
				//TODO: figure out how to port this
				/*
				aMessage.send(myUrl, theWebServiceURL, createEnvelope(theMethodName));            
		        
				//Receive the message response
				org.apache.soap.transport.SOAPTransport st = aMessage.getSOAPTransport();
				BufferedReader br = st.receive( );
				String line = br.readLine( );
				if(line == null) {
				} else {
					while (line != null) {
						theResult += line;
						line = br.readLine( );
					}
				}
				*/
			}
			catch (Exception e)
			{
				throw new //WSException("Error while sending message to PRONOM web services" +"\n"+e.getMessage());
				AlexandriaException("Error while sending message to PRONOM web services" +"\n"+e.Message);
			}
		    
		    
			// theResult now contains the complete result from the
			// webservice in XML format.
			try
			{
				theResult = PronomWebService.extractXMLelement(theResult, theMethodName + "Response");
			}
			catch(Exception)
			{
				throw new AlexandriaException("Unexpected response from PRONOM web service");
			}
		    
			try
			{
				theResult = PronomWebService.extractXMLelement(theResult, theXMLWrapper);
			}
			catch(Exception)
			{
				throw new AlexandriaException("Response from PRONOM web service did not contain the expected element");
			}
		    
			//indicate that the communication was successful
			isCommSuccess = true;
		    
			return theResult;
		}
    
    
		/** Create a SOAP envelope containing the web services request
		 * @param theMethodName  Name of the SOAP operation to call
		 */

		//TODO: figure out how to port this code 
		/*
		private static Envelope createEnvelope(String theMethodName)
		{
			// Create the required SOAP elements
			Envelope anEnvelope = new Envelope();
			Body aBody = new Body();
			Vector entries = new Vector();
			org.w3c.dom.Document doc = null;
		    
			// Put the request element into the body of the SOAP
			javax.xml.parsers.DocumentBuilder xdb = org.apache.soap.util.xml.XMLParserUtils.getXMLDocBuilder( );
			try {
				doc = xdb.newDocument();
				doc.appendChild(doc.createElement(theMethodName+"Request"));
			}
			catch (Exception e) {
			}
			entries.add(doc.getDocumentElement( ));
		    
			// Set up the SOAP components
			aBody.setBodyEntries(entries);
			anEnvelope.setBody(aBody);
		    
			return anEnvelope;
		}
		*/
    
		/** Extracts the contents of the named XML element from within a string
		* @param theStream  The full String
	    * @param theElement  Name of the XML element to be extracted from the string
		*/
	    private static string extractXMLelement(string theStream, string theElement)
	    {
			int start = theStream.IndexOf("<" + theElement + ">") +
		    theElement.Length + 2;
	        int end = theStream.IndexOf("</" + theElement + ">");
        
			//Extract a singe return parameter
			return theStream.Substring(start, end);
		}
	}
}
