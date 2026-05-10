namespace libreria_inmobiliaria.crearDTOS
{   
    public class ActivosFinancierosDtos
    {
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public decimal Valor { get; set; }
        public int RespaldoFinanciero { get; set; }
    }
}
