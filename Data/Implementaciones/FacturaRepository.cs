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
    public class FacturaRepository : IFacturaRepository
    {
        public bool Delete(int id)
        {
            List<ParametroSP> parametros = new List<ParametroSP>();
            {
                new ParametroSP()
                {
                    Name = "@nroFactura",
                    Value = id
                };
            }

            return DataHelper.GetInstance().ExecuteSpDml("spDeleteFactura", parametros);
        }

        public List<Factura> GetAll()
        {
            List<Factura> lst = new List<Factura>();

            //conectar BD 

            //Traer Registros
            var dt = DataHelper.GetInstance().ExecuteSPQuery("spGetAllFacturas");

            //Mapear
            foreach (DataRow row in dt.Rows)
            {
                Factura f = new Factura();

                f.nroFactura = (int)row["nroFactura"];
                f.fecha = (DateTime)row["fecha"];
                f.cliente = (string)row["cliente"];
                f.idFormaPago = (int)row["idFormaPago"];

                lst.Add(f);
            }

            return lst;
        }


        public bool Save(Factura factura)
        {
            List<ParametroSP> parametros = new List<ParametroSP>();
            {
                new ParametroSP("nroFactura", factura.nroFactura);
                new ParametroSP("fecha", factura.fecha);
                new ParametroSP("cliente", factura.cliente);
                new ParametroSP("idFormaPago", factura.idFormaPago);
            }
            return DataHelper.GetInstance().ExecuteSpDml("spSaveFactura", parametros);
        }
    }
}
