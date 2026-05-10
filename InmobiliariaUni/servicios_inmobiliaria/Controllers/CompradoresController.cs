using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CompradoresController : ControllerBase
    {
        private ICompradoresNegocio? ICompradoresnegocio { get; set; }

        public CompradoresController()
        {
            this.ICompradoresnegocio = new CompradoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Compradores> Consultar()
        {
            if (this.ICompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICompradoresnegocio!.Consultar();
        }

        [HttpPut]
        public Compradores Modificar(Compradores entidad)
        {
            if (this.ICompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICompradoresnegocio!.Modificar(entidad);
        }

        [HttpDelete("{Cedula}")]
        public string Eliminar(string Cedula)
        {
            if (this.ICompradoresnegocio == null)
                throw new Exception("No implementado");
            return this.ICompradoresnegocio!.Eliminar(Cedula);
        }
    }
}
