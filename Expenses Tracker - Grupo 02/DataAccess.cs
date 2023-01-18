using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Prueba
{
    class DataAccess
    {
        private List<Category> _categories;
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public DataAccess()
        {
            _categories = new List<Category>();
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        // Método para crear una nueva categoría
        public void CreateCategory(Category category)
        {
            _categories.Add(category);
        }

        // Método para obtener una categoría por su nombre
        public Category GetCategory(string name)
        {
            return _categories.FirstOrDefault(c => c.Name == name);
        }

        // Método para actualizar una categoría
        public void UpdateCategory(string name, string newName)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Name == name);
            if (existingCategory != null)
            {
                existingCategory.Name = newName;
            }
        }

        // Método para eliminar una categoría
        public void DeleteCategory(string name)
        {
            var existingCategory = _categories.FirstOrDefault(c => c.Name == name);
            if (existingCategory != null)
            {
                _categories.Remove(existingCategory);
            }
        }

        // Método para crear una nueva cuenta
        public void CreateAccount(Account account)
        {
            _accounts.Add(account);
        }

        // Método para obtener una cuenta por su nombre
        public Account GetAccountByName(string name)
        {
            return _accounts.FirstOrDefault(a => a.Name == name);
        }
        public List<Account> GetAccount()
        {
            return _accounts;
        }

        // Método para actualizar una cuenta
        public void UpdateAccount(Account account)
        {
            var existingAccount = _accounts.FirstOrDefault(a => a.Name == account.Name);
            if (existingAccount != null)
            {
                existingAccount.Name = account.Name;
                existingAccount.Balance = account.Balance;
            }
        }

        // Método para eliminar una cuenta
        public void DeleteAccount(string name)
        {
            var existingAccount = _accounts.FirstOrDefault(a => a.Name == name);
            if (existingAccount != null)
            {
                _accounts.Remove(existingAccount);
            }
        }

        // Método para crear una nueva transacción
        public void CreateTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
        // Método para obtener una transacción por su nombre
        public Transaction GetTransaction(string name)
        {
            return _transactions.FirstOrDefault(t => t.Name == name);
        }

        public List<Transaction> GetTransaction()
        {
            return _transactions;
        }

        // Método para actualizar una transacción
        public void UpdateTransaction(string name, Transaction newTransaction)
        {
            var existingTransaction = _transactions.FirstOrDefault(t => t.Name == name);
            if (existingTransaction != null)
            {
                existingTransaction.Type = newTransaction.Type;
                existingTransaction.Account = newTransaction.Account;
                existingTransaction.Category = newTransaction.Category;
                existingTransaction.Amount = newTransaction.Amount;
                existingTransaction.Currency = newTransaction.Currency;
                existingTransaction.Description = newTransaction.Description;
                existingTransaction.Date = newTransaction.Date;
            }
        }

        // Método para eliminar una transacción
        public void DeleteTransaction(string name)
        {
            var existingTransaction = _transactions.FirstOrDefault(t => t.Name == name);
            if (existingTransaction != null)
            {
                _transactions.Remove(existingTransaction);
            }
        }
    }

}
