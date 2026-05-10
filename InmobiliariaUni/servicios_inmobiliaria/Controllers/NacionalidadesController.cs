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
        private INacionalidadesNegocio? INaciondalidadesnegocio { get; set; }

        public NacionalidadesController()
        {
            this.INaciondalidadesnegocio = new NacionalidadesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Nacionalidades> Consultar()
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio.Consultar();
        }

        [HttpPost]
        public Nacionalidades Guardar(Nacionalidades entidad)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Nacionalidades Modificar(Nacionalidades entidad)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio!.Modificar(entidad);
        }
    }
}
