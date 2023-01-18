using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.accounts;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Expenses_Tracker___Grupo_02;

List<string> listAux = new List<string>();
accounts aux = new accounts();

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
            "New transaction.", "View transactions.", "Edit items.", "Delete items.", "Help", "Exit"
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
            listAux.Add(account);

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
                Console.WriteLine("\nWhat kind of account is it?");
                account = Console.ReadLine();
                listAux.Add(account);
            }

            //account = AnsiConsole.Prompt(
            //        new SelectionPrompt<string>()
            //        .Title("\nType of account {Savings/Current/etc...}: ")
            //        .AddChoices(new[] {
            //            "Add new account",
            //            for (int i = 0; i < listCuenta.Count; i++)
            //{
            //    Console.WriteLine(listCuenta[i]);
            //}
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
                goto Menu;
            default:
                break;
        }

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


        break;
    case "View transactions.":
        break;
    case "Edit items.":
        break;
    case "Delete items.":
        Items:
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
                var deletedAccount = new MultiSelectionPrompt<string>();
                foreach (var accounts in listAux)
                {
                    deletedAccount.AddChoice(accounts);
                }
                var deletedAccounts = AnsiConsole.Prompt(deletedAccount);

                foreach (string fruit in deletedAccounts)
                {
                    aux.eliminarCategoria(listAux,fruit);
                }

                Console.WriteLine($"You delete those accounts.");

                foreach (string fruit in listAux)
                {
                    Console.WriteLine(fruit);
                }

                option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(new[] {
                    "Back", "Exit"
                }));

                switch (option)
                {
                    case "Back":
                        Console.Clear();
                        goto Items;
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


