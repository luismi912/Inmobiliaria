using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ICiudadesNegocio
    {
        List<Ciudades> Consultar();
        Ciudades Guardar(Ciudades entidad);
        Ciudades Eliminar(Ciudades entidad);
        Ciudades Modificar(Ciudades entidad);
    }
}
