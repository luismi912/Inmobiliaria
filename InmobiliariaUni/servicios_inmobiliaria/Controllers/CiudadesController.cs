using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CiudadesController : ControllerBase
    {
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }

        public CiudadesController()
        {
            this.ICiudadesnegocio = new CiudadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Ciudades> Consultar()
        {
            if (this.ICiudadesnegocio == null)
                throw new Exception("No implementado");
            return this.ICiudadesnegocio.Consultar();
        }

        [HttpPost]
        public Ciudades Guardar(Ciudades entidad)
        {
            if (this.ICiudadesnegocio == null)
                throw new Exception("No implementado");
            return this.ICiudadesnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Ciudades entidad)
        {
            if (this.ICiudadesnegocio == null)
                throw new Exception("No implementado");
            return this.ICiudadesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public Ciudades Modificar(Ciudades entidad)
        {
            if (this.ICiudadesnegocio == null)
                throw new Exception("No implementado");
            return this.ICiudadesnegocio!.Modificar(entidad);
        }
    }
}

