using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class JefesSectores : Personas
    {
        public decimal PresupuestoSector { get; set; }
        public String? HorarioTrabajo { get; set; }
        public decimal Sueldo { get; set; }
        public int Sector { get; set; }
        public int AdministradorSector { get; set; }
        public int TipoContrato { get; set; }


        [ForeignKey("Sector")] public Sectores? _Sector { get; set; }
        [ForeignKey("AdministradoSector")] public AdministradoresDepartamentos? _AdministradorSector { get; set; }
        public List<EmpleadosSectores>? EmpleadosSectores { get; set; }
    }
}
