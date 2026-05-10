using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Codeudores : Personas    //RELACION 1 A 1 CON COMPRADORES CODEUDOR DEPENDE DE COMPRADOR
    {
        public decimal IngresosMensuales { get; set; }
        public int Comprador { get; set; }

        [ForeignKey("Comprador")] public Compradores? _Comprador { get; set; }   //UN CODEUDOR SOLO PUEDE TENER 1 COMPRADOR
        public List<Contratos>? Contratos { get; set; }   //PUEDE ESTAR EN MUCHO CONTRATOS POR PARTE DEL COMPRADOR
    }
}
