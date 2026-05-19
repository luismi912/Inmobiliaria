using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace presentacion_aspnetcore.Pages
{
    public class SectoresModel : PageModel
    {
        private ISectoresNegocio? ISectoresnegocio;
        private ICiudadesNegocio? ICiudadesnegocio;

        [BindProperty] public List<Sectores>? Lista { get; set; }
        [BindProperty] public Sectores? Sector { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Ciudades>? ListaCiudad { get; set; }

        public SectoresModel()
        {
            ISectoresnegocio = new SectoresNegocio();
            ICiudadesnegocio = new CiudadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void CargarListaCiudad()
        {
            ListaCiudad = ICiudadesnegocio!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (ISectoresnegocio == null)
                    return;
                Lista = ISectoresnegocio!.Consultar();
                CargarListaCiudad();
                Sector = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarListaCiudad();
            Sector = new Sectores()
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
                Sector = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                CargarListaCiudad();
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
                if (Sector == null)
                    return;
                if (Sector.Id == 0)
                    Sector = ISectoresnegocio!.Guardar(Sector!);
                else
                    Sector = ISectoresnegocio!.Modificar(Sector!);
                if (Sector.Id == 0)
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
                if (Sector == null)
                    return;
                ViewData["Mensaje"] = ISectoresnegocio!.Eliminar(Sector!);
                Sector = null;
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
                Sector = Lista!.FirstOrDefault(x => x.Id == data);
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