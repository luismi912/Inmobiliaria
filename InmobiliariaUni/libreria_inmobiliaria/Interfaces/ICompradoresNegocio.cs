using libreria_inmobiliaria.Entidades;

namespace libreria_inmobiliaria.Interfaces
{
    public interface ICompradoresNegocio
    {
        List<Compradores> Consultar();
        string Eliminar(String Cedula);
        Compradores Modificar(Compradores entidad);
    }
}
