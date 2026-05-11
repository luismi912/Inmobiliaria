using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{   
    public class ExperientesLaboralesDtos
    {
        public DateTime FechaIngreso { get; set; }
        public String? Cargo { get; set; }
        public int Antiguedad { get; set; }
        public String? EstadoLaboral { get; set; }
        public int Persona { get; set; }
    }
}
