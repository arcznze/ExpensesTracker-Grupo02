namespace TestExpensesTracker
{
    [TestClass]
    public class UnitTest
    {
        List<string> _listAccount = new List<string>();
        List<Transaction> _listTransaction = new List<Transaction>();

        [TestMethod]
        public void TestNewAccount()
        {
            // Arrange
            CRUDs aux = new CRUDs();
            string newAccount = "Savings";

            // Act
            aux.create(_listAccount, newAccount);

            // Assert
            Assert.IsTrue(_listAccount.Contains(newAccount));
        }

        [TestMethod]
        public void TestNewCategory()
        {
            // Arrange
            CRUDs aux = new CRUDs();
            string newCategory = "home";
            List<string> _listAccount = new List<string>();

            // Act
            aux.create(_listAccount, newCategory);

            // Assert
            Assert.IsTrue(_listAccount.Contains(newCategory));
        }

        [TestMethod]
        public void TestEditTransaction()
        {
            // Arrange
            CRUDs aux = new CRUDs();
            // Crear una transacción de prueba
            _listTransaction.Add(new Transaction
            {
                Name = "Test Transaction",
                Type = "Expense",
                Account = "Savings",
                Category = "Bills",
                Amount = 100,
                Description = "Test Description",
                Date = DateTime.Now
            });

            // Act
            var editedTransactions = new List<string> { "Test Transaction" };
            foreach (var transactions in editedTransactions)
            {
                var transactionToEdit = _listTransaction.FirstOrDefault(t => t.Name == transactions);
                var editOption = "Amount"; // Seleccionar opcion de edicion
                var editOption2 = "Category"; // Seleccionar opcion de edicion
                switch (editOption)
                {
                    case "Amount":
                        var newAmount = 150;
                        transactionToEdit.Amount = newAmount;
                        break;
                }
                switch (editOption2)
                {
                    case "Category":
                        var newCategory = "Homes";
                        transactionToEdit.Category = newCategory;
                        break;
                }
            }
            // Obtener la transacción editada
            var editedTransaction = _listTransaction.FirstOrDefault(t => t.Name == "Test Transaction");

            // Assert
            Assert.AreEqual(150, editedTransaction.Amount);
            Assert.AreEqual("Homes", editedTransaction.Category);
        }
    }
}
