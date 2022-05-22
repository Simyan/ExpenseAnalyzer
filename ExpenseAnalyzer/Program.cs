// See https://aka.ms/new-console-template for more information
using ExpenseAnalyzer.Models;
using ExpenseAnalyzer.ServiceLayer;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;



//Ask for file path
string filePath = @"C:\Users\simya\source\repos\ExpenseAnalyzer\ExpenseAnalyzer\Files\Test.txt";
Console.WriteLine("Enter File Path:");
filePath = Console.ReadLine();



////read file 
string[] lines = File.ReadAllLines(filePath);

TransactionService _transaction = new TransactionService();
var transactions = _transaction.ProcessRawTransactions(lines);
var isSuccess = _transaction.SubmitTransactionsAndVendors(transactions);


//Console.WriteLine($"Total expense: {GetTotalExpsense(transactions)}");
//Console.WriteLine($"Top N expense:");
//PrintTransactions(GetNTopExpense(transactions, 5));

//GetTransaction();

//SubmitTransactions();






void GetTransactionTest() {
    TransactionService _transaction = new TransactionService();
    var result = _transaction.GetTransaction(1);
    Console.WriteLine("Vendor of the transaction is: " + result.Description);  
}


//void SubmitTransactionsTest()
//{
//    ExpenseAnalyzer.Entities.Transaction t1 = new ExpenseAnalyzer.Entities.Transaction() {  Description = "Vendor1" , Amount = 10, PostingDate = DateTime.Now, TransactionDate = DateTime.Now, TypeUid = 1};
//    ExpenseAnalyzer.Entities.Transaction t2 = new ExpenseAnalyzer.Entities.Transaction() {  Description = "Vendor2", Amount = 15, PostingDate = DateTime.Now, TransactionDate = DateTime.Now, TypeUid = 1 };
//    ExpenseAnalyzer.Entities.Transaction t3 = new ExpenseAnalyzer.Entities.Transaction() {  Description = "Vendor3", Amount = 100, PostingDate = DateTime.Now, TransactionDate = DateTime.Now, TypeUid = 1 };
//    var ls = new List<ExpenseAnalyzer.Entities.Transaction>();
//    ls.Add(t1);
//    ls.Add(t2);
//    ls.Add(t3);
//    TransactionService _transaction = new TransactionService();
//    _transaction.SubmitTransactionsAndVendors(ls);
//}