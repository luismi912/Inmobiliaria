using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface IBienesNegocio
    {
        List<Bienes> Consultar();
        Bienes Guardar(Bienes entidad);
        string Eliminar(Bienes entidad);
        Bienes Modificar(Bienes entidad);
    }
}
