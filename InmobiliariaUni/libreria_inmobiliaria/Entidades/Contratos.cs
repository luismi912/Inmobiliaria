using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Contratos
    {
        public int Id { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public int Codeudor { get; set; }
        public int Cliente { get; set; }
        public int Comprador { get; set; }
        public int EmpleadoSector { get; set; }
        public int Propiedad { get; set; }

        [ForeignKey("Codeudor")] public Codeudores? _Codeudor { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Comprador")] public Compradores? _Comprador { get; set; }
        [ForeignKey("EmpleadoSector")] public EmpleadosSectores? _EmpleadoSector { get; set; }
        [ForeignKey("Propiedad")] public Propiedades? _Propiedad { get; set; }
    }
}
