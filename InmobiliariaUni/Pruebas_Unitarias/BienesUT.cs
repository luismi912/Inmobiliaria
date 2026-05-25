using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class BienesUT
    {
        private IConexion? conexion { get; set; }
        private Bienes? bien { get; set; }
        private RespaldosCodeudores? respaldosCodeudores { get; set; }
        private Compradores? comprador { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Codeudores? codeudor { get; set; }

        public BienesUT()
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
            this.respaldosCodeudores = this.apoyo!.GuardarRespaldoCodeudor(codeudor);

            this.bien = new Bienes()
            {
                Nombre = "Moto acuatica",
                Descripcion = "Esta moto fue comprada en descuento en una feria",
                FechaAdquisicion = DateTime.Now,
                ValorAdquisicion = 60000000,
                ValorActual = 50000000,
                RespaldoFinanciero = respaldosCodeudores.Id
            };

            this.conexion!.Bienes!.Add(this.bien);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.Bienes.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.bien!.ValorActual = 40000000;
            this.conexion!.Entry(this.bien).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.Bienes.Remove(this.bien!);
            this.conexion!.RespaldosCodeudores.Remove(this.respaldosCodeudores!);
            this.conexion!.Codeudores.Remove(this.codeudor!);
            this.conexion!.Compradores.Remove(this.comprador!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            
            this.conexion.SaveChanges();
        }
    }
}
