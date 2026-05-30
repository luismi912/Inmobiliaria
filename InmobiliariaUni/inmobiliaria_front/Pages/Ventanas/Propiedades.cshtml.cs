using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32.SafeHandles;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado")]
    public class PropiedadesModel : PageModel
    {
        [BindProperty] public IFormFile? ImagenFile { get; set; }

        private IPropiedadesNegocio? IPropiedadesnegocio;
        private ITiposPropiedadesNegocio? ITiposPropiedadesnegocio;
        private ISectoresNegocio? ISectoresnegocio;
        private IClientesNegocio? IClientesnegocio;
        private IRespaldosFinancierosNegocio? IRespaldosnegocio;

        public List<Propiedades>? propiedades { get; set; }
        [BindProperty] public Propiedades? propiedad { get; set; }
        public List<TiposPropiedades>? tiposPropiedades { get; set; }
        public List<Sectores>? sectores { get; set; }
        public List<Clientes>? clientes { get;set; }
        public List<RespaldosFinancieros>? respaldos { get; set; }
        public bool Borrando { get; set; }

        public PropiedadesModel()
        {
            IPropiedadesnegocio = new PropiedadesNegocio();
            ITiposPropiedadesnegocio = new TiposPropiedadesNegocio();
            ISectoresnegocio = new SectoresNegocio();
            IClientesnegocio = new ClientesNegocio();
            IRespaldosnegocio = new RespaldosFinancierosNegocio();
        }

        private void cargarlistas()
        {
            tiposPropiedades = ITiposPropiedadesnegocio!.Consultar();
            sectores = ISectoresnegocio!.Consultar();
            propiedades = IPropiedadesnegocio!.Consultar();
            clientes = IClientesnegocio!.Consultar();
            respaldos = IRespaldosnegocio!.Consultar();
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
                propiedades = IPropiedadesnegocio.Consultar();
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
            propiedades = null;
            propiedad = new Propiedades()
            {
                FechaConstruccion = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                propiedad = propiedades!.FirstOrDefault(x => x.Id == data);
                cargarlistas();
                propiedades = null;
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

                if (ImagenFile != null)
                {
                    var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "propiedades");
                    Directory.CreateDirectory(carpeta);

                    var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(ImagenFile.FileName)}";
                    var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        ImagenFile.CopyTo(stream);
                    }

                    propiedad!.Imagen = $"/imagenes/propiedades/{nombreArchivo}";
                }

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
                ViewData["Mensaje"] = IPropiedadesnegocio!.Eliminar(propiedad!);
                propiedad = null;
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
                propiedad = propiedades!.FirstOrDefault(x => x.Id == data);
                propiedades = null;
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