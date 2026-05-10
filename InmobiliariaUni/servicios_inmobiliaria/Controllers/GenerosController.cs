using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GenerosController : ControllerBase
    {
        private IGenerosNegocio? IGenerosnegocio { get; set; }

        public GenerosController()
        {
            this.IGenerosnegocio = new GenerosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Generos> Consultar()
        {
            if (this.IGenerosnegocio == null)
                throw new Exception("No implementado");
            return this.IGenerosnegocio.Consultar();
        }

        [HttpPost]
        public Generos Guardar(Generos entidad)
        {
            if (this.IGenerosnegocio == null)
                throw new Exception("No implementado");
            return this.IGenerosnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.IGenerosnegocio == null)
                throw new Exception("No implementado");
            return this.IGenerosnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Generos Modificar(Generos entidad)
        {
            if (this.IGenerosnegocio == null)
                throw new Exception("No implementado");
            return this.IGenerosnegocio!.Modificar(entidad);
        }
    }
}
