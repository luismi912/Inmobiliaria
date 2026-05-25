using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Personas
    {
        public int Id { get; set; }
		public String? Cedula { get; set; }
		public String? Nombre { get; set; }
		public String? Apellido { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public DateTime FechaRegistro { get; set; }
		public bool Estado { get; set; }
		public int Nacionalidad { get; set; }

        [ForeignKey("Nacionalidad")] public Nacionalidades? _Nacionalidad { get; set; }
		[NotMapped] public List<Direcciones>? Direcciones { get; set; }
        [NotMapped] public List<Telefonos>? Telefonos { get; set; }
        [NotMapped] public List<ExpedientesLaborales>? ExpedientesLaborales { get; set; }
    }
}
