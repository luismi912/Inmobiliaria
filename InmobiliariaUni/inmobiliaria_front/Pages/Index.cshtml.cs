using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages
{
    public class IndexModel : PageModel
    {
        private IUsuarioRolesNegocio? IUsuariosRolesnegocio { get; set; }
        [BindProperty] public UsuariosRoles? UsuarioRol { get; set; }

        public IndexModel()
        {
            IUsuariosRolesnegocio = new UsuariosRolesNegocio();
        }

        public void OnGet()
        {

        }

        public void OnPostIngresar()
        {
            if (UsuarioRol!.Rol == "")
                throw new Exception("No se a seleccionado ningun rol");
            if (UsuarioRol == null)
            {
                ViewData["Mensaje"] = "Correo o contraseña incorrectos, reintente por favor";
                return;
            }

            //ESTO ES UNA MEMORIA TEMPORAL, LA CUAL SIRVE PARA GUARDAR DATOS MIENTRAS EL USUARIO NAVEGA EN LA APP
            HttpContext.Session.SetString("Rol", UsuarioRol.Rol!);
            HttpContext.Session.SetInt32("Id", UsuarioRol.Id!);

            if (UsuarioRol.Rol == "EmpleadoSector")
                RedirectToPage("/VentanasEmpleadoSector/Pagina");

            else if (UsuarioRol.Rol == "JefeSector")
                RedirectToPage("/VentanasJefeSector/Pagina");

            else if (UsuarioRol.Rol == "AdministradorDepartamento")
                RedirectToPage("/VentanasAdministradorDepartamento/Pagina");
        }
    }
}
