using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IEmpleadosSectoresNegocio
    {
        List<EmpleadosSectores> Consultar();
        int ConsultarPorCedula(string cedula);
        EmpleadosSectores ConsultarUsuario(int Id);
        string Eliminar(EmpleadosSectores entidad);
        EmpleadosSectores Modificar(EmpleadosSectores entidad);
        EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos dto);
    }
}
