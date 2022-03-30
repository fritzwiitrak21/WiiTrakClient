using System.IO;
using Syncfusion.XlsIO;
using System.Data;

namespace WiiTrakClient.Services
{
    public class ExcelService
    {
        public MemoryStream CreateExcel(DataTable dt)
        {

            //Create an instance of ExcelEngine.
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook.
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Initialize DataTable and data get from SampleDataTable method.
                DataTable table = dt;

                //Export data from DataTable to Excel worksheet.
                worksheet.ImportDataTable(table, true, 1, 1);

                worksheet.UsedRange.AutofitColumns();

                //Save the document as a stream and return the stream.
                MemoryStream stream = new MemoryStream();
                //Save the created Excel document to MemoryStream.
                workbook.SaveAs(stream);
                return stream;
                
            }
            
        }
    }
}
