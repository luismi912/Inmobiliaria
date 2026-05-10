using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class UsuarioRoles
    {
        public int Id { get; set; }
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }
        public Personas? _Persona { get; set; }
    }
}
