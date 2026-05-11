using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContratosArriendosController : ControllerBase
    {
        private IContratosArriendosNegocio? IContratosArriendosnegocio { get; set; }

        public ContratosArriendosController()
        {
            this.IContratosArriendosnegocio = new ContratosArriendosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<ContratosArriendos> Consultar()
        {
            if (this.IContratosArriendosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosArriendosnegocio.Consultar();
        }

        [HttpPost]
        public ContratosArriendos Guardar(ContratosArriendos entidad)
        {
            if (this.IContratosArriendosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosArriendosnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(ContratosArriendos entidad)
        {
            if (this.IContratosArriendosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosArriendosnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public ContratosArriendos Modificar(ContratosArriendos entidad)
        {
            if (this.IContratosArriendosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosArriendosnegocio!.Modificar(entidad);
        }
    }
}

