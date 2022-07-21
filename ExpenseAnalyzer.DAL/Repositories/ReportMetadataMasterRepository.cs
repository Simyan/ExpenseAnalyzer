using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.Models;
using ExpenseAnalyzer.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAnalyzer.DAL.Repositories
{
    public class ReportMetadataMasterRepository : IReportMetadataMasterRepository
    {
        private readonly ExpenseAnalyzerContext _context;

        public ReportMetadataMasterRepository(ExpenseAnalyzerContext context)
        {
            _context = context;
        }

        public ReportMetadataDTO GetReportMetadata(int BankUId)
        {
            var result = _context.ReportMetadatas
                    .Where(x => x.BankMasterUid == BankUId)
                    .FirstOrDefault();

            return new ReportMetadataDTO
            {
                RowIndex = result.RowIndex,
                TableArea = result.TableArea,
                TableHeaders = result.TableHeaders.Split('|').ToList(),
                TableIndex = result.TableIndex
            };
                    
        }
    }
}
