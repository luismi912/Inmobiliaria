using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ITiposPropiedadesNegocio
    {
        TiposPropiedades Guardar(TiposPropiedades entidad);
        List<TiposPropiedades> Consultar();
        string Eliminar(int Id);
        TiposPropiedades Modificar(TiposPropiedades entidad);
    }
}
