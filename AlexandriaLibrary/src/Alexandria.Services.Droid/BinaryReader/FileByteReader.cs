using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Droid.BinaryReader
{
	public class FileByteReader : AbstractByteReader
	{
	    /**
     * Creates a new instance of FileByteReader
     *
     * <p>This constructor can set the <code>IdentificationFile</code> to 
     * a different file than the actual file used. For example, if <code>theIDFile</code>
     * is a URL or stream, and is too big to be buffered in memory, it could be written
     * to a temporary file.  This file would then be used as a backing file to store
     * the data.
     * 
     * @param theIDFile the file represented by this object
     * @param readFile <code>true</code> if the file is to be read
     * @param filePath the backing file (containing the data)
     */
    public FileByteReader(IdentificationFile theIDFile, bool readFile, String filePath) : base(theIDFile)
    {
        //super(theIDFile);
        this.file = new System.IO.FileInfo(filePath);
        if (readFile)
        {
            this.readFile();
        }
        
    }
    
    /**
     * Creates a new instance of FileByteReader
     *
     * <p>This constructor uses the same file to contain the data as is specified by
     * <code>theIDFile</code>.
     * 
     * @param theIDFile the source file from which the bytes will be read.
     * @param readFile <code>true</code> if the file is to be read
     */   
    public FileByteReader(IdentificationFile theIDFile, bool readFile) : this(theIDFile, readFile, theIDFile.getFilePath())
    {        
    }
    
    private byte[] fileBytes;
    private long myNumBytes;
    private long fileMarker;
    private bool isRandomAccess=false;
    private System.IO.FileStream myRandomAccessFile; //RandomAccessFile myRandomAccessFile;
    private long myRAFoffset = 0L;
    private int randomFileBufferSize = AnalysisController.FILE_BUFFER_SIZE;
    private static int MIN_RAF_BUFFER_SIZE = 1000000;
    private static int RAF_BUFFER_REDUCTION_FACTOR = 2;
    private System.IO.FileInfo file;
    
    /**
     * Reads in the binary file specified.
     *
     * <p>If there are any problems reading in the file, it gets classified as unidentified,
     * with an explanatory warning message.
     */
    private void readFile() {
        
        //If file is not readable or is empty, then it gets classified
        //as unidentified (with an explanatory warning)
        
        if( !file.Exists )
        {
            this.SetErrorIdentification();
            this.SetIdentificationWarning("File does not exist");
            return;
        }
        
        //TODO: figure out how to port this
        /*
        if( !file.canRead() )
        {
            this.SetErrorIdentification();
            this.SetIdentificationWarning("File cannot be read");
            return;
        }
        */
        
        if (System.IO.Directory.Exists(file.FullName))
        {
            this.SetErrorIdentification();
            this.SetIdentificationWarning("This is a directory, not a file");
            return;
        }
        
        //FileInputStream binStream;
        System.IO.FileStream binStream;
        
        try
        {
            binStream = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open); //FileInputStream(file);
        }
        catch (System.IO.FileNotFoundException)
        {
            this.SetErrorIdentification();
            this.SetIdentificationWarning("File disappeared or cannot be read");
            return;
        }
        
        try {
            
            int numBytes = 100; //binStream.available();
            
            if (numBytes > 0)
            {
                //BufferedInputStream buffStream = new BufferedInputStream(binStream);
                System.IO.BufferedStream buffStream = new System.IO.BufferedStream(binStream);
                
                fileBytes = new byte[numBytes];
                int len = buffStream.Read(fileBytes, 0, numBytes);
                
                if(len != numBytes) {
                    //This means that all bytes were not successfully read
                    this.SetErrorIdentification();
                    this.SetIdentificationWarning("Error reading file: "+ len.ToString() + " bytes read from file when " + numBytes.ToString() + " were expected");
                }
                else if(len != -1)
                {
                    //This means that the end of the file was not reached
                    this.SetErrorIdentification();
                    this.SetIdentificationWarning("Error reading file: Unable to read to the end");
                }
                else
                {
                    myNumBytes = (long) numBytes;
                }
                
                buffStream.Close();
            } else {
                //If file is empty , status is error
                this.SetErrorIdentification();
                myNumBytes = 0L;
                this.SetIdentificationWarning("Zero-length file");
                
            }
            binStream.Close();
            
            isRandomAccess = false;
        } catch(System.IO.IOException e) {
            this.SetErrorIdentification();
            this.SetIdentificationWarning("Error reading file: " + e.ToString());
        }
        catch(System.OutOfMemoryException)
        {
            try {
                myRandomAccessFile = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open); //RandomAccessFile(file,"r");
                isRandomAccess = true;
                
                //record the file size
                myNumBytes = myRandomAccessFile.Length;
                //try reading in a buffer
                myRandomAccessFile.Seek(0, System.IO.SeekOrigin.Begin); //(0L);
                bool tryAgain = true;
                while(tryAgain) {
                    try
                    {
                        fileBytes = new byte[(int)randomFileBufferSize];
                        myRandomAccessFile.Read(fileBytes, 0, randomFileBufferSize);
                        // .read(fileBytes);
                        tryAgain = false;
                    }
                    catch(OutOfMemoryException e4)
                    {
                        randomFileBufferSize = randomFileBufferSize/RAF_BUFFER_REDUCTION_FACTOR;
                        if(randomFileBufferSize< MIN_RAF_BUFFER_SIZE) {
                            throw e4;
                        }
                        
                    }
                }
                
                myRAFoffset = 0L;
            }
            catch (System.IO.FileNotFoundException)
            {
                this.SetErrorIdentification();
                this.SetIdentificationWarning("File disappeared or cannot be read");
            }
            catch(Exception e2)
            {
                try
                {
                    myRandomAccessFile.Close();
                }
                catch(System.IO.IOException)
                {
                }
                
                this.SetErrorIdentification();
                this.SetIdentificationWarning("Error reading file: " + e2.ToString());
            }
            
        }
    }    
    
    /**
     * Position the file marker at a given byte position.
     *
     * <p>The file marker is used to record how far through the file
     * the byte sequence matching algorithm has got.
     *
     * @param markerPosition   The byte number in the file at which to position the marker
     */
    public void setFileMarker(long markerPosition) {
        if ((markerPosition<-1L) || (markerPosition>this.getNumBytes()))
        {
            //throw new IllegalArgumentException
            throw new ArgumentException("  Unable to place a fileMarker at byte "
                    + markerPosition.ToString() + " in file "+this.myIDFile.getFilePath()+" (size = " + getNumBytes().ToString() + " bytes)");
        } else {
            this.fileMarker = markerPosition;
        }
    }
    
    /**
     * Gets the current position of the file marker.
     * @return the current position of the file marker
     */
    public long getFileMarker() { 
        return this.fileMarker; 
    }
    
    /**
     * Get a byte from file
     * @param fileIndex position of required byte in the file
     * @return the byte at position <code>fileIndex</code> in the file
     */
    public byte getByte(long fileIndex) {
        
        byte theByte=0;
        if(isRandomAccess) {
            //If the file is being read via random acces,
            //then read byte from buffer, otherwise read in a new buffer.
            long theArrayIndex = fileIndex-myRAFoffset;
            if(fileIndex>=myRAFoffset && theArrayIndex<randomFileBufferSize) {
                theByte = fileBytes[(int)(theArrayIndex)];
            } else {
                try {
                    //Create a new buffer:
                    /*
                    //When a new buffer is created, the requesting file position is
                    //taken to be the middle of the buffer.  This is so that it will
                    //perform equally well whether the file is being examined from
                    //start to end or from end to start
                    myRAFoffset = fileIndex - (myRAFbuffer/2);
                    if(myRAFoffset<0L) {
                        myRAFoffset = 0L;
                    }
                    System.out.println("    re-read file buffer");
                    myRandomAccessFile.seek(myRAFoffset);
                    myRandomAccessFile.read(fileBytes);
                    theByte = fileBytes[(int)(fileIndex-myRAFoffset)];
                     */
                    if(fileIndex<randomFileBufferSize) {
                        myRAFoffset = 0L;
                    } else if(fileIndex<myRAFoffset) {
                        myRAFoffset = fileIndex-randomFileBufferSize+1;
                    } else {
                        myRAFoffset = fileIndex;
                    }
                    //System.out.println("    re-read file buffer from "+myRAFoffset+ " for "+myRAFbuffer+" bytes");
                    //System.out.println("    seek start");
                    myRandomAccessFile.Seek(myRAFoffset, System.IO.SeekOrigin.Begin); // myRAFoffset
                    //System.out.println("        read start");
                    myRandomAccessFile.Read(fileBytes, 0, fileBytes.Length); // fileBytes
                    //System.out.println("            read end");
                    theByte = fileBytes[(int)(fileIndex-myRAFoffset)];
                    
                }
                catch(Exception)
                {
                }
            }
        }
        else
        {
            //If the file is not being read by random access, then the byte should be in the buffer array
            theByte = fileBytes[(int)fileIndex];
        }
        return theByte;
    }
    
    
    
    /**
     * Returns the number of bytes in the file
     */
		public long getNumBytes()
		{
			return myNumBytes;
		}
	}
}
