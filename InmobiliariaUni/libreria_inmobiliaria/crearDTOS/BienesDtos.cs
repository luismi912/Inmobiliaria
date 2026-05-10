namespace libreria_inmobiliaria.crearDTOS
{   
    public class BienesDtos
    {
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public decimal ValorAdquisicion { get; set; }
        public decimal ValorActual { get; set; }
        public int RespaldoFinanciero { get; set; }
    }
}
