//---------- LICENSED UNDER LGPL PLEASE READ CAREFULLY -------------
// IMDBServices - a library that offers acces to IMDB (Home Page:  htpp://sourceforge.net/projects/imdb-services)
//    Copyright (C) 2005 Sebastian Bota (eMail:  sebutzu@gmail.com)
//    This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option) any later version.
//    This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
//    You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Sockets;


namespace AlexandriaOrg.Alexandria.Imdb
{
	public class ImdbParser
	{

		public string FileCachePath = "";
		private string LoadString(string fileName)
		{
			if (FileCachePath.Length == 0)
				return "";

			string fName = FileCachePath + fileName;

			if (!File.Exists(fName))
				return "";

			StreamReader sr = new StreamReader(fName);
			string result = sr.ReadToEnd();
			sr.Close();
			sr.Dispose();

			return result;
		}
		private void SaveString(string fileName, string text)
		{
			if (FileCachePath.Length == 0)
				return;

			string fName = FileCachePath + fileName;

			StreamWriter sw = new StreamWriter(fName);
			sw.Write(text);
			sw.Close();
		}

		public string GetString(string fileName, string url)
		{
			string Page = "";
			WebClient web = new WebClient();

			try
			{
				Page = LoadString(fileName);
				if (Page.Length == 0)
				{

					//byte[] bytes = new byte[2*1024 * 1024];//2 Mb
					//Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					//s.ReceiveBufferSize = 1024 * 64;
					//s.Connect("www.imdb.com", 80);
					//String req = "GET " + url + " HTTP/1.1[CRLF]Host: www.imdb.com[CRLF]Connection: close[CRLF]Accept: text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5[CRLF]Accept-Language: en-us,en;q=0.5[CRLF]Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7[CRLF]User-Agent: Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.3) Gecko/20060426 Firefox/1.5.0.3[CRLF][CRLF]";
					//req = req.Replace("[CRLF]", "\r\n");
					//s.Send(Encoding.ASCII.GetBytes(req));
					//Page = "";
					//Int32 count = s.Receive(bytes, bytes.Length, 0);
					//Page += ASCIIEncoding.ASCII.GetString(bytes, 0, count);
					//while (count > 0)
					//{
					//    count = s.Receive(bytes, bytes.Length, 0);
					//    Page += ASCIIEncoding.ASCII.GetString(bytes, 0, count);
					//}
					//Page = Page.Substring(Page.IndexOf("<html>"));

					//===========

					HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.imdb.com" + url);
					request.AllowAutoRedirect = true;
					request.KeepAlive = true;
					//request.ConnectionGroupName
					request.UserAgent = global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString();
					//"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.2) Gecko/20060308 Firefox/1.5.0.2";
					
					String encoding = "utf-8";
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					if (response.ContentEncoding != String.Empty)
					{
						encoding = response.ContentEncoding;
					}
					System.IO.StreamReader stream = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
					Page = stream.ReadToEnd();
					stream.Close();


					//                    web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
					//                    Page = web.DownloadString(url);
					SaveString(fileName, Page);
				}
				Page = Decode(Page);
			}
			catch
			{
				Page = "";
			}
			return Page;
		}

		public byte[] LoadData(string fileName)
		{
			if (FileCachePath.Length == 0)
				return null;

			string fName = FileCachePath + fileName;

			if (!File.Exists(fName))
				return null;

			FileStream fs = new FileStream(fName, FileMode.Open);
			if (fs.Length == 0)
			{
				fs.Close();
				return null;
			}

			BinaryReader br = new BinaryReader(fs);
			byte[] result = br.ReadBytes((int)fs.Length);
			br.Close();
			fs.Close();

			return result;
		}

		public void SaveData(string fileName, byte[] data)
		{
			if (FileCachePath.Length == 0)
				return;

			string fName = FileCachePath + fileName;

			FileStream fs = new FileStream(fName, FileMode.CreateNew);
			BinaryWriter bw = new BinaryWriter(fs);
			bw.Write(data);
			bw.Close();
			fs.Close();
		}


		public ImdbParser()
		{
			#region Load the regex-es!
			try
			{
				rPowerSearch_Matches = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PowerSearch_Matches"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPowerSearch_OLRegion = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PowerSearch_OLRegion"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPowerSearch_LIRegion = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PowerSearch_LIRegion"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPowerSearch_MainValues = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PowerSearch_MainValues"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPowerSearch_AKA = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PowerSearch_AKA"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				rMovieSearch_LI = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieSearch_LI"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieSearch_MainValues = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieSearch_MainValues"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieSearch_AKA = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieSearch_AKA"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				rMovieDetails_Title = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Title"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Year = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Year"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Poster = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Poster"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Genre = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Genre"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_RatingVotes = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_RatingVotes"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_AKAS = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_AKAS"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_AKA = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_AKA"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_MPAA = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_MPAA"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Plot = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Plot"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Directors = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Directors"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Director = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Director"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Writers = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Writers"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Writer = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Writer"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Actors = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Actors"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Actor = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Actor"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rMovieDetails_Code = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["MovieDetails_Code"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

				rPersonDetails_Name = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Name"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_Headshot = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Headshot"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_DateOfBirth = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_DateOfBirth"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_Directed = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Directed"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_LIRegion = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_LIRegion"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_MainValues = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_MainValues"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_AKA = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_AKA"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_Writed = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Writed"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_Acted = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Acted"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_Bio = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_Bio"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_MostVoted = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_MostVoted"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_TRRegion = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_TRRegion"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
				rPersonDetails_MostVotedMain = new Regex(global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["PersonDetails_MostVotedMain"].ToString(), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

			}
			catch
			{
				//Here we will probably log an error!!!                
			}
			#endregion
		}

		#region Regex-es declarations
		private Regex rPowerSearch_Matches;
		private Regex rPowerSearch_OLRegion;
		private Regex rPowerSearch_LIRegion;
		private Regex rPowerSearch_MainValues;
		private Regex rPowerSearch_AKA;

		private Regex rMovieSearch_LI;
		private Regex rMovieSearch_MainValues;
		private Regex rMovieSearch_AKA;

		private Regex rMovieDetails_Title;
		private Regex rMovieDetails_Year;
		private Regex rMovieDetails_Poster;
		private Regex rMovieDetails_Genre;
		private Regex rMovieDetails_RatingVotes;
		private Regex rMovieDetails_AKAS;
		private Regex rMovieDetails_AKA;
		private Regex rMovieDetails_MPAA;
		private Regex rMovieDetails_Plot;
		private Regex rMovieDetails_Directors;
		private Regex rMovieDetails_Director;
		private Regex rMovieDetails_Writers;
		private Regex rMovieDetails_Writer;
		private Regex rMovieDetails_Actors;
		private Regex rMovieDetails_Actor;
		private Regex rMovieDetails_Code;


		private Regex rPersonDetails_Name;
		private Regex rPersonDetails_Headshot;
		private Regex rPersonDetails_DateOfBirth;
		private Regex rPersonDetails_Directed;
		private Regex rPersonDetails_LIRegion;
		private Regex rPersonDetails_MainValues;
		private Regex rPersonDetails_AKA;
		private Regex rPersonDetails_Writed;
		private Regex rPersonDetails_Acted;
		private Regex rPersonDetails_Bio;
		private Regex rPersonDetails_MostVoted;
		private Regex rPersonDetails_TRRegion;
		private Regex rPersonDetails_MostVotedMain;
		#endregion


		//Deprecated
		//private string Page;

		//public String ErrorMessage;

		//Retrieve movie data for fast selections...
		public List<Movie> GetBasicMovieData()
		{
			string Page = "";

			List<Movie> bmd = new List<Movie>();

			WebClient web = new WebClient();
			web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
			Page = web.DownloadString("http://us.imdb.com/List?showmyvotes=off&&page=/Title&&vid=on&&votes=5&&tv=on&&exact=off&&tvm=on&&skip=0");

			MatchCollection vMatchCollection_Matches = rPowerSearch_Matches.Matches(Page);
			if (vMatchCollection_Matches.Count != 1)
			{
				//Parsing error - must have exactly ONE match = the number of matches returned by search
			}
			int iNumber = 0;
			foreach (Match vMatch_Matches in vMatchCollection_Matches)
			{
				iNumber = int.Parse(vMatch_Matches.Groups["Matches"].Value);
			}

			Movie m;
			for (int i = 0; i < iNumber; i += 200)
			{
				//Yesss I am downloading this URL twice...so WHAT?
				web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
				Page = web.DownloadString("http://us.imdb.com/List?showmyvotes=off&&page=/Title&&vid=on&&votes=5&&tv=on&&exact=off&&tvm=on&&skip=" + i.ToString());
				//Position = 0;

				MatchCollection vMC_OLRegion = rPowerSearch_OLRegion.Matches(Page);
				if (vMC_OLRegion.Count != 1)
				{
					//Parsing error - must have exactly ONE match = <OL> ... </OL> region
				}
				foreach (Match vM_OLRegion in vMC_OLRegion)
				{
					MatchCollection vMC_LIRegion = rPowerSearch_LIRegion.Matches(vM_OLRegion.Groups["OLRegion"].Value);
					if (vMC_LIRegion.Count < 1 || vMC_LIRegion.Count > 200)
					{
						//Parsing error!!!too many LIs
					}
					foreach (Match vM_LIRegion in vMC_LIRegion)
					{
						MatchCollection vMC_MainValues = rPowerSearch_MainValues.Matches(vM_LIRegion.Groups["LIRegion"].Value);
						if (vMC_MainValues.Count != 1)
						{
							//Parsing...should be only one value per LI
						}
						foreach (Match vM_MainValues in vMC_MainValues)
						{
							m = new Movie();
							m.iCode = int.Parse(vM_MainValues.Groups["Code"].Value);
							m.lTitles.Add(new Movie.MovieTitle(vM_MainValues.Groups["Title"].Value, false));
							m.iYear = vM_MainValues.Groups["Year"].Value == "????" ? 0 : int.Parse(vM_MainValues.Groups["Year"].Value);
							m.iRating = (int)(10 * double.Parse(vM_MainValues.Groups["Rating"].Value));
							m.iVotes = int.Parse(vM_MainValues.Groups["Votes"].Value);
							MatchCollection vMC_AKA = rPowerSearch_AKA.Matches(vM_MainValues.Groups["Akas"].Value);
							foreach (Match vM_AKA in vMC_AKA)
							{
								m.lTitles.Add(new Movie.MovieTitle(vM_AKA.Groups["Title"].Value, true));
							}
							bmd.Add(m);
						}
					}
				}
			}
			return bmd;
		}

		public List<Movie> SearchMovie(String TitlePart)
		{
			string Page = "";

			List<Movie> result = new List<Movie>();
			//          String searchRequest = "http://www.imdb.com/find?more=tt;q=" + TitlePart.Replace(" " , "%20");
			String searchRequest = "http://www.imdb.com/find?s=tt;q=" + TitlePart.Replace(" ", "%20");

			WebClient web = new WebClient();
			web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
			Page = web.DownloadString(searchRequest);
			Page = Decode(Page);

			if (Page.IndexOf("<title>IMDb") != -1)
			{
				//Normal result as list of movies
				MatchCollection vMC_LI = rMovieSearch_LI.Matches(Page);
				if (vMC_LI.Count < 1)
				{
					//Parsing error!!!no LI found!!!
				}
				foreach (Match vM_LI in vMC_LI)
				{
					MatchCollection vMC_MainValues = rMovieSearch_MainValues.Matches(vM_LI.Groups["LI"].Value);
					if (vMC_MainValues.Count != 1)
					{
						//Parsing error!!!WTF?
					}
					foreach (Match vM_MainValues in vMC_MainValues)
					{
						Movie m = new Movie();
						m.iCode = long.Parse(vM_MainValues.Groups["Code"].Value);
						m.lTitles.Add(new Movie.MovieTitle(vM_MainValues.Groups["Title"].Value, false));
						String sYear = vM_MainValues.Groups["Year"].Value;
						if (sYear == "????")
						{
							m.iYear = 0;
						}
						else
						{
							m.iYear = int.Parse(sYear);
						}
						MatchCollection vMC_AKA = rMovieSearch_AKA.Matches(vM_MainValues.Groups["Akas"].Value);
						foreach (Match vM_AKA in vMC_AKA)
						{
							m.lTitles.Add(new Movie.MovieTitle(vM_AKA.Groups["Title"].Value, true));
						}
						result.Add(m);
					}
				}
			}
			else
			{
				//Damn we are lucky...and got a precize movie match
				//Position = 0;
				Movie b = new Movie();

				MatchCollection vMC_Title = rMovieDetails_Title.Matches(Page);
				if (vMC_Title.Count != 1)
				{
					//WTF? Something fishy happened!
				}
				foreach (Match vM_Title in vMC_Title)
				{
					b.lTitles.Add(new Movie.MovieTitle(vM_Title.Groups["Title"].Value, false));
				}

				MatchCollection vMC_Year = rMovieDetails_Year.Matches(Page);
				if (vMC_Year.Count != 1)
				{
					//WTF?
				}
				foreach (Match vM_Year in vMC_Year)
				{
					b.iYear = int.Parse(vM_Year.Groups["Year"].Value);
				}

				MatchCollection vMC_AKAS = rMovieDetails_AKAS.Matches(Page);
				if (vMC_AKAS.Count != 1)
				{
					//WTF?
				}
				foreach (Match vM_AKAS in vMC_AKAS)
				{
					MatchCollection vMC_AKA = rMovieDetails_AKA.Matches(vM_AKAS.Groups["Akas"].Value);
					if (vMC_AKA.Count < 1)
					{
						//WTF?
					}
					foreach (Match vM_AKA in vMC_AKA)
					{
						b.lTitles.Add(new Movie.MovieTitle(QCleanup(vM_AKA.Groups["Aka"].Value), true));
					}
				}

				//Must find the damn CODE!!!
				MatchCollection vMC_Code = rMovieDetails_Code.Matches(Page);
				if (vMC_Code.Count != 1)
				{
					//WTF?
				}
				foreach (Match vM_Code in vMC_Code)
				{
					b.iCode = long.Parse(vM_Code.Groups["Code"].Value);
				}

				result.Add(b);

			}
			return result;
		}



		public void FillMovieDetailsAsync(AsyncCommandFillMovie cmd, IAsyncProcessorDone processor)
		{
			//do the stuff!
			cmd.movie.iCode = cmd.code;
			string strMovieCode = cmd.code.ToString("0000000");

			Thread tMain = new Thread(new ParameterizedThreadStart(this.ThreadMovieMain));
			tMain.Start(new MovieParameters(cmd.movie, strMovieCode));

			Thread tPlot = new Thread(new ParameterizedThreadStart(this.ThreadMoviePlot));
			tPlot.Start(new MovieParameters(cmd.movie, strMovieCode));

			Thread tCredits = new Thread(new ParameterizedThreadStart(this.ThreadMovieCredits));
			tCredits.Start(new MovieParameters(cmd.movie, strMovieCode));

			tMain.Join();
			cmd.callback.OnFillMovieDetailsAsyncProgress(cmd.parameters);
			tPlot.Join();
			cmd.callback.OnFillMovieDetailsAsyncProgress(cmd.parameters);
			tCredits.Join();

			cmd.callback.OnFillMovieDetailsAsyncDone(cmd.parameters);

			processor.Done(cmd);
		}

		private int ParseYear(String strYear)
		{
			if (strYear == null)
				return 0;
			if (strYear.Length != 4)
				return 0;
			if (strYear == "????")
				return 0;
			int result = 0;
			try
			{
				result = int.Parse(strYear);
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		private bool ParseMovieMain(String Page, Movie movie)
		{
			String ErrorMessage = "";

			if (Page == null)
			{
				ErrorMessage += "FATAL: ParseMovieMain: Main page is null!\n";
				return false;
			}

			if (Page.Length == 0)
			{
				ErrorMessage += "FATAL: ParseMovieMain: Main page is empty!\n";
				return false;
			}
			//MUST BE SET UP OUTSIDE!!!
			//          movie.iCode = MovieCode;

			MatchCollection vMC_Title = rMovieDetails_Title.Matches(Page);
			if (vMC_Title.Count == 0)
			{
				ErrorMessage += "INFO: ParseMovieMain: Title not found! (Ignoring)!\n";
			}
			if (vMC_Title.Count > 1)
			{
				ErrorMessage += "INFO: ParseMovieMain: More than one title found! (Ignoring)!\n";
			}
			if (vMC_Title.Count > 0) //We only add the first title found...
			{
				movie.lTitles.Add(new Movie.MovieTitle(vMC_Title[0].Groups["Title"].Value, false));
			}

			MatchCollection vMC_Year = rMovieDetails_Year.Matches(Page);
			if (vMC_Year.Count == 0)
			{
				ErrorMessage += "INFO: ParseMovieMain: Year not found! (Ignoring)!\n";
			}
			if (vMC_Year.Count > 1)
			{
				ErrorMessage += "INFO: ParseMovieMain: More than one year found! (Ignoring)!\n";
			}
			if (vMC_Year.Count > 0)
			{
				movie.iYear = ParseYear(vMC_Year[0].Groups["Year"].Value);
			}
			MatchCollection vMC_Poster = rMovieDetails_Poster.Matches(Page);
			if (vMC_Poster.Count == 1)  //otherwise it's safe to assume there is no poster
			{
				movie.sPosterLink = vMC_Poster[0].Groups["Poster"].Value;
			}
			else
			{
				ErrorMessage += "INFO: ParseMovieMain: Poster not found!\n";
			}


			MatchCollection vMC_Genre = rMovieDetails_Genre.Matches(Page);
			if (vMC_Genre.Count < 1)
			{
				ErrorMessage += "INFO: ParseMovieMain: Genres not found!\n";
			}
			foreach (Match vM_Genre in vMC_Genre)
			{
				movie.lGenres.Add(vM_Genre.Groups["Genre"].Value);
			}

			MatchCollection vMC_RatingVotes = rMovieDetails_RatingVotes.Matches(Page);
			if (vMC_RatingVotes.Count == 1)
			{
				try
				{
					movie.iRating = (int)(10 * double.Parse(vMC_RatingVotes[0].Groups["Rating"].Value));
					movie.iVotes = int.Parse(vMC_RatingVotes[0].Groups["Votes"].Value.Replace(",", ""));
				}
				catch
				{
					movie.iRating = 0;
					movie.iVotes = 0;
					ErrorMessage += "INFO: ParseMovieMain: Incorrect rating (votes) found!\n";
				}
			}
			else
			{
				ErrorMessage += "INFO: ParseMovieMain: Rating (votes) not found!\n";
			}

			MatchCollection vMC_AKAS = rMovieDetails_AKAS.Matches(Page);
			if (vMC_AKAS.Count != 1)
			{
				ErrorMessage += "INFO: ParseMovieMain: AKAs section not found!\n";
			}
			foreach (Match vM_AKAS in vMC_AKAS)
			{
				MatchCollection vMC_AKA = rMovieDetails_AKA.Matches(vM_AKAS.Groups["Akas"].Value);
				if (vMC_AKA.Count < 1)
				{
					ErrorMessage += "INFO: ParseMovieMain: AKAs section contains no AKAs!\n";
				}
				foreach (Match vM_AKA in vMC_AKA)
				{
					movie.lTitles.Add(new Movie.MovieTitle(vM_AKA.Groups["Aka"].Value, true));
				}
			}

			MatchCollection vMC_MPAA = rMovieDetails_MPAA.Matches(Page);
			if (vMC_MPAA.Count == 1)
			{
				movie.sMPAA = vMC_MPAA[0].Groups["MPAA"].Value;
			}
			else
			{
				ErrorMessage += "INFO: ParseMovieMain: MPAA rating not found!\n";
			}
			/*
						MoviesRow["Tagline"] = QCleanup(QParseEx("", "<b class=\"ch\">Tagline:</b>", "<br><br>")).Replace("\n", "").Replace("(more)", "").Replace("(view trailer)", "");
						MoviesRow["PlotOutline"] = QCleanup(QParseEx("", "<b class=\"ch\">Plot Outline:</b>", "<br><br>")).Replace("\n", "").Replace("(more)", "").Replace("(view trailer)", "");
						MoviesRow["Runtime"] = QParseEx("", "<b class=\"ch\">Runtime:</b>", "<br>").Replace("\n", "");
						MoviesRow["Country"] = QParseEx("<b class=\"ch\">Country:</b>", ">", "<");
						MoviesRow["Language"] = QParseEx("<b class=\"ch\">Language:</b>", ">", "<");
						MoviesRow["Color"] = QParseEx("<b class=\"ch\">Color:</b>", ">", "<");
						MoviesRow["SoundMix"] = QParseEx("<b class=\"ch\">Sound Mix:</b>", ">", "<");
			*/

			return true;
		}

		private bool ParseMoviePlot(String Page, Movie movie)
		{
			String ErrorMessage = "";

			if (Page == null)
			{
				ErrorMessage += "FATAL: ParseMoviePlot: Plot page is null!\n";
				return false;
			}

			if (Page.Length == 0)
			{
				ErrorMessage += "FATAL: ParseMoviePlot: Plot page is empty!\n";
				return false;
			}

			MatchCollection vMC_Plot = rMovieDetails_Plot.Matches(Page);
			if (vMC_Plot.Count == 0)
			{
				ErrorMessage += "INFO: ParseMoviePlot: No plots found!\n";
			}
			foreach (Match vM_Plot in vMC_Plot)
			{
				movie.lPlots.Add(QCleanup(vM_Plot.Groups["Plot"].Value));
			}

			return true;
		}

		private bool ParseMovieCredits(String Page, Movie movie)
		{
			String ErrorMessage = "";

			if (Page == null)
			{
				ErrorMessage += "FATAL: ParseMovieCredits: Credits page is null!\n";
				return false;
			}

			if (Page.Length == 0)
			{
				ErrorMessage += "FATAL: ParseMovieCredits: Credits page is empty!\n";
				return false;
			}


			MatchCollection vMC_Directors = rMovieDetails_Directors.Matches(Page);
			if (vMC_Directors.Count != 1)
			{
				ErrorMessage += "INFO: ParseMovieCredits: Directors section not found!\n";
			}
			else
			{
				MatchCollection vMC_Director = rMovieDetails_Director.Matches(vMC_Directors[0].Groups["Directors"].Value);
				if (vMC_Director.Count < 1)
				{
					ErrorMessage += "INFO: ParseMovieCredits: No directors found!\n";
				}
				foreach (Match vM_Director in vMC_Director)
				{
					long code = 0;
					try
					{
						code = long.Parse(vM_Director.Groups["Code"].Value);
					}
					catch
					{
						ErrorMessage += "INFO: ParseMovieCredits: Unable to determine code for director!\n";
						code = 0;
					}
					if (code != 0)
					{
						movie.lDirectors.Add(new Person(code, vM_Director.Groups["Name"].Value));
					}
				}
			}

			MatchCollection vMC_Writers = rMovieDetails_Writers.Matches(Page);
			if (vMC_Writers.Count != 1)
			{
				ErrorMessage += "INFO: ParseMovieCredits: Writers section not found!\n";
			}
			else
			{
				MatchCollection vMC_Writer = rMovieDetails_Writer.Matches(vMC_Writers[0].Groups["Writers"].Value);
				if (vMC_Writer.Count < 1)
				{
					ErrorMessage += "INFO: ParseMovieCredits: No writers found!\n";
				}
				foreach (Match vM_Writer in vMC_Writer)
				{
					long code = 0;
					try
					{
						code = long.Parse(vM_Writer.Groups["Code"].Value);
					}
					catch
					{
						ErrorMessage += "INFO: ParseMovieCredits: Unable to determine code for writer!\n";
						code = 0;
					}
					if (code != 0)
					{
						movie.lWriters.Add(new Person(code, vM_Writer.Groups["Name"].Value));
					}
				}
			}

			MatchCollection vMC_Actors = rMovieDetails_Actors.Matches(Page);
			if (vMC_Actors.Count != 1)
			{
				ErrorMessage += "INFO: ParseMovieCredits: Actors section not found!\n";
			}
			else
			{
				MatchCollection vMC_Actor = rMovieDetails_Actor.Matches(vMC_Actors[0].Groups["Actors"].Value);
				if (vMC_Actor.Count < 1)
				{
					ErrorMessage += "INFO: ParseMovieCredits: No actors found!\n";
				}
				foreach (Match vM_Actor in vMC_Actor)
				{
					long code = 0;
					try
					{
						code = long.Parse(vM_Actor.Groups["Code"].Value);
					}
					catch
					{
						ErrorMessage += "INFO: ParseMovieCredits: Unable to determine code for actor!\n";
						code = 0;
					}
					if (code != 0)
					{
						movie.lActors.Add(new Person(code, vM_Actor.Groups["Name"].Value, vM_Actor.Groups["Role"].Value));
					}
				}
			}

			return true;
		}



		private void ThreadMovieMain(Object parameters)
		{
			MovieParameters mp = (MovieParameters)parameters;

			//String Page = GetString("m_" + mp.strMovieCode + "_combined.txt", "http://www.imdb.com/title/tt" + mp.strMovieCode + "/combined");
			String Page = GetString("m_" + mp.strMovieCode + "_combined.txt", "/title/tt" + mp.strMovieCode + "/maindetails");
			ParseMovieMain(Page, mp.movie);


			if (mp.movie.sPosterLink.Length > 0)
			{
				try
				{
					//Atentie poate nu e jpg..!!!
					mp.movie.bPoster = LoadData("m_" + mp.strMovieCode + "_poster.dat");
					if (mp.movie.bPoster == null)
					{
						WebClient web = new WebClient();
						web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
						mp.movie.bPoster = web.DownloadData(mp.movie.sPosterLink);
						SaveData("m_" + mp.strMovieCode + "_poster.dat", mp.movie.bPoster);
					}
					//Convert byte [] to Bitmap : Bitmap b = new Bitmap(new MemoryStream(byte[]), false);
				}
				catch
				{
				}
			}

		}

		private void ThreadMoviePlot(Object parameters)
		{
			MovieParameters mp = (MovieParameters)parameters;
			//String Page = GetString("m_" + mp.strMovieCode + "_plot.txt", "http://www.imdb.com/title/tt" + mp.strMovieCode + "/plotsummary");
			String Page = GetString("m_" + mp.strMovieCode + "_plot.txt", "/title/tt" + mp.strMovieCode + "/plotsummary");
			ParseMoviePlot(Page, mp.movie);//On error do nothing!

		}

		private void ThreadMovieCredits(Object parameters)
		{
			MovieParameters mp = (MovieParameters)parameters;
			//String Page = GetString("m_" + mp.strMovieCode + "_credits.txt", "http://www.imdb.com/title/tt" + mp.strMovieCode + "/fullcredits");
			String Page = GetString("m_" + mp.strMovieCode + "_credits.txt", "/title/tt" + mp.strMovieCode + "/fullcredits");
			ParseMovieCredits(Page, mp.movie);
		}

		public class MovieParameters
		{
			public MovieParameters(Movie movie, string strMovieCode)
			{
				this.movie = movie;
				this.strMovieCode = strMovieCode;
			}

			public Movie movie;
			public string strMovieCode;
		}

		public Movie GetMovieDetails(Int64 MovieCode)
		{
			Movie movie = new Movie();
			movie.iCode = MovieCode;
			string strMovieCode = MovieCode.ToString("0000000");

			Thread tMain = new Thread(new ParameterizedThreadStart(this.ThreadMovieMain));
			tMain.Start(new MovieParameters(movie, strMovieCode));

			Thread tPlot = new Thread(new ParameterizedThreadStart(this.ThreadMoviePlot));
			tPlot.Start(new MovieParameters(movie, strMovieCode));

			Thread tCredits = new Thread(new ParameterizedThreadStart(this.ThreadMovieCredits));
			tCredits.Start(new MovieParameters(movie, strMovieCode));

			#region oldCode
			/*
            Page = GetString("m_" + strMovieCode + "_combined.txt", "http://www.imdb.com/title/tt" + strMovieCode + "/combined");
            if (!ParseMovieMain(Page, movie))
            {
                return null;
            }

          //Plot summary
            Page = GetString("m_" + strMovieCode + "_plot.txt", "http://www.imdb.com/title/tt" + strMovieCode + "/plotsummary");
            ParseMoviePlot(Page, movie);//On error do nothing!

          //Full credits
            Page = GetString("m_" + strMovieCode + "_credits.txt", "http://www.imdb.com/title/tt" + strMovieCode + "/fullcredits");
            ParseMovieCredits(Page, movie);

            if (movie.sPosterLink.Length > 0)
            {
                try
                {
                    //Atentie poate nu e jpg..!!!
                    movie.bPoster = LoadData("m_" + strMovieCode + "_poster.dat");
                    if (movie.bPoster == null)
                    {
                        web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
                        movie.bPoster = web.DownloadData(movie.sPosterLink);
                        SaveData("m_" + strMovieCode + "_poster.dat", movie.bPoster);
                    }
                    //Convert byte [] to Bitmap : Bitmap b = new Bitmap(new MemoryStream(byte[]), false);
                }
                catch
                {
                }
            }
*/
			#endregion

			tMain.Join();
			tPlot.Join();
			tCredits.Join();
			return movie;
		}

		public Person GetPersonDetails(Int64 PersonCode)
		{
			string Page = "";

			Person person = new Person();

			WebClient web = new WebClient();
			string strPersonCode = PersonCode.ToString("0000000");

			//Page = GetString("p_" + strPersonCode + "_main.txt", "http://www.imdb.com/name/nm" + strPersonCode + "/");
			Page = GetString("p_" + strPersonCode + "_main.txt", "/name/nm" + strPersonCode + "/");
			if (Page.Length == 0)
			{
				//tragically exit
				return null;
			}

			person.iCode = PersonCode;

			MatchCollection vMC_Name = rPersonDetails_Name.Matches(Page);
			if (vMC_Name.Count != 1)
			{
				//WTF?
			}
			person.sName = vMC_Name[0].Groups["Name"].Value;

			string HeadshotLink = "";

			MatchCollection vMC_Headshot = rPersonDetails_Headshot.Matches(Page);
			if (vMC_Headshot.Count != 1)
			{
				//WTF?
			}
			else
			{
				HeadshotLink = vMC_Headshot[0].Groups["Headshot"].Value;
			}


			MatchCollection vMC_DateOfBirth = rPersonDetails_DateOfBirth.Matches(Page);
			if (vMC_DateOfBirth.Count != 1)
			{
				//WTF?
			}
			else
			{
				int day = int.Parse(vMC_DateOfBirth[0].Groups["Day"].Value);
				int year = int.Parse(vMC_DateOfBirth[0].Groups["Year"].Value);
				string month = vMC_DateOfBirth[0].Groups["Month"].Value;
				int imonth = 0;
				if (month == "January")
					imonth = 1;
				if (month == "February")
					imonth = 2;
				if (month == "March")
					imonth = 3;
				if (month == "April")
					imonth = 4;
				if (month == "May")
					imonth = 5;
				if (month == "June")
					imonth = 6;
				if (month == "July")
					imonth = 7;
				if (month == "August")
					imonth = 8;
				if (month == "September")
					imonth = 9;
				if (month == "October")
					imonth = 10;
				if (month == "November")
					imonth = 11;
				if (month == "December")
					imonth = 12;
				person.dDateOfBirth = new DateTime(year, imonth, day);
				person.hasDateOfBirth = true;
			}

			MatchCollection vMC_Acted = rPersonDetails_Acted.Matches(Page);
			if (vMC_Acted.Count != 1)
			{
				//WTF? - may be empty
			}
			else
			{
				MatchCollection vMC_LIRegion = rPersonDetails_LIRegion.Matches(vMC_Acted[0].Groups["Acted"].Value);
				if (vMC_LIRegion.Count <= 0)
				{
					//WTF?
				}
				foreach (Match vM_LIRegion in vMC_LIRegion)
				{
					MatchCollection vMC_MainValues = rPersonDetails_MainValues.Matches(vM_LIRegion.Groups["LIRegion"].Value);
					if (vMC_MainValues.Count != 1)
					{
						//WTF?
					}
					foreach (Match vM_MainValues in vMC_MainValues)
					{
						Movie m = new Movie();
						m.lTitles.Add(new Movie.MovieTitle(vM_MainValues.Groups["Title"].Value, false));
						m.iCode = long.Parse(vM_MainValues.Groups["Code"].Value);
						m.iYear = int.Parse(vM_MainValues.Groups["Year"].Value);
						MatchCollection vMC_AKA = rPersonDetails_AKA.Matches(vM_MainValues.Groups["AKAs"].Value);
						foreach (Match vM_AKA in vMC_AKA)
						{
							m.lTitles.Add(new Movie.MovieTitle(QCleanup(vM_AKA.Groups["AKA"].Value), true));
						}
						person.lCasted.Add(m);
					}
				}
			}

			//Directed

			MatchCollection vMC_Directed = rPersonDetails_Directed.Matches(Page);
			if (vMC_Directed.Count != 1)
			{
				//WTF? - may be empty
			}
			else
			{
				MatchCollection vMC_LIRegion = rPersonDetails_LIRegion.Matches(vMC_Directed[0].Groups["Directed"].Value);
				if (vMC_LIRegion.Count <= 0)
				{
					//WTF?
				}
				foreach (Match vM_LIRegion in vMC_LIRegion)
				{
					MatchCollection vMC_MainValues = rPersonDetails_MainValues.Matches(vM_LIRegion.Groups["LIRegion"].Value);
					if (vMC_MainValues.Count != 1)
					{
						//WTF?
					}
					foreach (Match vM_MainValues in vMC_MainValues)
					{
						Movie m = new Movie();
						m.lTitles.Add(new Movie.MovieTitle(vM_MainValues.Groups["Title"].Value, false));
						m.iCode = long.Parse(vM_MainValues.Groups["Code"].Value);
						m.iYear = int.Parse(vM_MainValues.Groups["Year"].Value);
						MatchCollection vMC_AKA = rPersonDetails_AKA.Matches(vM_MainValues.Groups["AKAs"].Value);
						foreach (Match vM_AKA in vMC_AKA)
						{
							m.lTitles.Add(new Movie.MovieTitle(QCleanup(vM_AKA.Groups["AKA"].Value), true));
						}
						person.lDirected.Add(m);
					}
				}
			}

			//Writed
			MatchCollection vMC_Writed = rPersonDetails_Writed.Matches(Page);
			if (vMC_Writed.Count != 1)
			{
				//WTF? - may be empty
			}
			else
			{
				MatchCollection vMC_LIRegion = rPersonDetails_LIRegion.Matches(vMC_Writed[0].Groups["Writed"].Value);
				if (vMC_LIRegion.Count <= 0)
				{
					//WTF?
				}
				foreach (Match vM_LIRegion in vMC_LIRegion)
				{
					MatchCollection vMC_MainValues = rPersonDetails_MainValues.Matches(vM_LIRegion.Groups["LIRegion"].Value);
					if (vMC_MainValues.Count != 1)
					{
						//WTF?
					}
					foreach (Match vM_MainValues in vMC_MainValues)
					{
						Movie m = new Movie();
						m.lTitles.Add(new Movie.MovieTitle(vM_MainValues.Groups["Title"].Value, false));
						m.iCode = long.Parse(vM_MainValues.Groups["Code"].Value);
						m.iYear = int.Parse(vM_MainValues.Groups["Year"].Value);
						MatchCollection vMC_AKA = rPersonDetails_AKA.Matches(vM_MainValues.Groups["AKAs"].Value);
						foreach (Match vM_AKA in vMC_AKA)
						{
							m.lTitles.Add(new Movie.MovieTitle(QCleanup(vM_AKA.Groups["AKA"].Value), true));
						}
						person.lWritten.Add(m);
					}
				}
			}
			if (HeadshotLink.Length > 0)
			{
				//atentie poate nu e jpg!!!
				person.bHeadshot = LoadData("p_" + strPersonCode + "_headshot.dat");
				if (person.bHeadshot == null)
				{
					web.Headers.Add("user-agent", global::AlexandriaOrg.Alexandria.Imdb.Properties.Settings.Default["UserAgent"].ToString());
					person.bHeadshot = web.DownloadData(HeadshotLink);
					SaveData("p_" + strPersonCode + "_headshot.dat", person.bHeadshot);
				}
			}


			//Donwload damn bio page
			//Page = GetString("p_" + strPersonCode + "_bio.txt", "http://www.imdb.com/name/nm" + strPersonCode + "/bio");
			Page = GetString("p_" + strPersonCode + "_bio.txt", "/name/nm" + strPersonCode + "/bio");
			if (Page.Length != 0)
			{
				MatchCollection vMC_Bio = rPersonDetails_Bio.Matches(Page);
				if (vMC_Bio.Count != 1)
				{
					//WTF?
				}
				else
				{
					person.sBio = QCleanup(vMC_Bio[0].Groups["Bio"].Value);
				}
			}

			//Most voted movies...damn...this is hard ;)
			//Page = GetString("p_" + strPersonCode + "_mostvoted.txt", "http://www.imdb.com/name/nm" + strPersonCode + "/filmovote");
			Page = GetString("p_" + strPersonCode + "_mostvoted.txt", "/name/nm" + strPersonCode + "/filmovote");
			if (Page.Length != 0)
			{
				MatchCollection vMC_MostVoted = rPersonDetails_MostVoted.Matches(Page);
				if (vMC_MostVoted.Count != 1)
				{
					//WTF?
				}
				else
				{
					MatchCollection vMC_TRRegion = rPersonDetails_TRRegion.Matches(vMC_MostVoted[0].Groups["MostVoted"].Value);
					foreach (Match vM_TRRegion in vMC_TRRegion)
					{
						MatchCollection vMC_MostVotedMain = rPersonDetails_MostVotedMain.Matches(vM_TRRegion.Groups["TRRegion"].Value);
						foreach (Match vM_MostVotedMain in vMC_MostVotedMain)
						{
							Int64 code = Int64.Parse(vM_MostVotedMain.Groups["Code"].Value);
							int votes = int.Parse(vM_MostVotedMain.Groups["Votes"].Value.Replace(",", ""));
							for (int index = 0; index < person.lCasted.Count; index++)
							{
								if (person.lCasted[index].iCode == code)
								{
									person.lCasted[index].iVotes = votes;
								}
							}

						}
					}
				}
			}

			return person;
		}

		#region Old parsing functions
		/*
        private string QParseEx(string Skip, string Before, string After)
        {
            if (Skip.Length > 0)
            {
                int pos = Page.IndexOf(Skip, Position);
                if (pos == -1)
                    return "";
                Position = pos + Skip.Length;
            }

            int startPos = Page.IndexOf(Before, Position);
            if (startPos == -1)
                return "";

            startPos += Before.Length;

            int endPos = Page.IndexOf(After, startPos);
            if (endPos == -1)
                return "";

            Position = endPos + After.Length;
            return Page.Substring(startPos, endPos - startPos);
        }


        private string QParse(string Before, string After)
        {
            int startPos = Page.IndexOf(Before, Position);
            if (startPos == -1)
                return "";

            startPos += Before.Length;

            int endPos = Page.IndexOf(After, startPos);
            if (endPos == -1)
                return "";

            Position = endPos + After.Length;
            return Page.Substring(startPos, endPos - startPos);
        }

        private bool QSkip(string Skip)
        {
            int pos = Page.IndexOf(Skip, Position);
            if (pos == -1)
                return false;

            Position = pos + Skip.Length;
            return true;
        }
        */
		#endregion

		#region Cleanup
		private string QCleanup(string Input)
		{
			do
			{
				int pos1 = Input.IndexOf("<");
				if (pos1 == -1)
					return Input;

				int pos2 = Input.IndexOf(">", pos1 + 1);
				if (pos2 == -1)
					return Input;

				Input = Input.Remove(pos1, pos2 - pos1 + 1);

			} while (true);

		}
		#endregion

		#region Decoding
		string Decode(string Input)
		{
			Input = Input.Replace("&#34;", "\"");
			Input = Input.Replace("&#39;", "`");
			Input = Input.Replace("&#38;", "&");
			Input = Input.Replace("&#60;", "less");
			Input = Input.Replace("&#62;", "greater");

			Input = Input.Replace("&#160;", " ");
			Input = Input.Replace("&#161;", "!");
			Input = Input.Replace("&#164;", "currency");
			Input = Input.Replace("&#162;", "cent");
			Input = Input.Replace("&#163;", "pound");
			Input = Input.Replace("&#165;", "yen");
			Input = Input.Replace("&#166;", "|");
			Input = Input.Replace("&#167;", "section");
			Input = Input.Replace("&#168;", "..");
			Input = Input.Replace("&#169;", "(C)");
			Input = Input.Replace("&#170;", "a");
			Input = Input.Replace("&#171;", "``");//<<
			Input = Input.Replace("&#172;", "not");
			Input = Input.Replace("&#173;", "-");
			Input = Input.Replace("&#174;", "(R)");
			Input = Input.Replace("&#8482;", "TM");
			Input = Input.Replace("&#175;", "-");
			Input = Input.Replace("&#176;", "o");
			Input = Input.Replace("&#177;", "+/-");
			Input = Input.Replace("&#178;", "^2");
			Input = Input.Replace("&#179;", "^3");
			Input = Input.Replace("&#180;", "`");
			Input = Input.Replace("&#181;", "u");
			Input = Input.Replace("&#182;", "P");
			Input = Input.Replace("&#183;", ".");
			Input = Input.Replace("&#184;", ",");
			Input = Input.Replace("&#185;", "^1");
			Input = Input.Replace("&#186;", "o");
			Input = Input.Replace("&#187;", "``");//>>
			Input = Input.Replace("&#188;", "1/4");
			Input = Input.Replace("&#189;", "1/2");
			Input = Input.Replace("&#190;", "3/4");
			Input = Input.Replace("&#191;", "?");
			Input = Input.Replace("&#215;", "x");
			Input = Input.Replace("&#247;", "/");
			Input = Input.Replace("&#192;", "A");
			Input = Input.Replace("&#193;", "A");
			Input = Input.Replace("&#194;", "A");
			Input = Input.Replace("&#195;", "A");
			Input = Input.Replace("&#196;", "A");
			Input = Input.Replace("&#197;", "A");
			Input = Input.Replace("&#198;", "AE");
			Input = Input.Replace("&#199;", "C");
			Input = Input.Replace("&#200;", "E");
			Input = Input.Replace("&#201;", "E");
			Input = Input.Replace("&#202;", "E");
			Input = Input.Replace("&#203;", "E");
			Input = Input.Replace("&#204;", "I");
			Input = Input.Replace("&#205;", "I");
			Input = Input.Replace("&#206;", "I");
			Input = Input.Replace("&#207;", "I");
			Input = Input.Replace("&#208;", "D");
			Input = Input.Replace("&#209;", "N");
			Input = Input.Replace("&#210;", "O");
			Input = Input.Replace("&#211;", "O");
			Input = Input.Replace("&#212;", "O");
			Input = Input.Replace("&#213;", "O");
			Input = Input.Replace("&#214;", "O");
			Input = Input.Replace("&#216;", "O");
			Input = Input.Replace("&#217;", "U");
			Input = Input.Replace("&#218;", "U");
			Input = Input.Replace("&#219;", "U");
			Input = Input.Replace("&#220;", "U");
			Input = Input.Replace("&#221;", "Y");
			Input = Input.Replace("&#222;", "P");
			Input = Input.Replace("&#223;", "ss");
			Input = Input.Replace("&#224;", "a");
			Input = Input.Replace("&#225;", "a");
			Input = Input.Replace("&#226;", "a");
			Input = Input.Replace("&#227;", "a");
			Input = Input.Replace("&#228;", "a");
			Input = Input.Replace("&#229;", "a");
			Input = Input.Replace("&#230;", "ae");
			Input = Input.Replace("&#231;", "c");
			Input = Input.Replace("&#232;", "e");
			Input = Input.Replace("&#233;", "e");
			Input = Input.Replace("&#234;", "e");
			Input = Input.Replace("&#235;", "e");
			Input = Input.Replace("&#236;", "i");
			Input = Input.Replace("&#237;", "i");
			Input = Input.Replace("&#238;", "i");
			Input = Input.Replace("&#239;", "i");
			Input = Input.Replace("&#240;", "eth");
			Input = Input.Replace("&#241;", "n");
			Input = Input.Replace("&#242;", "o");
			Input = Input.Replace("&#243;", "o");
			Input = Input.Replace("&#244;", "o");
			Input = Input.Replace("&#245;", "o");
			Input = Input.Replace("&#246;", "o");
			Input = Input.Replace("&#248;", "o");
			Input = Input.Replace("&#249;", "u");
			Input = Input.Replace("&#250;", "u");
			Input = Input.Replace("&#251;", "u");
			Input = Input.Replace("&#252;", "u");
			Input = Input.Replace("&#253;", "y");
			Input = Input.Replace("&#254;", "p");
			Input = Input.Replace("&#255;", "y");

			Input = Input.Replace("&#338;", "OE");
			Input = Input.Replace("&#339;", "oe");
			Input = Input.Replace("&#352;", "S");
			Input = Input.Replace("&#353;", "s");
			Input = Input.Replace("&#376;", "Y");
			Input = Input.Replace("&#710;", "^");
			Input = Input.Replace("&#732;", "~");
			Input = Input.Replace("&#8194;", " ");
			Input = Input.Replace("&#8195;", " ");
			Input = Input.Replace("&#8201;", " ");
			Input = Input.Replace("&#8204;", "|");
			Input = Input.Replace("&#8205;", "|");
			Input = Input.Replace("&#8206;", "|");
			Input = Input.Replace("&#8207;", "|");
			Input = Input.Replace("&#8211;", "-");
			Input = Input.Replace("&#8212;", "-");
			Input = Input.Replace("&#8216;", "`");
			Input = Input.Replace("&#8217;", "`");
			Input = Input.Replace("&#8218;", "`");
			Input = Input.Replace("&#8220;", "``");
			Input = Input.Replace("&#8221;", "``");
			Input = Input.Replace("&#8222;", "``");
			Input = Input.Replace("&#8224;", "+");
			Input = Input.Replace("&#8225;", "++");
			Input = Input.Replace("&#8230;", "...");
			Input = Input.Replace("&#8240;", "0/00");
			Input = Input.Replace("&#8249;", "(");
			Input = Input.Replace("&#8250;", ")");
			Input = Input.Replace("&#8264;", "euro");

			return Input;

		}
		#endregion
	}
}
