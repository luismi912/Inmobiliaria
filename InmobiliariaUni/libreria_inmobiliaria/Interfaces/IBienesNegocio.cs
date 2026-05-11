using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IBienesNegocio
    {
        List<Bienes> Consultar();
        string Eliminar(Bienes entidad);
        Bienes Modificar(Bienes entidad);
        Bienes Guardar(Bienes entidad);
    }
}
