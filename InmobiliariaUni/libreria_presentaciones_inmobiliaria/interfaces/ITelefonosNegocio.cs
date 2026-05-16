using libreria_inmobiliaria.Entidades;

namespace libreria_presentaciones_inmobiliaria.interfaces
{
    public interface ITelefonosNegocio
    {
        List<Telefonos> Consultar();
        Telefonos Guardar(Telefonos entidad);
        string Eliminar(Telefonos entidad);
        Telefonos Modificar(Telefonos entidad);
    }
}
