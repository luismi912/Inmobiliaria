namespace libreria_inmobiliaria.Entidades
{
    public class ContratosArriendos : Contratos
    {
        public decimal ValorMensual { get; set; }
        public int DiaPago { get; set; }
        public bool Renovable { get; set; }
    }
}
