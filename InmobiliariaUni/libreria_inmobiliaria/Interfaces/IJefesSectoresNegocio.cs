using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IJefesSectoresNegocio
    {
        List<JefesSectores> Consultar();
        string Eliminar(JefesSectores entidad);
        JefesSectores Modificar(JefesSectores entidad);
        JefesSectores Guardar(CrearUsuariosJefesDtos dto);
    }
}
