using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act._Practica_01_Iriarte_Franco.Data
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            //_connection = new SqlConnection(Properties.Resources.CadenaConexionLocal);
            _connection = new SqlConnection(s);
        }

        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public DataTable ExecuteSPQuery(string sp, List<ParametroSP>? parametros = null)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();

                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;

                if (parametros != null)
                {
                    foreach (ParametroSP p in parametros)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        // Método para ejecutar SPs con operaciones DML
        public bool ExecuteSpDml(string sp, List<ParametroSP>? parametros = null)
        {
            bool result;
            try
            {
                // Abrimos la conexión
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregamos parámetros si los hay
                if (parametros != null)
                {
                    foreach (ParametroSP p in parametros)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                int affectedRows = cmd.ExecuteNonQuery();

                result = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                // En caso de error, retornamos false
                result = false;
            }
            finally
            {
                // Cerramos la conexión
                _connection.Close();
            }

            return result;
        }



    }
}
