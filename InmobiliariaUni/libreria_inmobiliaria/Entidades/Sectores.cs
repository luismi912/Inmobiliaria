using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Sectores
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public int Ciudad { get; set; }

        [ForeignKey("Ciudad")] public Ciudades? _Ciudad { get; set; }
        public List<JefesSectores>? JefesSectores { get; set; }
        public List<EmpleadosSectores>? EmpleadosSectores { get; set; }
    }
}
