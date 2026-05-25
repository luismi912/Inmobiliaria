using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class DireccionesUT
    {
        private IConexion? conexion { get; set; }
        private Departamentos? departamento { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Ciudades? ciudad { get; set; }
        private Direcciones? direccion { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private Personas? persona { get; set; }

        public DireccionesUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
            this.apoyo = new EntidadesApoyoUT();
        }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Eliminar();
        }

        private void Guardar()
        {
            this.departamento = apoyo!.GuardarDepartamento();
            this.ciudad = apoyo.GuardarCiudad(departamento);
            this.nacionalidad = apoyo.GuardarNacionalidad();
            this.persona = apoyo.GuardarPersona(nacionalidad);

            this.direccion = new Direcciones()
            {
                TipoVia = "Carretera",
                Numero = "#81-86",
                Complemento = "Cerca al centro comercial premium plaza",
                Persona = persona.Id,
                Ciudad = ciudad.Id
            };

            this.conexion!.Direcciones!.Add(this.direccion);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Direcciones.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.direccion!.Numero = "#72-83";
            this.conexion!.Entry(this.direccion).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Direcciones.Remove(this.direccion!);
            this.conexion!.Personas.Remove(this.persona!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            this.conexion!.Ciudades.Remove(this.ciudad!);
            this.conexion!.Departamentos.Remove(this.departamento!);

            this.conexion.SaveChanges();
        }
    }
}
