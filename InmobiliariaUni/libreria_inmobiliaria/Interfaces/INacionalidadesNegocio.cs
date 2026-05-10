using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface INacionalidadesNegocio
    {
        Nacionalidades Guardar(Nacionalidades entidad);
        List<Nacionalidades> Consultar();
        string Eliminar(int Id);
        Nacionalidades Modificar(Nacionalidades entidad);
    }
}
