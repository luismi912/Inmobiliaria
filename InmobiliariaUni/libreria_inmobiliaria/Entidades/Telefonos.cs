using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Telefonos
    {
        public int Id { get; set; }
        public String? Numero { get; set; }
        public String? Prefijo { get; set; }
        public int Persona { get; set; }

        [ForeignKey("Persona")] public Personas? _Persona { get; set; }
    }
}
