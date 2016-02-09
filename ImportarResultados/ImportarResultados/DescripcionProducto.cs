using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImportarResultados
{
    public class DescripcionProducto
    {
        public static XElement root;
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaProduccion { get; set; }
        static DescripcionProducto ObtenerPorId(string SID)
        {
            if (root == null)
            {
                throw new InvalidOperationException("XElement no asignado.");
            }
            IEnumerable<XElement> sumaryItemsElements =
                from el in root.Elements("SummaryItems")
                select el;
            IEnumerable<XElement> sumaryItems =
                from el in sumaryItemsElements.First().Elements("SummaryItem")
                where (string)el.Attribute("SID") == SID
                select el;

            DescripcionProducto descripcion = new DescripcionProducto { Codigo = sumaryItems.First().Attribute() }


                return null;
        }
    }

}
