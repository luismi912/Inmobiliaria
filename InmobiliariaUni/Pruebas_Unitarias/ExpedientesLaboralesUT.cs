using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class ExpedientesLaboralesUT
    {
        private IConexion? conexion { get; set; }
        private Departamentos? departamento { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Ciudades? ciudad { get; set; }
        private ExpedientesLaborales? expediente { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private Personas? persona { get; set; }

        public ExpedientesLaboralesUT()
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

            this.expediente = new ExpedientesLaborales()
            {
                FechaIngreso = DateTime.Now,
                Cargo = "Auxiliar de bodega",
                Antiguedad = 0,
                EstadoLaboral = "En proceso de eleccion",
                Persona = persona.Id,
            };

            this.conexion!.ExpedientesLaborales!.Add(this.expediente);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.ExpedientesLaborales.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.expediente!.Antiguedad = 1;
            this.conexion!.Entry(this.expediente).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.ExpedientesLaborales.Remove(this.expediente!);
            this.conexion!.Personas.Remove(this.persona!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);

            this.conexion.SaveChanges();
        }
    }
}
