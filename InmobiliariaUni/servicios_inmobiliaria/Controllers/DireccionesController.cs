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
        private IDireccionesNegocio? IDireccionesnegocio { get; set; }

        public DireccionesController()
        {
            this.IDireccionesnegocio = new DireccionesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Direcciones> Consultar(int Id)
        {
            if (this.IDireccionesnegocio == null)
                throw new Exception("No implementado");
            return this.IDireccionesnegocio.Consultar(Id);
        }

        [HttpPost]
        public Direcciones Guardar(Direcciones entidad)
        {
            if (this.IDireccionesnegocio == null)
                throw new Exception("No implementado");
            return this.IDireccionesnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Direcciones entidad)
        {
            if (this.IDireccionesnegocio == null)
                throw new Exception("No implementado");
            return this.IDireccionesnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public Direcciones Modificar(Direcciones entidad)
        {
            if (this.IDireccionesnegocio == null)
                throw new Exception("No implementado");
            return this.IDireccionesnegocio!.Modificar(entidad);
        }
    }
}
