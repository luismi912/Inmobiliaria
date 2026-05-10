using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Direcciones
    {
        public int Id { get; set; }
        public String? TipoVia { get; set; }
        public String? NumeroVia { get; set; }
        public String? Complemento { get; set; }
        public int Persona { get; set; }
        public int Ciudad { get; set; }

        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
        [ForeignKey("Ciudad")] public Ciudades? _Ciudad { get; set; }
    }
}
