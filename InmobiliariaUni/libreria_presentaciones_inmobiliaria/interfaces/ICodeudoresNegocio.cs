using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ICodeudoresNegocio
    {
        List<Codeudores> Consultar();
        Codeudores Guardar(CrearUsuariosCodeudoresDtos codeudorDto);
        Codeudores Eliminar(Codeudores entidad);
        Codeudores Modificar(Codeudores entidad);
    }
}
