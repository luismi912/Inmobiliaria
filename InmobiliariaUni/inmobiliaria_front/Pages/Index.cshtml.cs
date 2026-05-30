using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace inmobiliaria_front.Pages
{
    public class IndexModel : PageModel
    {
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosnegocio { get; set; }
        private IJefesSectoresNegocio? IJefesSectoresnegocio { get; set; }
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocio { get; set; }
        private IUsuarioRolesNegocio? IUsuariosRolesnegocio { get; set; }
        [BindProperty] public UsuariosRoles? UsuarioRol { get; set; }

        public IndexModel()
        {
            IUsuariosRolesnegocio = new UsuariosRolesNegocio();
            IJefesSectoresnegocio = new JefesSectoresNegocio();
            IEmpleadosSectoresnegocio = new EmpleadosSectoresNegocio();
            IAdministradoresDepartamentosnegocio = new AdministradoresDepartamentosNegocio();
        }

        public void OnGet()
        {
            
        }

        public async Task OnPostBtIngresar()
        {
            try
            {
                //Verificamos que el usuario no se haya mandado null
                if (UsuarioRol == null)
                {
                    ViewData["Mensaje"] = "Correo o contraseña incorrectos, reintente por favor";
                    return;
                }

                if (UsuarioRol!.Rol == "")
                    throw new Exception("No se a seleccionado ningun rol");

                //Se realiza una busqueda si si existe el correo y que retorne la entidad relacionada al mismo
                var usuario = await IUsuariosRolesnegocio!.ConsultarCorreoAsync(UsuarioRol.Correo!)!;

                //Verificamos que en esa entidad que llamamos coincida con todos los datos que el usuario completo
                if (usuario.Correo != UsuarioRol.Correo || usuario.Contraseña != UsuarioRol.Contraseña || usuario.Rol != UsuarioRol.Rol)
                    throw new Exception("La contraseña o el correo son incorrectos, reintente por favor");

                //Los atributos con los cuales se identificara el usuario en la navegacion
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UsuarioRol.Correo!),
                new Claim(ClaimTypes.Role, UsuarioRol.Rol!),
            };

                //Añadimos el id en caso de que queramos utilizarlo en otras paginas
                if (usuario.Rol == "Administrador")
                {
                    var admin = IAdministradoresDepartamentosnegocio!.ConsultarUsuario(usuario.Id);
                    claims.Add(new Claim("IdPersona", admin.Id.ToString()));
                }
                else if (usuario.Rol == "Jefe")
                {
                    var jefe = IJefesSectoresnegocio!.ConsultarUsuario(usuario.Id);
                    claims.Add(new Claim("IdPersona", jefe.Id.ToString()));
                }
                else if (usuario.Rol == "Empleado")
                {
                    var empleado = IEmpleadosSectoresnegocio!.ConsultarUsuario(usuario.Id);
                    claims.Add(new Claim("IdPersona", empleado.Id.ToString()));
                }

                //Primero juntamos los datos con los que vamos a identificar al usuario
                var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Luego creamos la principal con el cual con esto se da de cuenta quien esta navegando en la pagina
                var principal = new ClaimsPrincipal(identidad);

                //El navegador guarda la cookie y ya se puede navegar libremente
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (usuario.Rol == "Administrador")
                    Response.Redirect("/Ventanas/Inicio");
                else if (usuario.Rol == "Jefe")
                    Response.Redirect("/Ventanas/Inicio");
                else
                    Response.Redirect("/Ventanas/Inicio");

            } catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public async Task<IActionResult> OnPostBtCerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
