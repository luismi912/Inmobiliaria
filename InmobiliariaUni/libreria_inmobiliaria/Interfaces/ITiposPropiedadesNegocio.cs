using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ITiposPropiedadesNegocio
    {
        TiposPropiedades Guardar(TiposPropiedades entidad);
        List<TiposPropiedades> Consultar();
        string Eliminar(TiposPropiedades entidad);
        TiposPropiedades Modificar(TiposPropiedades entidad);
    }
}
