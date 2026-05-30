using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Sectores
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public bool Estado { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public int Ciudad { get; set; }

        [ForeignKey("Ciudad")] public Ciudades? _Ciudad { get; set; }
        public JefesSectores? JefeSector { get; set; }
        [NotMapped] public List<EmpleadosSectores>? EmpleadosSectores { get; set; }
    }
}
