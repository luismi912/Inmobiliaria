using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TiposPropiedadesController : ControllerBase
    {
        private ITiposPropiedadesNegocio? ITiposPropiedadesnegocio { get; set; }

        public TiposPropiedadesController()
        {
            this.ITiposPropiedadesnegocio = new TiposPropiedadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<TiposPropiedades> Consultar()
        {
            if (this.ITiposPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.ITiposPropiedadesnegocio.Consultar();
        }

        [HttpPost]
        public TiposPropiedades Guardar(TiposPropiedades entidad)
        {
            if (this.ITiposPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.ITiposPropiedadesnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(TiposPropiedades entidad)
        {
            if (this.ITiposPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.ITiposPropiedadesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public TiposPropiedades Modificar(TiposPropiedades entidad)
        {
            if (this.ITiposPropiedadesnegocio == null)
                throw new Exception("No implementado");
            return this.ITiposPropiedadesnegocio!.Modificar(entidad);
        }
    }
}

