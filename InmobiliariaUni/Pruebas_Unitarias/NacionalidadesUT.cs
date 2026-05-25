using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class NacionalidadesUT
    {
        private IConexion? conexion { get; set; }
        private Nacionalidades? nacionalidad { get; set; }

        public NacionalidadesUT()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
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
            this.nacionalidad = new Nacionalidades()
            {
                Nombre = "Colombiana",
                Estado = true
            };

            this.conexion!.Nacionalidades!.Add(nacionalidad);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Nacionalidades.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.nacionalidad!.Estado = false;
            this.conexion!.Entry(this.nacionalidad).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            this.conexion.SaveChanges();
        }
    }
}
