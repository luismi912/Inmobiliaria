using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class TelefonosNegocio : ITelefonosNegocio
    {
        private IConexion? conexion { get; set; }

        public TelefonosNegocio ()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public Telefonos Guardar(Telefonos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.Telefonos!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Telefonos> Consultar(int Id)
        {
            var Lista = this.conexion!.Telefonos!.Where(p => p.Persona == Id).ToList();   //Buscamos lss Telefonos segun la persona

            return Lista;
        }

        public string Eliminar(int Id)
        {
            var telefono = this.conexion!.Telefonos!.FirstOrDefault(p => p.Id == Id);

            if (telefono == null)
                return "No exsite la entidad a eliminar";

            this.conexion.Telefonos.Remove(telefono);
            this.conexion.SaveChanges();

            return "Se elimino el telefono correctamente";
        }

        public Telefonos Modificar(Telefonos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
