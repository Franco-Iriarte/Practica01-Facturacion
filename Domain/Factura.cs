using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Domain
{
    public class Factura
    {
        public int nroFactura { get; set; }
        public DateTime fecha { get; set; }
        public string cliente { get; set; }
        public int idFormaPago { get; set; }


        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}

