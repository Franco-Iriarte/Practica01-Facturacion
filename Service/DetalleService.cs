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
   public class DetalleService
   {
      private IDetalleRepository _repository;

      public DetalleService()
      {
        _repository = new DetalleRepository();
      }

      public List<DetalleFactura> GetAll()
      {
         return _repository.GetAll();
      }

      public bool SaveFactura(DetalleFactura detalleFactura)
      {
        return _repository.Save(detalleFactura);
      }

      public bool DeleteFactura(int id)
      {
        return _repository.Delete(id);
      }
   }
}
