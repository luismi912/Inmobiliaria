using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ICiudadesNegocio
    {
        Ciudades Guardar(Ciudades entidad);
        List<Ciudades> Consultar();
        string Eliminar(int Id);
        Ciudades Modificar(Ciudades entidad);
    }
}
