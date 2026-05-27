using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.Win32.SafeHandles;

namespace inmobiliaria_front.Pages.Empleados
{
    public class DireccionesModel : PageModel
    {
        private IDireccionesNegocio? IDireccioesnegocio { get; set; }
        private IPersonasNegocio? IPersonasnegocio { get; set; }
        private ICiudadesNegocio? ICiudadesnegocio { get; set; }

        [BindProperty] public Direcciones? Direccion { get; set; }
        public List<Direcciones>? Direcciones { get; set; }
        public List<Ciudades>? Ciudades { get; set; }
        public List<Personas>? Personas { get; set; }
        public bool Borrando { get; set; }
        [BindProperty] public string? CedulaPersona { get; set; } = "0";

        public DireccionesModel()
        {
            IDireccioesnegocio = new DireccionesNegocio();
            IPersonasnegocio = new PersonasNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
        }

        private void cargarlistas()
        {
            Ciudades = ICiudadesnegocio!.Consultar();
            Personas = IPersonasnegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IDireccioesnegocio == null)
                    return;
                Direcciones = IDireccioesnegocio.Consultar();
                cargarlistas();
                Direccion = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            cargarlistas();
            Direccion = new Direcciones()
            {
                Persona = 0
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Direccion = Direcciones!.FirstOrDefault(x => x.Id == data);
                cargarlistas();
                Direcciones = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (Direccion == null)        
                    return;

                Direccion!.Persona = IPersonasnegocio!.ConsultarPorCedula(CedulaPersona!);

                if (Direccion.Persona == 0)
                    throw new Exception("No se pudo encontrar la cedula, reintente porfavor");
                if (Direccion.Id == 0)
                    Direccion = IDireccioesnegocio!.Guardar(Direccion!);
                else
                    Direccion = IDireccioesnegocio!.Modificar(Direccion!);

                if (Direccion.Id == 0)
                    return;

                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Direccion == null)
                    return;
                ViewData["Mensaje"] = IDireccioesnegocio!.Eliminar(Direccion!);
                Direccion = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            OnPostBtRefrescar();
            try
            {
                Direccion = Direcciones!.FirstOrDefault(x => x.Id == data);
                Direcciones = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}