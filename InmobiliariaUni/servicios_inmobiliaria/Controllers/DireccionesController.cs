using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DireccionesController : ControllerBase
    {
        private IDireccionesNegocio? INaciondalidadesnegocio { get; set; }

        public DireccionesController()
        {
            this.INaciondalidadesnegocio = new DireccionesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Direcciones> Consultar(int Id)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio.Consultar(Id);
        }

        [HttpPost]
        public Direcciones Guardar(Direcciones entidad)
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
        public Direcciones Modificar(Direcciones entidad)
        {
            if (this.INaciondalidadesnegocio == null)
                throw new Exception("No implementado");
            return this.INaciondalidadesnegocio!.Modificar(entidad);
        }
    }
}
