﻿

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SigesaData.Configuracion;
using SigesaData.Contrato;
using SigesaEntidades;
using System.Data;

namespace SigesaData.Implementacion.DB
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public readonly ConnectionStrings con;

        public UsuarioRepositorio(IOptions<ConnectionStrings> opctions)
        {
            con = opctions.Value;
        }
        public async Task<string> Editar(Usuario Objeto)
        {
            string respuesta = null;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarUsuario", conexion);
               
                cmd.Parameters.AddWithValue("@IdUsuario", Objeto.IdUsuario);
                cmd.Parameters.AddWithValue("@NumeroDocumentoIdentidad", Objeto.NumeroDocumentoIdentidad);
                cmd.Parameters.AddWithValue("@Nombre", Objeto.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", Objeto.Apellido);
                cmd.Parameters.AddWithValue("@Correo", Objeto.Correo);
                cmd.Parameters.AddWithValue("@Clave", Objeto.Clave);
                cmd.Parameters.AddWithValue("@IdRolUsuario", Objeto.RolUsuario.IdRolUsuario);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction=ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al editar usuario";
                }
            }

            return respuesta;
        }

        public async  Task<int> Eliminar(int Id)
        {
            int respuesta = 1;
            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_eliminarUsuario", conexion);
                cmd.Parameters.AddWithValue("@IdUsuario", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    respuesta = 0;
                }

            }
            return respuesta;

        }

        public async Task<string> Guardar(Usuario objeto)
        {
            string respuesta = "";
            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarUsuario", conexion);
                cmd.Parameters.AddWithValue("@NumeroDocumentoIdentidad", objeto.NumeroDocumentoIdentidad);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", objeto.Apellido);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Clave", objeto.Clave);
                cmd.Parameters.AddWithValue("@IdRolUsuario", objeto.RolUsuario.IdRolUsuario);
                cmd.Parameters.Add("@MsgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@MsgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al guardar usuario";
                }

            }
            return respuesta;
        }

        public async Task<List<Usuario>> Lista(int IdRolUsuario = 0)
        {
            List<Usuario> lista = new List<Usuario>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaUsuario", conexion);
                cmd.Parameters.AddWithValue("@IdRolUsuario", IdRolUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NumeroDocumentoIdentidad = dr["NumeroDocumentoIdentidad"].ToString()!,
                            Nombre = dr["Nombre"].ToString()!,
                            Apellido = dr["Apellido"].ToString()!,
                            Correo = dr["Correo"].ToString()!,
                            Clave = dr["Clave"].ToString()!,
                            RolUsuario = new RolUsuario()
                            {
                                IdRolUsuario = Convert.ToInt32(dr["IdRolUsuario"]),
                                Nombre = dr["NombreRol"].ToString()!,
                            },
                            FechaCreacion = dr["FechaCreacion"].ToString()!
                        });
                    }
                }
            }
            return lista;

        }

        public async Task<Usuario> Login(string DocumentoIdentidad, string Clave)
        {
            Usuario objeto = null!;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_loginUsuario", conexion);
                cmd.Parameters.AddWithValue("@DocumentoIdentidad", DocumentoIdentidad);
                cmd.Parameters.AddWithValue("@Clave", Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        objeto = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NumeroDocumentoIdentidad = dr["NumeroDocumentoIdentidad"].ToString()!,
                            Nombre = dr["Nombre"].ToString()!,
                            Apellido = dr["Apellido"].ToString()!,
                            Correo = dr["Correo"].ToString()!,
                            RolUsuario = new RolUsuario
                            {
                                Nombre = dr["NombreRol"].ToString()!,
                            }
                        };
                    }
                }
            }
            return objeto;

        }
    }
}
