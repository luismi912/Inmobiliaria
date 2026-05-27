using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IJefesSectoresNegocio
    {
        List<JefesSectores> Consultar();
        JefesSectores ConsultarUsuario(int Id);
        JefesSectores Guardar(CrearUsuariosJefesDtos jefeDto);
        string Eliminar(JefesSectores entidad);
        JefesSectores Modificar(JefesSectores entidad);
    }
}
