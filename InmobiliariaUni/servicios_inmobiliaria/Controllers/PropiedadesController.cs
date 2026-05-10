using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PropiedadesController : ControllerBase
    {
        private IPropiedadesNegocio? IPropiedadesnegocio { get; set; }

        public PropiedadesController()
        {
            this.IPropiedadesnegocio = new PropiedadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Propiedades> Consultar()
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Consultar();
        }

        [HttpDelete("{Id}")]
        public string Eliminar(int Id)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Propiedades Modificar(Propiedades entidad)
        {
            if (this.IPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.IPropiedadesnegocio!.Modificar(entidad);
        }
    }
}
