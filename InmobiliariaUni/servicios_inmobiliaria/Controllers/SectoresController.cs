using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SectoresController : ControllerBase
    {
        private ISectoresNegocio? ISectoresnegocio { get; set; }

        public SectoresController()
        {
            this.ISectoresnegocio = new SectoresNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Sectores> Consultar()
        {
            if (this.ISectoresnegocio == null)
                throw new Exception("No implementado");
            return this.ISectoresnegocio.Consultar();
        }

        [HttpPost]
        public Sectores Guardar(Sectores entidad)
        {
            if (this.ISectoresnegocio == null)
                throw new Exception("No implementado");
            return this.ISectoresnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.ISectoresnegocio == null)
                throw new Exception("No implementado");
            return this.ISectoresnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Sectores Modificar(Sectores entidad)
        {
            if (this.ISectoresnegocio == null)
                throw new Exception("No implementado");
            return this.ISectoresnegocio!.Modificar(entidad);
        }
    }
}

