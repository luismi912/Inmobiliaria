using libreria_inmobiliaria.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;

namespace presentacion_aspnetcore.Pages
{
    public class CiudadesModel : PageModel
    {
        private ICiudadesNegocio? ICiudadesnegocio;
        private IDepartamentosNegocio? IDepartamentosnegocio;

        [BindProperty] public List<Ciudades>? Lista { get; set; }
        [BindProperty] public Ciudades? Ciudad { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public List<Departamentos>? ListaDepartamento { get; set; }

        public CiudadesModel()
        {
            ICiudadesnegocio = new CiudadesNegocio();
            IDepartamentosnegocio = new DepartamentosNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void CargarListaDepartamento()
        {
            ListaDepartamento = IDepartamentosnegocio!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (ICiudadesnegocio == null)
                    return;
                Lista = ICiudadesnegocio!.Consultar();
                CargarListaDepartamento();
                Ciudad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarListaDepartamento();
            Ciudad = new Ciudades()
            {
                Estado = true,
                FechaCreacion = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ciudad = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                CargarListaDepartamento();
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
                if (Ciudad == null)
                    return;
                if (Ciudad.Id == 0)
                    Ciudad = ICiudadesnegocio!.Guardar(Ciudad!);
                else
                    Ciudad = ICiudadesnegocio!.Modificar(Ciudad!);
                if (Ciudad.Id == 0)
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
                if (Ciudad == null)
                    return;
                ViewData["Mensaje"] = ICiudadesnegocio!.Eliminar(Ciudad!);
                Ciudad = null;
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
                Ciudad = Lista!.FirstOrDefault(x => x.Id == data);
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