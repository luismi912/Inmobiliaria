using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Personas
    {
        public int Id { get; set; }
		public String? Cedula { get; set; }
		public String? PrimerNombre { get; set; }
		public String? SegundoNombre { get; set; }
        public String? PrimerApellido { get; set; }
        public String? SegundoApellido { get; set; }
		public String? Correo { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public DateTime FechaRegistro { get; set; }
		public bool Estado { get; set; }
		public int Nacionalidad { get; set; }
		public int UsuarioRol { get; set; }

		[ForeignKey("UsuarioRol")] public UsuarioRoles? _UsuarioRol { get; set; }
        [ForeignKey("Nacionalidad")] public Nacionalidades? _Nacionalidad { get; set; }
		public List<Direcciones>? Direcciones { get; set; }
		public List<Telefonos>? Telefonos { get; set; }
        public List<ExpedientesLaborales>? ExpedientesLaborales { get; set; }
    }
}
