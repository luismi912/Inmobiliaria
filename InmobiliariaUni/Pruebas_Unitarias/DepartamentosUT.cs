using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class DepartamentosUT
    {
        private IConexion? conexion { get; set; }
        private Departamentos? departamento { get; set; }

        public DepartamentosUT()
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
            this.departamento = new Departamentos()
            {
                Nombre = "Antioquia",
                Estado = true
            };

            this.conexion!.Departamentos!.Add(this.departamento);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Departamentos.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.departamento!.Estado = false;
            this.conexion!.Entry(this.departamento).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Departamentos.Remove(this.departamento!);
            this.conexion.SaveChanges();
        }
    }
}
