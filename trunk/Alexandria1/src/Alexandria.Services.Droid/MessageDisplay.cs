using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid
{
	public sealed class MessageDisplay
	{
		#region Private Static Fields
		//private static FFIT.GUI.FileIdentificationPane myMainPane = null;
		private static bool isGUIdisplay = false;
		private static bool hideXMLWarnings = false;
		private static int numberOfXmlWarnings = 0;
		#endregion
		
		#region Public Static Fields
	    public static string FILEEXTENSIONWARNING = "Possible file extension mismatch";
		public static string POSITIVEIDENTIFICATIONSTATUS = "Positively identified";
		public static string TENTATIVEIDENTIFICATIONSTATUS = "Tentatively identified";
		public static string UNIDENTIFIEDSTATUS = "Unable to identify";
	    #endregion
		
	    #region Public Static Methods
		/** Displays a special warning for unknown XML elements when reading XML files
		 * @param unknownElement The name of the element which was not recognised
		 * @param containerElement The name of the element which contains the unrecognised element
		 */
		public static void UnknownElementWarning(string unknownElement, string containerElement)
		{
			string theCMDMessage = "WARNING: Unknown XML element "+unknownElement+" found under "+containerElement+" ";
			string theGUIMessage = theCMDMessage+"\nDo you wish to hide any further XML reading errors for this file?";
			numberOfXmlWarnings++;
			
			if(isGUIdisplay)
			{
				if (!hideXMLWarnings)
				{
					hideXMLWarnings = false; //(javax.swing.JOptionPane.showConfirmDialog(myMainPane,theGUIMessage,"DROID warning", javax.swing.JOptionPane.YES_NO_OPTION,javax.swing.JOptionPane.WARNING_MESSAGE) == 0);
				}
			}
			else
			{
				System.Console.WriteLine(theCMDMessage); //System.out.println(theCMDMessage);
			}
		}
    
		/** Displays a special warning for unknown XML attributes when reading XML files
		* @param unknownAttribute The name of the attribute which was not recognised
		* @param containerElement The name of the element which contains the unrecognised attribute
		*/
		public static void UnknownAttributeWarning(string unknownAttribute, string containerElement)
		{
			string theCMDMessage = "WARNING: Unknown XML attribute "+unknownAttribute+" found for "+containerElement+" ";
			string theGUIMessage = theCMDMessage+"\nDo you wish to hide any further XML reading errors for this file?";
			numberOfXmlWarnings++;
			if (isGUIdisplay)
			{
				if (!hideXMLWarnings)
				{
					hideXMLWarnings = false; //(javax.swing.JOptionPane.showConfirmDialog(myMainPane,theGUIMessage,"DROID warning",javax.swing.JOptionPane.YES_NO_OPTION,javax.swing.JOptionPane.WARNING_MESSAGE) == 0);
				}
			}
			else
			{
				System.Console.WriteLine(theCMDMessage); //System.out.println(theCMDMessage);
			}
		}
    
		/** Displays a general warning
		* @param theWarning The text to be displayed
		*/
		public static void GeneralWarning(string theWarning)
		{
			string theMessage = "WARNING: " + theWarning; //.replaceFirst("java.lang.Exception: ","");
			if (isGUIdisplay)
			{
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theMessage,"DROID warning",javax.swing.JOptionPane.WARNING_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theMessage); //System.out.println(theMessage);
			}
		}

    
		/** Displays general information
		* @param theMessage The text to be displayed
		*/
		public static void GeneralInformation(string theMessage)
		{
			if(isGUIdisplay)
			{
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theMessage,"DROID information",javax.swing.JOptionPane.INFORMATION_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theMessage); //System.out.println(theMessage);
			}
		}
    
		/** Displays general information
		* @param theGUIMessage The text to be displayed in GUI mode
		* @param theCMDMessage The text to be displayed in command line mode
		*/
		public static void GeneralInformation(string theGUIMessage, string theCMDMessage)
		{
			if(isGUIdisplay)
			{
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theGUIMessage,"DROID information",javax.swing.JOptionPane.INFORMATION_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theCMDMessage); //System.out.println(theCMDMessage);
			}
		}

		/** Displays general information in the status bar
		* @param theMessage The text to be displayed
		*/
		public static void SetStatusText(string theMessage)
		{
			if(isGUIdisplay)
			{
				//myMainPane.setStatusText(theMessage);
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theMessage,"FFIT information",javax.swing.JOptionPane.INFORMATION_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theMessage); //System.out.println(theMessage);
			}
		}
    
		/** Displays general information in the status bar
		* @param theGUIMessage The text to be displayed in the status bar
		* @param theCMDMessage The text to be displayed in command line mode
		*/
		public static void SetStatusText(string theGUIMessage, string theCMDMessage)
		{
			if(isGUIdisplay)
			{
				//myMainPane.setStatusText(theGUIMessage);
			}
			else
			{
				System.Console.WriteLine(theCMDMessage); //System.out.println(theCMDMessage);
			}
		}
    
		/** Displays a general error
		* @param theWarning The text to be displayed
		*/
		public static void GeneralError(String theWarning) //throws Exception
		{
			string theMessage = "Error: "+theWarning;
			if(isGUIdisplay)
			{
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theMessage,"DROID error",javax.swing.JOptionPane.ERROR_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theMessage); //System.out.println(theMessage);
			}
			throw new AlexandriaException(theWarning);
		}

		/** Displays a fatal error and then exits
		* @param theWarning The text to be displayed
		*/
		public static void FatalError(String theWarning)
		{
			String theMessage = "Fatal Error: "+theWarning;
			if (isGUIdisplay)
			{
				//javax.swing.JOptionPane.showMessageDialog(myMainPane,theMessage,"DROID fatal error",javax.swing.JOptionPane.ERROR_MESSAGE);
			}
			else
			{
				System.Console.WriteLine(theMessage); //System.out.println(theMessage);
			}
			//System.exit(0);
		}
    
		/** Returns the accumulated number of XML warnings that have been received since this was last reset
		*/
		public static int GetNumberOfXmlWarnings()
		{
			return numberOfXmlWarnings;
		}
    
		/** Method to be called each time an XML file read is about to start:
		* sets the number of warnings to 0 and ensures that warnings are displayed again
		*/
		public static void ResetXmlRead()
		{
			numberOfXmlWarnings = 0;
			hideXMLWarnings = false;
		}
    
		/** Define the main pane to use for displaying GUI messages
		* @param theMainPane The pane to use for displaying GUI messages
		*/
		public static void InitialiseMainPane() //FFIT.GUI.FileIdentificationPane theMainPane)
		{
			//myMainPane = theMainPane;
			isGUIdisplay = true;
		}
    
		/** Respond to missing signature file on startup
		*/
		public static bool ExitDueToMissigSignatureFile()
		{
			if(isGUIdisplay)
			{
				//Confirm with user whether they would like to download new signature file
				// javax.swing.JOptionPane.showConfirmDialog(myMainPane,"Signature file was not found.\nDo you wish to download a new one from PRONOM web service?\nSelecting NO will exit the application." ,"Signature file not found",javax.swing.JOptionPane.YES_NO_OPTION,javax.swing.JOptionPane.QUESTION_MESSAGE) == javax.swing.JOptionPane.YES_OPTION
				bool downloadNewSignatureFile = true;
				if (downloadNewSignatureFile)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				System.Console.WriteLine("Signature file not loaded");
				return true;
			}
		}
		#endregion
	}
}
