using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ICompradoresNegocio
    {
        List<Compradores> Consultar();
        Compradores Guardar(CompradoresDtos compradorDto);
        string Eliminar(Compradores entidad);
        Compradores Modificar(Compradores entidad);
        int ConsultarPorCedula(string cedula);
    }
}
