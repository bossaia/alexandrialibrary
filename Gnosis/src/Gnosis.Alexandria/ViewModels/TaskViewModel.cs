﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Gnosis.Alexandria.ViewModels
{
    public abstract class TaskViewModel
        : ITaskViewModel, INotifyPropertyChanged
    {
        protected TaskViewModel(ILogger logger, ITask task, string name, string description, object icon)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (task == null)
                throw new ArgumentNullException("task");
            if (name == null)
                throw new ArgumentNullException("name");
            if (description == null)
                throw new ArgumentNullException("description");
            if (icon == null)
                throw new ArgumentNullException("icon");

            this.logger = logger;
            this.task = task;
            this.name = name;
            this.description = description;
            this.icon = icon;

            task.AddStoppedCallback(() => OnStopped());
            task.AddCancelledCallback(() => OnCancelled());
            task.AddCompletedCallback(() => OnCompleted());
            task.AddErrorCallback(error => OnError(error));
            task.AddFailedCallback(() => OnFailed());
            task.AddPausedCallback(() => OnPaused());
            task.AddProgressCallback(progress => OnProgressChanged(progress));
            task.AddResumedCallback(() => OnResumed());
            task.AddStartedCallback(() => OnStarted());
        }

        private readonly Guid id = Guid.NewGuid();
        private readonly ILogger logger;
        private readonly ITask task;
        private string name;
        private string description;
        private object icon;

        private readonly IList<Action<ITaskViewModel>> startedCallbacks = new List<Action<ITaskViewModel>>();
        private readonly IList<Action<ITaskViewModel>> cancelCallbacks = new List<Action<ITaskViewModel>>();

        private int progressCount = 0;
        private int progressMaximum = 100;
        private int errorCount;
        private ITaskItem item;

        private bool isSelected;
        private bool isCancelled;

        private void OnCancelled()
        {
            try
            {
                OnStatusChanged();
                OnPropertyChanged("IsCancelled");
                foreach (var callback in cancelCallbacks)
                    callback(this);
            }
            catch (Exception ex)
            {
                logger.Error("  OnCancelled", ex);
            }
        }

        private void OnStopped()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnStopped", ex);
            }
        }

        private void OnCompleted()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnCompleted", ex);
            }
        }

        private void OnProgressChanged(TaskProgress progress)
        {
            try
            {
                ProgressCount = progress.Count;
                ProgressMaximum = progress.Maximum;
            }
            catch (Exception ex)
            {
                logger.Error("  OnProgressChanged", ex);
            }
        }

        private void OnItemChanged(ITaskItem item)
        {
            try
            {
                this.item = item;
                
                if (item.Image != null)
                {
                    Icon = item.Image;
                }
                OnPropertyChanged("ElapsedVisibility");
            }
            catch (Exception ex)
            {
                logger.Error("  OnItemChanged", ex);
            }
        }

        private void OnError(TaskError error)
        {
            try
            {
                ErrorCount += 1;
            }
            catch (Exception ex)
            {
                logger.Error("  OnError", ex);
            }
        }

        private void OnFailed()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnFailed", ex);
            }
        }

        private void OnStarted()
        {
            try
            {
                OnStatusChanged();

                foreach (var callback in startedCallbacks)
                    callback(this);
            }
            catch (Exception ex)
            {
                logger.Error("  OnStarted", ex);
            }
        }

        private void OnPaused()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnPaused", ex);
            }
        }

        private void OnResumed()
        {
            try
            {
                OnStatusChanged();
            }
            catch (Exception ex)
            {
                logger.Error("  OnResumed", ex);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnStatusChanged()
        {
            OnPropertyChanged("Status");
            OnPropertyChanged("StatusName");
            OnPropertyChanged("Icon");
            OnPropertyChanged("RunningVisibility");
            OnPropertyChanged("StartVisibility");
            OnPropertyChanged("PauseVisibility");
            OnPropertyChanged("StopVisibility");
            OnPropertyChanged("PreviousVisibility");
            OnPropertyChanged("NextVisibility");
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            private set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return description; }
            private set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public object Icon
        {
            get { return icon; }
            protected set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        public TaskStatus Status
        {
            get { return task.Status; }
        }

        public string StatusName
        {
            get { return task.Status.ToString(); }
        }

        public ITaskItem CurrentItem
        {
            get { return task.Item; }
        }

        public int ErrorCount
        {
            get { return errorCount; }
            private set
            {
                errorCount = value;

                OnPropertyChanged("ErrorCount");
                OnPropertyChanged("ErrorVisibility");
            }
        }

        public int ProgressCount
        {
            get { return progressCount; }
            private set
            {
                progressCount = value;

                OnPropertyChanged("ProgressCount");
                OnPropertyChanged("ProgressVisibility");
            }
        }

        public int ProgressMaximum
        {
            get { return progressMaximum; }
            set
            {
                if (progressMaximum != value)
                {
                    progressMaximum = value;

                    OnPropertyChanged("ProgressMaximum");
                }
            }
        }

        public Visibility ProgressVisibility
        {
            get { return item != null && item.Duration > TimeSpan.Zero ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility RunningVisibility
        {
            get
            {
                return (task.Status == TaskStatus.Running) ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility ElapsedVisibility
        {
            get
            {
                return item != null && item.Duration > TimeSpan.Zero ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility StartVisibility
        {
            get 
            {
                return task.Status == TaskStatus.Running ? 
                    Visibility.Collapsed 
                    : Visibility.Visible;
            }
        }

        public Visibility PauseVisibility
        {
            get
            {
                return task.Status == TaskStatus.Running ?
                    Visibility.Visible 
                    : Visibility.Collapsed; 
            }
        }

        public Visibility StopVisibility
        {
            get
            {
                return (task.Status == TaskStatus.Running || task.Status == TaskStatus.Paused) ?
                    Visibility.Visible 
                    : Visibility.Collapsed;
            }
        }

        public Visibility PreviousVisibility
        {
            get
            {
                return item != null ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility NextVisibility
        {
            get
            {
                return item != null ?
                    Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
            set
            {
                if (!isCancelled && value)
                {
                    if (task.Status == TaskStatus.Running)
                    {
                        var result = MessageBox.Show("This task is currently running.\r\nAre you sure that you want to cancel it?", "Cancel Running Task?", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.No)
                            return;
                    }

                    isCancelled = true;
                    Cancel();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset()
        {
            task.Reset();
        }

        public void Start()
        {
            task.Start();
        }

        public void Stop()
        {
            task.Stop();
        }

        public void Pause()
        {
            task.Pause();
        }

        public void Resume()
        {
            task.Resume();
        }

        public void Cancel()
        {
            task.Cancel();
        }

        public void Previous()
        {
            task.PreviousItem();
        }

        public void Next()
        {
            task.NextItem();
        }

        public void AddStartedCallback(Action<ITaskViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            startedCallbacks.Add(callback);
        }

        public void AddCancelCallback(Action<ITaskViewModel> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            cancelCallbacks.Add(callback);
        }

        public void AddProgressCallback(Action<TaskProgress> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            task.AddProgressCallback(callback);
        }

        public void AddErrorCallback(Action<TaskError> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            task.AddErrorCallback(callback);
        }
    }

    public abstract class TaskViewModel<T>
        : TaskViewModel, ITaskViewModel<T>, INotifyPropertyChanged
    {
        protected TaskViewModel(ILogger logger, ITask<T> task, string name, string description, object icon)
            : base(logger, task, name, description, icon)
        {
        }

        protected readonly ITask<T> resultTask;

        #region ITaskViewModel<T> Members

        public void AddResultsCallback(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            resultTask.AddResultsCallback(callback);
        }

        #endregion
    }

}
