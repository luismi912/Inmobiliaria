using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Compradores : Personas  //RELACION 1 A 1 CON CODEUDORES, CODEUDOR DEPENDE DE COMPRADORES
    {
        public decimal PresupuestoMaximo { get; set; }

        [NotMapped] public Codeudores? _Codeudor { get; set; }   //CODEUDOR QUE DEPENDE DEL COMPRADOR
        [NotMapped] public List<Contratos>? Contratos { get; set; }   //PUEDE TENER MUCHOS CONTRATOS
    }
}
