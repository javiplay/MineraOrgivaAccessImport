using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImportarResultados
{
    class Program
    {
        private static Excel.Workbook MyBook = null;
        private static Excel.Application MyApp = null;
        private static Excel.Worksheet MySheet = null;

        static void Main(string[] args)
        {

            if (args.Length < 2)
            {
                Console.WriteLine("El programa necesita la ruta del archivo xls y del accdb.");
                return;
            }

            MyApp = new Excel.Application();
            MyApp.Visible = false;
            try
            {
                MyBook = MyApp.Workbooks.Open(args[0]);
            }
            catch (Exception e)
            {                                
                Console.WriteLine(e.Message);
                return;
            }

            MySheet = MyBook.Sheets[1];
            int lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

            Excel.Range sourceRange = MySheet.get_Range("A1", "T1");
            Excel.Range destinationRange = MySheet.get_Range("A2", "T2");
            sourceRange.Copy(Type.Missing);
            destinationRange.PasteSpecial(Excel.XlPasteType.xlPasteFormulas, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

            Excel.Range range = MySheet.get_Range("A1:A1", Type.Missing);
            range.EntireRow.Delete(Excel.XlDirection.xlUp);

            string fileNameSaved = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "Mod.xlsx";
            MyBook.SaveAs(fileNameSaved);


            MyBook.Close(0);
            MyApp.Quit();

            // parte de access
            string ConnString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + args[1] + ";Jet OLEDB:Global Partial Bulk Ops=1";
            OleDbConnection conn = new OleDbConnection(ConnString);
            conn.Open();


            string sql = @"insert into Analisis select * from [Excel 8.0;HDR=YES;DATABASE=" + fileNameSaved + @"].[Análisis Exportados$] s;";

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rows = 0;
            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Filas afectadas: " + rows);
                Console.WriteLine(e.Message);               
            }


            conn.Close();

            File.Delete(fileNameSaved);
            Console.WriteLine("Pulse una tecla...");
            Console.ReadKey();

        }
    }
}
