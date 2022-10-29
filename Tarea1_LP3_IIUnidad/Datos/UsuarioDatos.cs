using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class UsuarioDatos
    {
        public async Task<bool> LoginAsync(string contraseña, string correo)
        {
            bool valido = false;
            try
            {
                string sql = "SELECT 1 FROM usuario WHERE Contraseña=@Contraseña AND Correo=@Correo;";

                //conexion de mysql
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Contraseña", MySqlDbType.VarChar, 120).Value = contraseña;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 50).Value = correo;

                        valido = Convert.ToBoolean(await comando.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return valido;
        }

        public async Task<DataTable> DevolverListaAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM usuario";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public async Task<bool> InsertarAsync(Usuarios usuario)
        {
            bool inserto = false;
            try
            {
                string sql = "INSERT INTO usuario VALUES (@Contraseña, @Correo)";

                //conexion de mysql
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Contraseña", MySqlDbType.VarChar, 120).Value = usuario.Contraseña;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = usuario.Correo;
                        await comando.ExecuteNonQueryAsync();
                        inserto = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return inserto;
        }
        public async Task<bool> ActualizarAsync(Usuarios usuario)
        {
            bool actualizo = false;
            try
            {
                string sql = "UPDATE usuario SET Correo=@Correo WHERE Contraseña=@Contraseña";
                //conexion de mysql
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Contraseña", MySqlDbType.VarChar, 120).Value = usuario.Contraseña;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 50).Value = usuario.Correo;
                        await comando.ExecuteNonQueryAsync();
                        actualizo = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return actualizo;
        }
        public async Task<bool> EliminarAsync(string contraseña)
        {
            bool elimino = false;
            try
            {
                string sql = "DELETE FROM usuario WHERE Contraseña = @Contraseña";
                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Contraseña", MySqlDbType.VarChar, 20).Value = contraseña;
                        await comando.ExecuteNonQueryAsync();
                        elimino = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return elimino;
        }
    }
}
