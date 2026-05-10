using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RespaldosCompradoresController : ControllerBase
    {
        private IRespaldosCompradoresNegocio? IRespaldosCompradoresnegocio { get; set; }

        public RespaldosCompradoresController()
        {
            this.IRespaldosCompradoresnegocio = new RespaldosCompradoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<RespaldosCompradores> Consultar()
        {
            if (this.IRespaldosCompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCompradoresnegocio.Consultar();
        }

        [HttpPost]
        public RespaldosCompradores Guardar(RespaldosCompradores entidad)
        {
            if (this.IRespaldosCompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCompradoresnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.IRespaldosCompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCompradoresnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public RespaldosCompradores Modificar(RespaldosCompradores entidad)
        {
            if (this.IRespaldosCompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCompradoresnegocio!.Modificar(entidad);
        }
    }
}

