using System.Windows;
using System.Windows.Controls;

namespace TheWpfApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            Report reportPage = new Report(this.peopleListBox.SelectedItem);
            this.NavigationService.Navigate(reportPage);
        }
    }
}
