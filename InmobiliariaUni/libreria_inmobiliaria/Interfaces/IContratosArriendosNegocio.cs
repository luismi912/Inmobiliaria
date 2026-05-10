using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface IContratosArriendosNegocio
    {
        ContratosArriendos Guardar(ContratosArriendos entidad);
        List<ContratosArriendos> Consultar();
        string Eliminar(int Id);
        ContratosArriendos Modificar(ContratosArriendos entidad);
    }
}
