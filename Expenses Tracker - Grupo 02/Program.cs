using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.Accounts;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Expenses_Tracker___Grupo_02;

List<string> listAux = new List<string>();
Accounts aux = new Accounts();

//MENU PRINCIPAL
var tableTitle = new Table();
var option = "";
var rule = new Rule("Menu");
var tableMenu = new Table();
tableTitle.AddColumn("Intec - Expenses Tracker").Centered();
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
                Console.WriteLine("Enter the name of the transaction: ");
                var nameTransaction = Console.ReadLine();
                Console.Write("\nType [Expense/Income]: ");
                var type = Console.ReadLine();
                var account = "";
                if (listAux.Count == 0)
                {
                    Console.WriteLine("\nIt looks like you haven't created an account type yet");
                    Console.Write("What kind of account is it? ");
                    account = Console.ReadLine();
                    aux.createAccount(listAux, account);

                }
                else
                {
                    var accountAux = new SelectionPrompt<string>();
                    foreach (var accounts in listAux)
                    {
                        accountAux.AddChoice(accounts);
                    }
                    accountAux.AddChoice("Add new account");

                    account = AnsiConsole.Prompt(accountAux);

                    if (account == "Add new account")
                    {
                        Console.Write("\nWhat kind of account is it?");
                        account = Console.ReadLine();
                        aux.createAccount(listAux, account);
                    }
                }

                Console.Write("\nType of account [Savings/Current/etc...]: " + account.ToString() + "\n");
                Console.Write("\nCategory: ");
                var category = Console.ReadLine();
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
                aux.createAccount(listAux, newAccount);

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
                aux.readAccount(listAux);
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

                Console.Write("Which account would you like to change? ");
                var oldAccount = Console.ReadLine();
                Console.Write("To which? ");
                var newAccount = Console.ReadLine();
                aux.editAccount(listAux, oldAccount, newAccount);

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
                var deletedAccount = new MultiSelectionPrompt<string>().NotRequired();
                foreach (var accounts in listAux)
                {
                    deletedAccount.AddChoice(accounts);
                }

                var deletedAccounts = AnsiConsole.Prompt(deletedAccount);

                foreach (string fruit in deletedAccounts)
                {
                    aux.deleteAccount(listAux, fruit);
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
                break;
            case "Back":
                goto Menu;
            default:
                break;
        }

        break;
    case "Help":
        Console.WriteLine("Intec - Expenses Tracker will help you keep track of your money. \n" +
            "With simple and intuitive graphics you can check the progress of expenses.");
        break;
    default:
        break;
}


//List<string> myList = new List<string>();
//Class1 clase = new Class1();

//Menu:
//Console.Clear();
//Console.WriteLine("Que desea hacer?");
//Console.WriteLine("1. Agregar un nombre a la lista.");
//Console.WriteLine("2. Editar nombre de la lista.");
//Console.WriteLine("3. Eliminar nombre de la lista.");
//Console.WriteLine("4. Ver lista completa.");
//Console.WriteLine("5. Salir del programa.");
//int input = int.Parse(Console.ReadLine());



//switch (input)
//{
//    case 1:
//        Console.WriteLine("\nIngrese un nombre:");
//        string name = Console.ReadLine();
//        clase.crearCategoria(name);
//        Console.ReadKey();
//        goto Menu;

//    case 2:
//        Console.WriteLine("\nQue categoria desea editar?");
//        string edited = Console.ReadLine();
//        Console.WriteLine("\nCual es el nuevo nombre?");
//        string edit = Console.ReadLine();
//        clase.editarCategoria(edit,edited);
//        Console.ReadKey();
//        goto Menu;
//    case 3:
//        Console.WriteLine("\nIngrese el nombre a eliminar:");
//        string delete = Console.ReadLine();
//        clase.eliminarCategoria(delete);
//        Console.ReadKey();
//        goto Menu;
//    case 4:
//        Console.Write("\n");
//        clase.verCategorias();
//        Console.ReadKey();
//        goto Menu;
//    case 5:
//        break;
//    default:
//        Console.WriteLine("\nOpcion no permitida.");
//        break;
//}


