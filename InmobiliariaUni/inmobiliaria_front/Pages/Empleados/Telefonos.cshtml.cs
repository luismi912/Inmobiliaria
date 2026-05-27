using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32.SafeHandles;

namespace inmobiliaria_front.Pages.Empleados
{
    public class TelefonosModel : PageModel
    {
        private ITelefonosNegocio? ITelefonosnegocio { get; set; }
        private IPersonasNegocio? IPersonasnegocio { get; set; }

        [BindProperty] public Telefonos? Telefono { get; set; }
        public List<Telefonos>? telefonos { get; set; }
        public List<Personas>? Personas { get; set; }
        public bool Borrando { get; set; }
        [BindProperty] public string? CedulaPersona { get; set; } = "0";

        public TelefonosModel()
        {
            ITelefonosnegocio = new TelefonosNegocio();
            IPersonasnegocio = new PersonasNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        private void cargarlistas()
        {
            Personas = IPersonasnegocio!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                cargarlistas();
                if (ITelefonosnegocio == null)
                    return;
                telefonos = ITelefonosnegocio.Consultar();
                Telefono = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Telefono = new Telefonos()
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
                Telefono = telefonos!.FirstOrDefault(x => x.Id == data);
                telefonos = null;
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
                cargarlistas();
                if (Telefono == null)        
                    return;

                Telefono!.Persona = IPersonasnegocio!.ConsultarPorCedula(CedulaPersona!);

                if (Telefono.Persona == 0)
                    throw new Exception("No se pudo encontrar la cedula, reintente porfavor");
                if (Telefono.Id == 0)
                    Telefono = ITelefonosnegocio!.Guardar(Telefono!);
                else
                    Telefono = ITelefonosnegocio!.Modificar(Telefono!);

                if (Telefono.Id == 0)
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
                if (Telefono == null)
                    return;
                ViewData["Mensaje"] = ITelefonosnegocio!.Eliminar(Telefono!);
                Telefono = null;
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
                Telefono = telefonos!.FirstOrDefault(x => x.Id == data);
                telefonos = null;
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