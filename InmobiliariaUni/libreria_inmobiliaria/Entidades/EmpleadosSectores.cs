using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class EmpleadosSectores : Personas
    {
        public decimal Sueldo { get; set; }
        public String? HorarioTrabajo { get; set; }
        public int Sector { get; set; }
        public int JefeSector { get; set; }

        [ForeignKey("Sector")] public Sectores? _Sector { get; set; }
        [ForeignKey("JefeSector")] public JefesSectores? _JefeSector { get; set; }
        public List<Contratos>? Contratos { get; set; }
    }
}
