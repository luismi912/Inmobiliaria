using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DepartamentosController : ControllerBase
    {
        private IDepartamentosNegocio? IDepartamentosnegocio { get; set; }

        public DepartamentosController()
        {
            this.IDepartamentosnegocio = new DepartamentosNegocio();    //CADA VEZ QUE SE CREE LA CLASE EL CONSTRUCTOR LA INICIALIZA CON LA CLASE QUE IMPLEMENTA LOS METODOS
        }

        [HttpGet]
        public List<Departamentos> Consultar()
        {
            if (this.IDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IDepartamentosnegocio.Consultar();
        }

        [HttpPost]
        public Departamentos Guardar(Departamentos entidad)
        {
            if (this.IDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IDepartamentosnegocio!.Guardar(entidad);
        }

        [HttpDelete("Id")]
        public string Eliminar(int Id)
        {
            if (this.IDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IDepartamentosnegocio!.Eliminar(Id);
        }

        [HttpPut]
        public Departamentos Modificar(Departamentos entidad)
        {
            if (this.IDepartamentosnegocio == null)
                throw new Exception("No implementado");
            return this.IDepartamentosnegocio!.Modificar(entidad);
        }
    }
}

