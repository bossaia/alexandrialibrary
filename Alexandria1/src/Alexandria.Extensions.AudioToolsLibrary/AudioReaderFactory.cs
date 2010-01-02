using System;
using System.IO;
using System.Collections;
//using ATL.Logging;

namespace Alexandria.AudioToolsLibrary
{
	/// <summary>
	/// Factory for metadata and physical data readers
	/// </summary>
	public class AudioReaderFactory
	{
		// Defines the three types of supported "cross-format" metadata
		public const int TAG_ID3V1 = 0;
		public const int TAG_ID3V2 = 1;
		public const int TAG_APE = 2;

		// Count of the types defined above
		private const int TAG_TYPE_COUNT = 3;

		// Codec families
		//public const int CF_LOSSY		= 0; // Streamed, lossy data
		//public const int CF_LOSSLESS	= 1; // Streamed, lossless data
		//public const int CF_SEQ_WAV		= 2; // Sequenced with embedded sound library
		//public const int CF_SEQ			= 3; // Sequenced with codec-dependent sound library

		//public const int NB_CODEC_FAMILIES = 4;

		// Defines the default reading priority of the metadata
		//# to be improved for cross-tag reading
		private static int[] tagPriority = new int[TAG_TYPE_COUNT] { TAG_ID3V2, TAG_APE, TAG_ID3V1 };

		// The instance of this factory
		private static AudioReaderFactory theFactory = null;
	
		// Codec IDs
		//public const int CID_NONE		= -1;
		//public const int CID_MP3		= 0;
		//public const int CID_OGG		= 1;
		//public const int CID_MPC		= 2;
		//public const int CID_FLAC		= 3;
		//public const int CID_APE		= 4;
		//public const int CID_WMA		= 5;
		//public const int CID_MIDI		= 6;
		//public const int CID_AAC		= 7;
		//public const int CID_AC3		= 8;
		//public const int CID_OFR		= 9;
		//public const int CID_WAVPACK	= 10;
		//public const int CID_WAV		= 11;
		//public const int CID_PSF		= 12;
		//public const int CID_SPC		= 13;
		//public const int CID_DTS		= 14;
		//public const int CID_VQF		= 15;

		//public const int NB_CODECS = 16;

		// ------------------------------------------------------------------------------------------
		
		/// <summary>
		/// Gets the instance of this factory (Singleton pattern) 
		/// </summary>
		/// <returns>Instance of the AudioReaderFactory of the application</returns>
		public static AudioReaderFactory GetInstance()
		{
			if (null == theFactory)
			{
				theFactory = new AudioReaderFactory();
			}

			return theFactory;
		}


		/// <summary>
		/// Modifies the default reading priority of the metadata
		/// </summary>
		/// <param name="tag">Identifier of the metadata type</param>
		/// <param name="rank">Reading priority (0..TAG_TYPE_COUNT-1)</param>
		public void SetTagPriority(int tag, int rank)
		{
			tagPriority[rank] = tag;
		}

		
		public static Codec GetFormatIDFromPath(string path)
		{
			//int result = CID_NONE;
			Codec codec = Codec.None;

			if (File.Exists(path))
			{
				string ext = Path.GetExtension(path).ToUpper();

				if ( (".MP3" == ext) || (".MP2" == ext) || (".MP1" == ext) )
				{
					codec = Codec.MP3;
				}

				if ( (".OGG" == ext) )
				{
					codec = Codec.OGG;
				}

				if ( (".MP+" == ext) || (".MPC" == ext) )
				{
					codec = Codec.MPC;
				}

				if ( (".FLAC" == ext) )
				{
					codec = Codec.FLAC;
				}

				if ( (".APE" == ext) )
				{
					codec = Codec.APE;
				}

				if ( (".WMA" == ext) )
				{
					codec = Codec.WMA;
				}

				if ( (".MID" == ext) || (".MIDI" == ext) )
				{
					codec = Codec.MIDI;
				}

				if ( ".AAC" == ext || ext == ".M4A" || ext == "MP4" )
				{
					codec = Codec.AAC;
				}

				if ( ".AC3" == ext )
				{
					codec = Codec.AC3;
				}

				if ( ( ".OFR" == ext ) || (".OFS" == ext ) )
				{
					codec = Codec.OFR;
				}

				if ( ".WV" == ext )
				{
					codec = Codec.WAVPACK;
				}

				if ( ".WAV" == ext )
				{
					codec = Codec.WAV;
				}

				if (".PSF" == ext)
				{
					codec = Codec.PSF;
				}

				if (".SPC" == ext)
				{
					codec = Codec.SPC;
				}
			}

			return codec;
		}

		public IAudioReader GetDataReader(String path)
		{
			return GetDataReader(path, GetFormatIDFromPath(path));
		}

		/// <summary>
		/// Gets the appropriate physical data reader for a given file and format codec
		/// </summary>
		/// <param name="path">The path of the file</param>
		/// <param name="code">The codec of the file</param>
		/// <returns>AudioDataReader able to give info about the file's contents (or the dummy reader if the format is unknown)</returns>
		public IAudioReader GetDataReader(String path, Codec codec)
		{
			IAudioReader theDataReader = null;
			
			switch(codec)
			{
				case Codec.MP3:
					theDataReader = new BinaryLogic.TMPEGaudio();
					break;

				case Codec.OGG:
					theDataReader = new BinaryLogic.TOggVorbis();
					break;

				case Codec.MPC:
					theDataReader = new BinaryLogic.TMPEGplus();
					break;

				case Codec.FLAC:
					theDataReader = new BinaryLogic.TFLACFile();
					break;

				case Codec.APE:
					theDataReader = new BinaryLogic.TMonkey();
					break;

				case Codec.WMA:
					theDataReader = new BinaryLogic.TWMAfile();
					break;

				case Codec.MIDI:
					theDataReader = new BinaryLogic.Midi();
					break;

				case Codec.AAC:
					theDataReader = new BinaryLogic.TAACfile();
					break;

				case Codec.AC3:
					theDataReader = new BinaryLogic.TAC3();
					break;

				case Codec.OFR:
					theDataReader = new BinaryLogic.TOptimFrog();
					break;

				case Codec.WAVPACK:
					theDataReader = new BinaryLogic.TWAVPackfile();
					break;

				case Codec.WAV:
					theDataReader = new BinaryLogic.TWAVfile();
					break;

				case Codec.PSF:
					theDataReader = new BinaryLogic.TPSFFile();
					break;

				case Codec.SPC:
					theDataReader = new BinaryLogic.TSPCFile();
					break;

				default:
					theDataReader = new BinaryLogic.DummyReader();
					break;
			}

			theDataReader.ReadFromFile(path);

			return theDataReader;
		}


		/// <summary>
		/// Gets the appropriate metadata reader for a given file / physical data reader
		/// </summary>
		/// <param name="path">Path of the file</param>
		/// <param name="theDataReader">AudioDataReader produced for this file</param>
		/// <returns>Metadata reader able to give metadata info for this file (or the dummy reader if the format is unknown)</returns>
		public IMetadataReader GetMetaReader(String path, IAudioReader theDataReader)
		{
			IMetadataReader theMetaReader = null;
			
			// Step 1 : The physical reader may have already parsed the metadata
			for (int i=0; i<TAG_TYPE_COUNT; i++)
			{
				if ( (TAG_ID3V1 == tagPriority[i]) && (theDataReader.ID3v1.Exists) )
				{
					theMetaReader = theDataReader.ID3v1; break;
				}
				if ( (TAG_ID3V2 == tagPriority[i]) && (theDataReader.ID3v2.Exists) )
				{
					theMetaReader = theDataReader.ID3v2; break;
				}
				if ( (TAG_APE == tagPriority[i]) && (theDataReader.APEtag.Exists) )
				{
					theMetaReader = theDataReader.APEtag; break;
				}
			}

			// Step 2 : Nothing found in step 1 -> considerate specific tagging (data+meta file formats)
			if (null == theMetaReader)
			{
				//# improve something here
				/*
				if (theDataReader is BinaryLogic.TOggVorbis)
				{
					BinaryLogic.TOggVorbis theVorbis = new BinaryLogic.TOggVorbis();
					theVorbis.ReadFromFile(path);
					theMetaReader = theVorbis;
				}

				if (theDataReader is BinaryLogic.TWMAfile)
				{
					BinaryLogic.TWMAfile theWMA = new BinaryLogic.TWMAfile();
					theWMA.ReadFromFile(path);
					theMetaReader = theWMA;
				}

				if (theDataReader is BinaryLogic.TFLACFile)
				{
					BinaryLogic.TFLACFile theFLAC = new BinaryLogic.TFLACFile();
					theFLAC.ReadFromFile(path);
					theMetaReader = theFLAC;
				}

				if (theDataReader is BinaryLogic.TPSFFile)
				{
					BinaryLogic.TPSFFile thePSF = new BinaryLogic.TPSFFile();
					thePSF.ReadFromFile(path);
					theMetaReader = thePSF;
				}

				if (theDataReader is BinaryLogic.TSPCFile)
				{
					BinaryLogic.TSPCFile theSPC = new BinaryLogic.TSPCFile();
					theSPC.ReadFromFile(path);
					theMetaReader = theSPC;
				}*/
				if ((theDataReader is BinaryLogic.TOggVorbis) ||
					(theDataReader is BinaryLogic.TWMAfile) ||
					(theDataReader is BinaryLogic.TFLACFile) ||
					(theDataReader is BinaryLogic.TPSFFile) ||
					(theDataReader is BinaryLogic.TSPCFile) )
				{
					theMetaReader = (IMetadataReader)theDataReader; // Boorish but correct cast
				}
			}

			// Step 3 : default (no tagging at all - provides the dummy reader)
			if (null == theMetaReader) theMetaReader = new BinaryLogic.DummyTag();

			return theMetaReader;
		}
	}
}
