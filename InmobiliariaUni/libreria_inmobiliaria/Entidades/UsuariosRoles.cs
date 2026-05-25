using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class UsuariosRoles
    {
        public int Id { get; set; }
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }

        [NotMapped] public Personas? _Persona { get; set; }
        [NotMapped] public List<AdministradoresDepartamentos>? AdministradoresDepartamentos { get; set; }
        [NotMapped] public List<EmpleadosSectores>? EmpleadosSectores { get; set; }
        [NotMapped] public List<JefesSectores>? jefesSectores { get; set; }
    }
}
