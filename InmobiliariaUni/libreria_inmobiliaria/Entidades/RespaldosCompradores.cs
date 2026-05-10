using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class RespaldosCompradores : RespaldosFinancieros
    {
        public int Comprador { get; set; }

        [ForeignKey("Comprador")]  public Compradores? _Comprador { get; set; }
    }
}
