using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.Models
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public DateTime PostingDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public short? CategoryUId { get; set; } 
        public TransactionType Type { get; set; }

    }

    public enum TransactionType
    {
        Debit = 1,
        Credit,
        Refund
    }
}
