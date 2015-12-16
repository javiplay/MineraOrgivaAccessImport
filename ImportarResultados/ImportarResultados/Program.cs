using System;
using System.Collections.Generic;
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

            MyApp = new Excel.Application();
            MyApp.Visible = false;
            try { 
            MyBook = MyApp.Workbooks.Open(args[0]);
            } catch (Exception e)
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                //foreach (var d in Directory.EnumerateFiles(Directory.GetCurrentDirectory())) Console.WriteLine(d);
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

            MyBook.SaveAs(Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "Mod.xlsx");
            MyBook.Close(0);
            MyApp.Quit();

        }
    }
}
