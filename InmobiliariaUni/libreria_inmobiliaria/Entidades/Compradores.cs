using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Compradores : Personas  
    {
        public decimal PresupuestoMaximo { get; set; }

        [NotMapped] public Codeudores? _Codeudor { get; set; }   
        [NotMapped] public List<Contratos>? Contratos { get; set; }   
    }
}
