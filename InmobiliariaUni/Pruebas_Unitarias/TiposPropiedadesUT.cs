using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Pruebas_Unitarias
{
    [TestClass]
    public class TiposPropiedadesUT
    {
        private IConexion? conexion { get; set; }
        private TiposPropiedades? tipoPropiedad { get; set; }

        public TiposPropiedadesUT()
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
            this.tipoPropiedad = new TiposPropiedades()
            {
                Nombre = "Apartamento",
                Estado = true
            };

            this.conexion!.TiposPropiedades!.Add(this.tipoPropiedad);
            this.conexion!.SaveChanges();
        }

        private void Consultar()
        {
            var lista = this.conexion!.TiposPropiedades.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No hay forma de hacer consultas");
        }

        private void Modificar()
        {
            this.tipoPropiedad!.Estado = false;
            this.conexion!.Entry(this.tipoPropiedad).State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        public void Eliminar()
        {
            this.conexion!.TiposPropiedades.Remove(this.tipoPropiedad!);
            this.conexion.SaveChanges();
        }
    }
}
