using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Security.Permissions;

namespace Alexandria
{
	[Serializable]
	internal class TimerWorkItem : IAsyncResult
	{
		#region Private Fields
		private object[] args;
		private object asyncState;
		private bool completed;
		private Delegate method;
		[NonSerialized]
		private ManualResetEvent resetEvent;
		private object returnValue;
		#endregion

		#region Constructors
		internal TimerWorkItem(object asyncState, Delegate method, object[] args)
		{
			this.asyncState = asyncState;
			this.method = method;
			this.args = args;
			this.resetEvent = new ManualResetEvent(false);
		}
		#endregion
		
		#region Properties
		object IAsyncResult.AsyncState
		{
			get
			{
				return asyncState;
			}
		}
		
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return resetEvent;
			}
		}
		
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}
		
		bool IAsyncResult.IsCompleted
		{
			get
			{
				return Completed;
			}
		}
		
		bool Completed
		{
			get
			{
				lock (this)
				{
					return completed;
				}
			}
			set
			{
				lock (this)
				{
					completed = value;
				}
			}
		}

		internal object ReturnValue
		{
			get
			{
				object safeReturnValue;
				lock (this)
				{
					safeReturnValue = this.returnValue;
				}
				return safeReturnValue;
			}
			set
			{
				lock (this)
				{
					this.returnValue = value;
				}
			}

		}
		#endregion

		#region Internal Methods
		//This method is called on the worker thread to execute the method
		internal void CallBack()
		{
			ReturnValue = this.method.DynamicInvoke(this.args);
			
			//Method is done. Signal the world
			this.resetEvent.Set();
			Completed = true;
		}
		#endregion
	}
}
