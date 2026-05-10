using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ICodeudoresNegocio
    {
        List<Codeudores> Consultar();
        string Eliminar(String Cedula);
        Codeudores Modificar(Codeudores entidad);
        Codeudores Guardar(CrearUsuariosCodeudoresDtos dto);
    }
}
