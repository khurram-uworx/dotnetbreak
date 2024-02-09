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

        public Report(Person person) : this()
        {
            // Bind to expense report data.
            this.DataContext = person;
            person.PropertyChanged += delegate
            {
                this.DataGridExpenses.Items.Refresh();
            };
        }
    }
}
