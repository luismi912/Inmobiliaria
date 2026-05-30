using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ITelefonosNegocio
    {
        List<Telefonos> Consultar();
        string Eliminar(Telefonos entidad);
        Telefonos Modificar(Telefonos entidad);
        Telefonos Guardar(Telefonos entidad);
    }
}
