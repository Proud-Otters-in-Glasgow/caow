using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;
using System.ComponentModel;

namespace caow
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(processListView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                        direction = ListSortDirection.Ascending;
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                            direction = ListSortDirection.Descending;
                        else
                            direction = ListSortDirection.Ascending;
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    // TODO: this whole arrow business doesn't really work, fix/remove l8r
                    // Add an arrow to the sorted header
                    if (direction == ListSortDirection.Ascending)
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    else
                        headerClicked.Column.HeaderTemplate = Resources["HeaderTemplateArrowDown"] as DataTemplate;

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                        _lastHeaderClicked.Column.HeaderTemplate = null;

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)processListView.Items).CollectionChanged += ListView_CollectionChanged;
        }

        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ((INotifyCollectionChanged)processListView.Items).CollectionChanged -= ListView_CollectionChanged;
            if (_lastHeaderClicked != null)
            {
                var columnBinding = _lastHeaderClicked.Column.DisplayMemberBinding as Binding;
                var sortBy = columnBinding?.Path.Path ?? _lastHeaderClicked.Column.Header as string;

                Sort(sortBy, _lastDirection);
            }
            CPUGraph.RefreshChart();
            RAMGraph.RefreshChart();
            ((INotifyCollectionChanged)processListView.Items).CollectionChanged += ListView_CollectionChanged;
        }
    }
}
