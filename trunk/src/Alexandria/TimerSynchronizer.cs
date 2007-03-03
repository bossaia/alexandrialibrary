using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Security.Permissions;

namespace Alexandria
{
	[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
	public class TimerSynchronizer : ISynchronizeInvoke, IDisposable
	{
		#region Private Fields
		private TimerWorkerThread workerThread;
		private bool disposed;
		#endregion
		
		#region Properties
		public bool InvokeRequired
		{
			get
			{
				bool res = object.ReferenceEquals(Thread.CurrentThread, this.workerThread);
				return res;
			}
		}
		#endregion
		
		#region Constructors
		public TimerSynchronizer()
		{
			this.workerThread = new TimerWorkerThread();//this);
		}
		#endregion
		
		#region Finalizer
		~TimerSynchronizer()
		{
			Dispose(false);
		}
		#endregion
		
		#region Dispose
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.workerThread.Kill();
					this.workerThread.Dispose();
				}
			}
			disposed = true;
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Methods
		public IAsyncResult BeginInvoke(Delegate method, object[] args)
		{
			TimerWorkItem result = new TimerWorkItem(null, method, args);
			this.workerThread.QueueWorkItem(result);
			return result;
		}

		public object EndInvoke(IAsyncResult result)
		{
			if (result != null)
			{
				result.AsyncWaitHandle.WaitOne();
				TimerWorkItem workItem = (TimerWorkItem)result;
				return workItem.ReturnValue;
			}
			else return null;
		}

		public object Invoke(Delegate method, object[] args)
		{
			IAsyncResult asyncResult;
			asyncResult = BeginInvoke(method, args);
			return EndInvoke(asyncResult);
		}
		#endregion
	}
}