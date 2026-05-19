using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Clientes : Personas
    {
        public decimal PorcentajeComision { get; set; }
        public int Calificacion { get; set; }
        public int EmpleadoSector { get; set; }


        [ForeignKey("EmpleadoSector")] public EmpleadosSectores? _EmpleadoSector { get; set; }
        [NotMapped] public List<Contratos>? Contratos { get; set; }    //EL CLIENTE PUEDE TENER MUCHOS CONTRATOS
        [NotMapped] public List<Propiedades>? Propiedades { get; set; }
    }
}
