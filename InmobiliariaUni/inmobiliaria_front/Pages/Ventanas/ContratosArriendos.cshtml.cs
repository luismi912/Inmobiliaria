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
        public Propiedades? Propiedad { get; set; }

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
            //Buscamos las propiedades relacionadas con el sector en el que se encuentra el empleado
            propiedades = IPropiedadesnegocio!.Consultar();
            compradores = ICompradoresnegocio!.Consultar();
            codeudores = ICodeudoresnegocio!.Consultar();
            contratos = IContratosArriendosnegocio!.Consultar();
            contratosAceptados = IContratosArriendosnegocio.Consultar().Where(c => c.Estado == "Aceptado").ToList();
            clientes = IClientesnegocio!.Consultar();
            empleados = IEmpleadosSectoresnegocios!.Consultar();
        }

        public void cargarIdEmpleado()
        {
            //Sacamos el id del usuario que esta utilizando en el momento la aplicacion
            empleado = int.Parse(User.FindFirstValue("IdPersona")!);
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                cargarlistas();
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
                cargarIdEmpleado();
                cargarlistas();
                if (Contrato == null)
                    return;

                var pro = propiedades!.FirstOrDefault(x => x.Id == Contrato.Propiedad);

                if (pro == null)
                    throw new Exception("La propiedad seleccionada no existe");

                Contrato!.Cliente = pro.Cliente;

                Contrato!.EmpleadoSector = empleado;

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
                Contrato!.Estado = "Solicitud";
                Contrato = IContratosArriendosnegocio!.Modificar(Contrato!);
                contratos = null;
                Borrando = false;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtVerSolicitud(int data)
        {
            try
            {
                OnPostBtRefrescar();
                cargarlistas();
                Contrato = contratos!.FirstOrDefault(x => x.Id == data);
                contratos = null;
                Aceptar = true;
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
                cargarlistas();
                var contra = contratos!.FirstOrDefault(c => c.Id == Contrato!.Id);

                if (contra == null)
                    throw new Exception("Contrato no encontrado");

                contra!.Estado = "Aceptado";
                Contrato = IContratosArriendosnegocio!.Modificar(contra!);
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
            Aceptar = false;
        }
    }
}