using libreria_inmobiliaria.crearDTOS;
using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ICodeudoresNegocio
    {
        List<Codeudores> Consultar();
        Codeudores Guardar(CodeudoresDtos codeudorDto);
        string Eliminar(Codeudores entidad);
        Codeudores Modificar(Codeudores entidad);
    }
}
