using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class ClientesDtos
    {
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public string? CedulaRelacionEmpleado { get; set; }
        public decimal PorcentajeComision { get; set; }
        public int Calificacion { get; set; }
        public int Nacionalidad { get; set; }
        public int Empleado { get; set; }

        public DireccionesDtos Direccion { get; set; } = new();
        public TelefonosDtos Telefono { get; set; } = new();
        public ExpedientesLaborales Expediente { get; set; } = new();
    }
}
