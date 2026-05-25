using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class ActivosFinancierosUT
    {
        private IConexion? conexion { get; set; }
        private ActivosFinancieros? activo { get; set; }
        private RespaldosCodeudores? respaldosCodeudores { get; set; }
        private Compradores? comprador { get; set; }
        private Nacionalidades? nacionalidad { get; set; }
        private EntidadesApoyoUT? apoyo { get; set; }
        private Codeudores? codeudor { get; set; }

        public ActivosFinancierosUT()
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

            this.activo = new ActivosFinancieros()
            {
                Nombre = "Moto acuatica",
                Descripcion = "Esta empresa fue heredada de su abuelo",
                FechaAdquisicion = DateTime.Now,
                Valor = 250000000,
                RespaldoFinanciero = respaldosCodeudores.Id
            };

            this.conexion!.ActivosFinancieros!.Add(this.activo);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.ActivosFinancieros.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.activo!.Valor = 275000000;
            this.conexion!.Entry(this.activo).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.ActivosFinancieros.Remove(this.activo!);
            this.conexion!.RespaldosCodeudores.Remove(this.respaldosCodeudores!);
            this.conexion!.Codeudores.Remove(this.codeudor!);
            this.conexion!.Compradores.Remove(this.comprador!);
            this.conexion!.Nacionalidades.Remove(this.nacionalidad!);
            
            this.conexion.SaveChanges();
        }
    }
}
