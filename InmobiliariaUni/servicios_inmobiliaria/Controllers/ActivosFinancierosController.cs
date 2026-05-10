using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ActivosFinancierosController : ControllerBase
    {
        private IActivosFinancierosNegocio? IActivosFinancierosnegocio { get; set; }

        public ActivosFinancierosController()
        {
            this.IActivosFinancierosnegocio = new ActivosFinancierosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<ActivosFinancieros> Consultar()
        {
            if (this.IActivosFinancierosnegocio == null)
                throw new Exception("No implementado");
            return this.IActivosFinancierosnegocio!.Consultar();
        }

        [HttpDelete("{Id}")]
        public string Eliminar(int Id)
        {
            if (this.IActivosFinancierosnegocio == null)
                throw new Exception("No implementado");
            return this.IActivosFinancierosnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public ActivosFinancieros Modificar(ActivosFinancieros entidad)
        {
            if (this.IActivosFinancierosnegocio == null)
                throw new Exception("No implementado");
            return this.IActivosFinancierosnegocio!.Modificar(entidad);
        }
    }
}
