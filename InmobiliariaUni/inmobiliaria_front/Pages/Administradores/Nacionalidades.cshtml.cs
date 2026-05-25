using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace presentacion_aspnetcore.Pages
{
    public class NacionalidadesModel : PageModel
    {
        private INacionalidadesNegocio? INacionalidadesnegocio;

        [BindProperty] public List<Nacionalidades>? Lista { get; set; }
        [BindProperty] public Nacionalidades? Nacionalidad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public NacionalidadesModel()
        {
            INacionalidadesnegocio = new NacionalidadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (INacionalidadesnegocio == null)
                    return;
                Lista = INacionalidadesnegocio.Consultar();
                Nacionalidad = null;
                Borrando = false; 
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Nacionalidad = new Nacionalidades()
            {
                Estado = true
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Nacionalidad = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
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
                if (Nacionalidad == null)
                    return;
                if (Nacionalidad.Id == 0)
                    Nacionalidad = INacionalidadesnegocio!.Guardar(Nacionalidad!);
                else
                    Nacionalidad = INacionalidadesnegocio!.Modificar(Nacionalidad!);
                if (Nacionalidad.Id == 0)
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
                if (Nacionalidad == null)
                    return;
                ViewData["Mensaje"] = INacionalidadesnegocio!.Eliminar(Nacionalidad!);
                Nacionalidad = null;
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
                Nacionalidad = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
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