using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Codeudores : Personas    
    {
        public int Comprador { get; set; }

        [ForeignKey("Comprador")] public Compradores? _Comprador { get; set; }   
        [NotMapped] public List<Contratos>? Contratos { get; set; }   
        
    }
}
