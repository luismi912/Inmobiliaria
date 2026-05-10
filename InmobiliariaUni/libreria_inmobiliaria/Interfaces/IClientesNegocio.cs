using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IClientesNegocio
    {
        List<Clientes> Consultar();
        string Eliminar(String Cedula);
        Clientes Modificar(Clientes entidad);
    }
}
