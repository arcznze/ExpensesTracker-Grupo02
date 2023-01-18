using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.Category;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Prueba;


var dataAccess = new DataAccess();
var listCuenta = new List<string>();

var tableTitle = new Table();
var option = "";
var rule = new Rule("Menu");
var tableMenu = new Table();

//MENU PRINCIPAL

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
                    "New transaction", "View transactions", "Edit transactions", "Help", "Exit"
                }));

switch (option)
{
    case "New transaction":
        Console.WriteLine("Enter the name of the transaction: ");
        var nameTransaction = Console.ReadLine();
        Console.Write("\n");
        Console.Write("Type [Expense/Income]: ");
        var type = Console.ReadLine();
        var account = "";
        if (listCuenta.Count == 0)
        {
            Console.WriteLine("\nIt looks like you haven't created an account type yet");
            Console.WriteLine("What kind of account is it?");
            account = Console.ReadLine();
            var newAccount = new Account { Name = account };
            dataAccess.CreateAccount(newAccount);
            listCuenta.Add(account);
        }
        else
        {
            var accounts = dataAccess.GetAccount();
            account = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Type of account {Savings/Current/etc...}: ")
                .AddChoices(accounts.Select(a => a.Name).ToArray()));
        }
        Console.Write("\nCategory: ");
        var categoryName = Console.ReadLine();
        var category = dataAccess.GetCategory(categoryName);
        if (category == null)
        {
            category = new Category { Name = categoryName };
            dataAccess.CreateCategory(category);
        }
        Console.Write("Amount: ");
        var amount = Console.ReadLine();
        Console.Write("Description: ");
        var description = Console.ReadLine();

        var newTransaction = new Transaction
        {
            Name = nameTransaction,
            Type = type,
            Account = dataAccess.GetAccountByName(account),
            Category = category,
            Amount = decimal.Parse(amount),
            Description = description,
            Date = DateTime.Now
        };
        dataAccess.CreateTransaction(newTransaction);

        var tableNewTransaction = new Table();
        tableNewTransaction.AddColumn(nameTransaction);
        tableNewTransaction.AddColumn("X");
        tableNewTransaction.AddRow("Type", type);
        tableNewTransaction.AddRow("Type of account", account);
        tableNewTransaction.AddRow("Category", categoryName);
        tableNewTransaction.AddRow("Amount", amount);
        tableNewTransaction.AddRow("Description", description);
        tableNewTransaction.AddRow("Date / Time", newTransaction.Date.ToString());
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
                goto Menu;
            default:
                break;
        }
        break;

    //if (listCuenta.Count == 0)
    //{
    //    optionCuenta = AnsiConsole.Prompt(
    //        new SelectionPrompt<string>()
    //        .AddChoices(new[] {
    //            "Add new account"
    //        }));
    //}
    //else
    //{
    //    optionCuenta = AnsiConsole.Prompt(
    //        new SelectionPrompt<string>()
    //        .AddChoices(new[] {
    //            "Add new account", "View accounts", "Edit accounts", "Delete account"
    //        }));
    //}


    case "View transactions":
        var transactions = dataAccess.GetTransaction();
        var tableTransactions = new Table();
        tableTransactions.AddColumns("Name", "Type", "Account", "Category", "Amount", "Description", "Date / Time");


        foreach (var transaction in transactions)
        {
            if (transaction.Account != null && transaction.Category != null)
            {
                tableTransactions.AddRow(new[] {transaction.Name, transaction.Type, transaction.Account.Name,
                        transaction.Category.Name, transaction.Amount.ToString(),
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
    case "Edit transactions":
        //TODO: Implementar opción para editar transacciones usando DataAccess
        break;
    case "Help":
        Console.WriteLine("Intec - Expenses Tracker will help you keep track of your money. \n" +
            "With simple and intuitive graphics you can check the progress of expenses.");

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


