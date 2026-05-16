using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IEmpleadosSectoresNegocio
    {
        List<EmpleadosSectores> Consultar();
        EmpleadosSectores Guardar(CrearUsuariosEmpleadosDtos empleadoDto);
        string Eliminar(EmpleadosSectores entidad);
        EmpleadosSectores Modificar(EmpleadosSectores entidad);
    }
}
