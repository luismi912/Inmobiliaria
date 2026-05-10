using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IBienesNegocio
    {
        List<Bienes> Consultar();
        string Eliminar(int Id);
        Bienes Modificar(Bienes entidad);
        Bienes Guardar(Bienes entidad);
    }
}
