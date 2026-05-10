using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IJefesSectoresNegocio
    {
        List<JefesSectores> Consultar();
        string Eliminar(String Cedula);
        JefesSectores Modificar(JefesSectores entidad);
        string Guardar(CrearUsuariosJefesDtos dto);
    }
}
