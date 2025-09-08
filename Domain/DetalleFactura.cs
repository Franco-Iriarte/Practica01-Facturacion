using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Domain
{
    public class DetalleFactura
    {
        public int idDetalle { get; set; }
        public int nroFactura { get; set; }
        public int idArticulo { get; set; }
        public int cantidad { get; set; }
    }
}
