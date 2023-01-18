using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.CRUDs;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Expenses_Tracker___Grupo_02;



List<string> listAccount = new List<string>();
List<string> listCategory = new List<string>();
CRUDs aux = new CRUDs();

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
Console.WriteLine();


option = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .AddChoices(new[] {
            "New item.", "View items.", "Edit items.", "Delete items.", "Help", "Exit"
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
                    Console.WriteLine("You must fill the form.");
                    goto NewTrans;
                }
                Console.Write("\nType [Expense/Income]: ");
                var type = Console.ReadLine();
                var account = "";
                var category = "";
                if (listAccount.Count == 0)
                {
                    Console.WriteLine("\nIt looks like you haven't created an account type yet");
                    Console.Write("What kind of account is it? ");
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
                    Console.Write("What kind of category is it? ");
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
                var amount = Console.ReadLine();
                Console.Write("\nDescription: ");
                var description = Console.ReadLine();
                string dateTime = DateTime.Now.ToString();
                Console.Write("\n");

                var tableNewTransaction = new Table();
                tableNewTransaction.AddColumn(nameTransaction);
                tableNewTransaction.AddColumn("X");
                tableNewTransaction.AddRow("Type", type);
                tableNewTransaction.AddRow("Type of account", account);
                tableNewTransaction.AddRow("Category", category);
                tableNewTransaction.AddRow("Amount", amount);
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
                goto Menu;
            default:
                break;
        }

        break;
    case "Help":
        Console.WriteLine("Intec - Expense Tracker will help you keep track of your money. \n" +
            "With simple and intuitive graphics you can check the progress of expenses.");
        Console.ReadKey();
        goto Menu;
    default:
        break;
}