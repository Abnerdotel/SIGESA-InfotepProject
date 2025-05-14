﻿

using SigesaEntidades;

namespace SigesaData.Contrato
{
    public interface IUsuarioRepositorio
    {

        Task<List<Usuario>> Lista(int IdRolUsuario = 0);
        Task<Usuario> Login(string DocumentoIdentidad, string Clave);

        Task<string> Guardar(Usuario objeto);
        Task<string> Editar(Usuario Objeto);
        Task<int> Eliminar(int Id);
        
       
    }
}
