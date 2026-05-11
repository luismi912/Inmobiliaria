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
        private ITelefonosNegocio? ITelefonosnegocio { get; set; }

        public TelefonosController()
        {
            this.ITelefonosnegocio = new TelefonosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Telefonos> Consultar(int Id)
        {
            if (this.ITelefonosnegocio == null)
                throw new Exception("No implementado");
            return this.ITelefonosnegocio.Consultar(Id);
        }

        [HttpPost]
        public Telefonos Guardar(Telefonos entidad)
        {
            if (this.ITelefonosnegocio == null)
                throw new Exception("No implementado");
            return this.ITelefonosnegocio!.Guardar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Telefonos entidad)
        {
            if (this.ITelefonosnegocio == null)
                throw new Exception("No implementado");
            return this.ITelefonosnegocio!.Eliminar(entidad);
        }

        [HttpPut]
        public Telefonos Modificar(Telefonos entidad)
        {
            if (this.ITelefonosnegocio == null)
                throw new Exception("No implementado");
            return this.ITelefonosnegocio!.Modificar(entidad);
        }
    }
}
