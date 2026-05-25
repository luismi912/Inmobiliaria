using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace presentacion_aspnetcore.Pages
{
    public class TiposPropiedadesModel : PageModel
    {
        private ITiposPropiedadesNegocio? ITiposPropiedadesnegocio;

        [BindProperty] public List<TiposPropiedades>? Lista { get; set; }
        [BindProperty] public TiposPropiedades? TipoPropiedad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TiposPropiedadesModel()
        {
            ITiposPropiedadesnegocio = new TiposPropiedadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (ITiposPropiedadesnegocio == null)
                    return;
                Lista = ITiposPropiedadesnegocio.Consultar();
                TipoPropiedad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            TipoPropiedad = new TiposPropiedades()
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
                TipoPropiedad = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (TipoPropiedad == null)
                    return;
                if (TipoPropiedad.Id == 0)
                    TipoPropiedad = ITiposPropiedadesnegocio!.Guardar(TipoPropiedad!);
                else
                    TipoPropiedad = ITiposPropiedadesnegocio!.Modificar(TipoPropiedad!);
                if (TipoPropiedad.Id == 0)
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
                if (TipoPropiedad == null)
                    return;
                ViewData["Mensaje"] = ITiposPropiedadesnegocio!.Eliminar(TipoPropiedad!);
                TipoPropiedad = null;
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
                TipoPropiedad = Lista!.FirstOrDefault(x => x.Id == data);
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