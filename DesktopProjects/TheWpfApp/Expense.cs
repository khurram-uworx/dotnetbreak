using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TheWpfApp
{
    public class Expense
    {
        public string ExpenseType { get; set; }
        public int ExpenseAmount { get; set; }
    }

    public class Person : INotifyPropertyChanged
    {
        List<Expense> expenses = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; }
        public string Department { get; set; }
        
        public IEnumerable<Expense> Expenses { get { return this.expenses; } }

        public Person(string name, string department)
        {
            this.Name = name;
            this.Department = department;
        }

        public void AddExpense(Expense expense)
        {
            this.expenses.Add(expense);

            if (null != this.PropertyChanged)
                this.PropertyChanged(this, new PropertyChangedEventArgs("Expenses"));
        }
    }
}
