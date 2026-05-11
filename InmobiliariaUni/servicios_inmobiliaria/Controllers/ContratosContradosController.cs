using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ContratosContadosController : ControllerBase
    {
        private IContratosContadosNegocio? IContratosContadosnegocio { get; set; }

        public ContratosContadosController()
        {
            this.IContratosContadosnegocio = new ContratosContadosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<ContratosContados> Consultar()
        {
            if (this.IContratosContadosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosContadosnegocio.Consultar();
        }

        [HttpPost]
        public ContratosContados Guardar(ContratosContados entidad)
        {
            if (this.IContratosContadosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosContadosnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(ContratosContados entidad)
        {
            if (this.IContratosContadosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosContadosnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public ContratosContados Modificar(ContratosContados entidad)
        {
            if (this.IContratosContadosnegocio == null)
                throw new Exception("No implementado");
            return this.IContratosContadosnegocio!.Modificar(entidad);
        }
    }
}

