using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data
{
    class HelperBD
    {
        private static HelperBD instancia;
        private SqlConnection cnn;

        private HelperBD()
        {
            cnn = new SqlConnection("Data Source=DESKTOP-31UPIUI\\SQLEXPRESS;Initial Catalog=APIFacturacion;Integrated Security=True;Trust Server Certificate=True");
        }

        public static HelperBD ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new HelperBD();
            return instancia;
        }
        public DataTable ConsultaSQL(string spNombre, List<ParametroSP> values)
        {
            DataTable tabla = new DataTable();

            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (values != null)
            {
                foreach (ParametroSP oParametro in values)
                {
                    cmd.Parameters.AddWithValue(oParametro.Name, oParametro.Value);
                }
            }
            tabla.Load(cmd.ExecuteReader());
            cnn.Close();

            return tabla;
        }

        public int ConsultaEscalarSQL(string spNombre, string pOutNombre)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(spNombre, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();
            pOut.ParameterName = pOutNombre;
            pOut.DbType = DbType.Int32;
            pOut.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pOut);
            cmd.ExecuteNonQuery();
            cnn.Close();

            return (int)pOut.Value;
        }


        public int EjecutarSQL(string strSql, List<ParametroSP> values)
        {
            int afectadas = 0;
            SqlTransaction t = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = strSql;
                cmd.Transaction = t;

                if (values != null)
                {
                    foreach (ParametroSP param in values)
                    {
                        cmd.Parameters.AddWithValue(param.Name, param.Value);
                    }
                }

                afectadas = cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null) { t.Rollback(); }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

            }

            return afectadas;
        }

        public SqlConnection ObtenerConexion()
        {
            return this.cnn;
        }

    }
}
