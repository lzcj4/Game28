using Game28UI.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Game28UI
{
    /// <summary>
    /// Interaction logic for UCRoundList.xaml
    /// </summary>
    public partial class UCRoundList : UserControl
    {
        private RoundListViewModel ViewModel { get; set; }
        public UCRoundList()
        {
            InitializeComponent();
            this.ViewModel = new RoundListViewModel();
            this.DataContext = ViewModel;
            this.Loaded += (sender, e) => { this.ViewModel.QueryCommand.Execute(null); };
        }

        ListSortDirection currentDirection = ListSortDirection.Ascending;

        void Header_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader clickHeader = e.OriginalSource as GridViewColumnHeader;
            if (clickHeader.IsNull() || clickHeader.Column.IsNull())
            {
                return;
            }
            currentDirection = currentDirection == ListSortDirection.Ascending ? ListSortDirection.Descending :
                ListSortDirection.Ascending;
            this.ViewModel.RoundView.SortDescriptions.Clear();
            this.ViewModel.RoundView.SortDescriptions.Add(new SortDescription("ID", currentDirection));
        }

    }
}
