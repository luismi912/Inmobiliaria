using libreria_inmobiliaria.Entidades;
using System.ComponentModel.DataAnnotations.Schema;

namespace libreria_inmobiliaria.crearDTOS
{
    public class CrearUsuariosAdministradoresDtos
    {
        public String? Correo { get; set; }
        public String? Contraseña { get; set; }
        public String? Rol { get; set; }
        public AdministradoresDtos AdministradorDto { get; set; } = new ();
    }

    public class AdministradoresDtos
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public decimal PresupuestoDepartamento { get; set; }
        public String? HorarioTrabajo { get; set; }
        public decimal Sueldo { get; set; }
        public int Departamento { get; set; }
        public int Nacionalidad { get; set; }
        public int Genero { get; set; }
        public int UsuarioRol { get; set; }

        public DireccionesDtos Direccion { get; set; } = new();
        public TelefonosDtos Telefono { get; set; } = new();
    }
}
