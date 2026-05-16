using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class DireccionesNegocio : IDireccionesNegocio
    {
        private IConexion? conexion { get; set; }

        public DireccionesNegocio ()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Direcciones Guardar(Direcciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Direcciones!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Direcciones> Consultar(int Id)
        {
            var Lista = this.conexion!.Direcciones!.Where(p => p.Persona == Id).ToList();   //Buscamos las direcciones segun la persona

            return Lista;
        }

        public string Eliminar(Direcciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se encontro ningun registro a eliminar");

            this.conexion!.Direcciones.Remove(entidad);
            this.conexion.SaveChanges();

            return "La eliminacion se logro con exito";
        }

        public Direcciones Modificar(Direcciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
