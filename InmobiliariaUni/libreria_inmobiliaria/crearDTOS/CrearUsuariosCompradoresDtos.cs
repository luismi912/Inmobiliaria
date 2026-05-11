using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class CrearUsuariosCompradoresDtos
    {
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }
        public CompradoresDtos Comprador { get; set; } = new ();
    }

    public class CompradoresDtos
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public decimal PresupuestoMaximo { get; set; }
        public int UsuarioRol { get; set; }
        public int Nacionalidad { get; set; }
        public int Genero { get; set; }

        public DireccionesDtos Direccion { get; set; } = new();
        public TelefonosDtos Telefono { get; set; } = new();
        public ExpedientesLaborales Expediente { get; set; } = new();
        public RespaldosCompradoresDtos RespaldoComprador { get; set; } = new();
    }

    public class RespaldosCompradoresDtos
    {
        public int Comprador { get; set; }

        public BienesDtos Bien { get; set; } = new();
        public ActivosFinancierosDtos ActivoFinanciero { get; set; } = new();
    }
}
