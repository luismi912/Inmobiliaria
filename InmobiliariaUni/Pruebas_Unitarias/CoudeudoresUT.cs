using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class CodeudoresUT
    {
        private IConexion? conexion { get; set; }
        private Codeudores? codeudor { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private Compradores? comprador { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }

        public CodeudoresUT()
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
            this.comprador = this.apoyo!.GuardarComprador(this.nacionalidad);

            this.codeudor = new Codeudores()
            {
                Cedula = "1017928809",
                Nombre = "Luis fernando",
                Apellido = "Arango Martinez",
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
                Estado = true,
                Nacionalidad = this.nacionalidad.Id,
                Comprador = comprador.Id
            };

            this.conexion!.Codeudores!.Add(this.codeudor);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Codeudores.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.codeudor!.Estado = false;
            this.conexion!.Entry(this.codeudor).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Codeudores.Remove(this.codeudor!);
            this.conexion!.Compradores.Remove(this.comprador!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);        
            this.conexion.SaveChanges();
        }
    }
}
