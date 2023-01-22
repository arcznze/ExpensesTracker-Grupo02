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
        public void TestViewTransactions()
        {
            //Arrange
            //Add some test transactions to _listTransaction
            _listTransaction.Add(new Transaction
            {
                Name = "TestTransaction1",
                Type = "Expense",
                Account = "Savings",
                Category = "Home",
                Amount = 100,
                Description = "Test Description",
                Date = DateTime.Now
            });
            _listTransaction.Add(new Transaction
            {
                Name = "TestTransaction2",
                Type = "Income",
                Account = "Current",
                Category = "Bills",
                Amount = 200,
                Description = "Test Description",
                Date = DateTime.Now
            });
            var expectedResult = "Transaction Viewed successfully.";

            //Act
            Program.ViewItems();
            var result = Console.Out.ToString();

            //Assert
            StringAssert.Contains(result, expectedResult);
        }
    }
}
