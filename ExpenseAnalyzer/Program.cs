// See https://aka.ms/new-console-template for more information
using ExpenseAnalyzer.Models;
using ExpenseAnalyzer.ServiceLayer;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;
using Tabula;
using Tabula.Detectors;
using Tabula.Extractors;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

TransactionService _transaction = new TransactionService();

//PdfPig_ReadText();
//Tabula_ExtractTable_StreamMode();
Tabula_ExtractTable_LatticeMode();

//Ask for file path
//string filePath = @"C:\Users\simya\source\repos\ExpenseAnalyzer\ExpenseAnalyzer\Files\Test.txt";
//Console.WriteLine("Enter File Path:");
//filePath = Console.ReadLine();



////read file 
//string[] lines = File.ReadAllLines(filePath);

//var transactions = _transaction.ProcessRawTransactions(lines);
//var isSuccess = _transaction.SubmitTransactionsAndVendors(transactions);

//_transaction.GetTotalByCategory();


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


void PdfPig_ReadText()
{
    string input = "";
    using (PdfDocument document = PdfDocument.Open(@"C:\Users\simya\source\repos\Files\test.pdf"))
    {
        foreach (Page page in document.GetPages())
        {
            string pageText = page.Text;

            foreach (Word word in page.GetWords())
            {
                Console.WriteLine(word.Text);
                input = input + " " + word.Text;
            } 
        }
    }
}

void Tabula_ExtractTable_StreamMode()
{
    using (PdfDocument document = PdfDocument.Open(@"C:\Users\simya\source\repos\Files\test.pdf", new ParsingOptions() { ClipPaths = true }))
    {
        ObjectExtractor oe = new ObjectExtractor(document);
        PageArea page = oe.Extract(1);

        // detect canditate table zones
        SimpleNurminenDetectionAlgorithm detector = new SimpleNurminenDetectionAlgorithm();
        var regions = detector.Detect(page);

        IExtractionAlgorithm ea = new BasicExtractionAlgorithm();
        List<Table> tables = ea.Extract(page.GetArea(regions[0].BoundingBox)); // take first candidate area
        var table = tables[0];
        var rows = table.Rows;
    }
}

void Tabula_ExtractTable_LatticeMode()
{
    string path = @"C:\Users\simya\source\repos\Files\test.pdf";
    FileStream pdfStream = File.OpenRead(path);
    using (PdfDocument document = PdfDocument.Open(pdfStream, new ParsingOptions() { ClipPaths = true }))
    {
        ObjectExtractor oe = new ObjectExtractor(document);
        
        //Console.WriteLine(o.GetText());
        //Console.WriteLine(tel);
        //Console.WriteLine(te);
        //Console.WriteLine(tete);

        //foreach(var item in tel)
        //{
        //    Console.WriteLine(item.GetText());
        //}

        
        for(int i = 0; i < document.NumberOfPages; i++)
        {
            Console.WriteLine($"***************Page {i+1}*******************");
            PageArea page = oe.Extract(i+1);


            IExtractionAlgorithm ea = new SpreadsheetExtractionAlgorithm();
            List<Table> tables = ea.Extract(page);
            var output = tables[2].Rows[1];
            var o = output[0];
            var tel = output[0].TextElements;
            var te = output[0].TextElements[0];
            var tete = output[0].TextElements[0].TextElements[0];

            //Targeting the relevant table - start
            foreach (var table in tables)
            {

                Console.WriteLine("***************Table*******************");
                Console.WriteLine($"Cols: {table.ColumnCount}, Rows: {table.RowCount}, Area: {table.Area}, BoundingBox: {table.BoundingBox.ToString()}");
                foreach (var row in table.Rows)
                {
                    foreach (var t in row)
                    {
                        string textElement = "";
                        var test = string.Join(" | ", t.TextElements);
                        foreach (var e in t.TextElements)
                        {
                            textElement = textElement + " | " + e.GetText();
                        }
                        Console.WriteLine($"text: {textElement}");

                    }
                }
            }
            //Targeting the relevant table - end
        }



    }
}