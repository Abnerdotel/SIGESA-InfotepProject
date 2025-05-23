﻿namespace SigesaEntidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NumeroDocumentoIdentidad { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public RolUsuario RolUsuario { get; set; } = null!;
        public string FechaCreacion { get; set; } = null!;
    }
}
