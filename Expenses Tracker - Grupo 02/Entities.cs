using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba
{
    public class Category
    {
        public string Name { get; set; }
    }

    // Clase para representar una Cuenta
    public class Account
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    // Clase para representar una Transacción
    public class Transaction
    {
        public string Name { get; set; }
        public string Type { get; set; } // "Expense o Income"
        public Account Account { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
