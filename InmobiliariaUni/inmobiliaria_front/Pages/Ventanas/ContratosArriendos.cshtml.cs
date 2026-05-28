using libreria_inmobiliaria.Entidades;
using libreria_presentaciones_inmobiliaria.implemtanciones;
using libreria_presentaciones_inmobiliaria.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32.SafeHandles;
using System.Security.Claims;

namespace inmobiliaria_front.Pages.Ventanas
{
    [Authorize(Roles = "Empleado,Jefe")]
    public class ContratosArriendosModel : PageModel
    {
        private IContratosArriendosNegocio? IContratosArriendosnegocio { get; set; }
        private IClientesNegocio? IClientesnegocio { get; set; }
        private ICompradoresNegocio? ICompradoresnegocio { get; set; }
        private ICodeudoresNegocio? ICodeudoresnegocio { get; set; }
        private IPropiedadesNegocio? IPropiedadesnegocio { get; set; }
        private IEmpleadosSectoresNegocio? IEmpleadosSectoresnegocios { get; set; }

        [BindProperty] public ContratosArriendos? Contrato { get; set; }
        [BindProperty] public Propiedades? Propiedad { get; set; }

        public List<Propiedades>? propiedades { get; set; }
        public List<ContratosArriendos>? contratos { get; set; }
        public List<ContratosArriendos>? contratosAceptados { get; set; }
        public List<Clientes>? clientes { get; set; }
        public List<Codeudores>? codeudores { get; set; }
        public List<Compradores>? compradores { get; set; }
        public List<EmpleadosSectores>? empleados { get; set; }
        
        public bool Borrando { get; set; }
        public bool Aceptar { get; set; }
        public int empleado { get; set; }
        public int jefe { get; set; }

        [BindProperty] public string? CedulaCliente { get; set; } = "0";
        [BindProperty] public string? CedulaComprador { get; set; } = "0";
        [BindProperty] public string? CedulaCodeudor { get; set; } = "0";

        public ContratosArriendosModel()
        {
            IContratosArriendosnegocio = new ContratosArriendosNegocio();
            IClientesnegocio = new ClientesNegocio();
            ICompradoresnegocio = new CompradoresNegocio();
            ICodeudoresnegocio = new CodeudoresNegocio();
            IPropiedadesnegocio = new PropiedadesNegocio();
            IEmpleadosSectoresnegocios = new EmpleadosSectoresNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        private void cargarlistas()
        {
            //Sacamos el id del usuario que esta utilizando en el momento la aplicacion
            empleado = int.Parse(User.FindFirstValue("IdPersona")!);

            //Buscamos las propiedades relacionadas con el sector en el que se encuentra el empleado
            propiedades = IPropiedadesnegocio!.ConsultarSectorEmpleado(empleado);
            compradores = ICompradoresnegocio!.Consultar();
            codeudores = ICodeudoresnegocio!.Consultar();
            clientes = IClientesnegocio!.Consultar();
            empleados = IEmpleadosSectoresnegocios!.Consultar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (IContratosArriendosnegocio == null)
                    return;
                contratos = IContratosArriendosnegocio.Consultar();
                Contrato = null;
                Aceptar = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            cargarlistas();
            Contrato = new ContratosArriendos()
            {
               FechaContrato = DateTime.Now,
               FechaFinalizacion = DateTime.Now,
               Estado = "Pendiente"
            };
            Borrando = false;
            Aceptar = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                cargarlistas();
                Contrato = contratos!.FirstOrDefault(x => x.Id == data);
                contratos = null;
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
                if (Contrato == null)        
                    return;

                Contrato!.Cliente = IClientesnegocio!.ConsultarPorCedula(CedulaCliente!);
                Contrato!.Codeudor = ICodeudoresnegocio!.ConsultarPorCedula(CedulaCodeudor!);
                Contrato!.Comprador = ICompradoresnegocio!.ConsultarPorCedula(CedulaComprador!);
                Contrato!.EmpleadoSector = empleado;

                if (Contrato.Cliente == 0 || Contrato.Codeudor == 0 || Contrato.Comprador == 0)
                    throw new Exception("No se pudo encontrar la cedula, reintente porfavor");

                if (Contrato.Id == 0)
                    Contrato = IContratosArriendosnegocio!.Guardar(Contrato!);
                else
                    Contrato = IContratosArriendosnegocio!.Modificar(Contrato!);

                if (Contrato.Id == 0)
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
                if (Contrato == null)
                    return;
                ViewData["Mensaje"] = IContratosArriendosnegocio!.Eliminar(Contrato!);
                Contrato = null;
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
                Contrato = contratos!.FirstOrDefault(x => x.Id == data);
                contratos = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtSolicitud(int data)
        {
            try
            {
                OnPostBtRefrescar();
                cargarlistas();
                Contrato = contratos!.FirstOrDefault(x => x.Id == data);
                Propiedad = IPropiedadesnegocio!.ConsultarConContrato(Contrato!);
                contratos = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtAceptarContrato()
        {
            try
            {
                Contrato!.Estado = "Aceptado";
                Contrato = IContratosArriendosnegocio!.Modificar(Contrato!);
                OnPostBtCerrar();
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