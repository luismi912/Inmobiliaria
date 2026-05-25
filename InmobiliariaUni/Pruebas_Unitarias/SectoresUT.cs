using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class SectoresUT
    {
        private IConexion? conexion { get; set; }
        private Sectores? sector { get; set; }
        private Departamentos? departamento { get; set; }
        private Ciudades? ciudad { get; set; }
        public EntidadesApoyoUT? apoyo { get; set; }

        public SectoresUT()
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
            this.ciudad = apoyo!.GuardarCiudad(departamento);

            this.sector = new Sectores()
            {
                Nombre = "InmoTuya",
                Estado = true,
                Ciudad = ciudad.Id
            };

            this.conexion!.Sectores!.Add(this.sector!);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Ciudades.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.sector!.Estado = false;
            this.conexion!.Entry(this.sector).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Sectores.Remove(this.sector!);
            this.conexion!.Ciudades.Remove(this.ciudad!);
            this.conexion!.Departamentos.Remove(this.departamento!);
            this.conexion.SaveChanges();
        }
    }
}
