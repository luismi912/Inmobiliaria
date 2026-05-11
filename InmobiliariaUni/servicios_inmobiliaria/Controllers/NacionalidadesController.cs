using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NacionalidadesController : ControllerBase
    {
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }

        public NacionalidadesController()
        {
            this.INacionalidadesnegocio = new NacionalidadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Nacionalidades> Consultar()
        {
            if (this.INacionalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INacionalidadesnegocio.Consultar();
        }

        [HttpPost]
        public Nacionalidades Guardar(Nacionalidades entidad)
        {
            if (this.INacionalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INacionalidadesnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Nacionalidades entidad)
        {
            if (this.INacionalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INacionalidadesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public Nacionalidades Modificar(Nacionalidades entidad)
        {
            if (this.INacionalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INacionalidadesnegocio!.Modificar(entidad);
        }
    }
}
