// See https://aka.ms/new-console-template for more information
using ExpenseAnalyzer.Models;
using System.Globalization;
using System.IO;

//Ask for file path
string filePath = @"C:\Users\simya\source\repos\ExpenseAnalyzer\ExpenseAnalyzer\Files\Test.txt";
Console.WriteLine("Enter File Path:");
filePath = Console.ReadLine();



//read file 
string[] lines = File.ReadAllLines(filePath);
var transactions = ProcessRawTransactions(lines);


Console.WriteLine($"Total expense: {GetTotalExpsense(transactions)}");
Console.WriteLine($"Top N expense:");
PrintTransactions(GetNTopExpense(transactions, 5));




//extract details into a list of objects
List<Transaction> ProcessRawTransactions(string[] rawTransactions){
    List<Transaction> transactions = new List<Transaction>();

    try
    {
        foreach (var record in rawTransactions)
        {
            Transaction transaction = new();
            transaction.Type = TransactionType.Debit;

            var tokens = record.Split(" ");
            ExtractTransaction(transaction, tokens);

            transactions.Add(transaction);
        }
        return transactions; 
    }

    catch (Exception ex) {
        //TODO: Add log!
        Console.WriteLine($"Error: {ex.Message}");
        throw;
    }
    
    
}

static void ExtractTransaction(Transaction transaction, string[] tokens)
{
    //TODO: Add log!
    if (tokens.Length < 4) throw new Exception("Tokens must have at least 4 items");

    bool isTransactionDate = DateTime.TryParseExact(tokens[0],
                                                    "dd/MM/yyyy",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None,
                                                    out DateTime transactionDate);
    transaction.TransactionDate = transactionDate;

    bool isPostingDate = DateTime.TryParseExact(tokens[1],
                                                "dd/MM/yyyy",
                                                CultureInfo.InvariantCulture,
                                                DateTimeStyles.None,
                                                out DateTime postingDate);
    transaction.PostingDate = postingDate;

    transaction.Description = String.Join(" ", tokens[2..(tokens.Length - 1)]);

    //Handle for credited amount - has CR appended to the amount
    string amountText = tokens.Last();
    if (amountText.Substring(amountText.Length - 2) == "CR")
    {
        amountText = amountText.Substring(0, amountText.Length - 2);
        transaction.Type = TransactionType.Credit;
    }

    bool isAmount = Decimal.TryParse(amountText, out Decimal amount);
    transaction.Amount = amount;

    //TODO: add log!
    if (!(isTransactionDate && isPostingDate && isAmount))
        throw new NullReferenceException("Extracted Tokens cannot be null");
}


decimal GetTotalExpsense(List<Transaction> transactions)
{
    return transactions
            .Where(w => w.Type == TransactionType.Debit)
            .Sum(s => s.Amount);
}

List<Transaction> GetNTopExpense(List<Transaction> transactions, int n = 1)
{
    return transactions
            .Where(w => w.Type == TransactionType.Debit)
            .OrderByDescending(x => x.Amount)
            .Take(n).ToList();
}

void PrintTransactions(List<Transaction> transactions)
{
    foreach (var record in transactions)
    {
        Console.WriteLine($"Transaction Date: {record.TransactionDate}, " +
                          $"Posting Date: {record.PostingDate}, " +
                          $"Description: {record.Description}, " +
                          $"Amount: {record.Amount}, " +
                          $"Type: {record.Type.ToString()}");
    }
}