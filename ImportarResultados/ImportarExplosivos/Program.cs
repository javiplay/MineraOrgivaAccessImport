using System;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ImportarExplosivos
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            // cargar el xml

            Console.WriteLine("Pulse una tecla para seleccionar el XML de envío...");
            Console.ReadKey();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Seleccione el XML del envío...";
            openFileDialog1.InitialDirectory = @"c:\Users\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;



            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Shipment shipment = null;
                    string path = openFileDialog1.FileName;
                    // @"C:\Users\Javier\Dropbox\MineraOrgiva\20160126_032942_20160126081951.xml";


                    XmlSerializer serializer = new XmlSerializer(typeof(Shipment));

                    StreamReader reader = new StreamReader(path);

                    shipment = (Shipment)serializer.Deserialize(reader);


                    reader.Close();


                    Console.WriteLine("Pulse una tecla para seleccionar la base de datos access...");
                    Console.ReadKey();

                    OpenFileDialog openFileDialog2 = new OpenFileDialog();

                    openFileDialog2.Title = "Seleccione el XML del envío...";
                    openFileDialog2.InitialDirectory = @"c:\Users\";
                    openFileDialog2.Filter = "accdb files (*.accdb)|*.accdb|All files (*.*)|*.*";
                    openFileDialog2.FilterIndex = 1;
                    openFileDialog2.RestoreDirectory = true;


                    if (openFileDialog2.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {


                            string ConnString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog2.FileName;
                            OleDbConnection conn = new OleDbConnection(ConnString);
                            conn.Open();

                            InsertarEnvio(shipment, conn);
                            InsertarProductos(shipment, conn);

                            conn.Close();

                            Console.WriteLine("Pulse una tecla para finalizar...");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }





        }

        private static void InsertarEnvio(Shipment shipment, OleDbConnection conn)
        {
            string sql = ComandoSQLparEnvio(shipment);
            try
            {
                (new OleDbCommand(sql, conn)).ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("Última excepción: " + e.Message);
            }
        }

        private static string ComandoSQLparEnvio(Shipment s)
        {
            return
              @"insert into Shipment (ShipmentNumber, PurchaseOrderNumber, DeliveryNoteNumber, ExpectedDeliveryDate) values " +
              string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"")",
                              s.ShipmentNumber, s.PurchaseOrderNumber, s.DeliveryNoteNumber, s.ExpectedDeliveryDate);
        }

        private static void InsertarProductos(Shipment shipment, OleDbConnection conn)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnit su in shipment.Units) // insertamos los productos de primer nivel (cajas de goma2, cajas con detonadores y sacos)
            {
                string sql = ComandoSQLparaUnit(su, shipment);
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
                    InsertarUnitItems(su, conn, shipment);
                }

                if (su.Units != null && su.Units.Any()) // en el caso de las cajas de explosivo hay que meter las bolsas
                {
                    InsertarUnitUnits(su, conn, shipment);
                }


            }

            Console.WriteLine("Nuevas unidades de producto: " + inserted);
            Console.WriteLine("Productos no insertados: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);

        }

        private static void InsertarUnitUnits(ShipmentUnit su, OleDbConnection conn, Shipment shipment)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitUnit suu in su.Units)
            {
                string sql = ComandoSQLparaUnitUnit(suu, su, shipment);
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
                    InsertarUnitUnitItems(suu, conn, shipment);
                }

            }
            Console.WriteLine("Nuevas unidades para la unidad de producto: " + inserted);
            Console.WriteLine("Unidades no insertadas: " + omitted);
            if (omitted > 0) Console.WriteLine("Última excepción: " + lastException.Message);
        }

        private static void InsertarUnitUnitItems(ShipmentUnitUnit suu, OleDbConnection conn, Shipment shipment)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitUnitItem suui in suu.Items)
            {
                string sql = ComandoSQLparaUnitUnitItem(suui, suu, shipment);
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

        private static void InsertarUnitItems(ShipmentUnit su, OleDbConnection conn, Shipment shipment)
        {
            int inserted = 0;
            int omitted = 0;
            Exception lastException = null;

            foreach (ShipmentUnitItem sui in su.Items)
            {
                string sql = ComandoSQLparaUnitItem(sui, su, shipment);
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





        private static string ComandoSQLparaUnit(ShipmentUnit su, Shipment s)
        {
            ShipmentSummaryItem si = s.SummaryItems.Where(x => x.SID == su.SID).FirstOrDefault();
            return @"insert into ProductUnit " +
                "(UID, PSN, ItemQuantity, CountOfTradeUnits, PackagingLevel, ShipmentNumber, ProductName, ProductCode, ProductionDate) values " +
                string.Format(@"(""{0}"", ""{1}"", {2}, {3}, {4}, ""{5}"", ""{6}"", ""{7}"", ""{8}"")",
                                su.UID,
                                su.PSN,
                                su.ItemQuantity,
                                su.CountOfTradeUnits,
                                su.PackagingLevel,
                                s.ShipmentNumber,
                                si.ProducerProductName,
                                si.ProducerProductCode,
                                si.ProductionDate
                                );
        }

        private static string ComandoSQLparaUnitItem(ShipmentUnitItem sui, ShipmentUnit su, Shipment s)
        {
            ShipmentSummaryItem si = s.SummaryItems.Where(x => x.SID == sui.SID).FirstOrDefault();
            return
                @"insert into ProductUnit (UID, PSN, ParentUID, ShipmentNumber, ProductName, ProductCode, ProductionDate) values " +
                string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"", ""{5}"", ""{6}"")",
                                sui.UID, sui.PSN, su.UID, s.ShipmentNumber, si.ProducerProductName, si.ProducerProductCode, si.ProductionDate);
        }

        private static string ComandoSQLparaUnitUnit(ShipmentUnitUnit suu, ShipmentUnit su, Shipment s)
        {
            ShipmentSummaryItem si = s.SummaryItems.Where(x => x.SID == suu.SID).FirstOrDefault();
            return @"insert into ProductUnit " +
                "(UID, ParentUID, PSN, ItemQuantity, CountOfTradeUnits, PackagingLevel, ShipmentNumber, ProductName, ProductCode, ProductionDate) values " +
                string.Format(@"(""{0}"", ""{1}"", ""{2}"", {3}, {4}, {5}, ""{6}"", ""{7}"", ""{8}"", ""{9}"")",
                                suu.UID,
                                su.UID,
                                suu.PSN,
                                suu.ItemQuantity,
                                suu.CountOfTradeUnits,
                                suu.PackagingLevel,
                                s.ShipmentNumber,
                                si.ProducerProductName,
                                si.ProducerProductCode,
                                si.ProductionDate
                                );
        }

        private static string ComandoSQLparaUnitUnitItem(ShipmentUnitUnitItem suui, ShipmentUnitUnit suu, Shipment s)
        {
            ShipmentSummaryItem si = s.SummaryItems.Where(x => x.SID == suui.SID).FirstOrDefault();
            return
                @"insert into ProductUnit (UID, PSN, ParentUID, ShipmentNumber, ProductName, ProductCode, ProductionDate) values " +
                string.Format(@"(""{0}"", ""{1}"", ""{2}"", ""{3}"", ""{4}"", ""{5}"", ""{6}"")",
                                suui.UID, suui.PSN, suu.UID, s.ShipmentNumber, si.ProducerProductName, si.ProducerProductCode, si.ProductionDate);
        }
    }
}
