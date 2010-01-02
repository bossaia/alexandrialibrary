#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Telesophy.Alexandria.Model;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	public class TaskController
	{
		#region Constructors
		public TaskController()
		{
		}
		#endregion
		
		#region Private Constants
		private const int TASK_COL_NAME = 0; //"taskNameColumn";
		private const int TASK_COL_STATUS = 1; //"taskStatusColumn";
		private const int TASK_COL_PROGRESS = 2; //"taskProgressColumn";
		private const int TASK_COL_DETAILS = 3; //"taskDetailsColumn";

		private const string IMPORT_TASK_NAME = "Import Media";
		private const string IMPORT_TASK_STATUS_DEFAULT = "Running";
		private const string IMPORT_TASK_STATUS_COMPLETED = "Completed";
		private const string IMPORT_DETAILS_FORMAT = "{0} scanned / {1} imported / {2} errors";
		#endregion
		
		#region Private Fields
		private QueueController queueController;
		private PersistenceController persistenceController;
		private DataGridView grid;

		#region Private Import Fields
		private object importLock = new object();
		private TaskStatus importStatus = TaskStatus.Unknown;
		private DateTime importStart;
		private string importPath;
		private string importFileName;
		private int importScanCount;
		private int importHitCount;
		private int importErrorCount;
		private ImportStatusUpdateDelegate importStatusUpdateCallback;
		private DataGridViewRow importRow;
		#endregion
		
		#endregion
		
		#region Private Methods
		
		#region Private Import Methods
		private string GetImportProgess()
		{
			return (!string.IsNullOrEmpty(importFileName)) ? importFileName : "Scanning...";
		}
		
		private string GetImportDetails()
		{
			return string.Format(IMPORT_DETAILS_FORMAT, importScanCount, importHitCount, importErrorCount);
		}
		
		private void SetImportStatus(TaskStatus status)
		{
			lock(importLock)
			{
				importStatus = status;
			}
		}
		
		private void InitializeImport(string path)
		{
			SetImportStatus(TaskStatus.Running);
			
			importPath = path;
			importScanCount = 0;
			importHitCount = 0;
			importErrorCount = 0;
			importStart = DateTime.Now;
			importFileName = string.Empty;
		
			importRow = new DataGridViewRow();
			importRow.CreateCells(grid);
			importRow.Cells[TASK_COL_NAME].Value = IMPORT_TASK_NAME;
			RefreshImportRow();
			
			grid.Rows.Add(importRow);
		}
				
		private ImportStatusUpdateEventArgs GetImportEventArgs(string path)
		{
			return new ImportStatusUpdateEventArgs(importScanCount, importHitCount, importErrorCount, path);
		}
		
		private void RefreshImportRow()
		{
			if (importRow != null)
			{
				importRow.Cells[TASK_COL_STATUS].Value = importStatus.ToString();
				importRow.Cells[TASK_COL_PROGRESS].Value = GetImportProgess();
				importRow.Cells[TASK_COL_DETAILS].Value = GetImportDetails();
			}
		}
		#endregion
		
		#endregion
		
		#region Public Properties
		public QueueController QueueController
		{
			get { return queueController; }
			set { queueController = value; }
		}
		
		[CLSCompliant(false)]
		public PersistenceController PersistenceController
		{
			get { return persistenceController; }
			set { persistenceController = value; }
		}
		
		public DataGridView Grid
		{
			get { return grid; }
			set { grid = value; }
		}
		#endregion
		
		#region Public Methods
		public void RunSelectedTask()
		{
			if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
			{
				if (grid.SelectedRows[0] == importRow)
				{
					if (importStatus == TaskStatus.Paused || importStatus == TaskStatus.Pending)
						SetImportStatus(TaskStatus.Running);
					
					RefreshImportRow();
				}
			}			
		}
		
		public void PauseSelectedTask()
		{
			if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
			{
				if (grid.SelectedRows[0] == importRow)
				{
					if (importStatus == TaskStatus.Running)
						SetImportStatus(TaskStatus.Paused);
					else if (importStatus == TaskStatus.Paused)
						SetImportStatus(TaskStatus.Running);
					
					RefreshImportRow();
				}			
			}
		}
		
		public void CancelSelectedTask()
		{
			if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
			{
				if (grid.SelectedRows[0] == importRow)
				{
					SetImportStatus(TaskStatus.Cancelled);
					
					RefreshImportRow();
				}
			}
		}
		
		public void CancelAllTasks()
		{
			if (grid.Rows.Count > 0)
			{
				SetImportStatus(TaskStatus.Cancelled);
				
				RefreshImportRow();
			}
		}
		
		#region Public Import Methods
		public void BeginImportDirectory(string path, ImportStatusUpdateDelegate importStatusUpdateCallback)
		{			
			this.importStatusUpdateCallback = importStatusUpdateCallback;
			
			InitializeImport(path);

			MethodInvoker invoker = new MethodInvoker(ImportDirectory);
			AsyncCallback callback = new AsyncCallback(EndImportDirectory);
			invoker.BeginInvoke(callback, null);
		}

		public void EndImportDirectory(IAsyncResult result)
		{
			lock(importLock)
			{
				importStatus = TaskStatus.Completed;
			}
		
			if (importRow != null)
			{
				importRow.Cells[TASK_COL_STATUS].Value = IMPORT_TASK_STATUS_COMPLETED;
			}
		
			if (importStatusUpdateCallback != null)
			{
				TimeSpan importCompletedTime = DateTime.Now.Subtract(importStart);
				ImportStatusUpdateEventArgs args = new ImportStatusUpdateEventArgs(importScanCount, importHitCount, importErrorCount, importCompletedTime);
				importStatusUpdateCallback(this, args);
			}
		}

		private bool ContinueImport()
		{
			//if (importStatus == TaskStatus.Running)
				//System.Threading.Thread.Sleep(8000);
		
			switch (importStatus)
			{
				case TaskStatus.Cancelled:
					return false;
				case TaskStatus.Paused:
					while(importStatus == TaskStatus.Paused)
					{
						System.Threading.Thread.Sleep(3000);
					}
					return (importStatus != TaskStatus.Cancelled);
				default:
					return true;
			}
		}

		private void ImportDirectory()
		{
			ImportDirectory(importPath);
		}

		private void ImportDirectory(string path)
		{
			if (ContinueImport())	
			{
				if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
				{
					DirectoryInfo dir = new DirectoryInfo(path);
					foreach (FileInfo file in dir.GetFiles())
					{
						if (ContinueImport())
						{
							ImportFile(file.FullName);
						}
						else break;
					}
					foreach (DirectoryInfo subDirectory in dir.GetDirectories())
					{
						if (ContinueImport())
						{
							ImportDirectory(subDirectory.FullName);
						}
						else break;
					}
				}
			}
		}

		public void ImportFile(string path)
		{	
			if (System.IO.File.Exists(path) && !System.IO.Directory.Exists(path))
			{
				if (!string.IsNullOrEmpty(path) && QueueController.IsFormat(path, ContextHelper.GetAudioFormats()))
				{
					importFileName = new FileInfo(path).Name;
					importScanCount++;
				
					try
					{
						IMediaItem track = QueueController.GetMediaItem(new Uri(path));
						if (track != null)
						{
                            track.Source = Values.Source.Catalog;
							track.Type = MediaTypes.Audio;
							PersistenceController.SaveMediaItem(track);
							importHitCount++;

							if (importStatusUpdateCallback != null)
							{
								ImportStatusUpdateEventArgs args = GetImportEventArgs(path);
								importStatusUpdateCallback(this, args);
							}
						}
					}
					catch (Exception ex)
					{
						importErrorCount++;
						
						if (importStatusUpdateCallback != null)
						{
							ImportStatusUpdateEventArgs args = GetImportEventArgs(path);
							importStatusUpdateCallback(this, args);
						}
						
						MessageBox.Show(string.Format("The following file could not be imported: \n{0}\n\n{1}", path, ex.Message), "IMPORT ERROR");
					}
				}
				else
				{
					if (importStatusUpdateCallback != null)
					{
						ImportStatusUpdateEventArgs args = GetImportEventArgs(path);
						importStatusUpdateCallback(this, args);
					}
				}
				
				importRow.Cells[TASK_COL_PROGRESS].Value = GetImportProgess();
				importRow.Cells[TASK_COL_DETAILS].Value = GetImportDetails();
			}
		}		
		#endregion
		
		#endregion
	}
}
