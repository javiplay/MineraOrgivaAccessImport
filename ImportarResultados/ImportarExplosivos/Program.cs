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

            conn.Close();


            Console.WriteLine("Pulse una tecla...");
            Console.ReadKey();

        }

        private static void InsertarProductos(Shipment shipment, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnit su in shipment.Units) // insertamos los productos de primer nivel (cajas de goma2, cajas con detonadores y sacos)
            {
                string sql = ComandoSQLparaUnit(su);
                try
                {
                    (new OleDbCommand(sql, conn)).ExecuteNonQuery();
                    inserted++;
                }
                catch (Exception e)
                {
                    lastException = e;
                    //Console.WriteLine("Última excepción: " + lastException.Message);
                    omitted++;
                }

                if (su.Items != null && su.Items.Any()) // en el caso de los detonadores metemos cada detonador
                {
                    InsertarUnitItems(su, conn);
                }

                if (su.Units != null && su.Units.Any()) // en el caso de las cajas de explosivo hay que meter las bolsas
                {
                    InsertarUnitUnits(su, conn);
                }


            }

            Console.WriteLine("Nuevas unidades de producto: " + inserted);
            Console.WriteLine("Productos no insertados: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);

        }

        private static void InsertarUnitUnits(ShipmentUnit su, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitUnit suu in su.Units)
            {
                string sql = ComandoSQLparaUnitUnit(suu, su);
                try
                {
                    (new OleDbCommand(sql, conn)).ExecuteNonQuery();
                    inserted++;
                }
                catch (Exception e)
                {
                    lastException = e;
                    //Console.WriteLine("Última excepción: " + lastException.Message);
                    omitted++;
                }

                if (suu.Items != null && suu.Items.Any()) // en el caso de las bolsas metemos los cartuchos
                {
                    InsertarUnitUnitItems(suu, conn);
                }

            }
            Console.WriteLine("Nuevas unidades para la unidad de producto: " + inserted);
            Console.WriteLine("Unidades no insertadas: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);
        }

        private static void InsertarUnitUnitItems(ShipmentUnitUnit suu, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitUnitItem suui in suu.Items)
            {
                string sql = ComandoSQLparaUnitUnitItem(suui, suu);
                try
                {
                    (new OleDbCommand(sql, conn)).ExecuteNonQuery();
                    inserted++;
                }
                catch (Exception e)
                {
                    lastException = e;
                    //Console.WriteLine("Última excepción: " + lastException.Message);
                    omitted++;
                }
            }
            Console.WriteLine("Nuevos items de unidades de unidades de producto: " + inserted);
            Console.WriteLine("Items no insertados: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);
        }

        private static void InsertarUnitItems(ShipmentUnit su, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitItem sui in su.Items)
            {
                string sql = ComandoSQLparaUnitItem(sui, su);
                try
                {
                    (new OleDbCommand(sql, conn)).ExecuteNonQuery();
                    inserted++;
                }
                catch (Exception e)
                {
                    lastException = e;
                    //Console.WriteLine("Última excepción: " + lastException.Message);
                    omitted++;
                }
            }
            Console.WriteLine("Nuevos items de producto: " + inserted);
            Console.WriteLine("Items no insertados: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);
        }


        private static void InsertarDescripciones(Shipment shipment, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentSummaryItem si in shipment.SummaryItems)
            {
                string sql = ComandoSQLParaSummaryItem(si);

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                int rows = 0;
                try
                {
                    rows = cmd.ExecuteNonQuery();
                    inserted++;
                }
                catch (Exception e)
                {
                    omitted++;
                    lastException = e;
                }

            }
            Console.WriteLine("Nuevas descripciones: " + inserted);
            Console.WriteLine("Descripciones no insertadas: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);
        }

        private static string ComandoSQLParaSummaryItem(ShipmentSummaryItem si)
        {
            return @"insert into ProductDescriptions " +
                                "(SID, PSN, ProductionDate, PackagingLevel, ProductName, ProductCode) " +
                                "values " +
                                string.Format(@"(""{0}"", ""{1}"", ""{2}"", {3}, ""{4}"", ""{5}"")",
                                si.SID,
                                si.PSN,
                                si.ProductionDate,
                                si.PackagingLevel,
                                si.ProducerProductName,
                                si.ProducerProductCode);
        }

        private static string ComandoSQLparaUnit(ShipmentUnit su)
        {
            return @"insert into ProductUnit " +
                "(UID, PSN, SID, ItemQuantity, CountOfTradeUnits, PackagingLevel) " +
                "values " +
                string.Format(@"(""{0}"", ""{1}"", ""{2}"", {3}, {4}, {5})",
                su.UID,
                su.PSN,
                su.SID,
                su.ItemQuantity,
                su.CountOfTradeUnits,
                su.PackagingLevel);
        }

        private static string ComandoSQLparaUnitItem(ShipmentUnitItem sui, ShipmentUnit su)
        {
            return
                @"insert into ProductUnit (UID, PSN, SID, ParentUID) values " +
                string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"")",
                                sui.UID, sui.PSN, sui.SID, su.UID);
        }

        private static string ComandoSQLparaUnitUnit(ShipmentUnitUnit suu, ShipmentUnit su)
        {
            return
               @"insert into ProductUnit (UID, PSN, SID, ParentUID) values " +
               string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"")",
                               suu.UID, suu.PSN, suu.SID, su.UID);
        }

        private static string ComandoSQLparaUnitUnitItem(ShipmentUnitUnitItem suui, ShipmentUnitUnit suu)
        {
            return
               @"insert into ProductUnit (UID, PSN, SID, ParentUID) values " +
               string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"")",
                               suui.UID, suui.PSN, suui.SID, suu.UID);
        }
    }
}
