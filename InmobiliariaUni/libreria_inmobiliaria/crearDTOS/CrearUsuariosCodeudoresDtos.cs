using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class CrearUsuariosCodeudoresDtos
    {
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public int Comprador { get; set; }
        public int Nacionalidad { get; set; }

        public DireccionesDtos Direccion { get; set; } = new();
        public TelefonosDtos Telefono { get; set; } = new();
        public ExpedientesLaborales Expediente { get; set; } = new();
        public RespaldosCodeudoresDtos RespaldoCodeudor { get; set; } = new();
    }

    public class RespaldosCodeudoresDtos
    {
        public decimal DeudasTotales { get; set; }
        public decimal IngresosMensuales { get; set; }
        public string? Observaciones { get; set; }

        public BienesDtos Bien { get; set; } = new();
        public ActivosFinancierosDtos ActivoFinanciero { get; set; } = new();
    }
}
