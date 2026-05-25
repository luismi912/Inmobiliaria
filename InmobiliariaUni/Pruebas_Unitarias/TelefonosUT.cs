using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class TelefonosUT
    {
        private IConexion? conexion { get; set; }
        private Departamentos? departamento { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Ciudades? ciudad { get; set; }
        private Telefonos? telefono { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private Personas? persona { get; set; }

        public TelefonosUT()
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
            this.nacionalidad = apoyo!.GuardarNacionalidad();
            this.persona = apoyo.GuardarPersona(nacionalidad);

            this.telefono = new Telefonos()
            {
                Numero = "30130533241",
                Prefijo = "+57",
                Persona = persona.Id,
            };

            this.conexion!.Telefonos!.Add(this.telefono);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Telefonos.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.telefono!.Numero = "34342123123";
            this.conexion!.Entry(this.telefono).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Telefonos.Remove(this.telefono!);
            this.conexion!.Personas.Remove(this.persona!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);

            this.conexion.SaveChanges();
        }
    }
}
