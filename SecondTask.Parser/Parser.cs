using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondTask.Model.Models;

namespace SecondTask.Parser
{
    public class Parser
    {
        public List<Accounting> Parse(IFormFile uploadedFile)
        {
            var accountings = new List<Accounting>();
            if (uploadedFile != null)
            {
                using var workbook = new XLWorkbook(uploadedFile.OpenReadStream());
                var worksheets = workbook.Worksheets;
                foreach (var sheet in worksheets)
                {
                    var rows = sheet.RangeUsed().RowsUsed(); //gets used rows in excel file
                    ;
                    foreach (var row in rows.Where(x => Int32.TryParse(x.Cell(1).Value.ToString(), out int val))
                        .Where(x => Convert.ToInt32(x.Cell(1).Value.ToString()) > 99))
                    {
                        accountings.Add(new Accounting              //creates a new entity of one line in excel and adds to list
                        {
                            Account = Convert.ToInt32(row.Cell(1).Value.ToString()),
                            OpenBalance_A = Convert.ToDouble(row.Cell(2).Value.ToString()),
                            OpenBalance_P = Convert.ToDouble(row.Cell(3).Value.ToString()),
                            Turnover_Db = Convert.ToDouble(row.Cell(4).Value.ToString()),
                            TurnOver_Ct = Convert.ToDouble(row.Cell(5).Value.ToString()),
                            ClosBalance_A = Convert.ToDouble(row.Cell(6).Value.ToString()),
                            ClosBalance_P = Convert.ToDouble(row.Cell(7).Value.ToString())
                        });
                    }
                }

            }
            return accountings;
        }

        public FileContentResult GenerateFile(List<Model.Models.File> files)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add();     
                worksheet.Cell("A1").Value = "Б/сч";
                worksheet.Cell("B1").Value = "Входящее сальдо(Актив)";
                worksheet.Cell("C1").Value = " Входящее сальдо(Пассив)";    // adds first line into generated excel file
                worksheet.Cell("D1").Value = "Обороты(Дебет)";
                worksheet.Cell("E1").Value = "Обороты(Кредит)";
                worksheet.Cell("F1").Value = "Исходящее сальдо(Актив)";
                worksheet.Cell("G1").Value = "Исходящее сальдо(Пассив)";
                worksheet.Row(1).Style.Font.Bold = true;

                foreach (var file in files)
                {
                    worksheet.Cell(worksheet.RangeUsed().RowsUsed().Count() + 1, 1).Value = file.Name;
                    foreach (var line in file.Data)         //fills all data to file
                    {
                        int usedRows = worksheet.RangeUsed().RowsUsed().Count();
                        worksheet.Cell(usedRows + 1, 1).Value = line.Account;
                        worksheet.Cell(usedRows + 1, 2).Value = line.OpenBalance_A;
                        worksheet.Cell(usedRows + 1, 3).Value = line.OpenBalance_P;
                        worksheet.Cell(usedRows + 1, 4).Value = line.Turnover_Db;
                        worksheet.Cell(usedRows + 1, 5).Value = line.TurnOver_Ct;
                        worksheet.Cell(usedRows + 1, 6).Value = line.ClosBalance_A;
                        worksheet.Cell(usedRows + 1, 7).Value = line.ClosBalance_P;
                    }
                }
                //return workbook;
                //нумерация строк/столбцов начинается с индекса 1 (не 0)
                using (var stream = new MemoryStream())
                {
                    
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"Accounting{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}