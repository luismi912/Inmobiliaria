using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IGenerosNegocio
    {
        Generos Guardar(Generos entidad);
        List<Generos> Consultar();
        string Eliminar(Generos entidad);
        Generos Modificar(Generos entidad);
    }
}
