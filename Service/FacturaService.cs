using Act._Practica_01_Iriarte_Franco.Data.Implementaciones;
using Act._Practica_01_Iriarte_Franco.Data.Interfaces;
using Act._Practica_01_Iriarte_Franco.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Service
{
    public class FacturaService
    {
        private IFacturaRepository _repository;

        public FacturaService()
        {
            _repository = new FacturaRepository();
        }

        public List<Factura> GetAll()
        {
            return _repository.GetAll();
        }

        public bool SaveFactura(Factura factura)
        {
            return _repository.Save(factura);
        }

        public bool DeleteFactura(int id)
        {
            return _repository.Delete(id);
        }
    }
}
