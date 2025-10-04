using Act._Practica_01_Iriarte_Franco.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data.Interfaces
{
    public interface IDetalleRepository
    {
        List<DetalleFactura> GetAll();
        bool Save(DetalleFactura detallefactura);
        bool DDelete(int id);

    }

}
