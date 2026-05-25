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
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosnegocio { get; set; } 
        private IDepartamentosNegocio? IDepartamentosnegocio { get; set; }
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }

        [BindProperty] public CrearUsuariosAdministradoresDtos? AdminDto { get; set; } = new CrearUsuariosAdministradoresDtos();
        [BindProperty] public UsuariosRoles? UsuarioRol { get; set; } = new UsuariosRoles();
        [BindProperty] public ExpedientesLaborales? ExpedienteLaboral { get; set; } = new ExpedientesLaborales();
        [BindProperty] public Telefonos? Telefono { get; set; } = new Telefonos();
        [BindProperty] public Direcciones? Direccion { get; set; } = new Direcciones();


        //LISTAS QUE SE LLAMAN PARA PODER QUE EL USUARIO SELECCIONE SU A LA QUE PERTENECE 
        [BindProperty] public List<Departamentos>? Departamentos { get; set; }
        [BindProperty] public List<Nacionalidades>? Nacionalidades { get; set; }
        [BindProperty] public List<AdministradoresDepartamentos>? Administradores { get; set; }

        public IndexModel()
        {
            IAdministradoresDepartamentosnegocio = new AdministradoresDepartamentosNegocio();
            IDepartamentosnegocio = new DepartamentosNegocio();
            INacionalidadesnegocio = new NacionalidadesNegocio();
        }

        public void OnGet()
        {
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Departamentos = IDepartamentosnegocio!.Consultar();
            Administradores = IAdministradoresDepartamentosnegocio!.Consultar();
        }

        public void OnPostIngresar()
        {

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
