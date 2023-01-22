using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.CRUDs;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Expenses_Tracker___Grupo_02;
using System.Transactions;
using System.Collections;
using System.Linq;

public class Transaction
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Account { get; set; }
    public string Category { get; set; }
    public float Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        IBuscadorTasas buscadorTasas = new BuscadorTasas(); 
        Convertir convertidor = new Convertir(buscadorTasas); 

        List<Transaction> listTransaction = new List<Transaction>();
        List<float> balance = new List<float>();
        List<string> listAccount = new List<string>();
        List<string> listCategory = new List<string>();
        CRUDs aux = new CRUDs();
        float expenseChart = 0;
        float incomeChart = 0;

        //MENU PRINCIPAL
        var tableTitle = new Table();
        var option = "";
        var rule = new Rule("Menu");
        var tableMenu = new Table();
        tableTitle.AddColumn("Intec - Expense Tracker").Centered();
        tableTitle.Expand();
        tableTitle.Columns[0].Centered();

    Menu:
        AnsiConsole.Write(tableTitle);
        rule.Centered();
        AnsiConsole.Write(rule);
        Console.WriteLine("\n");
        option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
            "New item.", "View items.", "Edit items.", "Delete items.", "View balance report.", "Exit"
                }));

        switch (option)
        {
            //NEW ITEM
            case "New item.":
            NewItem:

                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "New transaction.", "New account.", "New category.", "Back", "Exit"
                }));

                switch (option)
                {
                    case "New transaction.":
                    NewTrans:
                        Console.Write("Enter the name of the transaction: ");
                        var nameTransaction = Console.ReadLine();
                        if (String.IsNullOrEmpty(nameTransaction))
                        {
                            Console.WriteLine("You must fill the form.\n");
                            Console.ReadKey();
                            Console.Clear();
                            goto NewTrans;
                        }
                        Console.Write("\nType [Expense/Income]: ");
                        var type = Console.ReadLine();
                        type = type.ToLower();
                        if (String.IsNullOrEmpty(nameTransaction))
                        {
                            Console.WriteLine("You must fill the form.\n");
                            Console.ReadKey();
                            Console.Clear();
                            goto NewTrans;
                        }
                        else if (type != "income" && type != "expense")
                        {
                            Console.WriteLine("You must enter a valid type.\n");
                            Console.ReadKey();
                            Console.Clear();
                            goto NewTrans;
                        }
                        var account = "";
                        var category = "";
                        if (listAccount.Count == 0)
                        {
                            Console.WriteLine("\nIt looks like you haven't created an account type yet");
                            Console.Write("What kind of account is it? [Savings/Current/etc...] ");
                            account = Console.ReadLine();
                            aux.create(listAccount, account);

                        }
                        else
                        {
                            var accountAux = new SelectionPrompt<string>();
                            foreach (var accounts in listAccount)
                            {
                                accountAux.AddChoice(accounts);
                            }
                            accountAux.AddChoice("Add new account");

                            account = AnsiConsole.Prompt(accountAux);

                            if (account == "Add new account")
                            {
                                Console.Write("\nWhat kind of account is it?");
                                account = Console.ReadLine();
                                aux.create(listAccount, account);
                            }
                        }

                        Console.Write("\nType of account [Savings/Current/etc...]: " + account.ToString() + "\n");

                        if (listCategory.Count == 0)
                        {
                            Console.WriteLine("\nIt looks like you haven't created a category type yet");
                            Console.Write("What kind of category is it? [Home, Bills, Health, etc...] ");
                            category = Console.ReadLine();
                            aux.create(listCategory, category);

                        }
                        else
                        {
                            var categoryAux = new SelectionPrompt<string>();
                            foreach (var categories in listCategory)
                            {
                                categoryAux.AddChoice(categories);
                            }
                            categoryAux.AddChoice("Add new category");

                            category = AnsiConsole.Prompt(categoryAux);

                            if (category == "Add new category")
                            {
                                Console.Write("\nWhat kind of category is it?");
                                category = Console.ReadLine();
                                aux.create(listCategory, category);
                            }
                        }


                        Console.Write("\nCategory: " + category + "\n");
                        Console.Write("\nAmount: ");
                        float amount = float.Parse(Console.ReadLine());

                        var rate = AnsiConsole.Prompt(
                            new SelectionPrompt<string>().Title("Is the amount in Dominican pesos or dollars?")
                                .AddChoices(new[] {
                    "RD$", "USD$"
                                }));

                        if (rate == "USD$")
                        {
                            amount = convertidor.ComprarDolares(amount);
                        }

                        Console.Write("\nDescription: ");
                        var description = Console.ReadLine();
                        if (String.IsNullOrEmpty(description))
                        {
                            Console.WriteLine("You must fill the form.\n");
                            Console.ReadKey();
                            Console.Clear();
                            goto NewTrans;
                        }
                        string dateTime = DateTime.Now.ToString();
                        Console.Write("\n");

                        if (type == "Expense" || type == "expense")
                        {
                            amount *= -1;
                        }

                        var newTransaction = new Transaction
                        {
                            Name = nameTransaction,
                            Type = type,
                            Account = account,
                            Category = category,
                            Amount = amount,
                            Description = description,
                            Date = DateTime.Now
                        };
                        listTransaction.Add(newTransaction);
                        balance.Add(amount);

                        for (int i = 0; i < balance.Count; i++)
                        {
                            if (balance[i] < 0)
                            {
                                expenseChart += float.Parse(balance[i].ToString());
                            }
                            else
                            {
                                incomeChart += float.Parse(balance[i].ToString());
                            }
                        }

                        var tableNewTransaction = new Table();
                        tableNewTransaction.AddColumn(nameTransaction);
                        tableNewTransaction.AddColumn("X");
                        tableNewTransaction.AddRow("Type", type);
                        tableNewTransaction.AddRow("Type of account", account);
                        tableNewTransaction.AddRow("Category", category);
                        tableNewTransaction.AddRow("Amount", "RD$" + amount.ToString());
                        tableNewTransaction.AddRow("Description", description);
                        tableNewTransaction.AddRow("Date / Time", dateTime);
                        AnsiConsole.Write(tableNewTransaction);

                        option = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .AddChoices(new[] {
                    "Back", "Exit"
                                }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto NewItem;
                        }
                        break;
                    case "New account.":
                        Console.Write("What kind of account is it? ");
                        var newAccount = Console.ReadLine();
                        aux.create(listAccount, newAccount);

                        Console.WriteLine("Account created.\n");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                            "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto NewItem;
                        }
                        break;
                    case "New category.":
                        Console.Write("What kind of category is it? ");
                        var newCategory = Console.ReadLine();
                        aux.create(listCategory, newCategory);

                        Console.WriteLine("Category created.\n");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                            "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto NewItem;
                        }
                        break;
                    case "Back":
                        Console.Clear();
                        goto Menu;
                }
                break;

            //VIEW ITEMS
            case "View items.":
            ViewItems:
                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                        "View transactions.", "View accounts.", "View categories.", "Back", "Exit"
                        }));

                switch (option)
                {
                    case "View transactions.":
                        var tableTransactions = new Table();
                        tableTransactions.AddColumns("Name", "Type", "Account", "Category", "Amount", "Description", "Date / Time");

                        if (listTransaction.Count == 0)
                        {
                            Console.WriteLine("No hay transacciones registradas.");

                        }
                        else
                        {
                            Console.WriteLine("Transacciones registradas: ");

                            foreach (var transaction in listTransaction)
                            {
                                tableTransactions.AddRow(new[] {transaction.Name, transaction.Type, transaction.Account,
                            transaction.Category, transaction.Amount.ToString(),
                            transaction.Description, transaction.Date.ToString() });
                            }
                        }

                        AnsiConsole.Write(tableTransactions);
                        option = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .AddChoices(new[] {
                            "Back", "Exit"
                                }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto Menu;
                            default:
                                break;
                        }
                        break;

                    case "View accounts.":
                        aux.read(listAccount);
                        Console.WriteLine("\n");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                        "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto ViewItems;
                        }
                        break;
                    case "View categories.":
                        aux.read(listCategory);
                        Console.WriteLine("\n");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                        "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto ViewItems;
                        }
                        break;
                    case "Back":
                        Console.Clear();
                        goto Menu;
                    default:
                        break;
                }
                break;
            case "Edit items.":
            EditItems:
                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                            "Edit transactions.", "Edit accounts.", "Edit categories.", "Back", "Exit"
                        }));

                switch (option)
                {
                    case "Edit transactions.":

                        Console.WriteLine("Select the transactions you want to edit.");
                        var editedTransaction = new MultiSelectionPrompt<string>().NotRequired();
                        foreach (var transactions in listTransaction)
                        {
                            editedTransaction.AddChoice(transactions.Name);
                        }

                        var editedTransactions = AnsiConsole.Prompt(editedTransaction);
                        foreach (var transactions in editedTransactions)
                        {
                            var transactionToEdit = listTransaction.FirstOrDefault(t => t.Name == transactions);
                            var editOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                .AddChoices(new[] { "Name", "Type", "Account", "Category", "Amount", "Description" }));
                            switch (editOption)
                            {
                                case "Name":
                                    Console.WriteLine("Enter the new name for the transaction: ");
                                    var newName = Console.ReadLine();
                                    transactionToEdit.Name = newName;
                                    break;
                                case "Type":
                                    Console.WriteLine("Enter the new type for the transaction: ");
                                    var newType = Console.ReadLine();
                                    transactionToEdit.Type = newType;
                                    break;
                                case "Account":
                                    Console.WriteLine("Enter the new account for the transaction: ");
                                    var newAccounts = Console.ReadLine();
                                    int index = listAccount.IndexOf(transactionToEdit.Account);
                                    if (index != -1)
                                    {
                                        listAccount[index] = newAccounts;
                                        transactionToEdit.Account = newAccounts;
                                    }
                                    else
                                    {
                                        Console.WriteLine("La cuenta no se encuentra en la lista de cuentas");
                                    }
                                    break;
                                case "Category":
                                    Console.WriteLine("Enter the new category for the transaction: ");
                                    var newCategorys = Console.ReadLine();
                                    index = listCategory.IndexOf(transactionToEdit.Category);
                                    if (index != -1)
                                    {
                                        listCategory[index] = newCategorys;
                                        transactionToEdit.Account = newCategorys;
                                    }
                                    else
                                    {
                                        Console.WriteLine("La cuenta no se encuentra en la lista de categorias");
                                    }
                                    break;
                                case "Amount":
                                    Console.WriteLine("Enter the new amount for the transaction: ");
                                    var newAmount = float.Parse(Console.ReadLine());
                                    transactionToEdit.Amount = newAmount;
                                    break;
                                case "Description":
                                    Console.WriteLine("Enter the new description for the transaction: ");
                                    var newDescription = Console.ReadLine();
                                    transactionToEdit.Description = newDescription;
                                    break;
                            }
                            Console.WriteLine("Transaction edited successfully");
                        }
                        Console.WriteLine($"You edited those transactions.");

                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .AddChoices(new[] {
                        "Back", "Exit"
                        }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto EditItems;
                        }
                        break;

                    case "Edit accounts.":
                        foreach (string accounts in listAccount)
                        {
                            Console.Write(accounts);
                        }
                        Console.Write("Which account would you like to change? ");
                        var oldAccount = Console.ReadLine();
                        Console.Write("To which? ");
                        var newAccount = Console.ReadLine();
                        aux.edit(listAccount, oldAccount, newAccount);

                        Console.WriteLine("Changes made.");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                            "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto EditItems;
                        }
                        break;
                    case "Edit categories.":
                        foreach (string category in listCategory)
                        {
                            Console.Write(category);
                        }
                        Console.Write("\nWhich category would you like to change? ");
                        var oldCategory = Console.ReadLine();
                        Console.Write("To which? ");
                        var newCategory = Console.ReadLine();
                        aux.edit(listCategory, oldCategory, newCategory);

                        Console.WriteLine("Changes made.");
                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {
                            "Back", "Exit"
                            }));
                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto EditItems;
                        }
                        break;
                    case "Back":
                        Console.Clear();
                        goto Menu;
                    default:
                        break;
                }
                break;

            //DELETE ITEMS
            case "Delete items.":
            DeletedItems:
                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "Delete transactions.", "Delete accounts.", "Delete categories.", "Back", "Exit"
                        }));

                switch (option)
                {
                    case "Delete transactions.":
                        Console.WriteLine("Select the transactions you want to remove.");
                        var deletedTransaction = new MultiSelectionPrompt<string>().NotRequired();
                        foreach (var transactions in listTransaction)
                        {
                            deletedTransaction.AddChoice(transactions.Name);
                        }

                        var deletedTransactions = AnsiConsole.Prompt(deletedTransaction);
                        foreach (string transactions in deletedTransactions)
                        {
                            listTransaction.RemoveAll(t => t.Name == transactions);
                        }

                        Console.WriteLine($"You delete those transactions.");

                        option = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .AddChoices(new[] {
                        "Back", "Exit"
                        }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto DeletedItems;
                        }
                        break;

                    case "Delete accounts.":
                        Console.WriteLine("Select the accounts you want to remove.");
                        var deletedAccount = new MultiSelectionPrompt<string>().NotRequired();
                        foreach (var accounts in listAccount)
                        {
                            deletedAccount.AddChoice(accounts);
                        }

                        var deletedAccounts = AnsiConsole.Prompt(deletedAccount);

                        foreach (string accounts in deletedAccounts)
                        {
                            aux.delete(listAccount, accounts);
                        }

                        Console.WriteLine($"You delete those accounts.");

                        option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "Back", "Exit"
                        }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto DeletedItems;
                        }
                        break;
                    case "Delete categories.":
                        Console.WriteLine("Select the categories you want to remove.");
                        var deletedCategory = new MultiSelectionPrompt<string>().NotRequired();
                        foreach (var categories in listCategory)
                        {
                            deletedCategory.AddChoice(categories);
                        }

                        var deletedCategories = AnsiConsole.Prompt(deletedCategory);

                        foreach (string categories in deletedCategories)
                        {
                            aux.delete(listCategory, categories);
                        }

                        Console.WriteLine($"You delete those categories.");

                        option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "Back", "Exit"
                        }));

                        switch (option)
                        {
                            case "Back":
                                Console.Clear();
                                goto DeletedItems;
                        }
                        break;
                    case "Back":
                        Console.Clear();
                        goto Menu;
                    default:
                        break;
                }

                break;
            case "View balance report.":
                AnsiConsole.Write(new BarChart()
                    .AddItem("Expense", expenseChart * -1, Color.Red)
                    .AddItem("Income", incomeChart, Color.Blue));

                Console.WriteLine("\n");

                option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {
                    "Back", "Exit"
                        }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        goto Menu;
                }
                break;
            default:
                break;
        }
    }
}