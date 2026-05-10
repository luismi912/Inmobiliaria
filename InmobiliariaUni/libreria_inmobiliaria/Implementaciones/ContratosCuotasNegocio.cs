using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Interfaces;
using libreria_inmobiliaria.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace libreria_inmobiliaria.Implementaciones
{
    public class ContratosCuotasNegocio : IContratosCuotasNegocio
    {
        private IConexion? conexion { get; set; }

        public ContratosCuotasNegocio()
        {
            conexion = new Conexion();
            this.conexion.StringConexion = Configuraciones.Obtener("Clave");
        }

        public ContratosCuotas Guardar(ContratosCuotas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("No se puede crear correctamente");

            this.conexion!.ContratosCuotas!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<ContratosCuotas> Consultar()
        {
            var Lista = this.conexion!.ContratosCuotas!.ToList();
            return Lista;
        }

        public string Eliminar(int Id)
        {
            var departamento = this.conexion!.ContratosCuotas!.FirstOrDefault(p => p.Id == Id);

            if (departamento == null)
                return "No exsite la entidad a eliminar";

            this.conexion.ContratosCuotas.Remove(departamento);
            this.conexion.SaveChanges();

            return "Se elimino la nacionalidad correctamente";
        }

        public ContratosCuotas Modificar(ContratosCuotas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            this.conexion!.Entry(entidad).State = EntityState.Modified;
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}
