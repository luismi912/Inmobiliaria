using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class CompradoresUT
    {
        private IConexion? conexion { get; set; }
        private Compradores? comprador { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }

        public CompradoresUT()
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
            this.nacionalidad = this.apoyo!.GuardarNacionalidad();

            this.comprador = new Compradores()
            {
                Cedula = "1017928809",
                Nombre = "Luis fernando",
                Apellido = "Arango Martinez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                PresupuestoMaximo = 350000000,
                Nacionalidad = this.nacionalidad.Id
            };

            this.conexion!.Compradores.Include(n => n._Nacionalidad);
            this.conexion!.Compradores!.Add(this.comprador);

            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Compradores.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.comprador!.Estado = false;
            this.conexion!.Entry(this.comprador).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Compradores.Remove(this.comprador!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);          
            this.conexion.SaveChanges();
        }
    }
}
