using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TheWpfApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        ObservableCollection<Person> persons = new();

        public Home()
        {
            InitializeComponent();

            this.ListBoxPeople.ItemsSource = persons;

            this.Loaded += async delegate
            {
                await Task.Delay(2000);

                var mike = new Person("Mike", "Legal");
                var lisa = new Person("Lisa", "Marketing");
                var john = new Person("John", "Engineering");
                var mary = new Person("Mary", "Finance");

                this.persons.Add(mike);
                this.persons.Add(lisa);
                this.persons.Add(john);
                this.persons.Add(mary);

                await Task.Delay(5000);

                mike.AddExpense(new Expense { ExpenseType = "Lunch", ExpenseAmount = 50 });
                mike.AddExpense(new Expense { ExpenseType = "Transportation", ExpenseAmount = 50 });

                lisa.AddExpense(new Expense { ExpenseType = "Document printing", ExpenseAmount = 50 });
                lisa.AddExpense(new Expense { ExpenseType = "Gift", ExpenseAmount = 125 });

                john.AddExpense(new Expense { ExpenseType = "Magazine subscription", ExpenseAmount = 50 });
                john.AddExpense(new Expense { ExpenseType = "New machine", ExpenseAmount = 6000 });
                john.AddExpense(new Expense { ExpenseType = "Software", ExpenseAmount = 500 });

                mary.AddExpense(new Expense { ExpenseType = "Dinner", ExpenseAmount = 100 });
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            Report reportPage = new Report(this.ListBoxPeople.SelectedItem as Person);
            this.NavigationService.Navigate(reportPage);
        }
    }
}
