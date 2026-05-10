using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IGenerosNegocio
    {
        Generos Guardar(Generos entidad);
        List<Generos> Consultar();
        string Eliminar(int Id);
        Generos Modificar(Generos entidad);
    }
}
