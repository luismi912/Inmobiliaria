using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class RespaldosFinancieros 
    {
        public int Id { get; set; }
        public decimal DeudasTotales { get; set; }
        public decimal IngresosMensuales { get; set; }
        public string? Observaciones { get; set; }

        [NotMapped] public List<Bienes>? Bienes { get; set; }
        [NotMapped] public List<ActivosFinancieros>? ActivosFinancieros { get; set; }
    }
}
