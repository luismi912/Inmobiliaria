using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace inmobiliaria_front.Pages
{
    [Authorize(Roles = "Administrador")]
    public class CrearJefeModel : PageModel
    {
        private IJefesSectoresNegocio? IJefesSectoresnegocio { get; set; }
        private INacionalidadesNegocio? INacionalidadesnegocio { get; set; }
        private ISectoresNegocio? ISectoresnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }
        private IAdministradoresDepartamentosNegocio? IAdministradoresDepartamentosnegocio { get; set; }
        private IUsuarioRolesNegocio? IUsuarioRolesnegocio { get; set; }

        //El usuario llena un campo en el formulario y se necesita ese valor en el servidor
        [BindProperty] public CrearUsuariosJefesDtos? JefeDto { get; set; }
        public List<AdministradoresDepartamentos>? Administradores { get; set; }
        public List<Nacionalidades>? Nacionalidades { get; set; }
        public List<Ciudades>? Ciudades { get; set; }
        public List<Sectores>? Sectores { get; set; }

        public CrearJefeModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
            IUsuarioRolesnegocio = new UsuariosRolesNegocio();
            IAdministradoresDepartamentosnegocio = new AdministradoresDepartamentosNegocio();
            IJefesSectoresnegocio = new JefesSectoresNegocio();
            ISectoresnegocio = new SectoresNegocio();
        }

        public void OnGet()
        {
            Refrescar();
        }

        public void Refrescar()
        {
            Administradores = IAdministradoresDepartamentosnegocio!.Consultar();
            Nacionalidades = INacionalidadesnegocio!.Consultar();
            Ciudades = ICiudadesnegocio!.Consultar();
            Sectores = ISectoresnegocio!.Consultar();
            JefeDto = new CrearUsuariosJefesDtos()
            {
                Jefe = new JefesDtos()
                {
                   Estado = true,
                   FechaNacimiento = DateTime.Now,
                   FechaRegistro = DateTime.Now,
                   Expediente = new ExpedientesLaborales() { FechaIngreso = DateTime.Now },
                }
            };
        }

        public void OnPostBtEnviar()
        {
            try
            {
                //Mandamos el correo a crear
                var usuario = IUsuarioRolesnegocio!.ConsultarCorreo(JefeDto!.Correo!);

                //verificamos que no existe, si existe retorna el usuario si no retorna null
                if (usuario != null)
                    throw new Exception("El correo que intentas crear ya existe");

                IJefesSectoresnegocio!.Guardar(JefeDto!);
                ViewData["Mensaje"] = "Se creó correctamente";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
            finally
            {
                Refrescar();
            }
        }
    }
}
