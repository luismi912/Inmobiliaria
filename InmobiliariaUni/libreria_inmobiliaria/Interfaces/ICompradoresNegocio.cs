using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ICompradoresNegocio
    {
        List<Compradores> Consultar();
        string Eliminar(Compradores entidad);
        Compradores Modificar(Compradores entidad);
        Compradores Guardar(CrearUsuariosCompradoresDtos dto);
    }
}
