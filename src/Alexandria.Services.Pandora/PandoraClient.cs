using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using Alexandria;

namespace Alexandria.Pandora
{
	public partial class PandoraClient
	{
		#region Constructors
		public PandoraClient()
		{
			//InitializeComponent();
			//this.Show();
			FindFiles();
			if (!System.IO.Directory.Exists(downloadDirectoryPath))
			{
				System.IO.Directory.CreateDirectory(downloadDirectoryPath);
			}
		}
		#endregion
		
		#region Private Fields
		private Dictionary<string, string> downloadURLList = new Dictionary<string, string>();
		private Dictionary<string, string> songTitleList = new Dictionary<string, string>();
		private Dictionary<string, string> artistNameList = new Dictionary<string, string>();
		private Dictionary<string, string> albumTitleList = new Dictionary<string, string>();
		private Dictionary<int, int> viewCountRelation = new Dictionary<int, int>();
		private WebClient client = new WebClient();
		private Queue<int> downloadQueue = new Queue<int>();
		private int count = 0;
		private bool findingFiles = false;
		private bool downloadingData = false;
		private string downloadDirectoryPath = @"C:\Pandora\";
		//System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		#endregion

		#region Private Methods
		
		#region CheckData
		private void CheckData(string localName)
		{
			downloadQueue.Clear();
		
			XmlTextReader reader = new XmlTextReader(localName);
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);
			reader.Close();
			int tagsPerSong = 0;

			XmlElement root = doc.DocumentElement;
			XmlNodeList list = root.SelectNodes("//methodResponse/params/param/value/array/data/value/struct/member");
			XmlNodeList songs = root.SelectNodes("//methodResponse/params/param/value/array/data/value");
			if ((list.Count % songs.Count) == 0)
			{
				tagsPerSong = list.Count / songs.Count;
			}
			else
			{
				//MessageBox.Show("Error in format, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//Application.Exit();
			}

			string artistSummary = null;
			string albumTitle = null;
			string audioUrl = null;
			string songTitle = null;

			for (int i = 0; i < list.Count; i++)
			{
				// Corresponds to a particular row
				XmlElement member = (XmlElement)list[i];

				string memberXml = member.InnerXml;
				int nameStart = memberXml.IndexOf("<name>");
				int nameEnd = memberXml.IndexOf("</name>");
				int valueStart = memberXml.IndexOf("<value>");
				int valueEnd = memberXml.IndexOf("</value>");

				string memberName = memberXml.Substring(nameStart + 6, nameEnd - nameStart - 6);
				string memberValue = memberXml.Substring(valueStart + 7, valueEnd - valueStart - 7);

				if (memberName == "artistSummary") artistSummary = memberValue;
				if (memberName == "albumTitle") albumTitle = memberValue;
				if (memberName == "audioURL") audioUrl = memberValue;
				if (memberName == "songTitle") songTitle = memberValue;

				if ((i != 0) && (((i + 1) % tagsPerSong) == 0))
				{
					// Access for a song completed
					count++;
					string[] itemValues = new string[3];
					itemValues[0] = songTitle;
					itemValues[1] = artistSummary;
					itemValues[2] = albumTitle;
					//ListViewItem item = new ListViewItem(itemValues);
					//listView1.Items.Add(item);
					//viewCountRelation.Add(listView1.Items.Count - 1, count);
					downloadURLList.Add(count.ToString(), audioUrl);
					songTitleList.Add(count.ToString(), songTitle);
					artistNameList.Add(count.ToString(), artistSummary);
					albumTitleList.Add(count.ToString(), albumTitle);
					
					// For now, add all avaialble items to the download queue
					downloadQueue.Enqueue(count);

					// Refresh for new values
					artistSummary = null;
					albumTitle = null;
					audioUrl = null;
					songTitle = null;
				}
			}
		}
		#endregion

		#region CheckDownloadQueue
		private void CheckDownloadQueue()
		{
			if (!downloadingData)
			{
				if (downloadQueue.Count != 0)
				{
					int i = downloadQueue.Dequeue();
					try
					{
						if (System.IO.File.Exists(downloadDirectoryPath + artistNameList[i.ToString()] + " - " + songTitleList[i.ToString()] + ".mp3"))
						{
							//DialogResult dr = MessageBox.Show("File has already been downloaded. Do you want to download it again?", "File already Downloaded", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
							bool dummyResult = true;
							if (dummyResult) //(dr == DialogResult.OK)
							{
								try
								{
									Uri url = new Uri(downloadURLList[i.ToString()]);
									client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
									client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
									client.DownloadFileAsync(url, downloadDirectoryPath + artistNameList[i.ToString()] + " - " + songTitleList[i.ToString()] + ".mp3");
									//label4.Text = "Currently downloading " + songTitleList[i.ToString()] + " by " + artistNameList[i.ToString()] + " from album " + albumTitleList[i.ToString()];
									downloadingData = true;
									//progressBar1.Value = 0;
								}
								catch (KeyNotFoundException)
								{
									//drexit = DialogResult.Yes;
									//MessageBox.Show("A butterfly fluttered in South America. I can no longer exits. Please call me again if you feel like", "Butterfly effect", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
								}
							}
							else
							{
								CheckDownloadQueue();
							}
						}
						else
						{
							Uri url = new Uri(downloadURLList[i.ToString()]);
							client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
							client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
							client.DownloadFileAsync(url, downloadDirectoryPath + artistNameList[i.ToString()] + " - " + songTitleList[i.ToString()] + ".mp3");
							//label4.Text = "Currently downloading " + songTitleList[i.ToString()] + " by " + artistNameList[i.ToString()] + " from album " + albumTitleList[i.ToString()];
							downloadingData = true;
							//progressBar1.Value = 0;
						}
					}
					catch (WebException)
					{
						//MessageBox.Show("Please check your internet connection.", "Connection Failure", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						downloadQueue.Clear();
					}
					catch (InvalidOperationException)
					{
						//MessageBox.Show("File in use by another application.", "File in user", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						CheckDownloadQueue();
					}
				}
				else
				{
					//label4.Text = "No songs in Queue";
					//progressBar1.Value = 0;
				}
			}
		}
		#endregion

		#region Private Event Methods
		
		#region Legacy Code
		/*
		#region OnCloseClick
		private void OnCloseClick(object sender, EventArgs e)
		{
			if (downloadingData)
			{
				//drexit = MessageBox.Show("Data download is in progress. Are you sure you want to quit?", "Data Downloading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				bool dummyResult = false;				
				if (dummyResult) //(drexit == DialogResult.Yes)
				{
					client.CancelAsync();
					//Application.Exit();
				}
			}
			else
			{
				//Application.Exit();
			}
		}
		#endregion

		#region OnRefreshClick
		private void OnRefreshClick(object sender, EventArgs e)
		{
			//if (downloadingData)
				//MessageBox.Show("Please wait for current downloads to finish", "Please wait", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			//else if (findingFiles)
				//MessageBox.Show("Please wait for current search to finish", "Please wait", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			//else
				//findFiles();
		}
		#endregion

		#region OnDownloadClick
		private void OnDownloadClick(object sender, EventArgs e)
		{
			// Find which items have been selected
			//for (int i = 0; i < listView1.Items.Count; i++)
			//{
				//if (listView1.Items[i].Checked)
				//{
					//downloadQueue.Enqueue(viewCountRelation[i]);
					//listView1.Items[i].Checked = false;
				//}
			//}
			//if (!downloadingData)
				//checkDownloadQueue();
		}
		#endregion
		 */
		#endregion
		
		#region client_DownloadFileCompleted
		void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			//progressBar1.Value = 0;
			CheckDownloadQueue();
			//label4.Text = "No songs in Queue";
			downloadingData = false;
		}
		#endregion

		#region client_DownloadProgressChanged
		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			//bool dummyResult = false;			
			//if (dummyResult) //(drexit == DialogResult.Yes)
			//{
				//client.CancelAsync();
				//Application.Exit();
			//}
			//progressBar1.Value = e.ProgressPercentage;
		}
		#endregion

		#endregion
		
		#endregion
		
		#region Public Properties
		public string DownloadDirectoryPath
		{
			get { return downloadDirectoryPath; }
			set { downloadDirectoryPath = value; }
		}
		
		public bool FindingFiles
		{
			get { return findingFiles; }
		}
		
		public bool DownloadingData
		{
			get { return downloadingData; }
		}
		#endregion
		
		#region Public Methods
		
		#region FindFiles
		public void FindFiles()
		{
			findingFiles = true;

			const int ERROR_NO_MORE_ITEMS = 259;

			viewCountRelation.Clear();
			downloadURLList.Clear();
			songTitleList.Clear();
			albumTitleList.Clear();
			artistNameList.Clear();
			//listView1.Items.Clear();

			// Local variables
			int cacheEntryInfoBufferSizeInitial = 0;
			int cacheEntryInfoBufferSize = 0;
			IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
			INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
			IntPtr enumHandle = IntPtr.Zero;
			bool returnValue = true;
			bool tempFileFound = false;

			// Start to delete URLs that do not belong to any group.
			enumHandle = NativeMethods.FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
			if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
				return;

			cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
			cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
			enumHandle = NativeMethods.FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);

			while (returnValue)
			{
				internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
				string localName = (string)Marshal.PtrToStringAnsi(internetCacheEntry.lpszLocalFileName);
				string remoteName = (string)Marshal.PtrToStringAnsi(internetCacheEntry.lpszSourceUrlName);
				if ((localName != null) && (remoteName != null))
				{
					if ((localName.Substring(localName.Length - 3).ToLower() == "xml") && (remoteName.Contains("pandora.com")) && (remoteName.Contains("getFragment")))
					{
						CheckData(localName);
						tempFileFound = true;
					}
				}

				cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
				if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
				{
					returnValue = false;
				}
				else
				{
					returnValue = NativeMethods.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
				}
				if (cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
				{
					cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
					cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
					returnValue = NativeMethods.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
				}
				if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
				{
					returnValue = false;
					if (!tempFileFound)
					{
						//MessageBox.Show("No data available yet. Please run Pandora.com in Internet Explorer", "Need to run pandora.com", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
			Marshal.FreeHGlobal(cacheEntryInfoBuffer);
			findingFiles = false;
		}
		#endregion
		
		#region DownloadFiles
		public void DownloadFiles()
		{
			if (!downloadingData)
				CheckDownloadQueue();
		}
		#endregion
		
		#region CancelDownload
		public void CancelDownload()
		{
			if (downloadingData)
			{
				client.CancelAsync();
			}
		}
		#endregion
		
		#endregion
	}
}