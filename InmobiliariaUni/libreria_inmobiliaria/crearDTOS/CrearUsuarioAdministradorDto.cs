using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class CrearUsuarioAdministradorDto
    {
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }
        public AdministradorDto AdministradorDto { get; set; } = new ();
    }

    public class AdministradorDto
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal PresupuestoDepartamento { get; set; }
        public String? HorarioTrabajo { get; set; }
        public bool Estado { get; set; }
        public decimal Sueldo { get; set; }
        public int Departamento { get; set; }
        public int UsuarioRol { get; set; }
        public int Nacionalidad { get; set; }
        public int Genero { get; set; }

        public DireccionDto Direccion { get; set; } = new();
        public TelefonoDto Telefono { get; set; } = new();
    }

    public class DireccionDto
    {
        public string? TipoVia { get; set; }
        public string? NumeroVia { get; set; }
        public string? Complemento { get; set; }
        public int Ciudad { get; set; }
    }

    public class TelefonoDto
    {
        public string? Numero { get; set; }
        public string? Prefijo { get; set; }
    }
}
