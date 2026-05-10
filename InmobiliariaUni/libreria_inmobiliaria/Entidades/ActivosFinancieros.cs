using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.Entidades
{
    public class ActivosFinancieros
    {
        public int Id { get; set; }
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public decimal Valor { get; set; }
        public int RespaldoFinanciero { get; set; }
        
        [ForeignKey("RespaldoFinanciero")] public RespaldosFinancieros? _RespaldoFinanciero { get; set; }
    }
}
