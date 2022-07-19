using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.BLL.Models
{
    public class BaseResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
    }
}
