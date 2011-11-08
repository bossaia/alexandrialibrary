﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.ViewModels;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private ILogger logger;
        private IMediaDetailRepository repository;
        private ITaskController taskController;
        private readonly ObservableCollection<IMediaDetailViewModel> viewModels = new ObservableCollection<IMediaDetailViewModel>();

        private readonly IDictionary<int, IDictionary<string, IList<string>>> resultsByType = new Dictionary<int, IDictionary<string, IList<string>>>();

        private void HandleSearchResults(IEnumerable<IMediaDetail> results)
        {
            try
            {
                if (results == null)
                {
                    System.Diagnostics.Debug.WriteLine("search results are null");
                    return;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("search results count: " + results.Count());

                    Action action = () =>
                        {
                            foreach (var detail in results)
                            {
                                var type = detail.Tag.Type.Id;
                                var value = detail.Tag.Tuple.ToString();
                                var thumbnailPath = detail.Thumbnail != null ? detail.Thumbnail.Location.ToString() : null;
                                if (resultsByType.ContainsKey(type))
                                {
                                    if (!resultsByType[type].ContainsKey(value))
                                    {
                                        resultsByType[type][value] = new List<string> { thumbnailPath };
                                        viewModels.Add(new MediaDetailViewModel(detail));
                                        //if (detail.Thumbnail != null)
                                            //detail.Thumbnail.Load();
                                    }
                                    else
                                    {
                                        if (!resultsByType[type][value].Contains(thumbnailPath))
                                        {
                                            resultsByType[type][value].Add(thumbnailPath);
                                            viewModels.Add(new MediaDetailViewModel(detail));
                                        }
                                        //else if (resultsByType[type][value].All(x => x == null) && thumbnailPath != null)
                                        //{

                                        //}
                                        //else
                                        //{
                                        //    var existing = viewModels.Where(x => x.Detail.Tag.Type.Id == detail.Tag.Type.Id && x.Value == value).FirstOrDefault();
                                        //    if (existing != null && existing.Thumbnail == null && thumbnailPath != null)
                                        //    {
                                        //        var index = viewModels.IndexOf(existing);
                                        //        viewModels.RemoveAt(index);
                                        //        viewModels.Insert(index, new MediaDetailViewModel(detail));
                                        //    }
                                        //}
                                    }
                                }
                                else
                                {
                                    //if (thumbnailPath != null)
                                    //{
                                    var list = new List<string>();
                                    if (thumbnailPath != null)
                                    {
                                        list.Add(thumbnailPath);
                                        viewModels.Add(new MediaDetailViewModel(detail));
                                    }

                                    resultsByType[type] = new Dictionary<string, IList<string>> { { value, list } };
                                    //}
                                }
                            }
                        };

                    this.Dispatcher.Invoke(action, DispatcherPriority.DataBind);
                }
            }
            catch (Exception ex)
            {
                logger.Error("  HandleSearchResults", ex);
            }
        }

        private void DoSearch()
        {
            try
            {
                var search = searchTextBox.Text;
                if (string.IsNullOrEmpty(search))
                    return;

                var task = repository.Search(search + "%");
                task.AddResultsCallback(results => HandleSearchResults(results));
                var taskViewModel = new SearchTaskViewModel(logger, task, search);
                taskController.AddTask(taskViewModel);
                task.Start();
            }
            catch (Exception ex)
            {
                logger.Error("  DoSearch", ex);
            }
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return || e.Key == Key.Enter)
                {
                    e.Handled = true;
                    DoSearch();
                }
            }
            catch (Exception ex)
            {
                logger.Error("  searchTextBox_KeyUp", ex);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            DoSearch();
        }

        public void Initialize(ILogger logger, IMediaDetailRepository repository, ITaskController taskController)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (taskController == null)
                throw new ArgumentNullException("taskController");

            this.logger = logger;
            this.repository = repository;
            this.taskController = taskController;

            try
            {
                searchList.ItemsSource = viewModels;
            }
            catch (Exception ex)
            {
                logger.Error("  SearchView.Initialize", ex);
            }
        }
    }
}
