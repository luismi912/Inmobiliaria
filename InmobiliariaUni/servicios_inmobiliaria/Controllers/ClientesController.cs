using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientesController : ControllerBase
    {
        private IClientesNegocio? IClientesnegocio { get; set; }

        public ClientesController()
        {
            this.IClientesnegocio = new ClientesNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Clientes> Consultar()
        {
            if (this.IClientesnegocio == null)
                throw new Exception("No implementado");
            return this.IClientesnegocio!.Consultar();
        }

        [HttpPut]
        public Clientes Modificar(Clientes entidad)
        {
            if (this.IClientesnegocio == null)
                throw new Exception("No implementado");
            return this.IClientesnegocio!.Modificar(entidad);
        }

        [HttpDelete]
        public string Eliminar(Clientes entidad)
        {
            if (this.IClientesnegocio == null)
                throw new Exception("No implementado");
            return this.IClientesnegocio!.Eliminar(entidad);
        }

        [HttpPost]
        public Clientes Guardar(CrearUsuariosClientesDtos dto)
        {
            if (this.IClientesnegocio == null)
                throw new Exception("No implementado");
            return this.IClientesnegocio!.Guardar(dto);
        }
    }
}
