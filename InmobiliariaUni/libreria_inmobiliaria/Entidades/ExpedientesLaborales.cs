using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class ExpedientesLaborales
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public String? Cargo { get; set; }
        public int Antiguedad { get; set; }
        public String? EstadoLaboral { get; set; }
        public int Persona { get; set; }

        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
    }
}
