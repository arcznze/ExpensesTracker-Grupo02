using System.ComponentModel.Design;
using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using static Expenses_Tracker___Grupo_02.accounts;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;


List<string> listCuenta = new List<string>();


//MENU PRINCIPAL
var tableTitle = new Table();
var option = "";
var optionCuenta = "";
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
            listCuenta.Add(account);

        }
        else
        {
            for (int i = 0; i < listCuenta.Count; i++)
            {
                account = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Type of account {Savings/Current/etc...}: ")
                    .AddChoices(new[] {
                                listCuenta[i],
                    }));
            }
        }
        Console.Write("\nType of account [Savings/Current/etc...]: " + account.ToString());
        Console.Write("\nCategory: ");
        var category = Console.ReadLine();
        Console.Write("Amount: ");
        var amount = Console.ReadLine();
        Console.Write("Description: ");
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
    case "View transactions":
        break;
    case "Edit transactions":
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


