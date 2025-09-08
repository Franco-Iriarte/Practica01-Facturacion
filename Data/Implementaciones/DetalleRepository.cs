using Act._Practica_01_Iriarte_Franco.Data.Interfaces;
using Act._Practica_01_Iriarte_Franco.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data.Implementaciones
{
    public class DetalleRepository : IDetalleRepository
    {
        public bool Delete(int id)
        {
            List<ParametroSP> parametros = new List<ParametroSP>();
            new ParametroSP()
            {
                Name = "@nroFactura",
                Value = id,
            };
            return DataHelper.GetInstance().ExecuteSpDml("spDeleteDetalleFacturaByFactura", parametros);
        }
        
    

        public List<DetalleFactura> GetAll()
        {
            List<DetalleFactura> lst = new List<DetalleFactura>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("spGetAllDetalles");

            foreach(DataRow row in dt.Rows)
            {
                DetalleFactura df = new DetalleFactura();
                df.idDetalle = (int)row["idDetalle"];
                df.nroFactura = (int)row["nroFactura"];
                df.idArticulo = (int)row["idArticulo"];
                df.cantidad = (int)row["cantidad"];
            }
            return lst;
        }

        public bool Save(DetalleFactura detalleFactura)
        {
            List<ParametroSP> parametros = new List<ParametroSP>
            {
                new ParametroSP("@idDetalle", detalleFactura.idDetalle),
                new ParametroSP("@nroFactura", detalleFactura.nroFactura),
                new ParametroSP("@idArticulo", detalleFactura.idArticulo),
                new ParametroSP("@cantidad", detalleFactura.cantidad),
            };
            return DataHelper.GetInstance().ExecuteSpDml("spSaveDetalleFactura", parametros);
        }
    }
}
