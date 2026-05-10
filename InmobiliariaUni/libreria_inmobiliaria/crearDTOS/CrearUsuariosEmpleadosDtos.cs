using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class CrearUsuariosEmpleadosDtos
    {
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }
        public EmpleadosDtos Empleado { get; set; } = new ();
    }

    public class EmpleadosDtos
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public decimal Sueldo { get; set; }
        public String? HorarioTrabajo { get; set; }
        public int UsuarioRol { get; set; }
        public int Sector { get; set; }
        public int JefeSector { get; set; }
        public int Nacionalidad { get; set; }
        public int Genero { get; set; }

        public DireccionesDtos Direccion { get; set; } = new();
        public TelefonosDtos Telefono { get; set; } = new();
    }
}
