using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ImportarExplosivos
{
    class Program
    {

      

        

        static void Main(string[] args)
        {
            // cargar el xml
            Shipment shipment = null;
            string path = @"C:\Users\Javier\Dropbox\MineraOrgiva\20160126_032942_20160126081951.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Shipment));

            StreamReader reader = new StreamReader(path);
            shipment = (Shipment)serializer.Deserialize(reader);
            reader.Close();




            Console.WriteLine("Pulse una tecla PARA TEST...");
            Console.ReadKey();


            string ConnString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Javier\Dropbox\MineraOrgiva\MaterialExplosivos.accdb;  ";
            OleDbConnection conn = new OleDbConnection(ConnString);
            conn.Open();


            InsertarDescripciones(shipment, conn);
            InsertarProductos(shipment, conn);


            // insertamos productos

            conn.Close();




            Console.WriteLine("Pulse una tecla...");
            Console.ReadKey();

        }

        private static void InsertarProductos(Shipment shipment, OleDbConnection conn)
        {
            // insertamos descripciones
            int insertados = 0;
            int omitidos = 0;
            Exception lastException = null;
            foreach (ShipmentUnit su in shipment.Units)
            {

                string sql =
                    @"insert into ProductUnit " +
                    "(UID, PSN, SID, ItemQuantity, CountOfTradeUnits, PackagingLevel) " +
                    "values " +
                    string.Format("(\"{0}\", \"{1}\", \"{2}\", {3}, \"{4}\", \"{5}\")",
                    su.UID,
                    su.PSN,
                    su.SID,
                    su.ItemQuantity,
                    su.CountOfTradeUnits,
                    su.PackagingLevel);


                OleDbCommand cmd = new OleDbCommand(sql, conn);
                int rows = 0;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                    insertados++;
                }
                catch (Exception e)
                {
                    
                    lastException = e;
                    Console.WriteLine("Última excepción: " + lastException.Message);
                    omitidos++;
                }

            }
            Console.WriteLine("Nuevas unidades de producto: " + insertados);
            Console.WriteLine("Productos no insertados: " + omitidos);
            Console.WriteLine("Última excepción: " + lastException.Message);

        }

        private static void InsertarDescripciones(Shipment shipment, OleDbConnection conn)
        {
            // insertamos descripciones
            int insertados = 0;
            int omitidos = 0;
            foreach (ShipmentSummaryItem si in shipment.SummaryItems)
            {

                string sql =
                    @"insert into ProductDescriptions " +
                    "(SID, PSN, ProductionDate, PackagingLevel, ProductName, ProductCode) " +
                    "values " +
                    string.Format("(\"{0}\", \"{1}\", \"{2}\", {3}, \"{4}\", \"{5}\")",
                    si.SID,
                    si.PSN,
                    si.ProductionDate,
                    si.PackagingLevel,
                    si.ProducerProductName,
                    si.ProducerProductCode);


                OleDbCommand cmd = new OleDbCommand(sql, conn);
                int rows = 0;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                    insertados++;
                }
                catch (Exception e)
                {
                    omitidos++;
                }

            }
            Console.WriteLine("Nuevas descripciones: " + insertados);
            Console.WriteLine("Descripciones no insertadas: " + omitidos);
        }
    }
}
