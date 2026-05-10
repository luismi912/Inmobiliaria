using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ContratosContadosNegocio : IContratosContadosNegocio
    {
        private IConexion? conexion { get; set; }

        public ContratosContadosNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public ContratosContados Guardar(ContratosContados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.ContratosContados!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<ContratosContados> Consultar()
        {
            var Lista = this.conexion!.ContratosContados!.ToList();
            return Lista;
        }

        public string Eliminar(int Id)
        {
            var departamento = this.conexion!.ContratosContados!.FirstOrDefault(p => p.Id == Id);

            if (departamento == null)
                return "No exsite la entidad a eliminar";

            this.conexion.ContratosContados.Remove(departamento);
            this.conexion.SaveChanges();

            return "Se elimino la nacionalidad correctamente";
        }

        public ContratosContados Modificar(ContratosContados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
