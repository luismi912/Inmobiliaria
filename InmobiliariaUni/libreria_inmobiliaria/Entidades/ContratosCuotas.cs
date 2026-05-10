namespace libreria_inmobiliaria.Entidades
{
    public class ContratosCuotas : Contratos
    {
        public int NumeroCuotas { get; set; }
        public decimal ValorCuotas { get; set; }
        public decimal PagoInicial { get; set; }
    }
}
