using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class Propiedades
    {
        public int Id { get; set; }
        public int NumeroHabitaciones { get; set; }
        public int NumeroBaños { get; set; }
        public bool Patio { get; set; }
        public bool Garaje { get; set; }
        public int Pisos { get; set; }
        public DateTime FechaConstruccion { get; set; }
        public decimal ValorPropiedad { get; set; }
        public decimal ValorArriendo { get; set; }
        public bool Disponible { get; set; }
        public int Cliente { get; set; }
        public int TipoPropiedad { get; set; }
        public int Sector { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("TipoPropiedad")] public TiposPropiedades? _TipoPropiedades { get; set; }
        [ForeignKey("Sector")] public Sectores? _Sector { get; set; }
        public List<Contratos>? Contratos { get; set; }
    }
}
