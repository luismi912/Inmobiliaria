using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TelefonosController : ControllerBase
    {
        private ITelefonosNegocio? INaciondalidadesnegocio { get; set; }

        public TelefonosController()
        {
            this.INaciondalidadesnegocio = new TelefonosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Telefonos> Consultar(int Id)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio.Consultar(Id);
        }

        [HttpPost]
        public Telefonos Guardar(Telefonos entidad)
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
        public Telefonos Modificar(Telefonos entidad)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio!.Modificar(entidad);
        }
    }
}
