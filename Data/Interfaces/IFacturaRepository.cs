using Act._Practica_01_Iriarte_Franco.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data.Interfaces
{
     interface IFacturaRepository
    {
        List<Factura> GetAll();
        bool Save (Factura factura);
        bool Delete(int id);
    }
}
