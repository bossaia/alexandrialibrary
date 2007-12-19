#region License (MIT)
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.IO;
using System.Net;
using System.Xml;

using Telesophy.Alexandria.Model;

namespace Alexandria.MusicDns
{
	/// <summary>
	/// This is factory for creating MusicDNS PUID's
	/// </summary>
	public class MetadataFactory
	{	
		#region Private Constants
		private const string musicDnsKey = "5fe4267b3a269f9463cd2a7f14ac6406";
		private const string url = "http://ofa.musicdns.org/ofa/1/track";
		private const string metadataNamespace = "http://musicbrainz.org/ns/mmd/1/";
		private const string fingerprint_request_format = "cid={0}&cvr={1}&fpt={2}&rmd={3:d}&brt={4:d}&fmt={5}&dur={6:d}&art={7}&ttl={8}&alb={9}&tnm={10:d}&gnr={11}&yrr={12}&enc={13}";
		private const string puid_request_format = "cid={0}&cvr={1}&uid={2}&rmd={3:d}&brt={4:d}&fmt={5}&dur={6:d}&art={7}&ttl={8}&alb={9}&tnm={10:d}&gnr={11}&yrr={12}&enc={13}";
		private const string clientVersion = "1.0.0.0";
		private const string waveFormat = "wav";
		private const string unknown = "unknown";
		#endregion
	
		//TODO: refactor these methods into the TrackInfo class
		#region Private Methods

		#region OldInitializeTrackValue
		private void InitializeTrackInfo(IntPtr buffer, int bufferLength, bool isLittleEndian, int sampleRate, bool isStereo)
		{
			int major = 0;
			int minor = 0;
			int revision = 0;
			NativeMethods.ofa_get_version(ref major, ref minor, ref revision);
			Version version = new Version(major, minor, revision, 0);

			byte[] bufferArray = new byte[bufferLength];
			//Marshal.Copy(buffer, bufferArray, 0, bufferLength);

			int numberOfSamples = bufferLength;
			if (isStereo) numberOfSamples /= 2;

			//IntPtr puidBuffer = IntPtr.Zero;
			string puidValue = string.Empty;
			int byteOrder = (isLittleEndian) ? 0 : 1;
			int stereo = (isStereo) ? 1 : 0;
			puidValue = NativeMethods.ofa_create_print(bufferArray, byteOrder, numberOfSamples, sampleRate, isStereo);

			//puidValue = Marshal.PtrToStringAnsi(puidBuffer);
			//Guid guid = new Guid(puidValue);
			//return new Puid(guid, version);
		}
		#endregion
		
		#region InitializeTrackInfo
		private TrackInfo InitializeTrackInfo(Uri path)
		{
			int srate = 0;
			int channels = 0;

			FileStream fs = File.Open(path.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
			if (fs == null)
				return null;


			if (fs.Seek(0, SeekOrigin.Begin) == -1L)
			{
				fs.Close();
				return null;
			}

			byte[] hdr = new byte[36];

			if (fs.Read(hdr, 0, 36) <= 0)
			{
				fs.Close();
				return null;
			}
			if (hdr[0] != 'R' || hdr[1] != 'I' || hdr[2] != 'F' || hdr[3] != 'F')
			{
				fs.Close();
				return null;
			}
			// Note: bytes 4 thru 7 contain the file size - 8 bytes
			if (hdr[8] != 'W' || hdr[9] != 'A' || hdr[10] != 'V' || hdr[11] != 'E')
			{
				fs.Close();
				return null;
			}
			if (hdr[12] != 'f' || hdr[13] != 'm' || hdr[14] != 't' || hdr[15] != ' ')
			{
				fs.Close();
				return null;
			}

			long extraBytes = hdr[16] + (hdr[17] << 8) + (hdr[18] << 16) + (hdr[19] << 24) - 16;
			int compression = hdr[20] + (hdr[21] << 8);
			// Type 1 is PCM/Uncompressed
			if (compression != 1)
			{
				fs.Close();
				return null;
			}
			channels = hdr[22] + (hdr[23] << 8);
			// Only mono or stereo PCM is supported in this example
			if (channels < 1 || channels > 2)
			{
				fs.Close();
				return null;
			}
			// Samples per second, independent of number of channels
			srate = hdr[24] + (hdr[25] << 8) + (hdr[26] << 16) + (hdr[27] << 24);
			// Bytes 28-31 contain the "average bytes per second", unneeded here
			// Bytes 32-33 contain the number of bytes per sample (includes channels)
			// Bytes 34-35 contain the number of bits per single sample
			int bits = hdr[34] + (hdr[35] << 8);
			// Supporting other sample depths will require conversion
			if (bits != 16)
			{
				fs.Close();
				return null;
			}

			// Skip past extra bytes, if any
			if (fs.Seek(36L + extraBytes, SeekOrigin.Begin) == -1L)
			{
				fs.Close();
				return null;
			}

			// Start reading the next frame.  Only supported frame is the data block
			byte[] b = new byte[8];
			if (fs.Read(b, 0, 8) <= 0)
			{
				fs.Close();
				return null;
			}
			// Do we have a fact block?
			if (b[0] == 'f' && b[1] == 'a' && b[2] == 'c' && b[3] == 't')
			{
				// Skip the fact block
				if (fs.Seek(36L + extraBytes + 12L, SeekOrigin.Begin) == -1L)
				{
					fs.Close();
					return null;
				}
				// Read the next frame
				if (fs.Read(b, 0, 8) <= 0)
				{
					fs.Close();
					return null;
				}
			}

			// Now look for the data block
			if (b[0] != 'd' || b[1] != 'a' || b[2] != 't' || b[3] != 'a')
			{
				fs.Close();
				return null;
			}
			int bytes = b[4] + (b[5] << 8) + (b[6] << 16) + (b[7] << 24);

			int ms = (bytes / 2) / (srate / 1000);
			if (channels == 2) ms /= 2;

			// No need to read the whole file, just the first 135 seconds
			int size = bytes;
			int sampleSize = 135;
			int bytesInNSecs = sampleSize * srate * 2 * channels;
			bytes = bytes > bytesInNSecs ? bytesInNSecs : bytes;

			byte[] samples = new byte[bytes];
			if (fs.Read(samples, 0, bytes) <= 0)
			{
				fs.Close();
				return null;
			}

			fs.Close();

			//AudioData data = new AudioData();
			//data.SetData(samples, Constants.OFA_LITTLE_ENDIAN, bytes/2, srate,
			//channels == 2 ? true : false, ms, Constants.WavFileExtension);

			bool stereo = (channels == 2) ? true : false;
			string fingerprint = NativeMethods.ofa_create_print(samples, 0, (int)bytes / 2, srate, stereo);
			
			TrackInfo info = new TrackInfo();
			info.FileName = path.ToString();
			info.Fingerprint = fingerprint;
			info.LengthInMS = ms;
			info.Format = waveFormat;			
			return info;
		}
		#endregion
		
		#region FillTrackInfo
		private void FillTrackInfo(TrackInfo info, bool lookupByFingerprint, bool getMetadata)
		{
			string requestString = url + "?" + string.Format(fingerprint_request_format,
				musicDnsKey.Trim(),
				clientVersion.Trim(),
				lookupByFingerprint ? info.Fingerprint : info.Puid,
				getMetadata ? "1" : "0",
				info.BitRate,
				info.Format, 
				info.LengthInMS, 
				string.IsNullOrEmpty(info.Artist) ? unknown : info.Artist,
				string.IsNullOrEmpty(info.Track) ? unknown : info.Track,
				string.IsNullOrEmpty(info.Album) ? unknown : info.Album,
				info.TrackNum, 
				string.IsNullOrEmpty(info.Genre) ? unknown : info.Genre,
				string.IsNullOrEmpty(info.Year) ? "0" : info.Year,
				info.Encoding);
					
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestString);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);

			#region Old Metadata Code
			//metadata meta = new metadata();
			//meta.Namespace = metadataNamespace;
			//meta.puid.Columns["id"].Namespace = "";
			//meta.puid.Columns["id"].Prefix = "";
			//meta.ReadXml(streamRead);
			#endregion
			
			string responseXml = reader.ReadToEnd();

			reader.Close();
			response.Close();

			XmlDocument xml = new XmlDocument();			
			xml.LoadXml(responseXml);
						
			foreach(XmlNode rootNode in xml.ChildNodes)
			{
				if (string.Compare(rootNode.Name, "metadata", true) == 0)
				{
					foreach (XmlNode metadataNode in rootNode.ChildNodes)
					{
						if (string.Compare(metadataNode.Name, "track", true) == 0)
						{
							foreach (XmlNode trackNode in metadataNode.ChildNodes)
							{
								if (string.Compare(trackNode.Name, "artist", true) == 0)
								{
									foreach (XmlNode artistNode in trackNode.ChildNodes)
									{
										if (string.Compare(artistNode.Name, "name", true) == 0)
											info.Artist = artistNode.InnerXml; //TODO: remove &amp; encoding
									}
								}
								else if (string.Compare(trackNode.Name, "puid-list", true) == 0)
								{
									foreach (XmlNode puidListNode in trackNode.ChildNodes)
									{
										if (string.Compare(puidListNode.Name, "puid", true) == 0)
											info.Puid = puidListNode.Attributes["id"].InnerXml;
									}
								}
							}
						}
					}
				}
			}

			#region Old Metadata Code
			/*
			if(meta.track.Rows.Count > 0)
			{
				if(!meta.track[0].IstitleNull())
					info.Track = meta.track[0].title;


				metadata._puid_listRow[] plrs = meta.track[0]._Getpuid_listRows();
				if(plrs.Length > 0)
				{
					metadata.puidRow[] prs = plrs[0].GetpuidRows();
				
					
					if((prs.Length > 0)&&(!prs[0].IsidNull()))
						info.Puid = prs[0].id;
				}
				

				metadata.artistRow[] ars = meta.track[0].GetartistRows();

				if((ars.Length > 0)&&(!ars[0].IsnameNull()))
					info.Artist = ars[0].name;
			}
			*/

			//MusicBrainz lookup
			//http://musicbrainz.org/show/puid/?puid=2e6d085b-bf25-10d7-4bce-66f21de0e798
			#endregion

			int major = 0;
			int minor = 0;
			int revision = 0;
			NativeMethods.ofa_get_version(ref major, ref minor, ref revision);
			Version version = new Version(major, minor, revision, 0);
			info.Version = version;
		}
		#endregion
	
		#endregion
	
		#region Public Methods
		public IMediaItem CreateAudioTrack(Uri path)
		{
			TrackInfo info = InitializeTrackInfo(path);
			FillTrackInfo(info, true, true);			
			return info;
		}
		#endregion							
	}
}
