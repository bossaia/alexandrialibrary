using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Alexandria
{
	internal class TimerWorkerThread : IDisposable
	{
		#region Private Fields
		private Thread threadObject;
		private bool endLoop;
		private Mutex endLoopMutex;
		private AutoResetEvent itemAdded;
		private Queue workItemQueue;
		private bool disposed;
		#endregion

		#region Private Properties
		private bool EndLoop
		{
			get
			{
				bool result = false;
				endLoopMutex.WaitOne();
				result = endLoop;
				endLoopMutex.ReleaseMutex();
				return result;
			}
			set
			{
				endLoopMutex.WaitOne();
				endLoop = value;
				endLoopMutex.ReleaseMutex();
			}
		}
		#endregion

		#region Private Methods
		private Thread CreateThread(bool autoStart)
		{
			if (this.threadObject != null)
			{
				//Debug.Assert(false);
				return this.threadObject;
			}

			ThreadStart threadStart = new ThreadStart(Run);
			this.threadObject = new Thread(threadStart);
			this.threadObject.Name = "TimerSynchronizer Worker Thread";

			if (autoStart) this.threadObject.Start();

			return this.threadObject;
		}

		//private void Start()
		//{
		//Debug.Assert(m_ThreadObj != null);
		//Debug.Assert(m_ThreadObj.IsAlive == false);
		//this.threadObject.Start();
		//}

		private bool QueueEmpty
		{
			get
			{
				lock (this.workItemQueue.SyncRoot)
				{
					if (this.workItemQueue.Count > 0)
					{
						return false;
					}
					return true;
				}
			}
		}

		private TimerWorkItem GetNext()
		{
			if (QueueEmpty)
			{
				return null;
			}
			lock (this.workItemQueue.SyncRoot)
			{
				return (TimerWorkItem)this.workItemQueue.Dequeue();
			}
		}

		private void Run()
		{
			while (!EndLoop)
			{
				while (!QueueEmpty)
				{
					if (EndLoop) return;

					TimerWorkItem workItem = GetNext();
					workItem.CallBack();
				}
				this.itemAdded.WaitOne();
			}
		}
		#endregion

		#region Constructors
		internal TimerWorkerThread()
		{
			this.endLoopMutex = new Mutex();
			this.itemAdded = new AutoResetEvent(false);
			this.workItemQueue = new Queue();
			CreateThread(true);
		}
		#endregion

		#region Finalizer
		~TimerWorkerThread()
		{
			Dispose(false);
		}
		#endregion

		#region IDisposable Members
		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.itemAdded.Close();
					this.endLoopMutex.Close();
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

		#region Internal Methods
		internal void QueueWorkItem(TimerWorkItem workItem)
		{
			lock (this.workItemQueue.SyncRoot)
			{
				this.workItemQueue.Enqueue(workItem);
				this.itemAdded.Set();
			}
		}

		internal void Kill()
		{
			//Kill is called on client thread - must use cached thread object
			//Debug.Assert(m_ThreadObj != null);
			if (!this.threadObject.IsAlive) return;

			EndLoop = true;
			this.itemAdded.Set();

			//Wait for thread to die
			this.threadObject.Join();

			if (this.endLoopMutex != null)
			{
				this.endLoopMutex.Close();
			}

			if (this.itemAdded != null)
			{
				this.itemAdded.Close();
			}
		}
		#endregion
	}	
}
