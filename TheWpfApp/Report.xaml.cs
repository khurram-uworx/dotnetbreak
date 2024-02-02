using System.Windows.Controls;

namespace TheWpfApp
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        public Report()
        {
            InitializeComponent();
        }

        public Report(object data) : this()
        {
            // Bind to expense report data.
            this.DataContext = data;
        }
    }
}
