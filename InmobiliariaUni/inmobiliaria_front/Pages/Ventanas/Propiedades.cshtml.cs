using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace presentacion_aspnetcore.Pages
{
    public class PropiedadesModel : PageModel
    {
        private IPropiedadesNegocio? IPropiedadesnegocio;
        private ITiposPropiedadesNegocio? ITiposPropiedadesnegocio;
        private ISectoresNegocio? ISectoresnegocio;

        [BindProperty] public List<Propiedades>? Lista { get; set; }
        [BindProperty] public Propiedades? propiedad { get; set; }
        [BindProperty] public List<TiposPropiedades>? listaTiposPropiedades { get; set; }
        [BindProperty] public List<Sectores>? listaSectores { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        public string MensajeBorrado { get; set; }

        public PropiedadesModel()
        {
            IPropiedadesnegocio = new PropiedadesNegocio();
            ITiposPropiedadesnegocio = new TiposPropiedadesNegocio();
            ISectoresnegocio = new SectoresNegocio();
        }

        private void cargarlistas()
        {
            listaTiposPropiedades = ITiposPropiedadesnegocio!.Consultar();
            listaSectores = ISectoresnegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IPropiedadesnegocio == null)
                    return;
                Lista = IPropiedadesnegocio.Consultar();
                cargarlistas();
                propiedad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            cargarlistas();
            propiedad = new Propiedades()
            {

            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                propiedad = Lista!.FirstOrDefault(x => x.Id == data);
                cargarlistas();
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
                if (propiedad == null)
                    return;
                if (propiedad.Id == 0)
                    propiedad = IPropiedadesnegocio!.Guardar(propiedad!);
                else
                    propiedad = IPropiedadesnegocio!.Modificar(propiedad!);
                if (propiedad.Id == 0)
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
                if (propiedad == null)
                    return;
                MensajeBorrado = IPropiedadesnegocio!.Eliminar(propiedad!);
                OnPostBtRefrescar();
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
                propiedad = Lista!.FirstOrDefault(x => x.Id == data);
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