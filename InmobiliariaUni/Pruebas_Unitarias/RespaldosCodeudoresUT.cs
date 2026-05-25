using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class RespaldosCodeudoresUT
    {
        private IConexion? conexion { get; set; }
        private RespaldosCodeudores? respaldosCodeudores { get; set; }
        private Compradores? comprador { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Codeudores? codeudor { get; set; }

        public RespaldosCodeudoresUT()
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
            this.codeudor = this.apoyo!.GuardarCodeudor(this.comprador);

            this.respaldosCodeudores = new RespaldosCodeudores()
            {
                Codeudor = codeudor.Id,
                IngresosMensuales = 7500000,
                DeudasTotales = 350000,
                Observaciones = "El comprador esta en un estado, adminitible en su respaldo primordial"
            };

            this.conexion!.RespaldosCodeudores!.Add(this.respaldosCodeudores);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.RespaldosCodeudores.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.respaldosCodeudores!.IngresosMensuales = 7000000;
            this.conexion!.Entry(this.respaldosCodeudores).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.RespaldosCodeudores.Remove(this.respaldosCodeudores!);
            this.conexion!.Codeudores.Remove(this.codeudor!);
            this.conexion!.Compradores.Remove(this.comprador!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            this.conexion.SaveChanges();
        }
    }
}
