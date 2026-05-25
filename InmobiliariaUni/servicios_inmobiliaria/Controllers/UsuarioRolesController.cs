using libreria_inmobiliaria.Entidades;
using libreria_inmobiliaria.Implementaciones;
using libreria_inmobiliaria.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioRolesController : ControllerBase
    {
        private IUsuariosRolesNegocio IUsuariosRolesnegocio { get; set; }

        public UsuarioRolesController ()
        {
            IUsuariosRolesnegocio = new UsuariosRolesNegocio();
        }

        [HttpGet]
        public List<UsuariosRoles> Consultar()
        {
            if (this.IUsuariosRolesnegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosRolesnegocio.Consultar();
        }

        [HttpPut]
        public UsuariosRoles Modificar(UsuariosRoles entidad)
        {
            if (this.IUsuariosRolesnegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosRolesnegocio!.Modificar(entidad);
        }

        [HttpDelete]
        public string Eliminar(UsuariosRoles entidad)
        {
            if (this.IUsuariosRolesnegocio == null)
                throw new Exception("No implementado");
            return this.IUsuariosRolesnegocio!.Eliminar(entidad);
        }
    }
}

