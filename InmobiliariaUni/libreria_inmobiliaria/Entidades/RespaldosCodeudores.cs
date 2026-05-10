using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class RespaldosCodeudores : RespaldosFinancieros
    {
        public int Codeudor { get; set; }

        [ForeignKey("Codeudor")] public Codeudores? _Codeudor { get; set; }
    }
}
