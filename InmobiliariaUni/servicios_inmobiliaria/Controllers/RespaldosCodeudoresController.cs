using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RespaldosCodeudoresController : ControllerBase
    {
        private IRespaldosCodeudoresNegocio? IRespaldosCodeudoresnegocio { get; set; }

        public RespaldosCodeudoresController()
        {
            this.IRespaldosCodeudoresnegocio = new RespaldosCodeudoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<RespaldosCodeudores> Consultar()
        {
            if (this.IRespaldosCodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCodeudoresnegocio.Consultar();
        }

        [HttpPost]
        public RespaldosCodeudores Guardar(RespaldosCodeudores entidad)
        {
            if (this.IRespaldosCodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCodeudoresnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.IRespaldosCodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCodeudoresnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public RespaldosCodeudores Modificar(RespaldosCodeudores entidad)
        {
            if (this.IRespaldosCodeudoresnegocio == null)
                throw new Exception("No implementado");
            return this.IRespaldosCodeudoresnegocio!.Modificar(entidad);
        }
    }
}

